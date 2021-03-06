USE [TimeTracker]
GO

CREATE OR ALTER PROC [dbo].[Report_Hours]
(
	@ContractId			uniqueidentifier			,
	@AspNetUsersId		nvarchar(450)		=	null,
	@dtStart			date				=	null,
	@dtEnd				date				=	null,
	@DaysToRun			int					=	7,
	@isMonth			bit					=	0
)AS
/***********************************************************************************\
Name:		Report_Hours
Purpose:	Get invoice 
			
Created:	2022.01.07
Creator:	David Sargent

			exec [Report_Hours] @ContractId='45AA0948-D06C-EC11-83CA-18CC18D70884',@AspNetUsersId=N'89302165-56b1-454e-a387-510dc293245a'
			
------------------------------------------------------------------------------------
Change Log:	
----------	---	--------------------------------------------------------------------
2022.01.07	DS	Created

\***********************************************************************************/
BEGIN
IF(@dtEnd is null)
BEGIN
	select @dtEnd = getdate()
END
IF(@dtStart is null)
BEGIN
	select @dtStart = DATEADD(day,-@DaysToRun,@dtEnd)
END

--select @dtStart 'start',@dtEnd 'end'
print @dtStart
print @dtEnd

select 
		e.[Date],
		e.StartTime 'Start',
		e.EndTime 'End',
		e.MinutesUsed / 60.0 'Time',
		ISNULL(c.EffortType,'Effort') + ' ' +  a.CompanyId + ' (' + ISNULL(a.[Description],'') + ') ' + ISNULL(e.[Description],'') 'Task/Notes',
		--a.[Description] 'AssignmentDescription',
		--e.[Description] 'EffortDescription',
		c.HourlyRate,
		ISNULL(u.FirstName,'') + ' ' +  ISNULL(u.LastName,'') 'Name'
		--c.CompanyName,
		--Sum(MinutesUsed) Over (Partition by CompanyName) 'TotalMinutes',
		--(Sum(MinutesUsed) Over (Partition by CompanyName) / 60.0) * HourlyRate 'InvoiceTotal',
		--c.Id 'ContractId'
		--,* 
	from Effort e
		inner join Assignment a on ( a.Id = e.AssignmentId ) 
		inner join Contract c on ( c.Id = a.ContractId )
		inner join AspNetUsers u on ( u.Id = c.AspNetUsersId )
	where 1=1
		and e.[Date] >= @dtStart
		and e.[Date] <= @dtEnd
		and c.AspNetUsersId = @AspNetUsersId
	order by e.[Date], e.StartTime

END