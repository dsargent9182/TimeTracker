USE [TimeTracker]
GO

CREATE OR ALTER PROC [dbo].[Effort_Get]
(
	@Id					uniqueidentifier = null,
	@AssignmentId		uniqueidentifier = null,
	@CompanyId			varchar(256) = null,
	@AspNetUsersId		nvarchar(450) = null,
	@IsAdmin			bit = 0,
	@IsActive			bit = null
)AS
/***********************************************************************************\
Name:		[Effort_Get]
Purpose:	Get details about effort
			
Created:	2022.01.02
Creator:	David Sargent

			exec [Effort_Get] @AspNetUsersId = '89302165-56b1-454e-a387-510dc293245a'

------------------------------------------------------------------------------------
Change Log:	
----------	---	--------------------------------------------------------------------
2022.01.02	DS	Created

\***********************************************************************************/
BEGIN

select 
		e.Id,
		e.AssignmentId,
		e.Date 'WorkDate',
		e.Description,
		e.StartTime,
		e.EndTime,
		e.IsActive,
		e.MinutesUsed,
		e.Created,
		e.DateM,
		a.CompanyId,
		a.Description 'AssignmentDescription',
		a.ContractId,
		a.IsActive 'AssignmentActive',
		a.MinutesAvailable 'AssignmentMinutesAllowed',
		c.AspNetUsersId,
		c.CompanyName,
		c.Description 'ContractDescription',
		c.HourlyRate,
		c.StartDate,
		c.EndDate,
		c.CompanyUrl,
		c.IsActive 'ContractIsActive'
	from Effort e
		inner join Assignment a on ( a.Id = e.AssignmentId ) 
		inner join [Contract] c on ( c.id = a.ContractId )
	where 1=1
		and e.Id = ISNULL(@Id,e.Id)
		and a.Id = ISNULL(@AssignmentId,a.Id)
		and CompanyId = ISNULL(@CompanyId,CompanyId)
		and c.AspNetUsersId = case when @IsAdmin = 1 then AspNetUsersId else @AspNetUsersId end
		and e.IsActive = ISNULL(@IsActive,e.IsActive)
	order by e.Date desc,e.StartTime
END

-- select *  from Assignment a inner join [Contract] c on ( c.id = a.ContractId )
-- select * From effort