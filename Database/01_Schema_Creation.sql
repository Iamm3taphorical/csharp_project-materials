 
 
 
 
 
 

 
 
 
 

 
CREATE TABLE [User] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [Name] NVARCHAR(100) NOT NULL,
    [Email] NVARCHAR(100) NOT NULL UNIQUE,
    [Password] NVARCHAR(255) NOT NULL,  
    [UserType] NVARCHAR(50) NOT NULL DEFAULT 'Standard'
);
GO

 
CREATE TABLE [ApiSource] (
    [ApiId] INT IDENTITY(1,1) PRIMARY KEY,
    [ApiName] NVARCHAR(100) NOT NULL,
    [BaseUrl] NVARCHAR(255) NOT NULL,
    [RateLimit] INT NOT NULL DEFAULT 60
);
GO

 
CREATE TABLE [Request] (
    [RequestId] INT IDENTITY(1,1) PRIMARY KEY,
    [UserId] INT NOT NULL,
    [ApiId] INT NULL,  
    [RequestType] NVARCHAR(50) NOT NULL,  
    [QueryKey] NVARCHAR(100) NOT NULL,  
    [RequestTime] DATETIME NOT NULL DEFAULT GETDATE(),
    
    CONSTRAINT [FK_Request_User] FOREIGN KEY ([UserId]) REFERENCES [User]([Id]),
    CONSTRAINT [FK_Request_ApiSource] FOREIGN KEY ([ApiId]) REFERENCES [ApiSource]([ApiId])
);
GO

 
 
CREATE TABLE [HashBucket] (
    [BucketId] INT PRIMARY KEY,  
    [BucketIndex] INT NOT NULL UNIQUE  
);
GO

 
 
CREATE TABLE [HashEntry] (
    [EntryId] INT IDENTITY(1,1) PRIMARY KEY,
    [BucketId] INT NOT NULL,
    [KeyValue] NVARCHAR(100) NOT NULL INDEX [IX_HashEntry_Key],  
    [DataJson] NVARCHAR(MAX) NOT NULL,  
    [NextEntryId] INT NULL,  
    [CreatedAt] DATETIME NOT NULL DEFAULT GETDATE(),

    CONSTRAINT [FK_HashEntry_Bucket] FOREIGN KEY ([BucketId]) REFERENCES [HashBucket]([BucketId]),
    CONSTRAINT [FK_HashEntry_Next] FOREIGN KEY ([NextEntryId]) REFERENCES [HashEntry]([EntryId])
);
GO

 
CREATE TABLE [CacheLog] (
    [LogId] INT IDENTITY(1,1) PRIMARY KEY,
    [RequestId] INT NOT NULL,
    [HitOrMiss] NVARCHAR(10) NOT NULL,  
    [AccessTime] DATETIME NOT NULL DEFAULT GETDATE(),

    CONSTRAINT [FK_CacheLog_Request] FOREIGN KEY ([RequestId]) REFERENCES [Request]([RequestId])
);
GO

 
 
 
 
DECLARE @i INT = 0;
WHILE @i < 1000
BEGIN
    INSERT INTO [HashBucket] ([BucketId], [BucketIndex]) VALUES (@i, @i);
    SET @i = @i + 1;
END
GO
