using SUSEP.Framework.Messages.Concrete.Request;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Susep.SISRH.Application.Requests
{
    public class FeriadoFiltroRequest : UsuarioLogadoRequest
    {
        [DataMember(Name = "descricao")]
        public String Descricao { get; set; }

        //[DataMember(Name = "data")]
        //public DateTime? Data { get; set; }
    }
}
