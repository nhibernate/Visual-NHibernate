In order to get started with the ArchAngel source code, you will need the following installed on your machine:

* DevExpress DXperience 8.2.6
* DevComponents DotNetBar - latest version
* Actipro Syntax Editor
* Actipro UI Studio

In order to have all of the unit tests pass, you also need the following installed:
* SQL Server Compact 3.5 SP1
* SQL Server 2005 (Express is fine).

You also need to set your SQL Server instance up to use Windows Authentication. The tests will setup and teardown a database called TestDatabase,
if you have a database you use called that, don't run the tests!