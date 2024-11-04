using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Susep.SISRH.Domain.AggregatesModel.UnidadesAggregate
{
    public interface IUnidadesRepository
    {
        Task<UnidadeDB> ObterAsync(Int64 unidadeId);
        Task<UnidadeDB> AdicionarAsync(UnidadeDB item);
        void Atualizar(UnidadeDB item);
    }
}
