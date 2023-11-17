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
    for (int i = 0; i < args.Length; i++)
    {
        ProcessCommand(args[i]);
    }
}
/// Process the next database command request
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
        else if (update == null) Lookup(ID, field);
        else Update(ID, field, update);
        if (slice.Length == 2)
        {
            operation = slice[1];
            // Could be empty if no field specified
            if (operation == "")
            {
                // Is a record delete command
                Delete(ID);
                return;
            }
        }
        if (debug) Console.Write($"Operation on ID '{ID}'");
        if (operation == null || update == null)
        {
            // Assuming the 'User_ID' column is unique
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
}

/// Functions to process database requests

void Dump(string ID)
{
    if (debug) Console.WriteLine("Output all fields");

    try
    {
        connection.Open();

        using (MySqlCommand command = new MySqlCommand($"SELECT * FROM user_accounts.users WHERE User_ID = '{ID}'", connection))
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
                    Console.WriteLine($"Location={reader["Location"]}");
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

// ... (Your existing code)


void Lookup(string ID, string field)
{
    if (debug)
        Console.WriteLine($" lookup field '{field}'");

    try
    {
        using (MySqlCommand command = new MySqlCommand($"SELECT {field} FROM user_accounts.users WHERE User_ID = '{ID}'", connection))
        {
            connection.Open();
            object result = command.ExecuteScalar();
            Console.WriteLine(result);
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

    using (MySqlCommand command = new MySqlCommand($"UPDATE user_accounts.users SET {field} = '{update}' WHERE User_ID = '{ID}'", connection))
    {
        command.ExecuteNonQuery();
        Console.WriteLine("OK");
    }
}

void Delete(string ID)
{
    if (debug) Console.WriteLine($"Delete record '{ID}' from DataBase");

    using (MySqlCommand command = new MySqlCommand($"DELETE FROM user_accounts.users WHERE User_ID = '{ID}'", connection))
    {
        command.ExecuteNonQuery();
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

                using (MySqlCommand command = new MySqlCommand($"UPDATE user_accounts.users SET Location = '{value}' WHERE User_ID = '{ID}'", connection))
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
            else if (line.StartsWith("GET /?name=") && line.EndsWith(" HTTP/1.1"))
            {
                if (debug) Console.WriteLine("Received a lookup request");

                String[] slices = line.Split(" ");
                String ID = slices[1].Substring(7);

                try
                {
                    

                    using (MySqlCommand command = new MySqlCommand($"SELECT Location FROM user_accounts.users WHERE User_ID = '{ID}'", connection))
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

