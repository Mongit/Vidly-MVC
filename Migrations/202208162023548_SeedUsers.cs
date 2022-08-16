namespace Vidly2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'8fcfcb02-8fe8-4ccf-903c-f33994dc7169', N'admin@vidly.com', 0, N'AGEHFqlz2oxVulnDhYm+afBeVvXaidd6fjuSaTEUGMGbPDGGL5bGr6wZgXwyimmeEg==', N'9b23d912-393f-41bb-b960-ab56c5c088ff', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'f354f342-7a6e-4dc3-8df3-ce325a0064ed', N'guest@vidly.com', 0, N'AMGLMvpMVZPFsEmD3ggkxxqrtij2nlYi8ZEZcLA8avW7OtVBho1AqjLpJWhZRw9Beg==', N'5d323b03-7b7c-41a5-bcdc-129f9041e566', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')
                    
                INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'a69af31e-ce84-4042-9959-8a29738d5e96', N'CanManageMovies')
        
                INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'8fcfcb02-8fe8-4ccf-903c-f33994dc7169', N'a69af31e-ce84-4042-9959-8a29738d5e96') 
                ");

        }
        
        public override void Down()
        {
        }
    }
}
