CREATE TABLE [Table1] (
	[Column1] int IDENTITY NOT NULL,
	[Column2] nvarchar(100) NULL,
	[Column3] datetime NOT NULL   
);

ALTER TABLE [Table1] ADD CONSTRAINT PK_Table1 PRIMARY KEY NONCLUSTERED ([Column1]);

CREATE UNIQUE INDEX [UQ__Table1__000000000000000C] ON [Table1] ([Column1] Asc);