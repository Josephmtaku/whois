// See https://aka.ms/new-console-template for more information
using MySql.Data.MySqlClient;
using System.Net.Sockets;
using System.Net;
using whois;
Boolean debug = true;

string connectionString = "server=localhost;user=root;database=user_accounts;port=3306;password=L3tM31n";
MySqlConnection connection = new MySqlConnection(connectionString);

if (args.Length == 0)
{
    Console.WriteLine("Starting Server");
    RunServer();
}
else
{
    string commandsString = string.Join(" ", args);
    ProcessCommands(commandsString);
}

void ProcessCommands(string commandsString)
{
    string[] commands = commandsString.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

    foreach (var command in commands)
    {
        ProcessCommand(command.Trim());
    }
}

void ProcessCommand(string command)
{
    if (debug) Console.WriteLine($"\nCommand: {command}");
    try
    {
        String[] slice = command.Split(new char[] { '?' }, 2);
        String ID = slice[0];
        String operation = null;
        String update = null;
        String field = null;
        if (slice.Length == 2)
        {
            operation = slice[1];
            String[] pieces = operation.Split(new char[] { '=' }, 2);
            field = pieces[0];
            if (pieces.Length == 2) update = pieces[1];
        }
        if (debug) Console.Write($"Operation on ID '{ID}'");
        if (operation == null) Dump(ID);
        else if (operation.ToLower() == "add") AddRecord(ID, field);
        else if (update == null) Lookup(ID, field);
        else Update(ID, field, update);
        if (slice.Length == 2)
        {
            operation = slice[1];
            // Could be empty if no field specified
            if (operation == "")
            {
                Delete(ID);
                return;
            }
        }
        if (debug) Console.Write($"Operation on ID '{ID}'");
        if (operation == null || update == null)
        {
            bool userExists;
            using (MySqlCommand checkUserCommand = new MySqlCommand($"SELECT COUNT(*) FROM user_accounts.users WHERE User_ID = '{ID}'", connection))
            {
                connection.Open();
                int userCount = Convert.ToInt32(checkUserCommand.ExecuteScalar());
                userExists = userCount > 0;
            }

            if (!userExists)
            {
                Console.WriteLine($"User '{ID}' not known");
                return;
            }
        }
    }
    catch (Exception e)
    {
        Console.WriteLine($"Fault in Command Processing: {e.ToString()}");
    }
    finally
    {
        connection.Close();
    }
}

void AddRecord(string ID, string field)
{
    Console.Write("Enter Surname: ");
    string surname = Console.ReadLine();

    Console.Write("Enter Fornames: ");
    string fornames = Console.ReadLine();

    Console.Write("Enter Phone: ");
    string phone = Console.ReadLine();

    Console.Write("Enter LoginID: ");
    string loginID = Console.ReadLine();

    Console.Write("Enter Title: ");
    string title = Console.ReadLine();

    Console.Write("Enter Position: ");
    string position = Console.ReadLine();

    Console.Write("Enter Email: ");
    string email = Console.ReadLine();

    Console.Write("Enter Location: ");
    string location = Console.ReadLine();

    Add(ID, surname, fornames, phone, loginID, title, position, email, location);
}
/// Functions to process database requests
void Dump(string ID)
{
    if (debug) Console.WriteLine("Output all fields");

    try
    {
        connection.Open();

        using (MySqlCommand command = new MySqlCommand($"SELECT u.User_ID, u.Surname, u.Fornames, u.Phone, " +
                                                       $"la.LoginID, t.Title, p.Position, e.Email, l.LocationName " +
                                                       $"FROM user_accounts.users u " +
                                                       $"JOIN LoginAccounts la ON u.LoginID = la.LoginAccountsID " +
                                                       $"JOIN Title t ON u.TitleID = t.TitleID " +
                                                       $"JOIN UserPosition up ON u.User_ID = up.UserID " +
                                                       $"JOIN Positions p ON up.PositionID = p.PositionID " +
                                                       $"JOIN UserEmail ue ON u.User_ID = ue.UserID " +
                                                       $"JOIN Email e ON ue.EmailID = e.EmailID " +
                                                       $"JOIN UserLocation ul ON u.User_ID = ul.UserID " +
                                                       $"JOIN Location l ON ul.LocationID = l.LocationID " +
                                                       $"WHERE u.User_ID = '{ID}'", connection))
        {
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine($"UserID={reader["User_ID"]}");
                    Console.WriteLine($"LoginID={reader["LoginID"]}");
                    Console.WriteLine($"Surname={reader["Surname"]}");
                    Console.WriteLine($"Fornames={reader["Fornames"]}");
                    Console.WriteLine($"Title={reader["Title"]}");
                    Console.WriteLine($"Position={reader["Position"]}");
                    Console.WriteLine($"Phone={reader["Phone"]}");
                    Console.WriteLine($"Email={reader["Email"]}");
                    Console.WriteLine($"Location={reader["LocationName"]}");
                }
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error in Dump operation: {ex.Message}");
    }
    finally
    {
        connection.Close();
    }
}
void Lookup(string ID, string field)
{
    if (debug)
        Console.WriteLine($" lookup field '{field}'");

    try
    {
        if (field.ToLower() == "location")
        {
            using (MySqlCommand command = new MySqlCommand($"SELECT l.LocationName FROM UserLocation ul " +
                                                           $"JOIN Location l ON ul.LocationID = l.LocationID " +
                                                           $"WHERE ul.UserID = '{ID}'", connection))
            {
                connection.Open();
                object result = command.ExecuteScalar();
                Console.WriteLine(result);
            }
        }
        else
        {
            using (MySqlCommand command = new MySqlCommand($"SELECT {field} FROM user_accounts.users WHERE User_ID = '{ID}'", connection))
            {
                object result = command.ExecuteScalar();
                Console.WriteLine(result);
            }
        }
    }
    finally
    {
        connection.Close();
    }
}
void Update(string ID, string field, string update)
{
    if (debug)
        Console.WriteLine($" update field '{field}' to '{update}'");

    try
    {
        if (field.ToLower() == "location")
        {
            using (MySqlCommand command = new MySqlCommand($"UPDATE UserLocation SET LocationID = '{update}' WHERE UserID = '{ID}'", connection))
            {
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        else
        {
            using (MySqlCommand command = new MySqlCommand($"UPDATE user_accounts.users SET {field} = '{update}' WHERE User_ID = '{ID}'", connection))
            {
                command.ExecuteNonQuery();
            }
        }

        Console.WriteLine("OK");
    }
    finally
    {
        connection.Close();
    }
}
void Add(string ID, string surname, string fornames, string phone, string loginID, string title, string position, string email, string location)
{
    try
    {
        connection.Open();
        using (MySqlCommand command = new MySqlCommand($"INSERT INTO users (User_ID, LoginID, Surname, Fornames, TitleID, Phone) " +
                                                       $"VALUES ({ID}, (SELECT LoginAccountsID FROM LoginAccounts WHERE LoginID = '{loginID}'), '{surname}', '{fornames}', " +
                                                       $"(SELECT TitleID FROM Title WHERE Title = '{title}'), '{phone}')", connection))
        {
            command.ExecuteNonQuery();
        }

        Console.WriteLine("Record added successfully.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error adding record: {ex.Message}");
    }
    finally
    {
        connection.Close();
    }
}
void Delete(string ID)
{
    if (debug) Console.WriteLine($"Delete record '{ID}' from DataBase");

    try
    {
        using (MySqlCommand command = new MySqlCommand($"DELETE FROM user_accounts.users WHERE User_ID = '{ID}'", connection))
        {
            connection.Open();
            command.ExecuteNonQuery();
        }
        // Also delete the corresponding entry from UserLocation
        using (MySqlCommand command = new MySqlCommand($"DELETE FROM UserLocation WHERE UserID = '{ID}'", connection))
        {
            command.ExecuteNonQuery();
        }
    }
    finally
    {
        connection.Close();
    }
}
void RunServer()
{
    TcpListener listener;
    Socket connection;
    NetworkStream socketStream;

    try
    {
        listener = new TcpListener(43);
        listener.Start();

        while (true)
        {
            connection = listener.AcceptSocket();
            connection.SendTimeout = 1000;
            connection.ReceiveTimeout = 1000;
            socketStream = new NetworkStream(connection);
            doRequest(socketStream);
            connection.Close();
            socketStream.Close();
        }
    }
    catch (Exception e)
    {
        Console.WriteLine(e.ToString());
    }
    finally
    {
        // Connection and socketStream will be closed automatically
    }
}

void doRequest(NetworkStream socketStream)
{
    StreamWriter sw = new StreamWriter(socketStream);
    StreamReader sr = new StreamReader(socketStream);

    if (debug) Console.WriteLine("Waiting for input from client...");

    try
    {
        String line = sr.ReadLine();

        if (line == null)
        {
            if (debug) Console.WriteLine("Ignoring null command");
            return;
        }

        Console.WriteLine($"Received Network Command: '{line}'");

        if (line == "POST / HTTP/1.1")
        {
            if (debug) Console.WriteLine("Received an update request");

            int content_length = 0;

            while (line != "")
            {
                if (line.StartsWith("Content-Length: "))
                {
                    content_length = Int32.Parse(line.Substring(16));
                }

                line = sr.ReadLine();

                if (debug) Console.WriteLine($"Skipped Header Line: '{line}'");
            }

            line = "";

            for (int i = 0; i < content_length; i++) line += (char)sr.Read();

            String[] slices = line.Split(new char[] { '&' }, 2);
            String ID = slices[0].Substring(5);
            String value = slices[1].Substring(9);

            if (debug) Console.WriteLine($"Received an update request for '{ID}' to '{value}'");

            // Validate location before updating
            if (IsValidLocation(value))
            {
                using (MySqlCommand command = new MySqlCommand($"UPDATE user_accounts.users u " +
                                                           $"JOIN UserLocation ul ON u.User_ID = ul.UserID " +
                                                           $"SET ul.LocationID = '{value}' " +
                                                           $"WHERE u.User_ID = '{ID}'", connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }

                sw.WriteLine("HTTP/1.1 200 OK");
                sw.WriteLine("Content-Type: text/plain");
                sw.WriteLine();
                sw.WriteLine("Update done");
                sw.Flush();
            }
            else
            {
                // if Location is not valid, send an error response
                sw.WriteLine("HTTP/1.1 400 Bad Request");
                sw.WriteLine("Content-Type: text/plain");
                sw.WriteLine();
                sw.WriteLine("Invalid location");
                sw.Flush();
            }   
        }
        else if (line.StartsWith("GET /?name=") && line.EndsWith(" HTTP/1.1"))
        {
            if (debug) Console.WriteLine("Received a lookup request");

            String[] slices = line.Split(" ");
            String ID = slices[1].Substring(7);

            try
            {
                using (MySqlCommand command = new MySqlCommand($"SELECT LocationName FROM user_accounts.users u " +
                                                       $"JOIN UserLocation ul ON u.User_ID = ul.UserID " +
                                                       $"JOIN Location l ON ul.LocationID = l.LocationID " +
                                                       $"WHERE u.User_ID = '{ID}'", connection))
                {
                    connection.Open();
                    object result = command.ExecuteScalar();

                    if (result != null)
                    {
                        sw.WriteLine("HTTP/1.1 200 OK");
                        sw.WriteLine("Content-Type: text/plain");
                        sw.WriteLine();
                        sw.WriteLine(result.ToString());
                        sw.Flush();
                        Console.WriteLine($"Performed Lookup on '{ID}' returning '{result}'");
                    }
                    else
                    {
                        sw.WriteLine("HTTP/1.1 404 Not Found");
                        sw.WriteLine("Content-Type: text/plain");
                        sw.WriteLine();
                        sw.Flush();
                        Console.WriteLine($"Performed Lookup on '{ID}' returning '404 Not Found'");
                    }
                }
            }
            finally
            {
                connection.Close();
            }
        }
        else
        {
            Console.WriteLine($"Unrecognized command: '{line}'");
            sw.WriteLine("HTTP/1.1 400 Bad Request");
            sw.WriteLine("Content-Type: text/plain");
            sw.WriteLine();
        }
    }
    catch (IOException e)
    {
        Console.WriteLine($"Error reading from the client: {e.Message}");
    }
    finally
    {
        try
        {
            sw.Close();
            sr.Close();
            socketStream.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error closing the streams or socket: {ex.Message}");
        }
    }
}
bool IsValidLocation(string location)
{
    if (int.TryParse(location, out int locationNumber))
    {
        // Check if the location number is between 1 and 5
        return locationNumber >= 1 && locationNumber <= 5;
    }
    // Invalid location if not a valid integer
    return false;
}
