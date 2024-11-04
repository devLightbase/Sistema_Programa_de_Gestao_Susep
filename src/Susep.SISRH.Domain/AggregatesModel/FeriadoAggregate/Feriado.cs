using Susep.SISRH.Domain.AggregatesModel.PactoTrabalhoAggregate;
using Susep.SISRH.Domain.AggregatesModel.PlanoTrabalhoAggregate;
using Susep.SISRH.Domain.AggregatesModel.UnidadeAggregate;
using SUSEP.Framework.SeedWorks.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Susep.SISRH.Domain.AggregatesModel.FeriadoAggregate
{
    public class Feriado : Entity
    {

        public Int64 FeriadoId { get; private set; }
        public DateTime Data { get; private set; }
        public Boolean Fixo { get; private set; }
        public String Descricao { get; private set; }
        public String UfId { get; private set; }
        public Int64 Situacao { get; private set; }

        public static Feriado Criar(Int64 feriadoId, DateTime data, Boolean fixo, String descricao, String ufid, Int64 situacao)
        {
            return new Feriado()
            {
                FeriadoId = feriadoId,
                Data = data,
                Fixo = fixo,
                Descricao = descricao,
                UfId = ufid,
                Situacao = situacao
            };
        }

        public void Alterar(Int64 feriadoId, DateTime data, Boolean fixo, String descricao, String ufid, Int64 situacao)
        {
            this.FeriadoId = feriadoId;
            this.Data = data;
            this.Fixo = fixo;
            this.Descricao = descricao;
            this.UfId = ufid;
            this.Situacao = situacao;
        }

    }
}
