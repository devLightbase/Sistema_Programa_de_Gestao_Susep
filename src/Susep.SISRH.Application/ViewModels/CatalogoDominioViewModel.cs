using SUSEP.Framework.Messages.Concrete.Request;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Susep.SISRH.Application.ViewModels
{
    public class CatalogoDominioViewModel
    {

        [DataMember(Name = "catalogodominioid")]
        public Int64 CatalogoDominioId { get; set; }

        [DataMember(Name = "classificacao")]
        public String Classificacao { get; set; }

        [DataMember(Name = "descricao")]
        public String Descricao { get; set; }

        [DataMember(Name = "ativo")]
        public Boolean Ativo { get; set; }

    }
}
