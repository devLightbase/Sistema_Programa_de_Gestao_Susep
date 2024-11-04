using SUSEP.Framework.SeedWorks.Domains;
using System;


namespace Susep.SISRH.Domain.AggregatesModel.TipoFuncaoAggregate
{
    public class TipoFuncao : Entity
    {
        public Int64 TipoFuncaoId { get; private set; }
        public String Descricao { get; private set; }
        public String CodigoFuncao { get; private set; }
        public Boolean IndicadorChefia { get; private set; }
        public Int64 Situacao { get; private set; }

        public static TipoFuncao Criar(Int64 tipofuncaoid, String descricao, String codigofuncao, Boolean indicadorchefia, Int64 situacao)
        {
            return new TipoFuncao()
            {
                TipoFuncaoId = tipofuncaoid,
                Descricao = descricao,
                CodigoFuncao = codigofuncao,
                IndicadorChefia = indicadorchefia,
                Situacao = situacao
            };
        }

        public void Alterar(Int64 tipofuncaoid, String descricao, String codigofuncao, Boolean indicadorchefia, Int64 situacao)
        {
            this.TipoFuncaoId = tipofuncaoid;
            this.Descricao = descricao;
            this.CodigoFuncao = codigofuncao;
            this.IndicadorChefia = indicadorchefia;
            this.Situacao = situacao;
        }
    }
}
