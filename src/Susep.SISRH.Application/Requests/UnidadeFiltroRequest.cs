using SUSEP.Framework.Messages.Concrete.Request;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Susep.SISRH.Application.Requests
{
    public class UnidadeFiltroRequest : UsuarioLogadoRequest
    {
        [DataMember(Name = "nome")]
        public String Nome { get; set; }

        [DataMember(Name = "sigla")]
        public String Sigla { get; set; }
    }
}
