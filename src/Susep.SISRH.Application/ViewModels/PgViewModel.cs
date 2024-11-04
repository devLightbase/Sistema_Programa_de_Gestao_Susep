using SUSEP.Framework.Messages.Concrete.Request;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Susep.SISRH.Application.ViewModels
{
    public class PgViewModel
    {
        [DataMember(Name = "sigla")]
        public String sigla { get; set; }

        [DataMember(Name = "descricao")]
        public String descricao { get; set; }

        [DataMember(Name = "n_pessoas")]
        public Int64 n_pessoas { get; set; }

        [DataMember(Name = "n_pg")]
        public Int64 n_pg { get; set; }

        [DataMember(Name = "n_vigentes")]
        public Int64 n_vigentes { get; set; }
    }
}
