using SUSEP.Framework.Messages.Concrete.Request;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Susep.SISRH.Application.ViewModels
{
    public class ContagemViewModel
    {
        [DataMember(Name = "unidades")]
        public Int64 unidades { get; set; }

        [DataMember(Name = "pessoas")]
        public Int64 pessoas { get; set; }

        [DataMember(Name = "unidades_lista")]
        public Int64 unidades_lista { get; set; }

        [DataMember(Name = "pactos_vigentes")]
        public Int64 pactos_vigentes { get; set; }
    }
}
