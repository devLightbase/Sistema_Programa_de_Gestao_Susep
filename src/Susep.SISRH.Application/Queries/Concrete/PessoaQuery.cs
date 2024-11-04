using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Susep.SISRH.Application.Queries.Abstractions;
using Susep.SISRH.Application.Queries.RawSql;
using Susep.SISRH.Application.Requests;
using Susep.SISRH.Application.ViewModels;
using Susep.SISRH.Domain.AggregatesModel.PessoaAggregate;
using Susep.SISRH.Domain.Enums;
using SUSEP.Framework.Result.Abstractions;
using SUSEP.Framework.Result.Concrete;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Susep.SISRH.Application.Queries.Concrete
{
    public class PessoaQuery : IPessoaQuery
    {
        private readonly IConfiguration Configuration;
        private readonly IUnidadeQuery UnidadeQuery;

        public PessoaQuery(IConfiguration configuration, IUnidadeQuery unidadeQuery)
        {
            Configuration = configuration;
            UnidadeQuery = unidadeQuery;
        }

        public async Task<IApplicationResult<DashboardViewModel>> ObterDashboardAsync(UsuarioLogadoRequest request)
        {
            var result = new ApplicationResult<DashboardViewModel>()
            {
                Result = new DashboardViewModel()
            };

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@pessoaId", request.UsuarioLogadoId, DbType.Int64, ParameterDirection.Input);

            using (var connection = new NpgsqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                try
                {
                    connection.Open();

	                var dashboardData = await connection.QueryMultipleAsync(PessoaRawSqls.ObterDashboard, parameters);
	
	                result.Result.PlanosTrabalho = dashboardData.Read<PlanoTrabalhoViewModel>().ToList();
	                result.Result.PactosTrabalho = dashboardData.Read<PactoTrabalhoViewModel>().ToList();
	                result.Result.Solicitacoes = dashboardData.Read<PactoTrabalhoSolicitacaoViewModel>().ToList();

                    connection.Close();
            	}
                catch (Exception ex)
	            {
	                Console.Write(ex.Message);
	            }
	
	        }

            return result;
        }

        public async Task<IApplicationResult<DadosPaginadosViewModel<SituacoesViewModel>>> ObterPaginaSituacoesAsync(SituacoesFiltroRequest request)
        {
            var result = new ApplicationResult<DadosPaginadosViewModel<SituacoesViewModel>>();

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@spsdescricao", request.Descricao, DbType.String, ParameterDirection.Input);

            parameters.Add("@offset", (request.Page - 1) * request.PageSize, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@pageSize", request.PageSize, DbType.Int32, ParameterDirection.Input);

            using (var connection = new NpgsqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                var dadosPaginados = new DadosPaginadosViewModel<SituacoesViewModel>(request);


                using (var multi = await connection.QueryMultipleAsync(PessoaRawSqls.ObterPaginaSituacoes, parameters))
                {
                    dadosPaginados.Registros = multi.Read<SituacoesViewModel>().ToList();
                    dadosPaginados.Controle.TotalRegistros = multi.ReadFirst<int>();
                    result.Result = dadosPaginados;
                }

                connection.Close();
            }

            return result;
        }

        public async Task<IApplicationResult<DadosPaginadosViewModel<TipoFuncaoViewModel>>> ObterPaginaTipoFuncaoAsync(TipoFuncaoFiltroRequest request)
        {
            var result = new ApplicationResult<DadosPaginadosViewModel<TipoFuncaoViewModel>>();

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@tfndescricao", request.Descricao, DbType.String, ParameterDirection.Input);

            parameters.Add("@offset", (request.Page - 1) * request.PageSize, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@pageSize", request.PageSize, DbType.Int32, ParameterDirection.Input);

            using (var connection = new NpgsqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                var dadosPaginados = new DadosPaginadosViewModel<TipoFuncaoViewModel>(request);


                using (var multi = await connection.QueryMultipleAsync(PessoaRawSqls.ObterPaginaTipoFuncao, parameters))
                {
                    dadosPaginados.Registros = multi.Read<TipoFuncaoViewModel>().ToList();
                    dadosPaginados.Controle.TotalRegistros = multi.ReadFirst<int>();
                    result.Result = dadosPaginados;
                }

                connection.Close();
            }

            return result;
        }

        public async Task<IApplicationResult<DadosPaginadosViewModel<TipoVinculoViewModel>>> ObterPaginaTipoVinculoAsync(TipoVinculoFiltroRequest request)
        {
            var result = new ApplicationResult<DadosPaginadosViewModel<TipoVinculoViewModel>>();

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@tvndescricao", request.Descricao, DbType.String, ParameterDirection.Input);

            parameters.Add("@offset", (request.Page - 1) * request.PageSize, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@pageSize", request.PageSize, DbType.Int32, ParameterDirection.Input);

            using (var connection = new NpgsqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                var dadosPaginados = new DadosPaginadosViewModel<TipoVinculoViewModel>(request);


                using (var multi = await connection.QueryMultipleAsync(PessoaRawSqls.ObterPaginaTipoVinculo, parameters))
                {
                    dadosPaginados.Registros = multi.Read<TipoVinculoViewModel>().ToList();
                    dadosPaginados.Controle.TotalRegistros = multi.ReadFirst<int>();
                    result.Result = dadosPaginados;
                }

                connection.Close();
            }

            return result;
        }

        public async Task<IApplicationResult<DadosPaginadosViewModel<FeriadoViewModel>>> ObterPaginaFeriadoAsync(FeriadoFiltroRequest request)
        {
            var result = new ApplicationResult<DadosPaginadosViewModel<FeriadoViewModel>>();

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@ferdescricao", request.Descricao, DbType.String, ParameterDirection.Input);
            //parameters.Add("@ferdata", request.Data, DbType.Date, ParameterDirection.Input);

            parameters.Add("@offset", (request.Page - 1) * request.PageSize, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@pageSize", request.PageSize, DbType.Int32, ParameterDirection.Input);

            using (var connection = new NpgsqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                var dadosPaginados = new DadosPaginadosViewModel<FeriadoViewModel>(request);


                using (var multi = await connection.QueryMultipleAsync(PessoaRawSqls.ObterPaginaFeriado, parameters))
                {
                    dadosPaginados.Registros = multi.Read<FeriadoViewModel>().ToList();
                    dadosPaginados.Controle.TotalRegistros = multi.ReadFirst<int>();
                    result.Result = dadosPaginados;
                }

                connection.Close();
            }

            return result;
        }

        public async Task<IApplicationResult<SituacoesViewModel>> ObterSituacaoPorIdAsync(Int64 id)
        {
            var result = new ApplicationResult<SituacoesViewModel>(null);

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@id", id, DbType.Int64, ParameterDirection.Input);

            using (var connection = new NpgsqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                SituacoesViewModel situacao;

                using (var dados = await connection.QueryMultipleAsync(PessoaRawSqls.ObterSituacaoPorId, parameters))
                {
                    var situacoes = dados.Read<SituacoesViewModel>().ToList();
                    situacao = situacoes.Where(a => a.SituacaoPessoaId == id).First();
                }

                result.Result = situacao;

                connection.Close();
            }

            return result;
        }

        public async Task<IApplicationResult<TipoFuncaoViewModel>> ObterTipoFuncaoPorIdAsync(Int64 id)
        {
            var result = new ApplicationResult<TipoFuncaoViewModel>(null);

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@id", id, DbType.Int64, ParameterDirection.Input);

            using (var connection = new NpgsqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                TipoFuncaoViewModel tipofuncao;

                using (var dados = await connection.QueryMultipleAsync(PessoaRawSqls.ObterTipoFuncaoPorId, parameters))
                {
                    var tipofuncoes = dados.Read<TipoFuncaoViewModel>().ToList();
                    tipofuncao = tipofuncoes.Where(a => a.TipoFuncaoId == id).First();
                }

                result.Result = tipofuncao;

                connection.Close();
            }

            return result;
        }
        public async Task<IApplicationResult<TipoVinculoViewModel>> ObterTipoVinculoPorIdAsync(Int64 id)
        {
            var result = new ApplicationResult<TipoVinculoViewModel>(null);

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@id", id, DbType.Int64, ParameterDirection.Input);

            using (var connection = new NpgsqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                TipoVinculoViewModel tipovinculo;

                using (var dados = await connection.QueryMultipleAsync(PessoaRawSqls.ObterTipoVinculoPorId, parameters))
                {
                    var tipovinculos = dados.Read<TipoVinculoViewModel>().ToList();
                    tipovinculo = tipovinculos.Where(a => a.TipoVinculoId == id).First();
                }

                result.Result = tipovinculo;

                connection.Close();
            }

            return result;
        }

        public async Task<IApplicationResult<FeriadoViewModel>> ObterFeriadoPorIdAsync(Int64 id)
        {
            var result = new ApplicationResult<FeriadoViewModel>(null);

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@id", id, DbType.Int64, ParameterDirection.Input);

            using (var connection = new NpgsqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                FeriadoViewModel feriado;

                using (var dados = await connection.QueryMultipleAsync(PessoaRawSqls.ObterFeriadoPorId, parameters))
                {
                    var feriados = dados.Read<FeriadoViewModel>().ToList();
                    feriado = feriados.Where(a => a.FeriadoId == id).First();
                }

                result.Result = feriado;

                connection.Close();
            }

            return result;
        }

        public async Task<IApplicationResult<DadosPaginadosViewModel<PessoaViewModel>>> ObterPorFiltroAsync(PessoaFiltroRequest request)
        {
            var result = new ApplicationResult<DadosPaginadosViewModel<PessoaViewModel>>();

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@pesNome", request.Nome, DbType.String, ParameterDirection.Input);
            parameters.Add("@unidadeId", request.UnidadeId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@catalogoDominioId", request.CatalogoDominioId, DbType.Int32, ParameterDirection.Input);

            parameters.Add("@offset", (request.Page - 1) * request.PageSize, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@pageSize", request.PageSize, DbType.Int32, ParameterDirection.Input);

            using (var connection = new NpgsqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                var dadosPaginados = new DadosPaginadosViewModel<PessoaViewModel>(request);


                using (var multi = await connection.QueryMultipleAsync(PessoaRawSqls.ObterPorFiltro, parameters))
                {
                    dadosPaginados.Registros = multi.Read<PessoaViewModel>().ToList();
                    dadosPaginados.Controle.TotalRegistros = multi.ReadFirst<int>();
                    result.Result = dadosPaginados;
                }

                connection.Close();
            }

            return result;
        }

        public async Task<IApplicationResult<PessoaViewModel>> ObterPorChaveAsync(Int64 pessoaId)
        {
            var result = new ApplicationResult<PessoaViewModel>();

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@pessoaId", pessoaId, DbType.Int64, ParameterDirection.Input);

            using (var connection = new NpgsqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                var dados = await connection.QueryFirstOrDefaultAsync<PessoaViewModel>(PessoaRawSqls.ObterPorChave, parameters);
                result.Result = dados;

                connection.Close();
            }

            return result;
        }

        public async Task<IApplicationResult<IEnumerable<DateTime>>> ObterDiasNaoUteisAsync(Int64 pessoaId, DateTime dataInicio, DateTime dataFim)
        {
            var result = new ApplicationResult<IEnumerable<DateTime>>();

            //Obtém os dados da pessoa
            var dadosPessoa = await this.ObterPorChaveAsync(pessoaId);

            //Obtém os feriados pela unidade da pessoa
            var feriados = await UnidadeQuery.ObterFeriadosPorUnidadeAsync(dadosPessoa.Result.UnidadeId, dataInicio, dataFim);

            result.Result = feriados.Result.Select(f => f.Date);

            return result;
        }

        public async Task<IApplicationResult<PessoaViewModel>> ObterDetalhesPorChaveAsync(long pessoaId)
        {
            var result = new ApplicationResult<PessoaViewModel>();

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@pessoaId", pessoaId, DbType.Int64, ParameterDirection.Input);

            using (var connection = new NpgsqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                var results = await connection.QueryMultipleAsync(PessoaRawSqls.ObterDetalhes, parameters);

                var pessoa = results.Read<PessoaViewModel>().FirstOrDefault();

                pessoa.Candidaturas = results.Read<PlanoTrabalhoAtividadeCandidatoViewModel>().ToList();

                var tarefas = results.Read<PlanoTrabalhoAtividadeItemViewModel>();

                var perfis = results.Read<PlanoTrabalhoAtividadeCriterioViewModel>();

                var historicoCandidaturas = results.Read<PlanoTrabalhoAtividadeCandidatoHistoricoViewModel>();

                foreach (PlanoTrabalhoAtividadeCandidatoViewModel candidatura in pessoa.Candidaturas)
                {
                    candidatura.Tarefas = tarefas.Where(r => r.PlanoTrabalhoAtividadeId == candidatura.PlanoTrabalhoAtividadeId).ToList();
                    candidatura.Perfis = perfis.Where(r => r.PlanoTrabalhoAtividadeId == candidatura.PlanoTrabalhoAtividadeId).ToList();
                    candidatura.Descricao = historicoCandidaturas.Where(r => r.PlanoTrabalhoAtividadeCandidatoId == candidatura.PlanoTrabalhoAtividadeCandidatoId && (r.SituacaoId == (int)SituacaoCandidaturaPlanoTrabalhoEnum.Aprovada || r.SituacaoId == (int)SituacaoCandidaturaPlanoTrabalhoEnum.Rejeitada)).OrderByDescending(r => r.data).FirstOrDefault()?.descricao;
                }

                result.Result = pessoa;

                connection.Close();
            }

            return result;
        }

        public async Task<IApplicationResult<IEnumerable<PessoaViewModel>>> ObterComPactoTrabalhoAsync()
        {
            var result = new ApplicationResult<IEnumerable<PessoaViewModel>>();
            using (var connection = new NpgsqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                var dados = await connection.QueryAsync<PessoaViewModel>(PessoaRawSqls.ObterComPactoTrabalho);
                result.Result = dados;

                connection.Close();
            }
            return result;
        }

        public async Task<IApplicationResult<IEnumerable<PessoaViewModel>>> ObterComboCompleto()
        {
            var result = new ApplicationResult<IEnumerable<PessoaViewModel>>();
            using (var connection = new NpgsqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                var dados = await connection.QueryAsync<PessoaViewModel>(PessoaRawSqls.ObterComboCompleto);
                result.Result = dados;

                connection.Close();
            }
            return result;
        }

        public async Task<IApplicationResult<IEnumerable<DadosComboViewModel>>> ObterComboUf()
        {
            var result = new ApplicationResult<IEnumerable<DadosComboViewModel>>();
            using (var connection = new NpgsqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                var dados = await connection.QueryAsync<DadosComboViewModel>(PessoaRawSqls.ObterComboUf);
                result.Result = dados;

                connection.Close();
            }
            return result;
        }

        public async Task<IApplicationResult<IEnumerable<DadosComboViewModel>>> ObterComboFuncoes()
        {
            var result = new ApplicationResult<IEnumerable<DadosComboViewModel>>();
            using (var connection = new NpgsqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                var dados = await connection.QueryAsync<DadosComboViewModel>(PessoaRawSqls.ObterComboFuncoes);
                result.Result = dados;

                connection.Close();
            }
            return result;
        }

        public async Task<IApplicationResult<IEnumerable<DadosComboViewModel>>> ObterComboVinculos()
        {
            var result = new ApplicationResult<IEnumerable<DadosComboViewModel>>();
            using (var connection = new NpgsqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                var dados = await connection.QueryAsync<DadosComboViewModel>(PessoaRawSqls.ObterComboVinculo);
                result.Result = dados;

                connection.Close();
            }
            return result;
        }

        public async Task<IApplicationResult<IEnumerable<DadosComboViewModel>>> ObterComboSituacoes()
        {
            var result = new ApplicationResult<IEnumerable<DadosComboViewModel>>();
            using (var connection = new NpgsqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                var dados = await connection.QueryAsync<DadosComboViewModel>(PessoaRawSqls.ObterComboSituacao);
                result.Result = dados;

                connection.Close();
            }
            return result;
        }

        public async Task<IApplicationResult<bool>> ValorDuplicadoAsync(Int64 pessoaId, String cpf)
        {
            var result = new ApplicationResult<bool>(null);

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@pessoaId", pessoaId, DbType.Int64, ParameterDirection.Input);
            parameters.Add("@cpf", cpf, DbType.String, ParameterDirection.Input);

            using (var connection = new NpgsqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                var dados = await connection.QueryAsync<int>(PessoaRawSqls.ObterDuplicacao, parameters);
                result.Result = dados.FirstOrDefault() > 0;

                connection.Close();
            }

            return result;
        }

        public async Task<IApplicationResult<bool>> SituacaoValorDuplicadoAsync(String Descricao)
        {
            var result = new ApplicationResult<bool>(null);

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@descricao", Descricao, DbType.String, ParameterDirection.Input);

            using (var connection = new NpgsqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                var dados = await connection.QueryAsync<int>(PessoaRawSqls.ObterDuplicacaoSituacao, parameters);
                result.Result = dados.FirstOrDefault() > 0;

                connection.Close();
            }

            return result;
        }

        public async Task<IApplicationResult<bool>> TipoFuncaoValorDuplicadoAsync(String Descricao)
        {
            var result = new ApplicationResult<bool>(null);

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@descricao", Descricao, DbType.String, ParameterDirection.Input);

            using (var connection = new NpgsqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                var dados = await connection.QueryAsync<int>(PessoaRawSqls.ObterDuplicacaoTipoFuncao, parameters);
                result.Result = dados.FirstOrDefault() > 0;

                connection.Close();
            }

            return result;
        }

        public async Task<IApplicationResult<bool>> TipoVinculoValorDuplicadoAsync(String Descricao)
        {
            var result = new ApplicationResult<bool>(null);

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@descricao", Descricao, DbType.String, ParameterDirection.Input);

            using (var connection = new NpgsqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                var dados = await connection.QueryAsync<int>(PessoaRawSqls.ObterDuplicacaoTipoVinculo, parameters);
                result.Result = dados.FirstOrDefault() > 0;

                connection.Close();
            }

            return result;
        }

        

        public async Task<IApplicationResult<bool>> DescricaoFeriadoDuplicadoAsync(String descricao)
        {
            var result = new ApplicationResult<bool>(null);

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@descricao", descricao, DbType.String, ParameterDirection.Input);

            using (var connection = new NpgsqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                var dados = await connection.QueryAsync<int>(PessoaRawSqls.ObterDuplicacaoFeriadoDescricao, parameters);
                result.Result = dados.FirstOrDefault() > 0;

                connection.Close();
            }

            return result;
        }

        public async Task<IApplicationResult<bool>> DataFeriadoDuplicadoAsync(DateTime data)
        {
            var result = new ApplicationResult<bool>(null);

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@data", data.Date, DbType.Date, ParameterDirection.Input);

            using (var connection = new NpgsqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                var dados = await connection.QueryAsync<int>(PessoaRawSqls.ObterDuplicacaoFeriadoData, parameters);
                result.Result = dados.FirstOrDefault() > 0;

                connection.Close();
            }

            return result;
        }

    }
}
