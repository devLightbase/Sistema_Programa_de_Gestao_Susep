using MediatR;
using Microsoft.AspNetCore.Mvc;
using SUSEP.Framework.Messages.Concrete.Request;
using System;
using System.Runtime.Serialization;

namespace Susep.SISRH.Application.Commands.TipoFuncao
{
    public class CadastrarTipoFuncaoCommand : BaseActionRequest, IRequest<IActionResult>
    {
        [DataMember(Name = "TipoFuncaoId")]
        public Int64 TipoFuncaoId { get; set; }

        [DataMember(Name = "Descricao")]
        public String Descricao { get; set; }

        [DataMember(Name = "CodigoFuncao")]
        public String CodigoFuncao { get; set; }

        [DataMember(Name = "IndicadorChefia")]
        public Boolean IndicadorChefia { get; set; }

        [DataMember(Name = "Situacao")]
        public Int64 Situacao { get; set; }
    }
}
