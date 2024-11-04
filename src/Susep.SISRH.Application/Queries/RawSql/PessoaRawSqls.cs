namespace Susep.SISRH.Application.Queries.RawSql
{
    public static class PessoaRawSqls
    {
        public static string ObterPorFiltro
        {
            get
            {
                return @"
					SELECT DISTINCT 
                           p.pessoaid
                          ,p.pesnome nome
                          ,p.unidadeid
                          ,u.undsiglacompleta unidade
                          ,sp.situacaopessoaid
                          ,sp.spsdescricao situacaopessoa
                          ,tv.tipovinculoid
                          ,tv.tvndescricao tipovinculo
                          ,p.cargahoraria
                    FROM dbo.pessoa p
                        INNER JOIN dbo.vw_unidadesiglacompleta u ON u.unidadeid = p.unidadeid   
					    INNER JOIN  dbo.situacaopessoa sp ON sp.situacaopessoaid = p.situacaoPessoaid
					    INNER JOIN  dbo.tipovinculo tv ON tv.tipovinculoid = p.tipovinculoid
                    WHERE   (@unidadeid IS NULL OR p.unidadeid = @unidadeid)
                            AND (@pesnome IS NULL OR translate(p.pesnome, 'áàâãäéèêëíìïóòôõöúùûüÁÀÂÃÄÉÈÊËÍÌÏÓÒÔÕÖÚÙÛÜçÇ', 'aaaaaeeeeiiiooooouuuuAAAAAEEEEIIIOOOOOUUUUcC')  ILIKE '%' || translate(@pesnome, 'áàâãäéèêëíìïóòôõöúùûüÁÀÂÃÄÉÈÊËÍÌÏÓÒÔÕÖÚÙÛÜçÇ', 'aaaaaeeeeiiiooooouuuuAAAAAEEEEIIIOOOOOUUUUcC') || '%')                    
                    ORDER BY nome ASC, unidadeid DESC, cargahoraria ASC

                    OFFSET @Offset ROWS
                    FETCH NEXT @PageSize ROWS ONLY;

                    SELECT COUNT(*)
                    FROM dbo.pessoa p
                        INNER JOIN dbo.vw_unidadesiglacompleta u ON u.unidadeid = p.unidadeid   
					    INNER JOIN  dbo.situacaopessoa sp ON sp.situacaopessoaid = p.situacaoPessoaid
					    INNER JOIN  dbo.tipovinculo tv ON tv.tipovinculoid = p.tipovinculoid
                   WHERE   (@unidadeid IS NULL OR p.unidadeid = @unidadeid)
                            AND (@pesnome IS NULL OR translate(p.pesnome, 'áàâãäéèêëíìïóòôõöúùûüÁÀÂÃÄÉÈÊËÍÌÏÓÒÔÕÖÚÙÛÜçÇ', 'aaaaaeeeeiiiooooouuuuAAAAAEEEEIIIOOOOOUUUUcC')  ILIKE '%' || translate(@pesnome, 'áàâãäéèêëíìïóòôõöúùûüÁÀÂÃÄÉÈÊËÍÌÏÓÒÔÕÖÚÙÛÜçÇ', 'aaaaaeeeeiiiooooouuuuAAAAAEEEEIIIOOOOOUUUUcC') || '%') 
	                      
                ";
            }
        }

        public static string ObterDetalhes
        {
            get
            {
                return @"
                        SELECT p.pessoaid
                                ,p.pesnome nome
                                ,p.unidadeid
                                ,v.undsiglacompleta unidade
                                ,p.pesemail as email
                                ,p.situacaopessoaid as situacaoPessoaId
                                ,p.tipovinculoid as tipoVinculoId
                                ,p.pesdatanascimento as dataNascimento
                                ,p.pescpf as cpf
                                ,p.pesmatriculasiape as matriculaSiape
                                ,p.cargahoraria as cargaHoraria
                                ,p.tipofuncaoid as tipoFuncaoId
                        FROM dbo.pessoa p
                        INNER JOIN dbo.vw_unidadesiglacompleta v ON v.unidadeid = p.unidadeid
                        WHERE p.pessoaid =  @pessoaid;
                    
                        SELECT 
	                        ptac.planotrabalhoatividadecandidatoid
	                        ,ptac.planotrabalhoatividadeid
	                        ,p.pesnome nome
	                        ,ptac.situacaoid 
	                        ,cds.descricao situacao
	                        ,pta.planotrabalhoid 
	                        ,pta.modalidadeexecucaoid
	                        ,cdm.descricao as modalidade
	                        ,pt.unidadeid
	                        ,un.undSigla as unidade
                        FROM dbo.pessoa p 
                            INNER JOIN programagestao.planotrabalhoatividadecandidato ptac 
                            ON p.pessoaid = ptac.pessoaid
                            INNER JOIN programagestao.planotrabalhoatividade pta 
                            ON pta.planotrabalhoatividadeid = ptac.planotrabalhoatividadeid
                            INNER JOIN programagestao.PlanoTrabalho pt 
                            ON pt.planotrabalhoid  = pta.planotrabalhoid 
                            INNER JOIN dbo.catalogodominio cds 
                            ON cds.catalogodominioid = ptac.situacaoid 
                            INNER JOIN dbo.catalogodominio cdm 
                            ON cdm.catalogodominioid = pta.modalidadeexecucaoid
                            INNER JOIN dbo.unidade un 
                            ON un.unidadeid =pt.unidadeid
                        WHERE p.pessoaid = @pessoaid;          

                        SELECT 
	                         ptai.planotrabalhoatividadeitemid
	                        ,ptai.planotrabalhoatividadeid
	                        ,ptai.itemcatalogoid
	                        ,ic.titulo as itemcatalogo
                        FROM dbo.pessoa p 
                            INNER JOIN programagestao.planotrabalhoatividadecandidato ptac 
                            ON p.pessoaid = ptac.pessoaid
                            INNER JOIN programagestao.planotrabalhoatividade pta 
                            ON pta.planotrabalhoatividadeid = ptac.planotrabalhoatividadeid
                            INNER JOIN programagestao.planotrabalhoatividadeItem ptai 
                            ON ptai.planotrabalhoatividadeid = pta.planotrabalhoatividadeid
                            INNER JOIN programagestao.itemcatalogo ic 
                            ON ic.itemcatalogoid = ptai.itemcatalogoid
                         WHERE p.pessoaid = @pessoaid;

                        SELECT 
	                         ptai.planotrabalhoatividadecriterioid
	                        ,ptai.planotrabalhoatividadeid
	                        ,ptai.criterioId
	                        ,cd.descricao criterio
                        FROM dbo.pessoa p 
                            INNER JOIN programagestao.planotrabalhoatividadecandidato ptac 
                            ON p.pessoaid = ptac.pessoaid
                            INNER JOIN programagestao.planotrabalhoatividade pta 
                            ON pta.planotrabalhoatividadeid = ptac.planotrabalhoatividadeid
                            INNER JOIN programagestao.planotrabalhoatividadeCriterio ptai 
                            ON ptai.planotrabalhoatividadeid = pta.planotrabalhoatividadeid
                            INNER JOIN dbo.catalogodominio cd 
                            ON cd.catalogodominioid = ptai.criterioId
                       WHERE p.pessoaid = @pessoaid;

                        SELECT
	                           ptach.planotrabalhoatividadecandidatohistoricoid
                              ,ptach.planotrabalhoatividadecandidatoid
                              ,ptach.situacaoid 
                              ,ptach.data
                              ,ptach.descricao
                              ,COALESCE(pe.pesnome, ptach.responsavelOperacao) responsavelOperacao
                        FROM  
                            programagestao.planotrabalhoatividadecandidato ptac 
                            INNER JOIN programagestao.planotrabalhoatividadecandidatoHistorico ptach 
                                ON ptac.planotrabalhoatividadecandidatoid = ptach.planotrabalhoatividadecandidatoid                            
	                        LEFT OUTER JOIN dbo.pessoa pe ON ptach.responsavelOperacao = CAST(pe.pessoaid AS VARCHAR(12))
                        WHERE ptac.pessoaid = @pessoaid
                        ORDER BY ptach.data DESC;
                ";
            }
        }


        public static string ObterDashboard
        {
            get
            {
                return @"
                        --planos não encerrados nas unidades em que a pessoa é chefe:
						select   p.planotrabalhoid
                                ,u1.undsiglacompleta unidade  
                                ,p.datainicio    
                                ,p.datafim
                                ,p.situacaoid
		                        ,cd2.descricao situacao                            
                        from programagestao.planotrabalho p
	                        inner join dbo.vw_unidadesiglacompleta u1 on u1.unidadeid = p.unidadeid
	                        inner join dbo.catalogodominio cd2 on p.situacaoid = cd2.catalogodominioid
							inner join (
								select u.undsiglacompleta
								from dbo.pessoa pe
                                    left outer join dbo.pessoaalocacaotemporaria a on a.pessoaid = pe.pessoaid and a.datafim is null
									inner join dbo.tipofuncao tf on tf.tipofuncaoid = pe.tipofuncaoid
									inner join dbo.vw_unidadesiglacompleta u on u.unidadeid = coalesce(a.unidadeid, pe.unidadeid)  
								where pe.pessoaid = @pessoaid and tf.tfnindicadorchefia = true
							) chefe on (u1.undsiglacompleta like chefe.undsiglacompleta || '%') 
						where p.situacaoid <= 309
						union 
						-- planos em execução que o servidor foi selecionado
						select   distinct p.planotrabalhoid
                                ,u1.undsiglacompleta unidade  
                                ,p.datainicio    
                                ,p.datafim
                                ,p.situacaoid
		                        ,cd2.descricao situacao                            
                        from programagestao.planotrabalho p
	                        inner join dbo.vw_unidadesiglacompleta u1 on u1.unidadeid = p.unidadeid
	                        inner join dbo.catalogodominio cd2 on p.situacaoid = cd2.catalogodominioid
							inner join programagestao.planotrabalhoatividade pa on pa.planotrabalhoid = p.planotrabalhoid
							inner join programagestao.planotrabalhoatividadecandidato pac on pa.planotrabalhoatividadeid = pac.planotrabalhoatividadeid
						where p.situacaoid = 309 and pac.pessoaid = @pessoaid and pac.situacaoid = 804 
						union
						-- planos em habilitação na(s) unidades do servidor
						select   p.planotrabalhoid
                                ,u1.undsiglacompleta unidade  
                                ,p.datainicio    
                                ,p.datafim
                                ,p.situacaoid
		                        ,cd2.descricao situacao                            
                        from programagestao.planotrabalho p
	                        inner join dbo.vw_unidadesiglacompleta u1 on u1.unidadeid = p.unidadeid
	                        inner join dbo.catalogodominio cd2 on p.situacaoid = cd2.catalogodominioid
							inner join (
								select coalesce(a.unidadeid, pe.unidadeid)  unidadeid
								from dbo.pessoa pe
                                    left outer join dbo.pessoaalocacaotemporaria a on a.pessoaid = pe.pessoaid and a.datafim is null
								where pe.pessoaid = @pessoaid
								union
								select u.unidadeidpai unidadeid
								from dbo.pessoa pe
                                    left outer join dbo.pessoaalocacaotemporaria a on a.pessoaid = pe.pessoaid and a.datafim is null
									inner join dbo.tipofuncao tf on tf.tipofuncaoid = pe.tipofuncaoid
									inner join dbo.vw_unidadesiglacompleta u on u.unidadeid = coalesce(a.unidadeid, pe.unidadeid)  
								where pe.pessoaid = @pessoaid and tf.tfnindicadorchefia = true
							) us on p.unidadeid = us.unidadeid
						where p.situacaoid = 307;
						
                        select   p.pactotrabalhoid
                                ,u1.undsiglacompleta unidade    
                                ,p.pessoaid pessoaid 
                                ,pe.pesnome pessoa
                                ,p.datainicio    
                                ,p.datafim        
                                ,p.situacaoid   
		                        ,cd2.descricao situacao                            
                        from programagestao.pactotrabalho p
	                        inner join dbo.vw_unidadesiglacompleta u1 on u1.unidadeid = p.unidadeid   
	                        inner join dbo.pessoa pe on pe.pessoaid = p.pessoaid  
	                        inner join dbo.catalogodominio cd2 on p.situacaoid = cd2.catalogodominioid
	                        inner join (
		                        select 
			                        case when pe.tipofuncaoid is null then pe.pessoaid else null end pessoaid
			                        ,u.undsiglacompleta 
		                        from dbo.pessoa pe
                                    left outer join dbo.pessoaalocacaotemporaria a on a.pessoaid = pe.pessoaid and a.datafim is null
			                        inner join dbo.vw_unidadesiglacompleta u on u.unidadeid = coalesce(a.unidadeid, pe.unidadeid)  
		                        where pe.pessoaid = @pessoaid
								union 
								select pe.pessoaid
			                           ,up.undsiglacompleta
		                        from dbo.pessoa pe
                                    left outer join dbo.pessoaalocacaotemporaria a on a.pessoaid = pe.pessoaid and a.datafim is null
			                        inner join dbo.vw_unidadesiglacompleta u on u.unidadeid = coalesce(a.unidadeid, pe.unidadeid) 
									inner join dbo.vw_unidadesiglacompleta up on up.unidadeid = u.unidadeidpai 
		                        where pe.pessoaid = @pessoaid and pe.tipofuncaoid is not null
	                        ) chefe on (u1.undsiglacompleta = chefe.undsiglacompleta and chefe.pessoaid is not null) or 
									   (u1.undsiglacompleta like chefe.undsiglacompleta || '%' and chefe.pessoaid is null) 
						where p.situacaoid <= 405 and 
                            (chefe.pessoaid is null or p.pessoaid = @pessoaid)
                        order by p.datainicio, p.datafim;
						
                        select  p.pactotrabalhoid
                                ,u1.undsiglacompleta unidade  
		                        ,pe.pesnome solicitante
		                        ,cd2.descricao tiposolicitacao
                                ,s.datasolicitacao
                        from programagestao.pactotrabalhosolicitacao s
	                        inner join programagestao.pactotrabalho p on s.pactotrabalhoid = p.pactotrabalhoid
	                        inner join dbo.vw_unidadesiglacompleta u1 on u1.unidadeid = p.unidadeid   
	                        inner join dbo.pessoa pe on pe.pessoaid = p.pessoaid  
	                        inner join dbo.catalogodominio cd2 on s.tiposolicitacaoid = cd2.catalogodominioid
	                        inner join (
		                        select 
			                        case when pe.tipofuncaoid is null then pe.pessoaid else null end pessoaid
			                        ,u.undsiglacompleta 
		                        from dbo.pessoa pe
                                    left outer join dbo.pessoaalocacaotemporaria a on a.pessoaid = pe.pessoaid and a.datafim is null
			                        inner join dbo.vw_unidadesiglacompleta u on u.unidadeid = coalesce(a.unidadeid, pe.unidadeid) 
		                        where pe.pessoaid = @pessoaid
								union 
								select pe.pessoaid
			                           ,up.undsiglacompleta
		                        from dbo.pessoa pe
                                    left outer join dbo.pessoaalocacaotemporaria a on a.pessoaid = pe.pessoaid and a.datafim is null
			                        inner join dbo.vw_unidadesiglacompleta u on u.unidadeid = coalesce(a.unidadeid, pe.unidadeid)  
									inner join dbo.vw_unidadesiglacompleta up on up.unidadeid = u.unidadeidpai 
		                        where pe.pessoaid = @pessoaid and pe.tipofuncaoid is not null
	                        ) chefe on (u1.undsiglacompleta = chefe.undsiglacompleta and chefe.pessoaid is not null) or 
									   (u1.undsiglacompleta like chefe.undsiglacompleta || '%' and chefe.pessoaid is null) 
                        where s.analisado = false and 
                              p.situacaoid = 405 and
                              (chefe.pessoaid is null or p.pessoaid = @pessoaid)
                        order by datasolicitacao
                ";
            }
        }

        public static string ObterPorChave
        {
            get
            {
                return @"
					SELECT p.pessoaid
                          ,p.pesnome nome
                          ,p.unidadeid
                          ,u.undsiglacompleta unidade
                          ,u.tipofuncaounidadeid
                          ,p.cargahoraria
                          ,p.tipofuncaoid
                          ,t.tfnindicadorchefia chefe
                    FROM dbo.pessoa p
                        LEFT OUTER JOIN dbo.tipofuncao t ON t.tipofuncaoid = p.tipofuncaoid
                        LEFT OUTER JOIN dbo.pessoaalocacaotemporaria a ON a.pessoaid = p.pessoaid AND a.datafim IS NULL
	                    INNER JOIN dbo.vw_unidadesiglacompleta u ON u.unidadeid = p.unidadeid 
                    WHERE p.pessoaid = @pessoaid
                ";
            }
        }


        public static string ObterComPactoTrabalho
        {
            get
            {
                return @"
					SELECT DISTINCT 
                          p.pessoaid
                          ,p.pesnome nome
                          ,p.unidadeid
                          ,u.undsiglacompleta unidade
                          ,u.tipofuncaounidadeid
                          ,p.cargahoraria
                          ,p.tipofuncaoid
                          ,t.tfnindicadorchefia chefe
                    FROM dbo.pessoa p
					    INNER JOIN dbo.vw_unidadesiglacompleta u ON u.unidadeid = p.unidadeid  
					    LEFT OUTER JOIN dbo.tipofuncao t ON t.tipofuncaoid = p.tipofuncaoid
	                    INNER JOIN programagestao.pactotrabalho pe ON pe.pessoaid = p.pessoaid 
					ORDER BY nome
                ";
            }
        }

        public static string ObterComboCompleto
        {
            get
            {
                return @"
					SELECT DISTINCT 
                          pessoaid
                          ,pesnome nome
                    FROM dbo.pessoa ORDER BY nome
                ";
            }
        }

        public static string ObterComboUf
        {
            get
            {
                return @"
					SELECT DISTINCT 
                          ufid as id
                          ,ufdescricao as descricao
                    FROM dbo.uf ORDER BY id
                ";
            }
        }

        public static string ObterComboFuncoes
        {
            get
            {
                return @"
					SELECT DISTINCT 
                          tipofuncaoid id
                          ,tfndescricao descricao
                    FROM dbo.tipofuncao 
                    WHERE situacao = 1
                    ORDER BY tfndescricao";
            }
        }

        public static string ObterComboVinculo
        {
            get
            {
                return @"
					SELECT DISTINCT 
                          tipovinculoid id
                          ,tvndescricao descricao
                    FROM dbo.tipovinculo
                    WHERE situacao = 1
                    ORDER BY tvndescricao";
            }
        }

        public static string ObterComboSituacao
        {
            get
            {
                return @"
					SELECT DISTINCT 
                          situacaopessoaid id
                          ,spsdescricao descricao
                    FROM dbo.situacaopessoa
                    WHERE situacao = 1
                    ORDER BY spsdescricao";
            }
        }

        public static string ObterPaginaSituacoes
        {
            get
            {
                return @"
					SELECT a.situacaopessoaid,
                           a.spsdescricao as Descricao,
                           a.situacao as Situacao
                    FROM dbo.situacaopessoa a
                    WHERE (@spsdescricao IS NULL OR UPPER(LTRIM(RTRIM(a.spsdescricao))) LIKE '%' || UPPER(LTRIM(RTRIM(@spsdescricao))) || '%')  
                    ORDER BY a.situacaopessoaid

                    OFFSET @Offset ROWS
                    FETCH NEXT @PageSize ROWS ONLY;

                    SELECT COUNT(*)
                    FROM dbo.situacaopessoa a
                    WHERE (@spsdescricao IS NULL OR UPPER(LTRIM(RTRIM(a.spsdescricao))) LIKE '%' || UPPER(LTRIM(RTRIM(@spsdescricao))) || '%')  
                ";
            }
        }

        public static string ObterPaginaTipoFuncao
        {
            get
            {
                return @"
					SELECT a.tipofuncaoid,
                           a.tfndescricao as Descricao,
                           a.tfncodigofuncao as CodigoFuncao,
                           a.situacao as Situacao
                    FROM dbo.tipofuncao a
                    WHERE (@tfndescricao IS NULL OR UPPER(LTRIM(RTRIM(a.tfndescricao))) LIKE '%' || UPPER(LTRIM(RTRIM(@tfndescricao))) || '%')  
                    ORDER BY a.tipofuncaoid

                    OFFSET @Offset ROWS
                    FETCH NEXT @PageSize ROWS ONLY;

                    SELECT COUNT(*)
                    FROM dbo.tipofuncao a
                    WHERE (@tfndescricao IS NULL OR UPPER(LTRIM(RTRIM(a.tfndescricao))) LIKE '%' || UPPER(LTRIM(RTRIM(@tfndescricao))) || '%')  
                ";
            }
        }

        public static string ObterPaginaTipoVinculo
        {
            get
            {
                return @"
					SELECT a.tipovinculoid,
                           a.tvndescricao as Descricao,
                           a.situacao as Situacao
                    FROM dbo.tipovinculo a
                    WHERE (@tvndescricao IS NULL OR UPPER(LTRIM(RTRIM(a.tvndescricao))) LIKE '%' || UPPER(LTRIM(RTRIM(@tvndescricao))) || '%')  
                    ORDER BY a.tipovinculoid

                    OFFSET @Offset ROWS
                    FETCH NEXT @PageSize ROWS ONLY;

                    SELECT COUNT(*)
                    FROM dbo.tipovinculo a
                    WHERE (@tvndescricao IS NULL OR UPPER(LTRIM(RTRIM(a.tvndescricao))) LIKE '%' || UPPER(LTRIM(RTRIM(@tvndescricao))) || '%')  
                ";
            }
        }

        public static string ObterPaginaFeriado
        {
            get
            {
                return @"
                    SELECT a.feriadoid,
                           a.ferdata as Data,
                           a.ferdescricao as Descricao,
                           a.ferfixo as Fixo,
                           a.ufid as UfId,
                           a.situacao as Situacao
                    FROM dbo.feriado a
                    WHERE (@ferdescricao IS NULL OR UPPER(LTRIM(RTRIM(a.ferdescricao))) LIKE '%' || UPPER(LTRIM(RTRIM(@ferdescricao))) || '%')
                    ORDER BY a.ferdata

                    OFFSET @Offset ROWS
                    FETCH NEXT @PageSize ROWS ONLY;

                    SELECT COUNT(*)
                    FROM dbo.feriado a
                    WHERE (@ferdescricao IS NULL OR UPPER(LTRIM(RTRIM(a.ferdescricao))) LIKE '%' || UPPER(LTRIM(RTRIM(@ferdescricao))) || '%')  
                ";
                //AND (@ferdata IS NULL OR a.ferdata = @ferdata)
            }
        }

        public static string ObterDuplicacao
        {
            get
            {
                return @"
                    SELECT COUNT(1)
                    FROM dbo.pessoa p
                    WHERE p.pescpf = @cpf
                    AND (@pessoaId IS NULL OR @pessoaId = 0 OR p.pessoaid <> @pessoaId);                
                ";
            }
        }

        public static string ObterDuplicacaoSituacao
        {
            get
            {
                return @"
                    SELECT COUNT(1)
                    FROM dbo.situacaopessoa sp
                    WHERE UPPER(LTRIM(RTRIM(sp.spsdescricao))) = UPPER(LTRIM(RTRIM(@descricao)));                
                ";
            }
        }

        public static string ObterDuplicacaoTipoFuncao
        {
            get
            {
                return @"
                    SELECT COUNT(1)
                    FROM dbo.tipofuncao tf
                    WHERE UPPER(LTRIM(RTRIM(tf.tfndescricao))) = UPPER(LTRIM(RTRIM(@descricao)));                
                ";
            }
        }

        public static string ObterDuplicacaoTipoVinculo
        {
            get
            {
                return @"
                    SELECT COUNT(1)
                    FROM dbo.tipovinculo tv
                    WHERE UPPER(LTRIM(RTRIM(tv.tvndescricao))) = UPPER(LTRIM(RTRIM(@descricao)));                
                ";
            }
        }

        public static string ObterDuplicacaoFeriadoDescricao
        {
            get
            {
                return @"
                    SELECT COUNT(1)
                    FROM dbo.feriado p
                    WHERE UPPER(LTRIM(RTRIM(p.ferdescricao))) = UPPER(LTRIM(RTRIM(@descricao)))
                ";
            }
        }

        public static string ObterDuplicacaoFeriadoData
        {
            get
            {
                return @"
                    SELECT COUNT(1)
                    FROM dbo.feriado p
                    WHERE p.ferdata = @data                                 
                ";
            }
        }

        public static string ObterSituacaoPorId
        {
            get
            {
                return @"
                    select a.situacaopessoaid, a.spsdescricao as Descricao, a.situacao as Situacao
                    from dbo.situacaopessoa a
                    where a.situacaopessoaid = @id
                ";
            }
        }

        public static string ObterTipoFuncaoPorId
        {
            get
            {
                return @"
                    select a.tipofuncaoid, 
                    a.tfndescricao as Descricao,
                    a.tfncodigofuncao as CodigoFuncao,
                    a.tfnindicadorchefia as IndicadorChefia,
                    a.situacao as Situacao
                    from dbo.tipofuncao a
                    where a.tipofuncaoid = @id
                ";
            }
        }

        public static string ObterTipoVinculoPorId
        {
            get
            {
                return @"
                    select a.tipovinculoid, 
                    a.tvndescricao as Descricao,
                    a.situacao as Situacao
                    from dbo.tipovinculo a
                    where a.tipovinculoid = @id
                ";
            }
        }

        public static string ObterFeriadoPorId
        {
            get
            {
                return @"
                    select a.feriadoid,
                           a.ferdata as Data,
                           a.ferdescricao as Descricao,
                           a.ferfixo as Fixo,
                           a.ufid as UfId,
                           a.situacao as Situacao
                    from dbo.feriado a
                    where a.feriadoid = @id
                ";
            }
        }
    }
}
