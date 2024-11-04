namespace Susep.SISRH.Application.Queries.RawSql
{
    public static class CatalogoRawSqls
    {

        public static string ObterPorChave
        {
            get
            {
                return @"
                    SELECT  c.catalogoid
		                    ,c.unidadeid
		                    ,u.undsiglacompleta sigla
		                    ,u.undDescricao nome
                    FROM programagestao.catalogo c
	                    INNER JOIN dbo.vw_unidadesiglacompleta u ON c.unidadeid = u.unidadeid
                    WHERE c.catalogoid = @catalogoid
                ";
            }
        }

        public static string ObterCatalogoDominioPorFiltro
        {
            get
            {
                return @"
                    SELECT a.catalogodominioid,
                           a.classificacao as Classificacao,
                           a.descricao as Descricao,
                           a.ativo as Ativo
                    FROM dbo.catalogodominio a
                    WHERE (@classificacao IS NULL OR UPPER(LTRIM(RTRIM(a.classificacao))) LIKE '%' || UPPER(LTRIM(RTRIM(@classificacao))) || '%')
                    AND (@descricao IS NULL OR UPPER(LTRIM(RTRIM(a.descricao))) LIKE '%' || UPPER(LTRIM(RTRIM(@descricao))) || '%')
                    ORDER BY a.catalogodominioid

                    OFFSET @Offset ROWS
                    FETCH NEXT @PageSize ROWS ONLY;

                    SELECT COUNT(*)
                    FROM dbo.catalogodominio a
                    WHERE (@classificacao IS NULL OR UPPER(LTRIM(RTRIM(a.classificacao))) LIKE '%' || UPPER(LTRIM(RTRIM(@classificacao))) || '%')  
                    AND (@descricao IS NULL OR UPPER(LTRIM(RTRIM(a.descricao))) LIKE '%' || UPPER(LTRIM(RTRIM(@descricao))) || '%')
                ";
            }
        }

        public static string ObterPgUnidade
        {
            get
            {
                return @"
                    SELECT S as sigla, D as descricao, count(DISTINCT CPF) as n_pessoas, count(DISTINCT PT) as n_pg, count(distinct CASE WHEN Situacao = 309 THEN PT ELSE null END) as n_vigentes
                    FROM (
                    SELECT DISTINCT
	                REGEXP_REPLACE(pe.pescpf, '(\d{3})(\d{3})(\d{3})', '\1.\2.\3-') AS CPF,
                    pe.pesnome as Nome,
	                pe.unidadeid AS UORG,
	                u.undsigla AS S,
	                u.unddescricao AS D,
	                u.ufid AS UF,
                    pta.planotrabalhoid as PT,
	                p.datainicio as DataInicio,
	                p.datafim as DataFim,
                    p.situacaoid as Situacao

                    FROM programagestao.planotrabalho p

                    INNER JOIN programagestao.planotrabalhoatividade pta ON p.planotrabalhoid = pta.planotrabalhoid

                    INNER JOIN programagestao.planotrabalhoatividadecandidato ptac ON pta.planotrabalhoatividadeid = ptac.planotrabalhoatividadeid

                    INNER JOIN dbo.pessoa pe ON ptac.pessoaid = pe.pessoaid

                    INNER JOIN dbo.unidade u ON u.unidadeid = pe.unidadeid
                    
                    WHERE (@sigla IS NULL OR UPPER(LTRIM(RTRIM(u.undsigla))) LIKE '%' || UPPER(LTRIM(RTRIM(@sigla))) || '%')
                    AND (@descricao IS NULL OR UPPER(LTRIM(RTRIM(u.unddescricao))) LIKE '%' || UPPER(LTRIM(RTRIM(@descricao))) || '%')

                    ORDER BY UORG) AS result
                    group by S, D

                    OFFSET @offset
					FETCH NEXT @pageSize ROWS ONLY;

                    SELECT count(*)
                    FROM (SELECT S as sigla, D as descricao, count(DISTINCT CPF) as n_pessoas, count(DISTINCT PT) as n_pg, count(distinct CASE WHEN Situacao = 309 THEN PT ELSE null END) as n_vigentes
                    FROM (
                    SELECT DISTINCT
	                REGEXP_REPLACE(pe.pescpf, '(\d{3})(\d{3})(\d{3})', '\1.\2.\3-') AS CPF,
                    pe.pesnome as Nome,
	                pe.unidadeid AS UORG,
	                u.undsigla AS S,
	                u.unddescricao AS D,
	                u.ufid AS UF,
                    pta.planotrabalhoid as PT,
	                p.datainicio as DataInicio,
	                p.datafim as DataFim,
                    p.situacaoid as Situacao

                    FROM programagestao.planotrabalho p

                    INNER JOIN programagestao.planotrabalhoatividade pta ON p.planotrabalhoid = pta.planotrabalhoid

                    INNER JOIN programagestao.planotrabalhoatividadecandidato ptac ON pta.planotrabalhoatividadeid = ptac.planotrabalhoatividadeid

                    INNER JOIN dbo.pessoa pe ON ptac.pessoaid = pe.pessoaid

                    INNER JOIN dbo.unidade u ON u.unidadeid = pe.unidadeid
                    
                    WHERE (@sigla IS NULL OR UPPER(LTRIM(RTRIM(u.undsigla))) LIKE '%' || UPPER(LTRIM(RTRIM(@sigla))) || '%')
                    AND (@descricao IS NULL OR UPPER(LTRIM(RTRIM(u.unddescricao))) LIKE '%' || UPPER(LTRIM(RTRIM(@descricao))) || '%')

                    ORDER BY UORG) AS result
                    group by S, D) AS result;
                ";
            }
        }

        public static string ObterProgramaGestaoModal
        {
            get
            {
                return @"
                    SELECT distinct PT, DataInicio as data_inicio, DataFim as data_fim, c.descricao as Situacao
                    FROM (
                    SELECT DISTINCT
	                REGEXP_REPLACE(pe.pescpf, '(\d{3})(\d{3})(\d{3})', '\1.\2.\3-') AS CPF,
                    pe.pesnome as Nome,
	                pe.unidadeid AS UORG,
	                u.undsigla AS S,
	                u.unddescricao AS D,
	                u.ufid AS UF,
                    pta.planotrabalhoid as PT,
	                p.datainicio as DataInicio,
	                p.datafim as DataFim,
	                p.situacaoid as Situacao

                    FROM programagestao.planotrabalho p

                    INNER JOIN programagestao.planotrabalhoatividade pta ON p.planotrabalhoid = pta.planotrabalhoid

                    INNER JOIN programagestao.planotrabalhoatividadecandidato ptac ON pta.planotrabalhoatividadeid = ptac.planotrabalhoatividadeid

                    INNER JOIN dbo.pessoa pe ON ptac.pessoaid = pe.pessoaid

                    INNER JOIN dbo.unidade u ON u.unidadeid = pe.unidadeid
                    
                    where u.undsigla = @sigla

                    ORDER BY UORG
                    ) AS result                    
                    inner join dbo.catalogodominio c on Situacao = c.catalogodominioid

                    OFFSET @offset
					FETCH NEXT @pageSize ROWS ONLY;

                    SELECT count(*)
                    FROM (SELECT distinct PT, DataInicio, DataFim, c.descricao
                    FROM (
                    SELECT DISTINCT
	                REGEXP_REPLACE(pe.pescpf, '(\d{3})(\d{3})(\d{3})', '\1.\2.\3-') AS CPF,
                    pe.pesnome as Nome,
	                pe.unidadeid AS UORG,
	                u.undsigla AS S,
	                u.unddescricao AS D,
	                u.ufid AS UF,
                    pta.planotrabalhoid as PT,
	                p.datainicio as DataInicio,
	                p.datafim as DataFim,
	                p.situacaoid as Situacao

                    FROM programagestao.planotrabalho p

                    INNER JOIN programagestao.planotrabalhoatividade pta ON p.planotrabalhoid = pta.planotrabalhoid

                    INNER JOIN programagestao.planotrabalhoatividadecandidato ptac ON pta.planotrabalhoatividadeid = ptac.planotrabalhoatividadeid

                    INNER JOIN dbo.pessoa pe ON ptac.pessoaid = pe.pessoaid

                    INNER JOIN dbo.unidade u ON u.unidadeid = pe.unidadeid
                    
                    where u.undsigla = @sigla

                    ORDER BY UORG
                    ) as result                    
                    inner join dbo.catalogodominio c on Situacao = c.catalogodominioid) as result2
                ";
            }
        }

        public static string ObterPactosVigentesModal
        {
            get
            {
                return @"
                    SELECT distinct PT, Nome, DataInicio as data_inicio, DataFim as data_fim
                    FROM (
                    SELECT DISTINCT
	                REGEXP_REPLACE(pe.pescpf, '(\d{3})(\d{3})(\d{3})', '\1.\2.\3-') AS CPF,
                    pe.pesnome as Nome,
	                pe.unidadeid AS UORG,
	                u.undsigla AS S,
	                u.unddescricao AS D,
	                u.ufid AS UF,
                    pta.planotrabalhoid as PT,
	                p.datainicio as DataInicio,
	                p.datafim as DataFim,
	                p.situacaoid as Situacao

                    FROM programagestao.planotrabalho p

                    INNER JOIN programagestao.planotrabalhoatividade pta ON p.planotrabalhoid = pta.planotrabalhoid

                    INNER JOIN programagestao.planotrabalhoatividadecandidato ptac ON pta.planotrabalhoatividadeid = ptac.planotrabalhoatividadeid

                    INNER JOIN dbo.pessoa pe ON ptac.pessoaid = pe.pessoaid

                    INNER JOIN dbo.unidade u ON u.unidadeid = pe.unidadeid
                    
                    where u.undsigla = @sigla

                    ORDER BY UORG
                    ) AS result  
                    where Situacao = 309

                    OFFSET @offset
					FETCH NEXT @pageSize ROWS ONLY;

                    SELECT count(*)
                    FROM (SELECT distinct PT, Nome, DataInicio, DataFim, Situacao
                    FROM (
                    SELECT DISTINCT
	                REGEXP_REPLACE(pe.pescpf, '(\d{3})(\d{3})(\d{3})', '\1.\2.\3-') AS CPF,
                    pe.pesnome as Nome,
	                pe.unidadeid AS UORG,
	                u.undsigla AS S,
	                u.unddescricao AS D,
	                u.ufid AS UF,
                    pta.planotrabalhoid as PT,
	                p.datainicio as DataInicio,
	                p.datafim as DataFim,
	                p.situacaoid as Situacao

                    FROM programagestao.planotrabalho p

                    INNER JOIN programagestao.planotrabalhoatividade pta ON p.planotrabalhoid = pta.planotrabalhoid

                    INNER JOIN programagestao.planotrabalhoatividadecandidato ptac ON pta.planotrabalhoatividadeid = ptac.planotrabalhoatividadeid

                    INNER JOIN dbo.pessoa pe ON ptac.pessoaid = pe.pessoaid

                    INNER JOIN dbo.unidade u ON u.unidadeid = pe.unidadeid
                    
                    where u.undsigla = @sigla

                    ORDER BY UORG
                    ) as result) as result2
                    where Situacao = 309
                ";
            }
        }

        public static string ObterContagemUnidades
        {
            get
            {
                return @"
                    SELECT  count(*) as unidades
                    FROM dbo.unidade;
                ";
            }
        }

        public static string ObterContagemPessoas
        {
            get
            {
                return @"
                    SELECT  count(*) as pessoas
                    FROM dbo.pessoa;
                ";
            }
        }

        public static string ObterContagem
        {
            get
            {
                return @"
                    SELECT count(*) as unidades_lista, SUM(n_vigentes) as pactos_vigentes
                    FROM (SELECT S as sigla, D as descricao, count(DISTINCT CPF) as n_pessoas, count(DISTINCT PT) as n_pg, count(distinct CASE WHEN Situacao = 309 THEN PT ELSE null END) as n_vigentes
                    FROM (
                    SELECT DISTINCT
	                REGEXP_REPLACE(pe.pescpf, '(\d{3})(\d{3})(\d{3})', '\1.\2.\3-') AS CPF,
                    pe.pesnome as Nome,
	                pe.unidadeid AS UORG,
	                u.undsigla AS S,
	                u.unddescricao AS D,
	                u.ufid AS UF,
                    pta.planotrabalhoid as PT,
	                p.datainicio as DataInicio,
	                p.datafim as DataFim,
                    p.situacaoid as Situacao
                    FROM programagestao.planotrabalho p
                    INNER JOIN programagestao.planotrabalhoatividade pta ON p.planotrabalhoid = pta.planotrabalhoid
                    INNER JOIN programagestao.planotrabalhoatividadecandidato ptac ON pta.planotrabalhoatividadeid = ptac.planotrabalhoatividadeid
                    INNER JOIN dbo.pessoa pe ON ptac.pessoaid = pe.pessoaid
                    INNER JOIN dbo.unidade u ON u.unidadeid = pe.unidadeid
                    ORDER BY UORG) AS result
                    group by S, D) AS result2;
                ";
            }
        }

        public static string ObterPorFiltro
        {
            get
            {
                return @"
                    SELECT  c.catalogoid
		                    ,c.unidadeid
		                    ,u.undsiglacompleta sigla
		                    ,u.undDescricao nome
                    FROM programagestao.catalogo c
	                    INNER JOIN dbo.vw_unidadesiglacompleta u ON c.unidadeid = u.unidadeid
                    WHERE @unidadeid IS NULL OR c.unidadeid = @unidadeid

                    ORDER BY u.undsiglacompleta

                    OFFSET @Offset ROWS
                    FETCH NEXT @PageSize ROWS ONLY;

                    SELECT COUNT(*)
                    FROM programagestao.catalogo c
	                    INNER JOIN dbo.vw_unidadesiglacompleta u ON c.unidadeid = u.unidadeid
                    WHERE @unidadeid IS NULL OR c.unidadeid = @unidadeid
                ";
            }
        }

        public static string ObterPorUnidade
        {
            get
            {
                return @"
                    SELECT  c.catalogoid
		                    ,c.unidadeid
		                    ,u.undsiglacompleta unidadeSigla		                   
                    FROM programagestao.catalogo c
	                    INNER JOIN dbo.vw_unidadesiglacompleta u ON c.unidadeid = u.unidadeid
                    WHERE c.unidadeid = @unidadeid
                ";
            }
        }

        public static string ObterItensPorUnidade
        {
            get
            {
                return @"
                    SELECT  i.itemcatalogoid 
							,i.titulo         
                             ,CASE
						    WHEN i.complexidade IS NULL OR i.complexidade = '' THEN i.titulo
						    ELSE i.titulo || ' - ' || i.complexidade
						  	END 
						  	AS tituloCompleto
                            ,i.complexidade 
							,i.calculoTempoId formaCalculoTempoItemcatalogoId
							,cd.descricao formaCalculoTempoItemcatalogo 
							,i.permiteRemoto permiteTrabalhoRemoto 
							,i.tempoPresencial tempoExecucaoPresencial
							,i.tempoRemoto tempoExecucaoRemoto   
							,i.descricao      
		                    ,a.assuntoid
		                    ,a.valor
                    FROM programagestao.Itemcatalogo i
	                    INNER JOIN programagestao.catalogoItemcatalogo ci ON ci.itemcatalogoid = i.itemcatalogoid
	                    INNER JOIN programagestao.catalogo c ON ci.catalogoid = c.catalogoid
	                    INNER JOIN dbo.catalogoDominio cd ON i.calculoTempoId = cd.catalogoDominioId
	                    LEFT OUTER JOIN programagestao.itemcatalogoassunto ia ON ia.itemcatalogoid = i.itemcatalogoid
	                    LEFT OUTER JOIN programagestao.Assunto a ON a.assuntoid = ia.assuntoId AND a.ativo = true
                    WHERE c.unidadeid = @unidadeid
                    ORDER BY i.titulo,i.complexidade 
                ";
            }
        }

    }
}
