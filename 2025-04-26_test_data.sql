SELECT TOP (1000) [Id]
      ,[Name]
      ,[Description]
      ,[CreatedAt]
      ,[UserId]
      ,[Tags]
  FROM [LearnTrack].[dbo].[Topics]

  order by CreatedAt desc


DECLARE @UserId UNIQUEIDENTIFIER = ''; -- user id
DECLARE @Counter INT = 1;

WHILE @Counter <= 200
BEGIN
    DECLARE @Id UNIQUEIDENTIFIER = NEWID();
    DECLARE @Name NVARCHAR(255) = CONCAT('Topic ', @Counter);
    DECLARE @Description NVARCHAR(MAX) = CONCAT('Desc for Topic ', @Counter, '.');
    DECLARE @CreatedAt DATETIME2 = DATEADD(DAY, -@Counter, GETDATE());

    -- build random tags
    DECLARE @Tags NVARCHAR(MAX);

    IF (@Counter % 5 = 0)
        SET @Tags = 'Backend,API,Security';
    ELSE IF (@Counter % 3 = 0)
        SET @Tags = 'Frontend,UI,UX';
    ELSE IF (@Counter % 2 = 0)
        SET @Tags = 'Database,SQL';
    ELSE
        SET @Tags = 'Misc';

    INSERT INTO [LearnTrack].[dbo].[Topics] ([Id], [Name], [Description], [CreatedAt], [UserId], [Tags])
    VALUES (@Id, @Name, @Description, @CreatedAt, @UserId, @Tags);

    SET @Counter = @Counter + 1;
END