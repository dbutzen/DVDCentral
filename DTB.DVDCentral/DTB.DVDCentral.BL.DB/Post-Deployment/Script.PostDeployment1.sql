/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

:r .\DefaultData\Customer.sql
:r .\DefaultData\Director.sql
:r .\DefaultData\Format.sql
:r .\DefaultData\Genre.sql
:r .\DefaultData\Movie.sql
:r .\DefaultData\MovieGenre.sql
:r .\DefaultData\Order.sql
:r .\DefaultData\OrderItem.sql
:r .\DefaultData\Rating.sql
:r .\DefaultData\User.sql