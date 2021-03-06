USE [TimeTracker]
GO

CREATE OR ALTER PROC [dbo].[Contract_Get]
(
	@Id					uniqueidentifier = null,
	@AspNetUsersId		nvarchar(450) = null,
	@IsActive			bit = null,
	@IsAdmin			bit = 0
)AS
/***********************************************************************************\
Name:		Contract_Get
Purpose:	Get details about a contract
			
Created:	2022.01.02
Creator:	David Sargent

			exec [Contract_Get]  @AspNetUsersId = '89302165-56b1-454e-a387-510dc293245a',@IsAdmin = 1
------------------------------------------------------------------------------------
Change Log:	
----------	---	--------------------------------------------------------------------
2022.01.02	DS	Created

\***********************************************************************************/
BEGIN

select * 
	from Contract
	where 1=1
		and Id = ISNULL(@Id,Id)
		and IsActive = ISNULL(@IsActive,IsActive)
		and AspNetUsersId = case when @IsAdmin = 1 then AspNetUsersId else @AspNetUsersId end
END

--select * from contract