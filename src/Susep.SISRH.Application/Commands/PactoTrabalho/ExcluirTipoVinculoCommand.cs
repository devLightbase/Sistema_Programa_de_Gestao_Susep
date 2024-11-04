using MediatR;
using Microsoft.AspNetCore.Mvc;
using SUSEP.Framework.Messages.Concrete.Request;
using System;
using System.Runtime.Serialization;

namespace Susep.SISRH.Application.Commands.TipoVinculo
{
    public class ExcluirTipoVinculoCommand : BaseActionRequest, IRequest<IActionResult>
    {
        [DataMember(Name = "TipoVinculoId")]
        public Int64 TipoVinculoId { get; set; }

        [DataMember(Name = "Descricao")]
        public String Descricao { get; set; }

    }
}
