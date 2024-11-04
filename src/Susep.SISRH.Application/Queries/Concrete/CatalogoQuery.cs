using Dapper;
using Microsoft.Extensions.Configuration;
using SUSEP.Framework.Messages.Concrete.Request;
using SUSEP.Framework.Result.Abstractions;
using SUSEP.Framework.Result.Concrete;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Data.SqlTypes;
using System.Transactions;
using Susep.SISRH.Application.Queries.Abstractions;
using Susep.SISRH.Application.ViewModels;
using Susep.SISRH.Application.Requests;
using Susep.SISRH.Application.Queries.RawSql;
using Microsoft.Data.SqlClient;
using Npgsql;

namespace Susep.SISRH.Application.Queries.Concrete
{
    public class CatalogoQuery : ICatalogoQuery
    {
        private readonly IConfiguration Configuration;

        public class Contagem
        {
            public long unidades_lista;
            public long pactos_vigentes;
        }

        public CatalogoQuery(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public async Task<IApplicationResult<CatalogoViewModel>> ObterPorChaveAsync(Guid catalogoid)
        {
            var result = new ApplicationResult<CatalogoViewModel>();

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@catalogoid", catalogoid, DbType.Guid, ParameterDirection.Input);

            using (var connection = new NpgsqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                var dados = await connection.QueryAsync<CatalogoViewModel, UnidadeViewModel, CatalogoViewModel>(
                    CatalogoRawSqls.ObterPorChave,
                    (catalogo, unidade) =>
                    {
                        catalogo.Unidade = unidade;
                        return catalogo;
                    },
                    splitOn: "unidadeId",
                    param: parameters
                );
                result.Result = dados.FirstOrDefault();                

                connection.Close();
            }

            return result;
        }

        public async Task<IApplicationResult<DadosPaginadosViewModel<CatalogoViewModel>>> ObterPorFiltroAsync(CatalogoFiltroRequest request)
        {
            var result = new ApplicationResult<DadosPaginadosViewModel<CatalogoViewModel>>();

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@unidadeId", request.UnidadeId, DbType.Int32, ParameterDirection.Input);

            parameters.Add("@offset", (request.Page - 1) * request.PageSize, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@pageSize", request.PageSize, DbType.Int32, ParameterDirection.Input);

            using (var connection = new NpgsqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                var dadosPaginados = new DadosPaginadosViewModel<CatalogoViewModel>(request);
                using (var multi = await connection.QueryMultipleAsync(CatalogoRawSqls.ObterPorFiltro, parameters))
                {
                    dadosPaginados.Registros = multi.Read<CatalogoViewModel, UnidadeViewModel, CatalogoViewModel>((catalogo, unidade) =>
                    {
                        catalogo.Unidade = unidade;
                        return catalogo;
                    }, splitOn: "unidadeId").ToList();
                    dadosPaginados.Controle.TotalRegistros = multi.ReadFirst<int>();
                    result.Result = dadosPaginados;
                }                

                connection.Close();
            }

            return result;
        }

        public async Task<IApplicationResult<DadosPaginadosViewModel<CatalogoDominioViewModel>>> ObterCatalogoDominioPorFiltroAsync(CatalogoDominioFiltroRequest request)
        {
            var result = new ApplicationResult<DadosPaginadosViewModel<CatalogoDominioViewModel>>();

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@classificacao", request.Classificacao, DbType.String, ParameterDirection.Input);
            parameters.Add("@descricao", request.Descricao, DbType.String, ParameterDirection.Input);

            parameters.Add("@offset", (request.Page - 1) * request.PageSize, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@pageSize", request.PageSize, DbType.Int32, ParameterDirection.Input);

            using (var connection = new NpgsqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                var dadosPaginados = new DadosPaginadosViewModel<CatalogoDominioViewModel>(request);


                using (var multi = await connection.QueryMultipleAsync(CatalogoRawSqls.ObterCatalogoDominioPorFiltro, parameters))
                {
                    dadosPaginados.Registros = multi.Read<CatalogoDominioViewModel>().ToList();
                    dadosPaginados.Controle.TotalRegistros = multi.ReadFirst<int>();
                    result.Result = dadosPaginados;
                }

                connection.Close();
            }

            return result;
        }

        public async Task<IApplicationResult<DadosPaginadosViewModel<PgViewModel>>> ObterPgUnidadeAsync(PgUnidadeFiltroRequest request)
        {
            var result = new ApplicationResult<DadosPaginadosViewModel<PgViewModel>>();

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@sigla", request.Sigla, DbType.String, ParameterDirection.Input);
            parameters.Add("@descricao", request.Descricao, DbType.String, ParameterDirection.Input);

            parameters.Add("@offset", (request.Page - 1) * request.PageSize, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@pageSize", request.PageSize, DbType.Int32, ParameterDirection.Input);

            using (var connection = new NpgsqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                var dadosPaginados = new DadosPaginadosViewModel<PgViewModel>(request);

                using (var multi = await connection.QueryMultipleAsync(CatalogoRawSqls.ObterPgUnidade, parameters))
                {
                    dadosPaginados.Registros = multi.Read<PgViewModel>().ToList();
                    dadosPaginados.Controle.TotalRegistros = multi.ReadFirst<int>();
                    result.Result = dadosPaginados;
                }

                connection.Close();
            }

            return result;
        }

        public async Task<IApplicationResult<DadosPaginadosViewModel<ProgramaGestaoModalViewModel>>> ObterProgramaGestaoModal(ProgramaGestaoModalFiltroRequest request)
        {
            var result = new ApplicationResult<DadosPaginadosViewModel<ProgramaGestaoModalViewModel>>();

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@sigla", request.Sigla, DbType.String, ParameterDirection.Input);

            parameters.Add("@offset", (request.Page - 1) * request.PageSize, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@pageSize", request.PageSize, DbType.Int32, ParameterDirection.Input);

            using (var connection = new NpgsqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                var dadosPaginados = new DadosPaginadosViewModel<ProgramaGestaoModalViewModel>(request);

                using (var multi = await connection.QueryMultipleAsync(CatalogoRawSqls.ObterProgramaGestaoModal, parameters))
                {
                    dadosPaginados.Registros = multi.Read<ProgramaGestaoModalViewModel>().ToList();
                    dadosPaginados.Controle.TotalRegistros = multi.ReadFirst<int>();
                    result.Result = dadosPaginados;
                }

                connection.Close();
            }

            return result;
        }

        public async Task<IApplicationResult<DadosPaginadosViewModel<PactosVigentesModalViewModel>>> ObterPactosVigentesModal(PactosVigentesModalFiltroRequest request)
        {
            var result = new ApplicationResult<DadosPaginadosViewModel<PactosVigentesModalViewModel>>();

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@sigla", request.Sigla, DbType.String, ParameterDirection.Input);

            parameters.Add("@offset", (request.Page - 1) * request.PageSize, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@pageSize", request.PageSize, DbType.Int32, ParameterDirection.Input);

            using (var connection = new NpgsqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                var dadosPaginados = new DadosPaginadosViewModel<PactosVigentesModalViewModel>(request);

                using (var multi = await connection.QueryMultipleAsync(CatalogoRawSqls.ObterPactosVigentesModal, parameters))
                {
                    dadosPaginados.Registros = multi.Read<PactosVigentesModalViewModel>().ToList();
                    dadosPaginados.Controle.TotalRegistros = multi.ReadFirst<int>();
                    result.Result = dadosPaginados;
                }

                connection.Close();
            }

            return result;
        }

        public async Task<IApplicationResult<ContagemViewModel>> ObterContagem()
        {
            var result = new ApplicationResult<ContagemViewModel>(null);

            using (var connection = new NpgsqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                var contagem = new ContagemViewModel();

                using (var dados = await connection.QueryMultipleAsync(CatalogoRawSqls.ObterContagemUnidades))
                {
                    var contagemUnidades = dados.Read<long>().ToList();
                    contagem.unidades = contagemUnidades[0];
                }
                using (var dados = await connection.QueryMultipleAsync(CatalogoRawSqls.ObterContagemPessoas))
                {
                    var contagemPessoas = dados.Read<long>().ToList();
                    contagem.pessoas = contagemPessoas[0];
                }
                using (var dados = await connection.QueryMultipleAsync(CatalogoRawSqls.ObterContagem))
                {
                    var contagens = dados.Read<Contagem>().ToList();
                    var count = contagens.First();
                    contagem.unidades_lista = count.unidades_lista;
                    contagem.pactos_vigentes = count.pactos_vigentes;
                }

                result.Result = contagem;

                connection.Close();
            }

            return result;
        }

        public async Task<IApplicationResult<CatalogoViewModel>> ObterPorUnidadeAsync(Int32 unidadeId)
        {
            var result = new ApplicationResult<CatalogoViewModel>();

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@unidadeId", unidadeId, DbType.Int32, ParameterDirection.Input);

            using (var connection = new NpgsqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                var dados = await connection.QueryAsync<CatalogoViewModel>(CatalogoRawSqls.ObterPorUnidade, parameters);

                var itens = await connection.QueryAsync<ItemCatalogoViewModel>(ItemCatalogoRawSqls.ObterPorUnidade, parameters);

                var retorno = dados.FirstOrDefault();
                
                retorno.Itens= itens;

                result.Result = retorno;

                connection.Close();
            }

            return result;
        }

        public async Task<IApplicationResult<IEnumerable<ItemCatalogoViewModel>>> ObterItensPorUnidadeAsync(Int32 unidadeId)
        {
            var result = new ApplicationResult<IEnumerable<ItemCatalogoViewModel>>();

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@unidadeId", unidadeId, DbType.Int32, ParameterDirection.Input);

            using (var connection = new NpgsqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                var itemCatalogoDictionary = new Dictionary<Guid, ItemCatalogoViewModel>();

                var dados = await connection.QueryAsync<ItemCatalogoViewModel, AssuntoViewModel, ItemCatalogoViewModel>(
                    CatalogoRawSqls.ObterItensPorUnidade, 
                    (itemCatalogo, assunto) =>
                    {
                        ItemCatalogoViewModel itemCatalogoEntry;

                        if (!itemCatalogoDictionary.TryGetValue(itemCatalogo.ItemCatalogoId, out itemCatalogoEntry))
                        {
                            itemCatalogoEntry = itemCatalogo;
                            itemCatalogoEntry.Assuntos = new List<AssuntoViewModel>();
                            itemCatalogoDictionary.Add(itemCatalogoEntry.ItemCatalogoId, itemCatalogoEntry);
                        }
                        
                        if (assunto != null)
                        {
                            itemCatalogoEntry.Assuntos.AsList().Add(assunto);
                        }

                        return itemCatalogoEntry;

                    },
                    splitOn: "assuntoId",
                    param: parameters);

                result.Result = dados.Distinct().ToList();

                connection.Close();
            }

            return result;
        }
    }
}
