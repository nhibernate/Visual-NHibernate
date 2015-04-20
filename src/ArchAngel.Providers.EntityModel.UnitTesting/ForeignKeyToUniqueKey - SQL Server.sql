CREATE TABLE [PrimaryTable]( [PrimaryID] [int] NOT NULL )

CREATE UNIQUE NONCLUSTERED INDEX [UQ_Table_1] ON [PrimaryTable] ( [PrimaryID] ASC )

CREATE TABLE [dbo].[ForeignTable] ([ForeignID] [int] NOT NULL) 

ALTER TABLE [ForeignTable] ADD CONSTRAINT [FK_Table1] FOREIGN KEY([ForeignID]) REFERENCES [PrimaryTable] ([PrimaryID])
