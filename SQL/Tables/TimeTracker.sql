Use TimeTracker
go

CREATE TABLE [Contract]
(
	Id					uniqueidentifier primary key default newsequentialId(),
	AspNetUsersId		nvarchar(450) not null,
	CompanyName			varchar(256) not null,
	CompanyUrl			varchar(256) null,
	EffortType			varchar(40),
	[Description]		varchar(256) not null,
	HourlyRate			money not null,
	StartDate			date not null,
	EndDate				date,
	Created				datetime default getdate(),
	DateM				datetime,
	IsActive			bit default 1,
	Constraint Con_HourlyRate_GreaterThanZero Check(HourlyRate > 0),
	Constraint FK_Contract_AspNetUsersId_AspNetUsers_Id Foreign Key (AspNetUsersId) References AspNetUsers(Id)
)

Create Table Assignment
(	
	Id					uniqueidentifier primary key default newsequentialId(),
	ContractId			uniqueidentifier not null,
	CompanyId			varchar(256) not null,
	[Description]		varchar(256) not null,
	MinutesAvailable	int not null,
	Created				datetime default getdate(),
	DateM				datetime,
	IsActive			bit default 1,
	Constraint FK_Task_ContractId_Contract_Id Foreign key(ContractId)
	References [Contract](Id),
	Index IX_Task_CompanyId Nonclustered(CompanyId)
)

Create Table Effort
(
	Id					uniqueidentifier primary key default newsequentialId(),
	AssignmentId		uniqueidentifier not null,
	[Date]				date not null,
	StartTime			datetime not null,
	EndTime				datetime not null,
	MinutesUsed			int not null,
	Description			varchar(256),
	Created				datetime default getdate(),
	DateM				datetime,
	IsActive			bit default 1,
	Constraint FK_Hours_AssignmentId_Assignment_Id Foreign Key(AssignmentId) 
	References Assignment(Id),
	Constraint Con_Hours_TotalTime_NotNegative CHECK (MinutesUsed >= 0),
	Index IX_Hours_Date NonClustered([Date])
)

/*
drop table effort
drop table Assignment
drop table [contract]
*/