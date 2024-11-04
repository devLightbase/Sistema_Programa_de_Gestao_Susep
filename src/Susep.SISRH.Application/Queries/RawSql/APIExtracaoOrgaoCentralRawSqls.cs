namespace Susep.SISRH.Application.Queries.RawSql
{
    public static class APIExtracaoOrgaoCentralRawSqls
    {

        public static string ObterPlanosTrabalho
        {
            get
            {
                return @"
					
                    SELECT
                        pacto.pactotrabalhoid PactoTrabalhoId 
                        ,pes.pesmatriculasiape MatriculaSIAPE 
                        ,pes.pescpf CPF 
                        ,pes.pesnome pessoa 
                        ,und.undcodigosiorg CodigoUnidadeSIORGExercicio 
                        ,und.undsiglacompleta NomeUnidadeSIORGExercicio 
                        ,CASE WHEN pacto.formaexecucaoid = 101 THEN 1 ELSE 2 END FormaExecucaoId 
                        ,(pes.cargahoraria * 5) CargaHorariaSemanal 
                        ,pacto.datainicio datainicio 
                        ,pacto.datafim datafim 
                        ,pacto.tempototaldisponivel cargahorariatotal
                    FROM programagestao.pactotrabalho pacto
	                    INNER JOIN dbo.pessoa pes ON pes.pessoaid = pacto.pessoaid
	                    INNER JOIN dbo.vw_unidadesiglacompleta und on und.unidadeid = pes.unidadeid


                    SELECT
                        pitem.pactotrabalhoid
                        ,item.itemcatalogoid ItemCatalogoId 
                        ,item.titulo Titulo 
                        ,item.complexidade Complexidade 
                        ,item.definicaocomplexidade DefinicaoComplexidade 
                        ,item.tempopresencial TempoExecucaoPresencial 
                        ,item.temporemoto TempoExecucaoRemoto 
                        ,item.entregasesperadas EntregasEsperadas 
                        ,pitem.quantidade QuantidadeEntregas 
                        ,pitem.quantidade QuantidadeEntregasRealizadas 
                        ,pitem.nota Nota 
                        ,pitem.justificativa Justificativa 
                    FROM programagestao.pactotrabalhoatividade pitem
	                    INNER JOIN programagestao.itemcatalogo item ON pitem.itemcatalogoid = item.itemcatalogoid


                ";
            }
        }

    }
}
