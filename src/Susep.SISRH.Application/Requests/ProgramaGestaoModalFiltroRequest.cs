using SUSEP.Framework.Messages.Concrete.Request;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Susep.SISRH.Application.Requests
{
    public class ProgramaGestaoModalFiltroRequest : UsuarioLogadoRequest
    {
        [DataMember(Name = "sigla")]
        public String Sigla { get; set; }
    }
}
