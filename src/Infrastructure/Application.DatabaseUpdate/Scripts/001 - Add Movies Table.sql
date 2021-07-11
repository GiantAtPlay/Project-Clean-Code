CREATE TABLE Movies(
   Id int NOT NULL PRIMARY KEY IDENTITY(1,1),
   Title varchar(100) NULL,
   Description varchar(200) NULL,
   Genre varchar(25) NULL,
   Created datetime NOT NULL
)
