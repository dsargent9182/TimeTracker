USE [TimeTracker]
GO

CREATE OR ALTER PROC [dbo].[Contract_Update]
(
	@Id				uniqueidentifier = null,
	@AspNetUsersId	nvarchar(450),
	@CompanyName	varchar(256),
	@Description	varchar(256),
	@StartDate		date,
	@EndDate		date = null,
	@HourlyRate		money
)AS
/***********************************************************************************\
Name:		Contract_Update
Purpose:	Create or update details about a contract
			
Created:	2022.01.02
Creator:	David Sargent

			exec [Contract_Update]	@CompanyName = 'Nuvei', 
									@Description = 'Ongoing contract', 
									@HourlyRate = 45, 
									@StartDate = '2021-01-01', 
									@AspNetUsersId = '89302165-56b1-454e-a387-510dc293245a',
									@Id = '45AA0948-D06C-EC11-83CA-18CC18D70884' 
------------------------------------------------------------------------------------
Change Log:	
----------	---	--------------------------------------------------------------------
2022.01.02	DS	Created

\***********************************************************************************/

BEGIN

IF( @CompanyName = '' )
BEGIN
	select @CompanyName = null
END
IF( @Description = '' )
BEGIN
	select @Description = null
END

IF(@Id is not null)
BEGIN
BEGIN
	Update Contract
		set
			Description = @Description,
			HourlyRate = @HourlyRate,
			StartDate = @StartDate,
			EndDate = @EndDate,
			DateM = GETDATE()
		where 1=1
			and Id = @Id
			and AspNetUsersId = @AspNetUsersId
END

END
ELSE
BEGIN
Insert into Contract(CompanyName,Description,HourlyRate,StartDate,EndDate,AspNetUsersId)
	select @CompanyName,@Description,@HourlyRate,@StartDate,@EndDate,@AspNetUsersId
END


END

--select * from Contract