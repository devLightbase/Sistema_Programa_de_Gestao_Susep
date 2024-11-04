using MediatR;
using Microsoft.AspNetCore.Mvc;
using SUSEP.Framework.Messages.Concrete.Request;
using System;
using System.Runtime.Serialization;

namespace Susep.SISRH.Application.Commands.Pessoa
{
    public class CadastrarPessoaCommand : BaseActionRequest, IRequest<IActionResult>
    {
        [DataMember(Name = "pessoaId")]
        public Int64 PessoaId { get; set; }

        [DataMember(Name = "nome")]
        public String Nome { get; set; }

        [DataMember(Name = "email")]
        public String Email { get; set; }

        [DataMember(Name = "unidadeId")]
        public Int64 UnidadeId { get; set; }

        [DataMember(Name = "cargaHorariaDb")]
        public Int32? CargaHorariaDb { get; set; }

        [DataMember(Name = "tipoFuncaoId")]
        public Int64? TipoFuncaoId { get; set; }

        [DataMember(Name = "situacaoPessoaId")]
        public Int64? SituacaoPessoaId { get; set; }

        [DataMember(Name = "tipoVinculoId")]
        public Int64? TipoVinculoId { get; set; }

        [DataMember(Name = "cpf")]
        public String Cpf { get; set; }

        [DataMember(Name = "matriculaSiape")]
        public String MatriculaSiape { get; set; }

        [DataMember(Name = "dataNascimento")]
        public DateTime? DataNascimento { get; set; }
    }
}
