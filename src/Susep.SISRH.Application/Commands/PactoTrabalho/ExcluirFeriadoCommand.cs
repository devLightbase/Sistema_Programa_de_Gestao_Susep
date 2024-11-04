using MediatR;
using Microsoft.AspNetCore.Mvc;
using SUSEP.Framework.Messages.Concrete.Request;
using System;
using System.Runtime.Serialization;

namespace Susep.SISRH.Application.Commands.Feriado
{
    public class ExcluirFeriadoCommand : BaseActionRequest, IRequest<IActionResult>
    {
        [DataMember(Name = "FeriadoId")]
        public Int64 FeriadoId { get; set; }

        [DataMember(Name = "Data")]
        public DateTime Data { get; set; }

        [DataMember(Name = "Descricao")]
        public String Descricao { get; set; }

        [DataMember(Name = "Fixo")]
        public bool Fixo { get; set; }

        [DataMember(Name = "UfId")]
        public String UfId { get; set; }
    }
}
