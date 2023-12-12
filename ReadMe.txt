*******************************************************************************************************************************************
Please follow All the steps below to make sure the application runs as expected.
*******************************************************************************************************************************************

In other to use the static client, An Sql schema has to be created and then the test data needs to be inserted into the tables. 
In orther to do this, Follow the steps below:

*******************************************************************************************************************************************
TO CREATE THE SCHEMA:
*******************************************************************************************************************************************

1. Open MySQL Workbench and click on the new connection icon from the MySQL Workbench home screen.
2. In the new connection dialog:

                                a. Enter “user_connection” as the Connection name.
                                b. Select TCP/IP as the Connection method.
                                c. Enter the following details:

                                                                a. Hostname: 127.0.0.1 or localhost
                                                                b. Port number: 3306
                                                                c. Username: root
                                                                d. Password: L3tM31n
3. Click the “Test connection” button. A successful connection message should appear if the parameters are correct.
4. Click the "OK" button to save your connection, and you'll see it listed under the Connections tab on the home screen.
5.From the MySQL Workbench home screen, navigate to File > Open SQL Script.
6. Select the file named "createSchema.sql" in the folder called "Scripts, Evidence and EER" from the submitted Zip file.
7. Now, go to Query > Execute All. This will execute the SQL script.
If the steps are followed correctly, the schema named "user_accounts" should be created.

*******************************************************************************************************************************************
TO POPULATE THE TABLE WITH THE TEST DATA:
*******************************************************************************************************************************************
1. From the MySQL workbench home screen, navigate to file > open SQL script
2. Select the file called testdata.sql script in the folder called "Scripts, Evidence and EER", which can be found in the Zip file submitted 
3. Now select Query > Execute All. 
The test data should be inserted into the Tables if the steps are followed properly.

*******************************************************************************************************************************************
TESTING SQL TABLES WITH SCRIPT (OPTIONAL)
*******************************************************************************************************************************************
(OPTIONAL) To test the database, an sql script with a few sql Query statements can be executed. This can be found in the folder called "Testing" 
inside the "Scripts, Evidence and EER" folder from the submitted Zip file.

*******************************************************************************************************************************************
EER DIAGRAM LOCATION:
*******************************************************************************************************************************************
The EER Diagram can be located in the folder called "Scripts, Evidence and EER", which can be found in the Zip file submitted.

*******************************************************************************************************************************************
EVIDENCE OF 3RD NORMAL FORM LOCATION:
*******************************************************************************************************************************************
The Evidence is called "Steps of how the 3rd normal form was generated" and can be located in the folder called "Scripts, Evidence and EER",
which can be found in the Zip file submitted.

*******************************************************************************************************************************************
RUNNING THE EXECUTABLE:
*******************************************************************************************************************************************
The whois.exe file acts both as a static client and server.

**********************
To use it as a server: 
**********************
The executable (.exe) file can be found in "App Executable" folder in the zip file submitted. To start the server, double click the whois executable
and it should start as a server waiting for input from a client which can be requests from a web browser or by typing URLs similar to the example below
into the address field:

http://localhost:43/?name=202215249

*****************************
To use it as a static client:
*****************************
For this program to be used as a static client, Open the "App Executable" folder and then click in the address bar and then type CMD and press enter.
This should open command prompt in the correct folder. At the prompt we can run our `whois` program and supply the username we wish looked up. 
Example:
G:\whois\App Executable>whois 202215249

Command: 202215249
Operation on ID '202215249'Output all fields
UserID=202215249
LoginID=smith23
Surname=Smith
Fornames=Will
Title=Mr
Position=Doctor
Phone=+447440402381
Email=W.Smith@gmail.com
Location=FBR-052
Operation on ID '202215249'

***************************************************
To update location
*************************************************** 
Please note To update a location both while using the static client or server using a web browser, please use the corresponding location ID, 
which should be a number between 1 and 5. Locations are uniquely identified by these numeric IDs. If a numeric value isnt entererd or is over the limit,
it returns an error message 'Invalid location'. 

G:\whois\App Executable>whois 202012345?Location=4

Command: 202012345?Location=4
Operation on ID '202012345' update field 'Location' to '4'
OK
Operation on ID '202012345'

********************************
Adding Data to the Sql database
********************************
To add data follow this example:
G:\whois\App Executable>whois 123?add

Command: 123?add
Operation on ID '123'Enter Surname: Mtaku
Enter Fornames: Zulai
Enter Phone: +44780895656
Enter LoginID: mtaku23
Enter Title: Mrs
Enter Position: Admin Officer
Enter Email: z.mtaku@gmail.com
Enter Location: 4
Record added successfully.
Operation on ID '123'

*******************************************************************************************************************************************
MULTI-THREADING:
*******************************************************************************************************************************************
The ProcessCommands method splits the input string into individual commands using the space ( ) 
as a separator. It then processes each command using the existing ProcessCommand method. 
This way, multiple commands can be passed as arguments separated by semicolons when running the program.
Example:
G:\whois\App Executable>whois 202123458?Location 202103482?Location
Command: 202123458?Location
Operation on ID '202123458' lookup field 'Location'
RB-336
Operation on ID '202123458'
Command: 202103482?Location
Operation on ID '202103482' lookup field 'Location'
ASB-LLT
Operation on ID '202103482'

