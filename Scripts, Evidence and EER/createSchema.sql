CREATE SCHEMA IF NOT EXISTS User_Accounts DEFAULT CHARACTER SET utf8mb4;
USE User_Accounts;
SET FOREIGN_KEY_CHECKS=0;

CREATE TABLE IF NOT EXISTS users (
    User_ID INT PRIMARY KEY,
    LoginID INT NOT NULL,
    Surname VARCHAR(45),
    Fornames VARCHAR(45) NOT NULL,
    TitleID INT NOT NULL,
    Phone VARCHAR(15) NOT NULL,
    FOREIGN KEY (LoginID) REFERENCES LoginAccounts(LoginAccountsID),
    FOREIGN KEY (TitleID) REFERENCES Title(TitleID)
);
CREATE TABLE IF NOT EXISTS Title(
	TitleID INT PRIMARY KEY,
    Title VARCHAR(45) NOT NULL
);
CREATE TABLE IF NOT EXISTS Location (
    LocationID INT PRIMARY KEY,
    LocationName VARCHAR(45) NOT NULL
);

CREATE TABLE IF NOT EXISTS Positions (
    PositionID INT PRIMARY KEY,
    Position VARCHAR(45) NOT NULL
);

CREATE TABLE IF NOT EXISTS Email (
    EmailID INT PRIMARY KEY,
    Email VARCHAR(45) NOT NULL
);

CREATE TABLE IF NOT EXISTS UserLocation(
    UserLocationID INT PRIMARY KEY,
    UserID INT NOT NULL,
    LocationID INT NOT NULL,
    FOREIGN KEY (UserID) REFERENCES users(User_ID),
    FOREIGN KEY (LocationID) REFERENCES Location(LocationID)
);

CREATE TABLE IF NOT EXISTS UserPosition(
	UserPositionID INT PRIMARY KEY,
    UserID INT NOT NULL,
    PositionID INT NOT NULL,
    foreign KEY (PositionID) REFERENCES Positions(PositionID),
    FOREIGN KEY (UserID) REFERENCES users(User_ID)
);

CREATE TABLE IF NOT EXISTS UserEmail(
	UserEmailID INT PRIMARY KEY,
    UserID INT NOT NULL,
    EmailID INT NOT NULL,
    FOREIGN KEY (UserID) REFERENCES users(User_ID),
    FOREIGN KEY (EmailID) REFERENCES Email(EmailID)
);

CREATE TABLE IF NOT EXISTS LoginAccounts (
    LoginAccountsID INT PRIMARY KEY,
    LoginID VARCHAR(45) NOT NULL
);
