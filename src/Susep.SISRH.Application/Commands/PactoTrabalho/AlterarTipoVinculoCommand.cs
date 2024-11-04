using MediatR;
using Microsoft.AspNetCore.Mvc;
using SUSEP.Framework.Messages.Concrete.Request;
using System;
using System.Runtime.Serialization;

namespace Susep.SISRH.Application.Commands.TipoVinculo
{
    public class AlterarTipoVinculoCommand : BaseActionRequest, IRequest<IActionResult>
    {
        [DataMember(Name = "TipoVinculoId")]
        public Int64 TipoVinculoId { get; set; }

        [DataMember(Name = "Descricao")]
        public String Descricao { get; set; }

        [DataMember(Name = "Situacao")]
        public Int64 Situacao { get; set; }

    }
}
