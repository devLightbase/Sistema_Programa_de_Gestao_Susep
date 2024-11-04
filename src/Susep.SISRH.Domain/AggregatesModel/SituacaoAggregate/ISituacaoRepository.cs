using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Susep.SISRH.Domain.AggregatesModel.SituacaoAggregate
{
    public interface ISituacaoRepository
    {
        Task<Situacao> ObterAsync(Int64 situacaoPessoaId);
        Task<Situacao> AdicionarAsync(Situacao item);
        void Atualizar(Situacao item);
        void Excluir(Situacao item);
    }
}
