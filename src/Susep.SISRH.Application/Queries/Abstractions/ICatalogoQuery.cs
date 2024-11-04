using Susep.SISRH.Application.Requests;
using Susep.SISRH.Application.ViewModels;
using SUSEP.Framework.Messages.Concrete.Request;
using SUSEP.Framework.Result.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Susep.SISRH.Application.Queries.Abstractions
{
    public interface ICatalogoQuery
    {
        Task<IApplicationResult<DadosPaginadosViewModel<CatalogoViewModel>>> ObterPorFiltroAsync(CatalogoFiltroRequest request);
        Task<IApplicationResult<DadosPaginadosViewModel<CatalogoDominioViewModel>>> ObterCatalogoDominioPorFiltroAsync(CatalogoDominioFiltroRequest request);
        Task<IApplicationResult<DadosPaginadosViewModel<PgViewModel>>> ObterPgUnidadeAsync(PgUnidadeFiltroRequest request);
        Task<IApplicationResult<DadosPaginadosViewModel<ProgramaGestaoModalViewModel>>> ObterProgramaGestaoModal(ProgramaGestaoModalFiltroRequest request);
        Task<IApplicationResult<DadosPaginadosViewModel<PactosVigentesModalViewModel>>> ObterPactosVigentesModal(PactosVigentesModalFiltroRequest request);
        Task<IApplicationResult<ContagemViewModel>> ObterContagem();
        Task<IApplicationResult<CatalogoViewModel>> ObterPorChaveAsync(Guid catalogoid); 
        Task<IApplicationResult<CatalogoViewModel>> ObterPorUnidadeAsync(Int32 unidadeId);
        Task<IApplicationResult<IEnumerable<ItemCatalogoViewModel>>> ObterItensPorUnidadeAsync(Int32 unidadeId);
        
    }
}
