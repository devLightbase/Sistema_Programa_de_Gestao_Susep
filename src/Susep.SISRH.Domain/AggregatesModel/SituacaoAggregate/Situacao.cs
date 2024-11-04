using SUSEP.Framework.SeedWorks.Domains;
using System;

namespace Susep.SISRH.Domain.AggregatesModel.SituacaoAggregate
{
    public class Situacao : Entity
    {
        public Int64 SituacaoPessoaId { get; private set; }
        public String Descricao { get; private set; }
        public Int64 situacao { get; private set; }

        public static Situacao Criar(Int64 situacaoPessoaId, String descricao, Int64 Situacao)
        {
            return new Situacao()
            {
                SituacaoPessoaId = situacaoPessoaId,
                Descricao = descricao,
                situacao = Situacao
            };
        }

        public void Alterar(Int64 situacaoPessoaId, String descricao, Int64 Situacao)
        {
            this.SituacaoPessoaId = situacaoPessoaId;
            this.Descricao = descricao;
            this.situacao = Situacao;
        }
    }
}
