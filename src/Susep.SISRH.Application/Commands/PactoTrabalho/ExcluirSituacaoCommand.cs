using MediatR;
using Microsoft.AspNetCore.Mvc;
using SUSEP.Framework.Messages.Concrete.Request;
using System;
using System.Runtime.Serialization;

namespace Susep.SISRH.Application.Commands.Situacao
{
    public class ExcluirSituacaoCommand : BaseActionRequest, IRequest<IActionResult>
    {
        [DataMember(Name = "SituacaoPessoaId")]
        public Int64 SituacaoPessoaId { get; set; }

        [DataMember(Name = "Descricao")]
        public String Descricao { get; set; }
    }
}
