using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Susep.SISRH.Domain.AggregatesModel.TipoFuncaoAggregate
{
    public interface ITipoFuncaoRepository
    {
        Task<TipoFuncao> ObterAsync(Int64 tipoFuncaoId);
        Task<TipoFuncao> AdicionarAsync(TipoFuncao item);
        void Atualizar(TipoFuncao item);
        void Excluir(TipoFuncao item);
    }
}
