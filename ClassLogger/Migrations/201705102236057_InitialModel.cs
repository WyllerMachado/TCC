namespace ClassLogger.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Alunos",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        CursoId = c.Int(),
                        PeriodoId = c.Int(),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Cursos", t => t.CursoId)
                .ForeignKey("dbo.Periodos", t => t.PeriodoId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.CursoId)
                .Index(t => t.PeriodoId);
            
            CreateTable(
                "dbo.Boletins",
                c => new
                    {
                        AlunoId = c.String(nullable: false, maxLength: 128),
                        TurmaId = c.Int(nullable: false),
                        NotaAv1 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NotaAv2 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NotaAv3 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NotaFinal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Presencas = c.Int(nullable: false),
                        Aprovado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.AlunoId, t.TurmaId })
                .ForeignKey("dbo.Alunos", t => t.AlunoId, cascadeDelete: true)
                .ForeignKey("dbo.Turmas", t => t.TurmaId, cascadeDelete: true)
                .Index(t => t.AlunoId)
                .Index(t => t.TurmaId);
            
            CreateTable(
                "dbo.Turmas",
                c => new
                    {
                        TurmaId = c.Int(nullable: false, identity: true),
                        DisciplinaId = c.Int(nullable: false),
                        ProfessorId = c.String(nullable: false, maxLength: 128),
                        PeriodoId = c.Int(nullable: false),
                        Curso_CursoId = c.Int(),
                    })
                .PrimaryKey(t => t.TurmaId)
                .ForeignKey("dbo.Cursos", t => t.Curso_CursoId)
                .ForeignKey("dbo.Disciplinas", t => t.DisciplinaId, cascadeDelete: true)
                .ForeignKey("dbo.Periodos", t => t.PeriodoId, cascadeDelete: true)
                .ForeignKey("dbo.Professores", t => t.ProfessorId, cascadeDelete: true)
                .Index(t => t.DisciplinaId)
                .Index(t => t.ProfessorId)
                .Index(t => t.PeriodoId)
                .Index(t => t.Curso_CursoId);
            
            CreateTable(
                "dbo.Aulas",
                c => new
                    {
                        AulaId = c.Int(nullable: false, identity: true),
                        TurmaId = c.Int(nullable: false),
                        DataInicio = c.DateTime(nullable: false),
                        Duracao = c.Time(nullable: false, precision: 7),
                        Codigo = c.Int(nullable: false),
                        Encerrada = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.AulaId)
                .ForeignKey("dbo.Turmas", t => t.TurmaId, cascadeDelete: true)
                .Index(t => t.TurmaId);
            
            CreateTable(
                "dbo.RegistrosDeAluno",
                c => new
                    {
                        AulaId = c.Int(nullable: false),
                        AlunoId = c.String(nullable: false, maxLength: 128),
                        HorarioEntrada = c.DateTime(nullable: false),
                        HorarioSaida = c.DateTime(nullable: false),
                        Permanencia = c.Time(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => new { t.AulaId, t.AlunoId })
                .ForeignKey("dbo.Alunos", t => t.AlunoId, cascadeDelete: true)
                .ForeignKey("dbo.Aulas", t => t.AulaId, cascadeDelete: true)
                .Index(t => t.AulaId)
                .Index(t => t.AlunoId);
            
            CreateTable(
                "dbo.Disciplinas",
                c => new
                    {
                        DisciplinaId = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false),
                        Ementa = c.String(),
                    })
                .PrimaryKey(t => t.DisciplinaId);
            
            CreateTable(
                "dbo.Cursos",
                c => new
                    {
                        CursoId = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false),
                        Coordenador_UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.CursoId)
                .ForeignKey("dbo.Coordenadores", t => t.Coordenador_UserId)
                .Index(t => t.Coordenador_UserId);
            
            CreateTable(
                "dbo.Periodos",
                c => new
                    {
                        PeriodoId = c.Int(nullable: false, identity: true),
                        CursoId = c.Int(nullable: false),
                        Nome = c.String(nullable: false),
                        DataInicio = c.DateTime(nullable: false),
                        DataFim = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PeriodoId)
                .ForeignKey("dbo.Cursos", t => t.CursoId, cascadeDelete: true)
                .Index(t => t.CursoId);
            
            CreateTable(
                "dbo.Professores",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Nome = c.String(nullable: false),
                        Cpf = c.String(nullable: false),
                        DataNascimento = c.DateTime(nullable: false, storeType: "date"),
                        Celular = c.String(),
                        Foto = c.Binary(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Coordenadores",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.TurmaAlunoes",
                c => new
                    {
                        Turma_TurmaId = c.Int(nullable: false),
                        Aluno_UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Turma_TurmaId, t.Aluno_UserId })
                .ForeignKey("dbo.Turmas", t => t.Turma_TurmaId, cascadeDelete: true)
                .ForeignKey("dbo.Alunos", t => t.Aluno_UserId, cascadeDelete: true)
                .Index(t => t.Turma_TurmaId)
                .Index(t => t.Aluno_UserId);
            
            CreateTable(
                "dbo.CursoDisciplinas",
                c => new
                    {
                        Curso_CursoId = c.Int(nullable: false),
                        Disciplina_DisciplinaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Curso_CursoId, t.Disciplina_DisciplinaId })
                .ForeignKey("dbo.Cursos", t => t.Curso_CursoId, cascadeDelete: true)
                .ForeignKey("dbo.Disciplinas", t => t.Disciplina_DisciplinaId, cascadeDelete: true)
                .Index(t => t.Curso_CursoId)
                .Index(t => t.Disciplina_DisciplinaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Coordenadores", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Cursos", "Coordenador_UserId", "dbo.Coordenadores");
            DropForeignKey("dbo.Alunos", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Alunos", "PeriodoId", "dbo.Periodos");
            DropForeignKey("dbo.Alunos", "CursoId", "dbo.Cursos");
            DropForeignKey("dbo.Boletins", "TurmaId", "dbo.Turmas");
            DropForeignKey("dbo.Turmas", "ProfessorId", "dbo.Professores");
            DropForeignKey("dbo.Professores", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Turmas", "PeriodoId", "dbo.Periodos");
            DropForeignKey("dbo.Turmas", "DisciplinaId", "dbo.Disciplinas");
            DropForeignKey("dbo.Turmas", "Curso_CursoId", "dbo.Cursos");
            DropForeignKey("dbo.Periodos", "CursoId", "dbo.Cursos");
            DropForeignKey("dbo.CursoDisciplinas", "Disciplina_DisciplinaId", "dbo.Disciplinas");
            DropForeignKey("dbo.CursoDisciplinas", "Curso_CursoId", "dbo.Cursos");
            DropForeignKey("dbo.Aulas", "TurmaId", "dbo.Turmas");
            DropForeignKey("dbo.RegistrosDeAluno", "AulaId", "dbo.Aulas");
            DropForeignKey("dbo.RegistrosDeAluno", "AlunoId", "dbo.Alunos");
            DropForeignKey("dbo.TurmaAlunoes", "Aluno_UserId", "dbo.Alunos");
            DropForeignKey("dbo.TurmaAlunoes", "Turma_TurmaId", "dbo.Turmas");
            DropForeignKey("dbo.Boletins", "AlunoId", "dbo.Alunos");
            DropIndex("dbo.CursoDisciplinas", new[] { "Disciplina_DisciplinaId" });
            DropIndex("dbo.CursoDisciplinas", new[] { "Curso_CursoId" });
            DropIndex("dbo.TurmaAlunoes", new[] { "Aluno_UserId" });
            DropIndex("dbo.TurmaAlunoes", new[] { "Turma_TurmaId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Coordenadores", new[] { "UserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Professores", new[] { "UserId" });
            DropIndex("dbo.Periodos", new[] { "CursoId" });
            DropIndex("dbo.Cursos", new[] { "Coordenador_UserId" });
            DropIndex("dbo.RegistrosDeAluno", new[] { "AlunoId" });
            DropIndex("dbo.RegistrosDeAluno", new[] { "AulaId" });
            DropIndex("dbo.Aulas", new[] { "TurmaId" });
            DropIndex("dbo.Turmas", new[] { "Curso_CursoId" });
            DropIndex("dbo.Turmas", new[] { "PeriodoId" });
            DropIndex("dbo.Turmas", new[] { "ProfessorId" });
            DropIndex("dbo.Turmas", new[] { "DisciplinaId" });
            DropIndex("dbo.Boletins", new[] { "TurmaId" });
            DropIndex("dbo.Boletins", new[] { "AlunoId" });
            DropIndex("dbo.Alunos", new[] { "PeriodoId" });
            DropIndex("dbo.Alunos", new[] { "CursoId" });
            DropIndex("dbo.Alunos", new[] { "UserId" });
            DropTable("dbo.CursoDisciplinas");
            DropTable("dbo.TurmaAlunoes");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Coordenadores");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Professores");
            DropTable("dbo.Periodos");
            DropTable("dbo.Cursos");
            DropTable("dbo.Disciplinas");
            DropTable("dbo.RegistrosDeAluno");
            DropTable("dbo.Aulas");
            DropTable("dbo.Turmas");
            DropTable("dbo.Boletins");
            DropTable("dbo.Alunos");
        }
    }
}
