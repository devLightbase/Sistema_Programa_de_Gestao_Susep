namespace Susep.SISRH.Application.Queries.RawSql
{
    public static class UnidadeRawSqls
    {

        public static string ObterAtivas
        {
            get
            {
                return @"
					SELECT DISTINCT u.unidadeid as Id
	                                ,u.undsiglacompleta as Descricao   
                                    ,u.unidadeid
	                                ,u.undsiglacompleta siglacompleta
                                    ,u.undsigla as Sigla 
                                    ,u.unidadeidpai     
                    FROM dbo.vw_unidadesiglacompleta u
                    WHERE situacaounidadeid = @situacaoAtiva
                    ORDER BY u.undsiglacompleta
                ";
            }
        }
        public static string ObterPorFiltro
        {
            get
            {
                return @"
					SELECT und1.unidadeid
	                      ,und1.undsigla as sigla
                          ,und1.unddescricao as nome
                          ,und1.ufid
                          ,und1.tipounidadeid
                          ,und1.situacaounidadeid
                          ,und1.undnivel
                          ,und1.tipofuncaounidadeid
                          ,und1.undcodigosiorg
                          ,und1.undcodigosgc
                          ,und1.email 
                          ,und1.pessoaidchefe 
                          ,und1.pessoaidchefesubstituto 
                    FROM dbo.unidade as und1 WHERE (@sigla IS NULL OR UPPER(LTRIM(RTRIM(und1.undsigla))) LIKE '%' || UPPER(LTRIM(RTRIM(@sigla))) || '%')  
                    AND (@nome IS NULL OR UPPER(LTRIM(RTRIM(und1.unddescricao))) LIKE '%' || UPPER(LTRIM(RTRIM(@nome))) || '%')  
                    ORDER BY und1.undsigla

                    OFFSET @Offset ROWS
                    FETCH NEXT @PageSize ROWS ONLY;

                    SELECT COUNT(*)
                    FROM dbo.unidade u
                    WHERE (@sigla IS NULL OR UPPER(LTRIM(RTRIM(u.undsigla))) LIKE '%' || UPPER(LTRIM(RTRIM(@sigla))) || '%')  
                    AND (@nome IS NULL OR UPPER(LTRIM(RTRIM(u.unddescricao))) LIKE '%' || UPPER(LTRIM(RTRIM(@nome))) || '%')  
                ";
            }
        }

        public static string ObterComPlanoTrabalho
        {
            get
            {
                return @"
					SELECT DISTINCT u.unidadeid as Id
	                                ,u.undsiglacompleta as Descricao   
                                    ,u.unidadeid
	                                ,u.undsiglacompleta siglacompleta
                                    ,u.unidadeidpai   
                    FROM dbo.vw_unidadesiglacompleta u
                        INNER JOIN programagestao.planotrabalho p ON u.unidadeid = p.unidadeid                    
                    ORDER BY u.undsiglacompleta
                ";
            }
        }

        public static string ObterSemCatalogoCadastrado
        {
            get
            {
                return @"
					SELECT DISTINCT u.unidadeid as Id
	                                ,u.undsiglacompleta as Descricao   
                                    ,u.unidadeid
	                                ,u.undsiglacompleta siglacompleta
                                    ,u.unidadeidpai         
                    FROM dbo.vw_unidadesiglacompleta u
                        LEFT OUTER JOIN programagestao.catalogo c ON u.unidadeid = c.unidadeid  
                    WHERE c.unidadeid IS NULL AND u.situacaounidadeid = 1
                    ORDER BY u.undsiglacompleta
                ";
            }
        }

        public static string ObterComCatalogoCadastrado
        {
            get
            {
                return @"
					SELECT DISTINCT u.unidadeid as Id
	                                ,u.undsiglacompleta as Descricao   
                                    ,u.unidadeid
	                                ,u.undsiglacompleta siglacompleta
                                    ,u.unidadeidpai        
                    FROM dbo.vw_unidadesiglacompleta u
                        LEFT OUTER JOIN programagestao.catalogo c ON u.unidadeid = c.unidadeid  
                    WHERE c.unidadeid IS NOT NULL
                    ORDER BY u.undsiglacompleta
                ";
            }
        }

        public static string ObterUnidadePorChave
        {
            get
            {
                return @"
                    SELECT  u.unidadeid
                        ,undsigla as Sigla
                        ,unddescricao as nome
                        ,unidadeidpai as unidadeIdPai
                        ,tipounidadeId as tipoUnidadeId
                        ,situacaounidadeid as situacaoUnidadeId
                        ,email
                        ,ufid as ufId
                        ,undnivel as nivel
                        ,tipofuncaounidadeid as tipoFuncaoUnidadeId
                        ,pessoaidchefe as pessoaIdChefe
                        ,pessoaidchefesubstituto as pessoaIdChefeSubstituto
                        ,undcodigosiorg as codSiorg
                        ,undcodigosgc as codSgc
                    FROM dbo.unidade u WHERE u.unidadeid = @unidadeid
                ";
            }
        }

        public static string ObterPorChave
        {
            get
            {
                return @"
                    SELECT  u.unidadeid
                        ,undsigla
                        ,unddescricao nome
                        ,unidadeidpai
                        ,tipounidadeId
                        ,situacaounidadeid
                        ,ufid
                        ,undnivel Nivel
                        ,tipofuncaounidadeid
                        ,undsiglacompleta siglacompleta
                        ,email
                        ,quantidadeservidores
                    FROM dbo.vw_unidadesiglacompleta vu
                        INNER JOIN (SELECT
                                        u.unidadeid
                                        ,count(p.pessoaid) quantidadeservidores
                                    FROM dbo.vw_unidadesiglacompleta u
                                        LEFT OUTER JOIN dbo.pessoa p ON u.unidadeid = p.unidadeid
                                    WHERE u.unidadeid = @unidadeid
                                    GROUP BY u.unidadeid) u ON vu.unidadeid = U.unidadeid
                ";
            }
        }

        public static string ObterQuantidadeServidoresPorChave
        {
            get
            {
                return @"
                    SELECT  upg.unidadeid
                        ,undsigla
                        ,unddescricao nome
                        ,unidadeidpai
                        ,tipounidadeid
                        ,situacaounidadeid
                        ,ufid
                        ,undnivel nivel
                        ,tipofuncaounidadeid
                        ,undsiglacompleta siglacompleta
                        ,email
                        ,quantidadeservidores
                    FROM dbo.vw_unidadesiglacompleta vu
                            INNER JOIN (
                                        SELECT up.unidadeid, count(1) quantidadeservidores
                                        FROM (SELECT p.pessoaid, p.unidadeid
                                              FROM dbo.pessoa p
                                              WHERE p.situacaopessoaid = 1

                                              UNION 

                                              SELECT p.pessoaid, pat.unidadeid
                                              FROM dbo.pessoaalocacaotemporaria pat
                                                  INNER JOIN dbo.pessoa p on p.pessoaid = pat.pessoaid
                                              WHERE p.situacaopessoaid = 1 
                                                  and (pat.datainicio <= NOW()) and (pat.datafim is null or pat.datafim <= NOW())
                                        ) up 
                                        WHERE up.unidadeid in (SELECT unidadeid
                                                               FROM dbo.vw_unidadesiglacompleta
                                                               WHERE undsiglacompleta like (SELECT u.undsiglacompleta || '%'
                                                                                            FROM dbo.vw_unidadesiglacompleta u
                                                                                             WHERE u.unidadeid = @unidadeid))
                                        GROUP BY up.unidadeid) upg on vu.unidadeid = upg.unidadeid
                ";
            }
        }

        public static string ObterFeriados
        {
            get
            {
                return @"
                    SELECT  make_date(date_part( 'year',@dataInicio::date)::int,date_part( 'month',ferdata)::int, date_part( 'day',ferdata)::int) dado
                            ,ferfixo fixo
                            ,ufid ufid 
                    FROM dbo.feriado f
                      WHERE ((f.ferfixo = true and f.situacao = 1
  	                      and make_date(date_part( 'year',@dataInicio::date)::int,date_part( 'month',ferdata)::int, date_part( 'day',ferdata)::int) >= make_date(date_part( 'year',@dataInicio::date)::int,date_part( 'month',@dataInicio::date)::int, date_part( 'day',@dataInicio::date)::int)
  	                      and make_date(date_part( 'year',@dataFim::date)::int,date_part( 'month',ferdata)::int, date_part( 'day',ferdata)::int) <= make_date(date_part( 'year',@dataFim::date)::int,date_part( 'month',@dataFim::date)::int, date_part( 'day',@dataFim::date)::int))
  	                      OR 
                           (f.ferfixo = false AND ferdata >= @dataInicio AND ferdata <= @dataFim AND f.situacao = 1))					  
                        AND (f.ufid IS NULL 
                             OR f.ufid = (SELECT ufid 
                                          FROM dbo.unidade 
                                          WHERE unidadeid = @unidadeId))
                      
                ";
            }
        }

        public static string ObterModalidadesExecucaoPorUnidade
        {
            get
            {
                return @"
                    SELECT catalogodominioid as id, descricao
                    FROM programagestao.unidademodalidadeexecucao ue
                        INNER JOIN dbo.catalogodominio cd ON cd.catalogodominioid = ue.modalidadeexecucaoid
                    WHERE unidadeid = @unidadeid
                    ORDER BY descricao
                ";
            }
        }

        public static string ObterPessoasDadosComboPorUnidade
        {
            get
            {
                return @"
                    SELECT pessoaid as id, pesnome descricao
                    FROM dbo.pessoa p
			            INNER JOIN dbo.vw_unidadesiglacompleta u ON u.unidadeid = p.unidadeid 
                    WHERE (p.unidadeid = @unidadeid) OR (u.unidadeidpai = @unidadeid and p.tipofuncaoid IS NOT NULL) 
                    ORDER BY pesnome
                ";
            }
        }

        public static string ObterPessoasPorUnidade
        {
            get
            {
                return @"
                    SELECT *
                        FROM (
                                SELECT 
                                    p.pessoaid, 
                                    pesNome nome, 
                                    pesEmail email, 
                                    tipoFuncaoId, 
                                    p.unidadeId, 
                                    CASE WHEN u1.unidadeId IS NOT NULL THEN 1 ELSE 0 END AS chefe
                                FROM dbo.Pessoa p
                                    LEFT OUTER JOIN dbo.Unidade u1 ON u1.unidadeId = p.unidadeId AND (u1.pessoaIdChefe = p.pessoaId OR u1.pessoaIdChefeSubstituto = p.pessoaId)
                                WHERE p.situacaoPessoaId = 1

                                UNION 

                                SELECT 
                                    p.pessoaid, 
                                    pesNome nome, 
                                    pesEmail email, 
                                    tipoFuncaoId, 
                                    pat.unidadeId, 
                                    CASE WHEN u1.unidadeId IS NOT NULL THEN 1 ELSE 0 END AS chefe
                                FROM dbo.PessoaAlocacaoTemporaria pat
                                    INNER JOIN dbo.Pessoa p ON p.pessoaId = pat.pessoaId
                                    LEFT OUTER JOIN dbo.Unidade u1 ON u1.unidadeId = p.unidadeId AND (u1.pessoaIdChefe = p.pessoaId OR u1.pessoaIdChefeSubstituto = p.pessoaId)
                                WHERE p.situacaoPessoaId = 1 
                                    AND (pat.dataInicio <= NOW()) AND (pat.dataFim IS NULL OR pat.dataFim <= NOW())
                        ) up 
                        WHERE up.unidadeId IN (
                            SELECT unidadeId
                            FROM dbo.VW_UnidadeSiglaCompleta
                            WHERE undSiglaCompleta like (
                                SELECT u.undSiglaCompleta || '%'
                                FROM dbo.VW_UnidadeSiglaCompleta u
                                WHERE u.unidadeId = @unidadeid 
                                )
                            )
                        ORDER BY up.nome
                ";
            }
        }
        public static string ObterChefesPorUnidade
        {
            get
            {
                return @"
                    SELECT pessoaid pessoaid, pesnome nome, pesemail email
                    FROM dbo.pessoa p
	                    INNER JOIN dbo.vw_unidadesiglacompleta u ON p.unidadeid = u.unidadeid
                    WHERE p.tipofuncaoid IS NOT NULL
	                    and u.undsiglacompleta IN @siglas
                ";
            }
        }

        public static string ObterSubordinadasPorUnidade
        {
            get
            {
                return @"
                    SELECT unidadeid id
	                       ,undsiglacompleta descricao
                    FROM dbo.vw_unidadesiglacompleta u
                        WHERE undsiglacompleta LIKE (SELECT undsiglacompleta || '%' FROM dbo.vw_unidadesiglacompleta us WHERE us.unidadeid = @unidadeid)
                    ORDER BY undsiglacompleta
                ";
            }
        }

        public static string ValorDuplicadoAsync
        {
            get
            {
                return @"
                    SELECT COUNT(1)
                    FROM dbo.unidade u
                    WHERE (UPPER(LTRIM(RTRIM(u.undsigla))) = UPPER(LTRIM(RTRIM(@sigla)))
                    OR UPPER(LTRIM(RTRIM(u.unddescricao))) = UPPER(LTRIM(RTRIM(@descricao))))
                    AND  (@unidadeid IS NULL OR u.unidadeid <> @unidadeid);
                ";
            }
        }

    }
}
