namespace BET.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init_data : DbMigration
    {
        public override void Up()
        {
	        Sql(@"
			INSERT INTO [dbo].[Users]
				   ([Login]
				   ,[Password]
           ,[FullName]
				   ,[IsAdmin]
				   ,[IsDeleted]
				   ,[IsActive])
			 VALUES
				   ('admin@test.com','admin!', 'admin', 1, 0, 1);");

	        Sql(@"
			INSERT INTO [dbo].[Users]
				   ([Login]
				   ,[Password]
           ,[FullName]
				   ,[IsAdmin]
				   ,[IsDeleted]
				   ,[IsActive])
			 VALUES
				   ('user@test.com','user!', 'user', 0, 0, 1);");	    

	        Sql(@" 
			INSERT INTO [dbo].[Products]
				   ([Name]
				   ,[Price]
           ,[Quantity]
				   ,[Image]
				   ,[LastModifiedUserId]
				   ,[LastModifiedDateTime])
			 VALUES
				   ('Product 1',10, 10, '', 1, getdate());");
	        Sql(@" 
			INSERT INTO [dbo].[Products]
				   ([Name]
				   ,[Price]
           ,[Quantity]
				   ,[Image]
				   ,[LastModifiedUserId]
				   ,[LastModifiedDateTime])
			 VALUES
				   ('Product 2',5, 10, '', 1, getdate());");
	        Sql(@" 
			INSERT INTO [dbo].[Products]
				   ([Name]
				   ,[Price]
           ,[Quantity]
				   ,[Image]
				   ,[LastModifiedUserId]
				   ,[LastModifiedDateTime])
			 VALUES
				   ('Product 3',15, 8, '', 1, getdate());");
		}
        
        public override void Down()
        {
	        Sql(@"
DELETE FROM [dbo].[Products]  
DELETE FROM [dbo].[Users]  
");
        }
    }
}
