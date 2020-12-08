/****** Script for SelectTopNRows command from SSMS  ******/
--SELECT TOP (1000) [ID]
--      ,[Nazwa]
--  FROM [final15].[dbo].[Wojewodztwa]


--  Use final18;

--  BULK INSERT Miasta FROM  'E:\_Semestr 7\do inzynierki\miasta.csv'  
--  WITH
--(
--    FIRSTROW = 2
--	)


--	Truncate TABLE Wojewodztwo
--DELETE FROM Wojewodztwo
--DBCC CHECKIDENT ('Wojewodztwo',RESEED, 0)

--  BULK INSERT Wojewodztwo FROM  'E:\_Semestr 7\do inzynierki\wojewodztwa.csv'  
--  WITH
--(
--    FIRSTROW = 2,
--	CODEPAGE = '65001',
--	FIELDTERMINATOR = ';'
--	)


	--Truncate table Miasto;
--	Create table temp_miasto (ID int, Nazwa nvarchar(max), WojewodztwoID int, PowiatID int);
--  BULK INSERT temp_miasto FROM  'E:\_Semestr 7\do inzynierki\miasta.csv'  
--  WITH
--(
--    FIRSTROW = 2,
--	CODEPAGE = '65001',
--	FIELDTERMINATOR = ';',
--	KEEPNULLS
--	)


--	  BULK INSERT Powiat FROM  'E:\_Semestr 7\do inzynierki\powiaty.csv'  
--  WITH
--(
--    FIRSTROW = 2,
--	CODEPAGE = '65001',
--	FIELDTERMINATOR = ';'
--	)

--	Create Table temp_powiat (ID int not null,WojewodztwoId int not null ,Nazwa nvarchar(max))
--		  BULK INSERT temp_powiat FROM  'E:\_Semestr 7\do inzynierki\powiaty.csv'  
--  WITH
--(
--    FIRSTROW = 2,
--	CODEPAGE = '65001',
--	FIELDTERMINATOR = ';'
--	)

	--Insert into Powiat (Nazwa, WojewodztwoID)
--SELECT temp_powiat.Nazwa , temp_powiat.WojewodztwoId/2 FROM temp_powiat 




--SELECT Powiat.ID as powiatID, temp_powiat.ID as tpID, temp_powiat.Nazwa, Powiat.Nazwa as nazwa1,temp_powiat.WojewodztwoID as wojNotBy2
--INTO JOINEDone 
--FROM Powiat
--INNER JOIN temp_powiat ON Powiat.Nazwa = temp_powiat.Nazwa
--AND temp_powiat.WojewodztwoID = Powiat.WojewodztwoId*2
		
--DECLARE @Curr1 int = 0
--DECLARE @End1 int = (Select COUNT(Nazwa) from temp_miasto) 

--WHILE @Curr1 < @End1
--BEGIN
--	Insert into Miasto(Nazwa, WojewodztwoID,PowiatID)
--		SELECT 
--		Nazwa =(SELECT Nazwa from temp_miasto WHERE temp_miasto.ID = @Curr1) , 
--		WojewodztwoID =(SELECT WojewodztwoID/2 from temp_miasto WHERE temp_miasto.ID = @Curr1), 
--		PowiatID =(SELECT JOINEDone.powiatID  from 
--		JOINEDone 
--		Where  JOINEDone.tpID = (SELECT PowiatID from temp_miasto WHERE temp_miasto.ID = @Curr1) 
--		AND  JOINEDone.wojNotBy2 =(SELECT WojewodztwoID from temp_miasto WHERE temp_miasto.ID = @Curr1) )

--	 SET @Curr1 = @Curr1 +1
--END;

--SELECT Powiat.ID as powiatID, temp_powiat.ID as wojID, temp_powiat.Nazwa, Powiat.Nazwa as nazwa1
--FROM Powiat
--INNER JOIN temp_powiat ON Powiat.Nazwa = temp_powiat.Nazwa AND temp_powiat.WojewodztwoID = Powiat.WojewodztwoId*2 ORDER BY Powiat.Nazwa;



--DELETE FROM Miasto
--DBCC CHECKIDENT ('Miasto',RESEED, 0)
--DELETE FROM Wojewodztwo
--DBCC CHECKIDENT ('Wojewodztwo',RESEED, 0)


  use final20;
Create Table miasta22 (
Nazwa nvarchar(max),
Rodzaj nvarchar(max) ,
Gmina nvarchar(max) ,
Powiat nvarchar(max) ,
Województwo nvarchar(max) ,
ID nvarchar(max) 


)
		  BULK INSERT miasta22 FROM  'E:\_Semestr 7\do inzynierki\urzedowy_wykaz_nazw_miejscowosci_2019CSV.csv'  
  WITH
(
    FIRSTROW = 2,
	CODEPAGE = '65001',
	FIELDTERMINATOR = ';'
	)
	

	--SELECT DISTINCT  Powiat,  Województwo FROM miasta23 Order by Województwo , Powiat;


		Insert into Wojewodztwo(Nazwa)   SELECT DISTINCT   Województwo FROM miasta22 Order by Województwo ;


SELECT   Powiat,Województwo
into #tempPW --!!!!
FROM miasta22 GROUP BY Województwo,Powiat


INSERT INTO Powiat 
SELECT #tempPW.Powiat as Nazwa,  Wojewodztwo.ID as WojewodztwoID
FROM #tempPW
INNER JOIN Wojewodztwo ON #tempPW.Województwo = Wojewodztwo.Nazwa


	--	SELECT DISTINCT * FROM miasta23  WHERE Rodzaj NOT LIKE '%czêœæ%' AND Rodzaj NOT LIKE '%przysió³ek%' ORDER BY Nazwa_miejscowoœci



		SELECT DISTINCT Nazwa,Rodzaj, Gmina, Powiat, Województwo 
		into #tempM
		FROM miasta22  WHERE Rodzaj NOT LIKE '%czêœæ%' AND Rodzaj NOT LIKE '%przysió³ek%' ORDER BY Nazwa;
		--SELECT * FROM #tempM;



INSERT INTO Miasto
SELECT  DISTINCT [#tempM].Nazwa AS Nazwa, Wojewodztwo.ID AS WojewodztwoID, Powiat.ID AS PowiatID

FROM            [#tempM] INNER JOIN
                         Wojewodztwo ON [#tempM].Województwo = Wojewodztwo.Nazwa INNER JOIN
                         Powiat ON [#tempM].Powiat = Powiat.Nazwa
ORDER BY Nazwa

drop table if exists [#tempM];
drop table if exists #tempPW;


SELECT * FROM Miasto where Nazwa  LIKE '%Miêdz%'
