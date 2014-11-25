
create procedure DropSPViewsTables
as

--delete our tables first
declare @table_count int;
declare @iter int;
-- we know we're going to have tables the first time through
set @table_count = 1 ;
set @iter = 0;
-- check to see how many tables we have in the database
while @table_count > 0
  begin
		exec sp_MSforeachtable "DROP TABLE ? PRINT '? dropped' ";
		SET @table_count = (select COUNT(*) from sysobjects where type = 'U'and name <> 'dtproperties');
		SET @iter = @iter + 1;
  end
print 'iterations: ' + CAST(@iter as varchar);

-- variable to object name
declare @name  varchar(100)
-- variable to hold object type
declare @xtype char(1)
-- variable to hold sql string
declare @sqlstring nvarchar(1000)

declare SPViews_cursor cursor for
SELECT sysobjects.name, sysobjects.xtype
FROM sysobjects
  join sysusers on sysobjects.uid = sysusers.uid
where OBJECTPROPERTY(sysobjects.id, N'IsProcedure') = 1
  or OBJECTPROPERTY(sysobjects.id, N'IsView') = 1 and sysusers.name =
'USERNAME'

open SPViews_cursor

fetch next from SPViews_cursor into @name, @xtype

while @@fetch_status = 0
  begin
-- test object type if it is a stored procedure
   if @xtype = 'P'
      begin
        set @sqlstring = 'drop procedure ' + @name
        exec sp_executesql @sqlstring
        set @sqlstring = ' '
      end
-- test object type if it is a view
   if @xtype = 'V'
      begin
         set @sqlstring = 'drop view ' + @name
         exec sp_executesql @sqlstring
         set @sqlstring = ' '
      end

-- get next record
    fetch next from SPViews_cursor into @name, @xtype
  end

close SPViews_cursor
deallocate SPViews_cursor
