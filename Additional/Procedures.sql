CREATE PROCEDURE dbo.AddUser
    @Id varchar(36),
    @Role varchar(50),
	@Name varchar(50),
	@Email varchar(50),
	@Password varchar(50),
	@CreateAt datetime

AS 

    SET NOCOUNT ON;
    INSERT INTO [dbo].[Users]
           ([Id]
           ,[Role]
           ,[Name]
           ,[Email]
           ,[Password]
           ,[CreateAt])
     VALUES
           (@Id
           ,@Role
           ,@Name
           ,@Email
           ,@Password
           ,@CreateAt)
GO


---------

CREATE PROCEDURE dbo.GetUserById
    @Id varchar(36)

AS 

    SET NOCOUNT ON;
    SELECT [Id]
      ,[Role]
      ,[Name]
      ,[Email]
      ,[Password]
      ,[CreateAt]
  FROM [dbo].[Users]
  WHERE [Id] = @Id
GO

--------

CREATE PROCEDURE dbo.GetUserByEmail
    @Email varchar(36)

AS 

    SET NOCOUNT ON;
    SELECT [Id]
      ,[Role]
      ,[Name]
      ,[Email]
      ,[Password]
      ,[CreateAt]
  FROM [dbo].[Users]
  WHERE [Email] = @Email
GO


--------


CREATE PROCEDURE dbo.GetEventByName
    @Name varchar(50)

AS 

    SET NOCOUNT ON;
    SELECT [Id]
      ,[Name]
      ,[Description]
      ,[CreateAt]
      ,[StartDate]
      ,[EndDate]
      ,[UpdateDate]
  FROM [dbo].[Events]
  WHERE [Name] = @Name
GO

---------

CREATE PROCEDURE dbo.AddEvent
		@Id uniqueidentifier,
		@Name varchar(256),
        @Description varchar(256),
        @CreateAt datetime,
        @StartDate datetime,
        @EndDate datetime,
        @UpdateDate datetime

AS 

    INSERT INTO [dbo].[Events]
           ([Id]
           ,[Name]
           ,[Description]
           ,[CreateAt]
           ,[StartDate]
           ,[EndDate]
           ,[UpdateDate])
     VALUES
           (@Id
           ,@Name
           ,@Description
           ,@CreateAt
           ,@StartDate
           ,@EndDate
           ,@UpdateDate)
GO


-----

CREATE PROCEDURE dbo.GetEventById
    @Id uniqueidentifier

AS 

    SET NOCOUNT ON;
    SELECT [Id]
      ,[Name]
      ,[Description]
      ,[CreateAt]
      ,[StartDate]
      ,[EndDate]
      ,[UpdateDate]
  FROM [dbo].[Events]
  WHERE [Id]= @Id
GO


------


CREATE PROCEDURE dbo.BrowseEvent
    @Name varchar(50)

AS 

    SET NOCOUNT ON;
    SELECT [Id]
      ,[Name]
      ,[Description]
      ,[CreateAt]
      ,[StartDate]
      ,[EndDate]
      ,[UpdateDate]
  FROM [dbo].[Events]
  WHERE [Name] like '%' + @Name + '%'
GO


-------

CREATE PROCEDURE dbo.DeleteEvent
    @Id uniqueidentifier

AS 

    SET NOCOUNT ON;
    DELETE FROM [dbo].[Events]
	WHERE [Id]= @Id
GO

-------


CREATE PROCEDURE dbo.UpdateEvent
		@Id uniqueidentifier,
		@Name varchar(256),
        @Description varchar(256),
        @CreateAt datetime,
        @StartDate datetime,
        @EndDate datetime,
        @UpdateDate datetime

AS 

    UPDATE [dbo].[Events]
   SET [Name] = @Name,
      [Description] = @Description,
      [CreateAt] = @CreateAt,
      [StartDate] = @StartDate,
      [EndDate] = @EndDate,
      [UpdateDate] = @UpdateDate
 WHERE [Id] = @Id
GO


--------


CREATE PROCEDURE dbo.AddTicket
		@Id uniqueidentifier,
		@EventId uniqueidentifier,
        @Seating int,
        @Price decimal(18,0)

AS 

    INSERT INTO [dbo].[Tickets]
           ([Id]
           ,[EventId]
           ,[Seating]
           ,[Price])
     VALUES
           (@Id
           ,@EventId
           ,@Seating
           ,@Price)
GO

-------

CREATE PROCEDURE dbo.GetTicketsByEventId
		@EventId uniqueidentifier

AS 

    SELECT [Id]
      ,[EventId]
      ,[Seating]
      ,[Price]
      ,[UserId]
      ,[PurchasedAt]
  FROM [dbo].[Tickets]
  WHERE [EventId] = @EventId
GO

-----------

CREATE PROCEDURE dbo.PurchaseTicket
		@TicketId uniqueidentifier,
		@PurchasedAt datetime,
		@UserId uniqueidentifier

AS 

   UPDATE [dbo].[Tickets]
   SET [PurchasedAt] = @PurchasedAt,
		[UserId] = @UserId
 WHERE [Id] = @TicketId
GO

-----------

CREATE PROCEDURE dbo.CanceledTicket
		@TicketId uniqueidentifier

AS 

   UPDATE [dbo].[Tickets]
   SET [PurchasedAt] = null,
		[UserId] = null
 WHERE [Id] = @TicketId
GO

 ----------------------

CREATE TYPE [dbo].[UT_Tickets] AS TABLE(
	[Id] [uniqueidentifier] NOT NULL,
	[EventId] [uniqueidentifier] NOT NULL,
	[Seating] [int] NOT NULL,
	[Price] [decimal](18, 0) NOT NULL,
	[UserId] [uniqueidentifier] NULL,
	[PurchasedAt] [datetime] NULL
);

------

CREATE PROCEDURE dbo.AddTickets
		@TicketsArray Ticket READONLY

AS
	Declare @id uniqueidentifier
	select top 1 @id = [Id] from @TicketsArray;
	while @id is not null
	begin
		
		Select * from @TicketsArray Where [Id] = @id;

		select top 1 @id = [Id] from @TicketsArray;
	end
GO


   CREATE PROCEDURE dbo.[AddTickets] (@Tickets [UT_Tickets])  
    AS  
    BEGIN  
      
    INSERT INTO dbo.Tickets   
    SELECT * FROM @Tickets 
    END 



   CREATE PROCEDURE dbo.AddTicketsTable (@Tickets [UT_Tickets] ReadOnly)  
    AS  
    BEGIN  
      
    INSERT INTO dbo.Tickets   
    SELECT * FROM @Tickets 
    END 