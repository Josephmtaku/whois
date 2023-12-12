USE User_Accounts;

INSERT INTO users (`User_ID`,`LoginID`,`Surname`,`Fornames`,`TitleID`,`Phone`) VALUES (202012345,1,'Tompsett','Charles Philip Arthur George',4,'+441482465222');
INSERT INTO users (`User_ID`,`LoginID`,`Surname`,`Fornames`,`TitleID`,`Phone`) VALUES (202103482,2,'Mtaku','Joseph',1,'+447404879754');
INSERT INTO users (`User_ID`,`LoginID`,`Surname`,`Fornames`,`TitleID`,`Phone`) VALUES (202123458,3,'Gates','Bill',1,'+2349027643817');
INSERT INTO users (`User_ID`,`LoginID`,`Surname`,`Fornames`,`TitleID`,`Phone`) VALUES (202145590,4,'','Dora',2,'+447883394035');
INSERT INTO users (`User_ID`,`LoginID`,`Surname`,`Fornames`,`TitleID`,`Phone`) VALUES (202215249,5,'Smith','Will',1,'+447440402381');
INSERT INTO users (`User_ID`,`LoginID`,`Surname`,`Fornames`,`TitleID`,`Phone`) VALUES (202244594,6,'Garry','Kelly',3,'+447839502545');
INSERT INTO users (`User_ID`,`LoginID`,`Surname`,`Fornames`,`TitleID`,`Phone`) VALUES (202248890,7,'Philip','Arthur',3,'+447394905238');
INSERT INTO users (`User_ID`,`LoginID`,`Surname`,`Fornames`,`TitleID`,`Phone`) VALUES (202268503,8,'Doe','John',3,'+447299402994');
INSERT INTO users (`User_ID`,`LoginID`,`Surname`,`Fornames`,`TitleID`,`Phone`) VALUES (202349586,9,'Nolan','Lily',5,'+447548596828');
INSERT INTO users (`User_ID`,`LoginID`,`Surname`,`Fornames`,`TitleID`,`Phone`) VALUES (202358939,10,'Laker','Emma',6,'+447576035999');

INSERT INTO UserPosition (`UserPositionID`,`UserID`,`PositionID`) VALUES (1,202012345,1);
INSERT INTO UserPosition (`UserPositionID`,`UserID`,`PositionID`) VALUES (2,202103482,2);
INSERT INTO UserPosition (`UserPositionID`,`UserID`,`PositionID`) VALUES (3,202123458,3);
INSERT INTO UserPosition (`UserPositionID`,`UserID`,`PositionID`) VALUES (4,202145590,5);
INSERT INTO UserPosition (`UserPositionID`,`UserID`,`PositionID`) VALUES (5,202215249,6);
INSERT INTO UserPosition (`UserPositionID`,`UserID`,`PositionID`) VALUES (6,202244594,5);
INSERT INTO UserPosition (`UserPositionID`,`UserID`,`PositionID`) VALUES (7,202248890,4);
INSERT INTO UserPosition (`UserPositionID`,`UserID`,`PositionID`) VALUES (8,202268503,4);
INSERT INTO UserPosition (`UserPositionID`,`UserID`,`PositionID`) VALUES (9,202349586,4);
INSERT INTO UserPosition (`UserPositionID`,`UserID`,`PositionID`) VALUES (10,202358939,4);

INSERT INTO UserLocation (`UserLocationID`,`UserID`,`LocationID`) VALUES (1,202012345,1);
INSERT INTO UserLocation (`UserLocationID`,`UserID`,`LocationID`) VALUES (2,202103482,2);
INSERT INTO UserLocation (`UserLocationID`,`UserID`,`LocationID`) VALUES (3,202123458,1);
INSERT INTO UserLocation (`UserLocationID`,`UserID`,`LocationID`) VALUES (4,202145590,5);
INSERT INTO UserLocation (`UserLocationID`,`UserID`,`LocationID`) VALUES (5,202215249,3);
INSERT INTO UserLocation (`UserLocationID`,`UserID`,`LocationID`) VALUES (6,202244594,4);
INSERT INTO UserLocation (`UserLocationID`,`UserID`,`LocationID`) VALUES (7,202248890,3);
INSERT INTO UserLocation (`UserLocationID`,`UserID`,`LocationID`) VALUES (8,202268503,2);
INSERT INTO UserLocation (`UserLocationID`,`UserID`,`LocationID`) VALUES (9,202349586,4);
INSERT INTO UserLocation (`UserLocationID`,`UserID`,`LocationID`) VALUES (10,202358939,5);

INSERT INTO UserEmail (`UserEmailID`,`UserID`,`EmailID`) VALUES (1,202012345,1);
INSERT INTO UserEmail (`UserEmailID`,`UserID`,`EmailID`) VALUES (2,202103482,2);
INSERT INTO UserEmail (`UserEmailID`,`UserID`,`EmailID`) VALUES (3,202123458,3);
INSERT INTO UserEmail (`UserEmailID`,`UserID`,`EmailID`) VALUES (4,202145590,4);
INSERT INTO UserEmail (`UserEmailID`,`UserID`,`EmailID`) VALUES (5,202215249,5);
INSERT INTO UserEmail (`UserEmailID`,`UserID`,`EmailID`) VALUES (6,202244594,6);
INSERT INTO UserEmail (`UserEmailID`,`UserID`,`EmailID`) VALUES (7,202248890,1);
INSERT INTO UserEmail (`UserEmailID`,`UserID`,`EmailID`) VALUES (8,202268503,1);
INSERT INTO UserEmail (`UserEmailID`,`UserID`,`EmailID`) VALUES (9,202349586,1);
INSERT INTO UserEmail (`UserEmailID`,`UserID`,`EmailID`) VALUES (10,202358939,1);

INSERT INTO Positions (`PositionID`,`Position`) VALUES (1,'Lecturer Of Computer Science');
INSERT INTO Positions (`PositionID`,`Position`) VALUES (2,'Student');
INSERT INTO Positions (`PositionID`,`Position`) VALUES (3,'Professor of Business Management ');
INSERT INTO Positions (`PositionID`,`Position`) VALUES (4,'Admin Officer');
INSERT INTO Positions (`PositionID`,`Position`) VALUES (5,'Professor of Environmental Science');
INSERT INTO Positions (`PositionID`,`Position`) VALUES (6,'Doctor');

INSERT INTO Title (`TitleID`,`Title`) VALUES (1,'Mr');
INSERT INTO Title (`TitleID`,`Title`) VALUES (2,'Mrs');
INSERT INTO Title (`TitleID`,`Title`) VALUES (3,'Doctor');
INSERT INTO Title (`TitleID`,`Title`) VALUES (4,'Eur ing');
INSERT INTO Title (`TitleID`,`Title`) VALUES (5,'Professor');
INSERT INTO Title (`TitleID`,`Title`) VALUES (6,'Miss');

INSERT INTO LoginAccounts (`LoginAccountsID`,`LoginID`) VALUES (1,'tompsett23');
INSERT INTO LoginAccounts (`LoginAccountsID`,`LoginID`) VALUES (2,'mtaku23');
INSERT INTO LoginAccounts (`LoginAccountsID`,`LoginID`) VALUES (3,'gates23');
INSERT INTO LoginAccounts (`LoginAccountsID`,`LoginID`) VALUES (4,'joshua23');
INSERT INTO LoginAccounts (`LoginAccountsID`,`LoginID`) VALUES (5,'smith23');
INSERT INTO LoginAccounts (`LoginAccountsID`,`LoginID`) VALUES (6,'garry23');
INSERT INTO LoginAccounts (`LoginAccountsID`,`LoginID`) VALUES (7,'philip23');
INSERT INTO LoginAccounts (`LoginAccountsID`,`LoginID`) VALUES (8,'doe23');
INSERT INTO LoginAccounts (`LoginAccountsID`,`LoginID`) VALUES (9,'nolan23');
INSERT INTO LoginAccounts (`LoginAccountsID`,`LoginID`) VALUES (10,'laker23');

INSERT INTO Location (`LocationID`,`LocationName`) VALUES (1,'RB-336');
INSERT INTO Location (`LocationID`,`LocationName`) VALUES (2,'ASB-LLT');
INSERT INTO Location (`LocationID`,`LocationName`) VALUES (3,'RB-LTA');
INSERT INTO Location (`LocationID`,`LocationName`) VALUES (4,'WB-LT1');
INSERT INTO Location (`LocationID`,`LocationName`) VALUES (5,'FBR-052');

INSERT INTO Email (`EmailID`,`Email`) VALUES (1,'admin@example.com');
INSERT INTO Email (`EmailID`,`Email`) VALUES (2,'j.mtaku-2021@hull.ac.uk');
INSERT INTO Email (`EmailID`,`Email`) VALUES (3,'B.Gates-2021@hull.ac.uk');
INSERT INTO Email (`EmailID`,`Email`) VALUES (4,'D.Joshua@gmail.com');
INSERT INTO Email (`EmailID`,`Email`) VALUES (5,'W.Smith@gmail.com');
INSERT INTO Email (`EmailID`,`Email`) VALUES (6,'K.Garry@hull.ac.uk');