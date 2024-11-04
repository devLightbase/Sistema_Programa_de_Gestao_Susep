using SUSEP.Framework.SeedWorks.Domains;
using System;


namespace Susep.SISRH.Domain.AggregatesModel.TipoVinculoAggregate
{
    public class TipoVinculo : Entity
    {
        public Int64 TipoVinculoId { get; private set; }
        public String Descricao { get; private set; }
        public Int64 Situacao { get; private set; }

        public static TipoVinculo Criar(Int64 tipoVinculoId, String descricao, Int64 situacao)
        {
            return new TipoVinculo()
            {
                TipoVinculoId = tipoVinculoId,
                Descricao = descricao,
                Situacao = situacao
            };
        }

        public void Alterar(Int64 tipoVinculoId, String descricao, Int64 situacao)
        {
            this.TipoVinculoId = tipoVinculoId;
            this.Descricao = descricao;
            this.Situacao = situacao;
        }
    }
}
