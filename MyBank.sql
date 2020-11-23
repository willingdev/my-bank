CREATE TABLE [dbo].[customer] (
    [id] int IDENTITY(1,1) PRIMARY KEY,
    [name] nvarchar NOT NULL,
    [last_name] nvarchar,
    [address] nvarchar,
    [mobile] nvarchar
);
CREATE TABLE [dbo].[account] (
    [id] nvarchar(20)  ,
    [customer_id] int  ,
    [created] datetime2,
    [updated] datetime2,
    [total_money] money,
     primary key (id, customer_id)
);