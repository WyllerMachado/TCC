namespace ClassLogger.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedAdminCoordRoles : DbMigration
    {
        public override void Up()
        {
            Sql(@"INSERT INTO [dbo].[AspNetUsers] ([Id], [Nome], [Cpf], [DataNascimento], [Celular], [Foto], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'b9c70820-ac6e-43e9-9423-5ff631373533', N'Wyller Ribeiro Machado', N'136.610.917-19', N'1994-11-21', N'(21) 98159-2776', NULL, N'admin@tcc.com', 0, N'ADgBNSPPVU1Fscebum9UWw/KgR8Ly/WOChCuqpLRwU26BIFeYlylA2Y5ahiUjdwJOQ==', N'25c59c09-c74e-4975-a264-ef91e0303c6a', NULL, 0, 0, NULL, 1, 0, N'admin@tcc.com')
                  INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'7bc333b3-0693-4aa2-8d04-2eb4d1e1b3e6', N'Administrador')
                  INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'de409f58-62d1-4573-9b8a-100b41cbddc0', N'Coordenador')
                  INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'b9c70820-ac6e-43e9-9423-5ff631373533', N'7bc333b3-0693-4aa2-8d04-2eb4d1e1b3e6')"
            );
        }
        
        public override void Down()
        {
        }
    }
}
