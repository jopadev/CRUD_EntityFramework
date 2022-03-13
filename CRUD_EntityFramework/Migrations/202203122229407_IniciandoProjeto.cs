namespace CRUD_EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IniciandoProjeto : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CondicoesLivros",
                c => new
                    {
                        CondicaoId = c.Int(nullable: false, identity: true),
                        Descricao = c.String(),
                        Observacao = c.String(maxLength: 300, unicode: false),
                    })
                .PrimaryKey(t => t.CondicaoId);
            
            CreateTable(
                "dbo.Livros",
                c => new
                    {
                        LivroId = c.Int(nullable: false, identity: true),
                        Nome = c.String(maxLength: 120, unicode: false),
                        NumeroEdicao = c.String(maxLength: 10, fixedLength: true, unicode: false),
                        Autor = c.String(maxLength: 120, unicode: false),
                        Editora = c.String(maxLength: 120, unicode: false),
                        NumeroExemplar = c.Int(nullable: false),
                        CondicaoLivroId = c.Int(nullable: false),
                        DisponivelPraVenda = c.Boolean(nullable: false),
                        PrecoVenda = c.Decimal(nullable: false, precision: 10, scale: 2),
                        Observacao = c.String(maxLength: 300, unicode: false),
                    })
                .PrimaryKey(t => t.LivroId)
                .ForeignKey("dbo.CondicoesLivros", t => t.CondicaoLivroId, cascadeDelete: true)
                .Index(t => t.CondicaoLivroId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Livros", "CondicaoLivroId", "dbo.CondicoesLivros");
            DropIndex("dbo.Livros", new[] { "CondicaoLivroId" });
            DropTable("dbo.Livros");
            DropTable("dbo.CondicoesLivros");
        }
    }
}
