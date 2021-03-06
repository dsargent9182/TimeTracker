USE [TimeTracker]
GO

CREATE OR ALTER PROC [dbo].[Assignment_Get]
(
	@Id					uniqueidentifier = null,
	@ContractId			uniqueidentifier = null,
	@CompanyId			varchar(256) = null,
	@AspNetUsersId		nvarchar(450) = null,
	@IsAdmin			bit = 0
)AS
/***********************************************************************************\
Name:		Assignment_Get
Purpose:	Create or update details about a task
			
Created:	2022.01.02
Creator:	David Sargent

			exec [Assignment_Get] @AspNetUsersId = '4f5062d0-0975-4c42-b16b-a7ec4f06c178' --sargent.dev
			exec Assignment_Get @AspNetUsersId=N'89302165-56b1-454e-a387-510dc293245a',@Id='45AA0948-D06C-EC11-83CA-18CC18D70884',@ContractId=NULL,@CompanyId=NULL,@IsAdmin=0
------------------------------------------------------------------------------------
Change Log:	
----------	---	--------------------------------------------------------------------
2022.01.02	DS	Created

\***********************************************************************************/
BEGIN

--Get hours used per assignment
select a.id 'AssignmentId',a.Description'Assignment',sum(e.MinutesUsed) 'MinutesUsed'
		into #tmp_20220107_EffortMinutes
	from Assignment a
		inner join Effort e on ( e.AssignmentId = a.id )
		inner join [Contract] c on ( c.id = a.ContractId )
		and c.AspNetUsersId = case when @IsAdmin = 1 then AspNetUsersId else @AspNetUsersId end
	group by a.Id,a.Description


--select * from #tmp_20220107_EffortMinutes
		

select 
		a.Id,
		a.ContractId,
		a.CompanyId,
		a.Description,
		ISNULL(a.MinutesAvailable,0.00) 'MinutesAvailable',
		ISNULL(em.MinutesUsed,0.00) 'MinutesUsed',
		ISNULL(a.MinutesAvailable,0.00) - ISNULL(em.MinutesUsed,0.00) 'MinutesRemaining',
		a.Created,
		a.DateM,
		a.IsActive,
		c.AspNetUsersId,
		c.CompanyName,
		c.CompanyUrl,
		c.Description 'ContractDescription',
		c.HourlyRate,
		c.StartDate,
		c.EndDate,
		c.IsActive 'ContractIsActive'
	from Assignment a
		inner join [Contract] c on ( c.id = a.ContractId )
		left outer join #tmp_20220107_EffortMinutes em on ( em.AssignmentId = a.Id )
	where 1=1
		and a.Id = ISNULL(@Id,a.Id)
		and ContractId = ISNULL(@ContractId,ContractId)
		and CompanyId = ISNULL(@CompanyId,CompanyId)
		and c.AspNetUsersId = case when @IsAdmin = 1 then AspNetUsersId else @AspNetUsersId end


drop table #tmp_20220107_EffortMinutes
END

-- select *  from Assignment a inner join [Contract] c on ( c.id = a.ContractId )
