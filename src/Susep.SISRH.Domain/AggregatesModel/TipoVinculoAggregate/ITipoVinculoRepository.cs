using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Susep.SISRH.Domain.AggregatesModel.TipoVinculoAggregate
{
    public interface ITipoVinculoRepository
    {
        Task<TipoVinculo> ObterAsync(Int64 tipoVinculoId);
        Task<TipoVinculo> AdicionarAsync(TipoVinculo item);
        void Atualizar(TipoVinculo item);
        void Excluir(TipoVinculo item);
    }
}
