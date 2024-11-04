using SUSEP.Framework.Messages.Concrete.Request;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Susep.SISRH.Application.ViewModels
{
    public class PactosVigentesModalViewModel
    {
        [DataMember(Name = "Nome")]
        public String Nome { get; set; }

        [DataMember(Name = "data_inicio")]
        public DateTime data_inicio { get; set; }

        [DataMember(Name = "data_fim")]
        public DateTime data_fim { get; set; }
        
    }
}
