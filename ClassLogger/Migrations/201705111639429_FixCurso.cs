namespace ClassLogger.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixCurso : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Cursos", name: "Coordenador_UserId", newName: "CoordenadorId");
            RenameIndex(table: "dbo.Cursos", name: "IX_Coordenador_UserId", newName: "IX_CoordenadorId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Cursos", name: "IX_CoordenadorId", newName: "IX_Coordenador_UserId");
            RenameColumn(table: "dbo.Cursos", name: "CoordenadorId", newName: "Coordenador_UserId");
        }
    }
}
