using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Susep.SISRH.Application.ViewModels
{
    [DataContract(Namespace = "http://www.susep.gov.br/sisrh/viewmodels/", Name = "feriado")]
    public class FeriadoViewModel
    {
        [DataMember(Name = "FeriadoId")]
        public Int64 FeriadoId { get; set; }

        [DataMember(Name = "Data")]
        public DateTime Data { get; set; }

        [DataMember(Name = "Descricao")]
        public String Descricao { get; set; }

        [DataMember(Name = "Fixo")]
        public bool Fixo { get; set; }

        [DataMember(Name = "UfId")]
        public String UfId { get; set; }

        [DataMember(Name = "Situacao")]
        public Int64 Situacao { get; set; }
    }
}
