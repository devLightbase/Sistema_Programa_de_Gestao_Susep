namespace Susep.SISRH.Application.Queries.RawSql
{
    public static class DominioRawSqls
    {

        public static string ObterDominios
        {
            get
            {
                return @"
                    SELECT catalogodominioid as id, descricao
                    FROM dbo.catalogodominio
                    WHERE classificacao = @classificacao 
                        AND ativo = true
                    ORDER BY descricao
                    ";
            }
        }

        public static string ObterPorChave
        {
            get
            {
                return @"
                    SELECT catalogodominioid as id, descricao
                    FROM dbo.catalogodominio
                    WHERE catalogodominioid = @id                        
                    ORDER BY descricao
                    ";
            }
        }
    }
}
