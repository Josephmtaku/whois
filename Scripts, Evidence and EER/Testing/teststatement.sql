SELECT users.*
FROM User_Accounts.users
JOIN User_Accounts.UserLocation ON users.User_ID = UserLocation.UserID
JOIN User_Accounts.Location ON UserLocation.LocationID = Location.LocationID
WHERE Location.LocationName = 'RB-336';

DELETE users.*
FROM User_Accounts.users
JOIN User_Accounts.UserLocation ON users.User_ID = UserLocation.UserID
JOIN User_Accounts.Location ON UserLocation.LocationID = Location.LocationID
WHERE Location.LocationName = 'RB-336';
