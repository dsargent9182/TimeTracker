USE [TimeTracker]
GO

CREATE OR ALTER PROC [dbo].[Effort_Update]
(
	@Id					uniqueidentifier	= null,
	@AssignmentId		uniqueidentifier	= null,
	@AspNetUsersId		nvarchar(450),
	@Date				date				= null,
	@Description		varchar(256)		= null,
	@StartTime			datetime			= null ,
	@EndTime			datetime			= null,
	@IsActive			bit					= null,
	@IsAdmin			bit					= 0
)AS
/***********************************************************************************\
Name:		Effort_Update
Purpose:	Create or update details about effort
			
Created:	2022.01.02
Creator:	David Sargent

			exec [Effort_Update]	@AspNetUsersId = '89302165-56b1-454e-a387-510dc293245a',
									@Id = '07BD184C-C06D-EC11-83CB-00090FFE0001',
									@IsActive = 0,
									@AssignmentId = 'A36C68ED-B66D-EC11-83CB-00090FFE0001',
									@Id = 'A9E7009F-BF6D-EC11-83CB-00090FFE0001',
									@Date = '2022-02-01', 
									@StartTime = '2022-01-02 16:39',
									@EndTime = '2022-01-02 17:59',
			
------------------------------------------------------------------------------------
Change Log:	
----------	---	--------------------------------------------------------------------
2022.01.02	DS	Created

\***********************************************************************************/
BEGIN
IF(@StartTime > @EndTime)
BEGIN
	RAISERROR('Start time cannot be after end time',16,1)
	return;
END

IF(@Id is null and @AssignmentId is null)
BEGIN
	RAISERROR('A value for Id and/or AssignmentId must be provided',16,1)
	return;
END

DECLARE @minutes int
select @minutes = DATEDiff(MINUTE,@StartTime,@EndTime)

IF(@Id IS Null)
BEGIN

Insert into Effort(AssignmentId,Date,StartTime,EndTime,MinutesUsed,Description)
		select @AssignmentId,@Date,@StartTime,@EndTime, @minutes,@Description

END
ELSE
BEGIN
	IF NOT EXISTS(select * 
							from Effort e 
								inner join Assignment a on (e.AssignmentId = a.Id )
							where 1=1
								and e.Id = @Id
								and a.Id = ISNULL(@AssignmentId,a.Id) )
	BEGIN
		RAISERROR('A record with the provided effortId and/or assignment id was not found',16,1)
		return;
	END

	ELSE IF NOT EXISTS(
							select * 
								from Effort e 
									inner join Assignment a on (e.AssignmentId = a.Id ) 
									inner join [Contract] c on ( c.id = a.ContractId ) 
								where 1=1
									and e.Id = @Id
									and c.AspNetUsersId = case when @IsAdmin = 1 then c.AspNetUsersId else @AspNetUsersId end
									and a.Id = ISNULL(@AssignmentId,a.Id)
				)
	BEGIN
		
		RAISERROR('You do not have access to update this record',16,1)
		return;

	END ELSE 
	BEGIN
	print 'begin update'
		Update e 
			set 
				e.Date = ISNULL(@Date,e.Date),
				e.StartTime = ISNULL(@StartTime,e.StartTime),
				e.EndTime = ISNULL(@EndTime,e.EndTime),
				e.MinutesUsed = ISNULL(@minutes,e.MinutesUsed),
				e.Description = ISNULL(@Description,e.Description),
				e.DateM = GETDATE(),
				e.IsActive = ISNULL(@IsActive,e.IsActive)
			from Effort e
				inner join Assignment a on ( e.AssignmentId = a.Id )
				inner join [Contract] c on ( c.id = a.ContractId )
			where 1=1
				and e.Id = @Id
				and c.AspNetUsersId = case when @IsAdmin = 1 then c.AspNetUsersId else @AspNetUsersId end
				and a.Id = ISNULL(@AssignmentId,a.Id)

	END
END
END
/*
select * from contract
select * from assignment
select * from Effort
*/
