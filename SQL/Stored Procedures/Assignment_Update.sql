USE [TimeTracker]
GO

CREATE OR ALTER   PROC [dbo].[Assignment_Update]
(
	@Id					uniqueidentifier	=	null,
	@AspNetUsersId		nvarchar(450)			,
	@ContractId			uniqueidentifier	=	null,
	@CompanyId			varchar(256)			,
	@Description		varchar(256)			,
	@MinutesAvailable	int						,
	@IsAdmin			bit					=	0,
	@IsActive			bit					=	1
)AS
/***********************************************************************************\
Name:		[Assignment_Update]
Purpose:	Create or update details about a assignment
			
Created:	2022.01.02
Creator:	David Sargent

			exec [Assignment_Update]	@CompanyId = '432695', 
										@Description = 'Meetings', 
										@MinutesAvailable = 600, 
										@ContractId = '45AA0948-D06C-EC11-83CA-18CC18D70884',
										@AspNetUsersId = '89302165-56b1-454e-a387-510dc293245a'
------------------------------------------------------------------------------------
Change Log:	
----------	---	--------------------------------------------------------------------
2022.01.02	DS	Created

\***********************************************************************************/

BEGIN
	IF(@Id is null and @ContractId is null)
	BEGIN
		RAISERROR('A value must be provided for either AssignmentId or ContractId or both',16,1)
		return;
	END

	IF(@Id is not null)
	BEGIN
		Update a
			Set 
				a.CompanyId = @CompanyId,
				a.Description = @Description,
				a.MinutesAvailable = @MinutesAvailable,
				a.DateM = getdate()
			from Assignment a
				inner join Contract c on ( c.id = a.ContractId )
			where 1=1
				and a.Id = @Id
				and ContractId = ISNULL(@ContractId,ContractId)
				and c.AspNetUsersId = case when @isAdmin = 1 then c.AspNetUsersId else @AspNetUsersId end 
	END ELSE 
	BEGIN
		IF NOT EXISTS(select * from Contract where Id = @ContractId and AspNetUsersId = case when @isAdmin = 1 then AspNetUsersId else @AspNetUsersId end  )
		BEGIN
			RAISERROR('You do not have permission to this contract',16,1)
			return;
		END ELSE
		BEGIN
		Insert into Assignment(ContractId,CompanyId,Description,MinutesAvailable)
			select @ContractId,@CompanyId,@Description,@MinutesAvailable
		END
	END
END
/*
select * from Assignment
select * from Contract
*/