﻿using SUSEP.Framework.Messages.Concrete.Request;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Susep.SISRH.Application.Requests
{
    public class PgUnidadeFiltroRequest : UsuarioLogadoRequest
    {
        [DataMember(Name = "sigla")]
        public String Sigla { get; set; }

        [DataMember(Name = "descricao")]
        public String Descricao { get; set; }
    }
}