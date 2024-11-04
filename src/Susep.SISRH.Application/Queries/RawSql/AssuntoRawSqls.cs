namespace Susep.SISRH.Application.Queries.RawSql
{
    public static class AssuntoRawSqls
    {

        public static string ObterPorFiltro
        {
            get
            {
                return @"
					SELECT a.assuntoid,
                           a.valor,
                           a.hierarquia,
                           a.nivel,
                           a.ativo
                    FROM programagestao.vw_assuntochavecompleta a
                    WHERE (@valor IS NULL OR a.valor LIKE '%' || @valor || '%')  
                    ORDER BY a.hierarquia

                    OFFSET @Offset ROWS
                    FETCH NEXT @PageSize ROWS ONLY;

                    SELECT COUNT(*)
                    FROM programagestao.vw_assuntochavecompleta a
                    WHERE (@valor IS NULL OR a.valor LIKE '%' || @valor || '%')  
                ";
            }
        }

        public static string ObterPorId
        {
            get
            {
                return @"
                    select a.assuntoid, a.valor, a.assuntopaiid, a.ativo
                    from programagestao.Assunto a
                    where a.assuntoid = @id

                    UNION

                    select p.assuntoid, p.valor, p.assuntopaiid, p.ativo
                    from programagestao.Assunto p
                    where p.assuntoid = (
	                    select a.assuntopaiid
	                    from programagestao.Assunto a
	                    where a.assuntoid = @id
                    );                
                ";
            }
        }
        public static string ObterAtivos
        {
            get
            {
                return @"
                    SELECT a.assuntoId,
                           a.valor,
                           a.hierarquia,
                           a.nivel,
                           a.ativo
                    FROM programagestao.vw_assuntochavecompleta a
                    WHERE a.ativo = true
                    ORDER BY a.hierarquia;                
                ";
            }
        }

        public static string ObterPorTexto
        {
            get
            {
                return @"
                    SELECT a.assuntoid,
                            a.valor,
                            a.hierarquia,
                            a.nivel,
                            a.ativo
                    FROM programagestao.vw_assuntochavecompleta a
                    WHERE a.ativo = true
                    AND  (lower(a.valor) like '%' || lower(@texto) || '%' 
                    OR    lower(a.hierarquia) like '%' || lower(@texto) || '%')
                    ORDER BY a.hierarquia;                
                ";
            }
        }

        public static string ObterIdsDeTodosOsPais
        {
            get
            {
                return @"
                    WITH cte_assuntos_pais AS (

	                    -- Nível corrente
	                    SELECT 
		                    assuntoid, 
		                    assuntopaiid 
	                    FROM programagestao.Assunto
	                    WHERE assuntoid = @assuntoid

	                    UNION ALL

	                    -- Todos os pais
	                    SELECT 
		                    pai.assuntoid, 
		                    pai.assuntopaiid 
	                    FROM programagestao.Assunto pai
	                    JOIN cte_assuntos_pais corrente ON pai.assuntoid = corrente.assuntopaiid

                    )
                    SELECT assuntoid
                    FROM cte_assuntos_pais
                    WHERE assuntoid <> @assuntoid;
                ";
            }
        }

        public static string ObterIdsDeTodosOsFilhos
        {
            get
            {
                return @"
                    WITH cte_assuntos_filhos AS (

	                    -- Nível corrente
	                    SELECT 
		                    assuntoid, 
		                    assuntopaiid 
	                    FROM programagestao.Assunto
	                    WHERE assuntoid = @assuntoid

	                    UNION ALL

	                    -- Todos os filhos
	                    SELECT 
		                    filho.assuntoid, 
		                    filho.assuntopaiid 
	                    FROM programagestao.Assunto filho
	                    JOIN cte_assuntos_filhos corrente ON filho.assuntopaiid = corrente.assuntoid

                    )
                    SELECT assuntoid
                    FROM cte_assuntos_filhos
                    WHERE assuntoid <> @assuntoid;
                ";
            }
        }

        public static string ObterPorValor
        {
            get
            {
                return @"
                    SELECT COUNT(1)
                    FROM programagestao.Assunto a
                    WHERE UPPER(LTRIM(RTRIM(a.valor))) = UPPER(LTRIM(RTRIM(@valor)))
                    AND  (@assuntoid IS NULL OR a.assuntoid <> @assuntoid);                
                ";
            }
        }

    }
}