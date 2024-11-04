using System;
using System.Runtime.Serialization;


namespace Susep.SISRH.Application.ViewModels
{
    public class TipoVinculoViewModel
    {
        [DataMember(Name = "tipovinculoid")]
        public Int64 TipoVinculoId { get; set; }

        [DataMember(Name = "tvndescricao")]
        public String Descricao { get; set; }

        [DataMember(Name = "situacao")]
        public Int64 Situacao { get; set; }
    }
}