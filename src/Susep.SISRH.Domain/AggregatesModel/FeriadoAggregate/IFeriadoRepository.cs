using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Susep.SISRH.Domain.AggregatesModel.FeriadoAggregate
{
    public interface IFeriadoRepository
    {
        Task<Feriado> ObterAsync(Int64 feriadoId);
        Task<Feriado> AdicionarAsync(Feriado item);
        void Atualizar(Feriado item);
        void Excluir(Feriado item);
    }
}
