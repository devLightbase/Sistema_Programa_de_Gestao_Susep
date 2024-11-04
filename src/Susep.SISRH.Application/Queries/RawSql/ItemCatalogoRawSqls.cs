namespace Susep.SISRH.Application.Queries.RawSql
{
    public static class ItemCatalogoRawSqls
    {


        public static string ObterPorChave
        {
            get
            {
                return @"
					SELECT  i.itemcatalogoid 
							,i.titulo         
							,i.calculotempoid formacalculotempoitemcatalogoid
							,cd.descricao formacalculotempoitemcatalogo 
							,i.permiteremoto permitetrabalhoremoto 
							,i.tempoPresencial tempoexecucaopresencial
							,i.tempoRemoto tempoexecucaoremoto   
							,i.descricao   
                            ,i.complexidade   
                            ,i.definicaocomplexidade   
                            ,i.entregasesperadas    
                            ,(SELECT CAST(COUNT(1) AS bit) FROM programagestao.pactotrabalhoatividade WHERE itemcatalogoid = i.itemcatalogoid) temPactoCadastrado
                            ,(SELECT CAST(COUNT(1) AS bit) FROM programagestao.catalogoitemcatalogo WHERE itemcatalogoid = i.itemcatalogoid) temUnidadeAssociada                    
                    FROM programagestao.itemcatalogo i
	                    INNER JOIN dbo.catalogodominio cd ON i.calculotempoid = cd.catalogodominioid
                    WHERE i.itemcatalogoid = @itemcatalogoid;

                    SELECT  a.assuntoid
		                    ,a.valor
		                    ,a.hierarquia
                    FROM programagestao.itemcatalogoassunto ica
	                    LEFT OUTER JOIN programagestao.vw_assuntochavecompleta a ON ica.assuntoid = a.assuntoid
                    WHERE ica.itemcatalogoid = @itemcatalogoid;                
                ";
            }
        }


        public static string ObterPorUnidade
        {
            get
            {
                return @"
				SELECT  i.itemcatalogoid 
		                ,i.titulo         
		                ,i.calculotempoid formacalculotempoitemcatalogoid
		                ,cd.descricao formacalculotempoitemcatalogo 
		                ,i.permiteremoto permitetrabalhoremoto 
		                ,i.tempopresencial tempoexecucaopresencial
		                ,i.temporemoto tempoexecucaoremoto   
		                ,i.descricao      
                        ,i.complexidade   
                        ,i.definicaocomplexidade   
                        ,i.entregasesperadas  		
                FROM programagestao.itemcatalogo i
	                INNER JOIN dbo.catalogodominio cd ON i.calculotempoid = cd.catalogodominioid
	                INNER JOIN programagestao.catalogoitemcatalogo cic ON i.itemcatalogoid = cic.itemcatalogoid
	                INNER JOIN programagestao.catalogo cat ON cic.catalogoId = cat.catalogoId
                WHERE cat.unidadeid = @unidadeid
                ORDER BY i.titulo, i.complexidade
                ";
            }
        }

        public static string ObterPorFiltro
        {
            get
            {
                return @"
					SELECT  i.itemcatalogoid 
							,i.titulo             
							,i.calculotempoid formacalculotempoitemcatalogoid
							,cd.descricao formacalculotempoitemcatalogo 
							,i.permiteremoto permitetrabalhoremoto 
							,i.tempoPresencial tempoexecucaopresencial
							,i.tempoRemoto tempoexecucaoremoto   
							,i.descricao       
                            ,i.complexidade   
                            ,i.definicaocomplexidade   
                            ,i.entregasesperadas 
                    FROM programagestao.itemcatalogo i
	                    INNER JOIN dbo.catalogodominio cd ON i.calculotempoid = cd.catalogodominioid
                    WHERE   (@titulo IS NULL OR i.titulo LIKE '%' || @titulo || '%')
                            AND (@formacalculotempoid IS NULL OR i.calculotempoid = @formacalculotempoid)
                            AND (@permitetrabalhoremoto IS NULL OR i.permiteremoto = @permitetrabalhoremoto)

                    ORDER BY i.titulo, i.complexidade

                    OFFSET @Offset ROWS
                    FETCH NEXT @PageSize ROWS ONLY;

                    SELECT COUNT(*)
                    FROM programagestao.itemcatalogo i
	                    INNER JOIN dbo.catalogodominio cd ON i.calculotempoid = cd.catalogodominioid
                    WHERE   (@titulo IS NULL OR i.titulo = '%' || @titulo || '%')
                            AND (@formacalculotempoid IS NULL OR i.calculotempoid = @formacalculotempoid)
                            AND (@permitetrabalhoremoto IS NULL OR i.permiteremoto = @permitetrabalhoremoto)
                ";
            }
        }



        public static string ObterTodos
        {
            get
            {
                return @"
					SELECT  i.itemcatalogoid 
							,i.titulo             
							,i.calculotempoid formacalculotempoitemcatalogoid
							,cd.descricao formacalculotempoitemcatalogo 
							,i.permiteremoto permitetrabalhoremoto 
							,i.tempoPresencial tempoexecucaopresencial
							,i.tempoRemoto tempoexecucaoremoto   
							,i.descricao        
                            ,i.complexidade   
                            ,i.definicaocomplexidade   
                            ,i.entregasesperadas
                    FROM programagestao.itemcatalogo i
                    ORDER BY i.titulo, i.complexidade   
                ";
            }
        }
    }
}
