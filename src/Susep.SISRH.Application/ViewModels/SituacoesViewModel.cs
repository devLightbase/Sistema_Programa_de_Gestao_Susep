using System;
using System.Runtime.Serialization;

namespace Susep.SISRH.Application.ViewModels
{
    public class SituacoesViewModel
    {
        [DataMember(Name = "situacaopessoaid")]
        public Int64 SituacaoPessoaId { get; set; }

        [DataMember(Name = "spsdescricao")]
        public String Descricao { get; set; }

        [DataMember(Name = "situacao")]
        public Int64 Situacao { get; set; }

    }
}
