using System;
using System.Runtime.Serialization;


namespace Susep.SISRH.Application.ViewModels
{
    public class TipoFuncaoViewModel
    {
        [DataMember(Name = "tipofuncaoid")]
        public Int64 TipoFuncaoId { get; set; }

        [DataMember(Name = "tfndescricao")]
        public String Descricao { get; set; }

        [DataMember(Name = "tfncodigofuncao")]
        public String CodigoFuncao { get; set; }

        [DataMember(Name = "tfnindicadorchefia")]
        public Boolean IndicadorChefia { get; set; }

        [DataMember(Name = "Situacao")]
        public Int64 Situacao { get; set; }

    }
}
