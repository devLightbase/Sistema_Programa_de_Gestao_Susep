using Microsoft.AspNetCore.Mvc;
using Susep.SISRH.Application.Requests;
using Susep.SISRH.Application.ViewModels;
using SUSEP.Framework.Messages.Concrete.Request;
using SUSEP.Framework.Result.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Susep.SISRH.Application.Queries.Abstractions
{
    public interface IPessoaQuery
    {
        Task<IApplicationResult<DashboardViewModel>> ObterDashboardAsync(UsuarioLogadoRequest request); 
        Task<IApplicationResult<DadosPaginadosViewModel<SituacoesViewModel>>> ObterPaginaSituacoesAsync(SituacoesFiltroRequest request);
        Task<IApplicationResult<DadosPaginadosViewModel<FeriadoViewModel>>> ObterPaginaFeriadoAsync(FeriadoFiltroRequest request);
        Task<IApplicationResult<DadosPaginadosViewModel<TipoFuncaoViewModel>>> ObterPaginaTipoFuncaoAsync(TipoFuncaoFiltroRequest request);
        Task<IApplicationResult<DadosPaginadosViewModel<TipoVinculoViewModel>>> ObterPaginaTipoVinculoAsync(TipoVinculoFiltroRequest request);
        Task<IApplicationResult<SituacoesViewModel>> ObterSituacaoPorIdAsync(Int64 id);
        Task<IApplicationResult<TipoFuncaoViewModel>> ObterTipoFuncaoPorIdAsync(Int64 id);
        Task<IApplicationResult<TipoVinculoViewModel>> ObterTipoVinculoPorIdAsync(Int64 id);
        Task<IApplicationResult<FeriadoViewModel>> ObterFeriadoPorIdAsync(Int64 id);
        Task<IApplicationResult<DadosPaginadosViewModel<PessoaViewModel>>> ObterPorFiltroAsync(PessoaFiltroRequest request);
        Task<IApplicationResult<PessoaViewModel>> ObterPorChaveAsync(Int64 pessoaId);
        Task<IApplicationResult<IEnumerable<DateTime>>> ObterDiasNaoUteisAsync(Int64 pessoaId, DateTime dataInicio, DateTime dataFim);
        Task<IApplicationResult<PessoaViewModel>> ObterDetalhesPorChaveAsync(Int64 pessoaId);
        Task<IApplicationResult<IEnumerable<PessoaViewModel>>> ObterComPactoTrabalhoAsync();
        Task<IApplicationResult<IEnumerable<PessoaViewModel>>> ObterComboCompleto(); 
        Task<IApplicationResult<IEnumerable<DadosComboViewModel>>> ObterComboFuncoes();
        Task<IApplicationResult<IEnumerable<DadosComboViewModel>>> ObterComboVinculos();
        Task<IApplicationResult<IEnumerable<DadosComboViewModel>>> ObterComboSituacoes();
        Task<IApplicationResult<IEnumerable<DadosComboViewModel>>> ObterComboUf();
        Task<IApplicationResult<bool>> ValorDuplicadoAsync(Int64 pessoaId, String cpf);
        Task<IApplicationResult<bool>> SituacaoValorDuplicadoAsync(String Descricao);
        Task<IApplicationResult<bool>> TipoFuncaoValorDuplicadoAsync(String Descricao);
        Task<IApplicationResult<bool>> TipoVinculoValorDuplicadoAsync(String Descricao);
        Task<IApplicationResult<bool>> DescricaoFeriadoDuplicadoAsync(String descricao);
        Task<IApplicationResult<bool>> DataFeriadoDuplicadoAsync(DateTime data);
    }
}
