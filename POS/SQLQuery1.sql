/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP (1000) [customerID]
      ,[fName]
      ,[lName]
      ,[Address]
      ,[City]
      ,[Province]
      ,[Phone]
      ,[Email]
      ,[Note]
      ,[RegisterDate]
      ,[Lv]
  FROM [dbo].[tblcustomer]

ALTER TABLE tblcustomer
ALTER COLUMN RegisterDate date;

SELECT * FROM tblcustomer;

INSERT INTO tblcustomer (customerID,fName,lName,Address,City,Province,Phone,Email,RegisterDate)VALUES(NEXT VALUE FOR SQcustomer,'Dodam2','Shin','ph3503','Toronto','ON','4379817523','dodam@gmail.com',GETDATE());

ALTER TABLE tblcustomer
ALTER COLUMN RegisterDate datetime;

select * from tblCustomer;

INSERT INTO tblcustomer (customerID,fName,lName,Address,City,Province,Phone,Email,RegisterDate,Note)VALUES(NEXT VALUE FOR SQcustomer,'test07','test07','ph3503','Toronto','ON','4379817523','dodam@gmail.com',GETDATE(),'한국어테스트');

SELECT customerID AS NO,(fName + '  '+ lName ) AS NAME, email AS 'E-MAIL',city AS 'CITY',Province AS 'PROVINCE',convert(varchar, RegisterDate,106) AS 'REGISTER DATE' from tblCustomer;

 
ALTER TABLE tblcustomer
ALTER COLUMN Province varchar(20);

select * from tblCustomer;
INSERT INTO tblcustomer (customerID,fName,lName,Address,City,Province,Phone,Email,RegisterDate,Note)VALUES(NEXT VALUE FOR SQcustomer,N'도담',N'신',N'일원동',N'Toronto','ON','4379817523','dodam@gmail.com',GETDATE(),N'한국어테스트');


select * from tblItem;


INSERT INTO tblItem(itemName, itemImage)  
select 'testimage',BulkColumn
FROM OPENROWSET(Bulk 'C:\Users\susur\Pictures\test.jpg', SINGLE_BLOB) AS img;




ALTER TABLE tblItem
ALTER COLUMN itemImage varbinary(max);

INSERT INTO tblCustomer 
            (customerID,fName,lName,Email,Phone,Address,City,Province,PostalCode,Note,RegisterDate)
            VALUES(NEXT VALUE FOR SQcustomer,(N'송송'),(N'송송'),('{2}'),('{3}'),(N'송송'),(N'송송'),('{6}'),('{7}'),(N'한국어한국어'),GETDATE());