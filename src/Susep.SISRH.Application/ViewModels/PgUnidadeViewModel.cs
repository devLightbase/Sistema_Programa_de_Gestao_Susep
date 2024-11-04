using SUSEP.Framework.Messages.Concrete.Request;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Susep.SISRH.Application.ViewModels
{
    public class PgUnidadeViewModel
    {

        [DataMember(Name = "sigla")]
        public String Sigla { get; set; }

        [DataMember(Name = "descricao")]
        public String Descricao { get; set; }

        [DataMember(Name = "n_pessoas")]
        public Int64 N_Pessoas { get; set; }

        [DataMember(Name = "n_pg")]
        public Int64 N_Pg { get; set; }

        [DataMember(Name = "n_vigentes")]
        public Int64 N_Vigentes { get; set; }
    }
}
