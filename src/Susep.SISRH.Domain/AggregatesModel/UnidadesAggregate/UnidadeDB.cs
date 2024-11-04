using Susep.SISRH.Domain.AggregatesModel.CatalogoAggregate;
using Susep.SISRH.Domain.AggregatesModel.PactoTrabalhoAggregate;
using Susep.SISRH.Domain.AggregatesModel.PlanoTrabalhoAggregate;
using SUSEP.Framework.SeedWorks.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Susep.SISRH.Domain.AggregatesModel.UnidadeAggregate;

namespace Susep.SISRH.Domain.AggregatesModel.UnidadesAggregate
{
    /// <summary>
    /// Representa as unidades/setores
    /// </summary>
    public class UnidadeDB : Entity
    {

        public Int64 Unidadeid { get; private set; }
        public String Nome { get; private set; }
        public String Sigla { get; private set; }
        public Int64 Nivel { get; private set; }
        public Int64? UnidadeIdPai { get; private set; }
        public Int64? PessoaIdChefe { get; private set; }
        public Int64? PessoaIdChefeSubstituto { get; private set; }
        public String Email { get; private set; }
        public Int64 TipoFuncaoUnidadeId { get; private set; }
        public Int64 TipoUnidadeId { get; private set; }
        public Int64 SituacaoUnidadeId { get; private set; }
        public Int64 CodSiorg { get; private set; }
        public Int64 CodSgc { get; private set; }
        public String UfId { get; private set; }
        
        public static UnidadeDB Criar(Int64 unidadeId, String nome, String sigla, Int64 nivel, Int64? unidadeIdPai, String email, String ufId
            , Int64? pessoaIdChefe, Int64? pessoaIdChefeSubstituto, Int64 tipoFuncaoUnidadeId, Int64 situacaoUnidadeId, Int64 tipoUnidadeId,
            Int64 codSiorg, Int64 codSgc)
        {
            return new UnidadeDB()
            {
                Unidadeid = unidadeId,
                Nome = nome,
                Sigla = sigla,
                Nivel = nivel,
                UnidadeIdPai = unidadeIdPai,
                Email = email,
                PessoaIdChefe = pessoaIdChefe,
                PessoaIdChefeSubstituto = pessoaIdChefeSubstituto,
                TipoFuncaoUnidadeId = tipoFuncaoUnidadeId,
                SituacaoUnidadeId = situacaoUnidadeId,
                TipoUnidadeId = tipoUnidadeId,
                UfId = ufId,
                CodSiorg = codSiorg,
                CodSgc = codSgc
            };
        }

        public void Alterar(Int64 unidadeId, String nome, String sigla, Int64 nivel, Int64? unidadeIdPai, String email, String ufId
            , Int64? pessoaIdChefe, Int64? pessoaIdChefeSubstituto, Int64 tipoFuncaoUnidadeId, Int64 situacaoUnidadeId, Int64 tipoUnidadeId,
            Int64 codSiorg, Int64 codSgc)
        {
            this.Unidadeid = unidadeId;
            this.Nome = nome;
            this.Sigla = sigla;
            this.Nivel = nivel;
            this.UnidadeIdPai = unidadeIdPai;
            this.Email = email;
            this.PessoaIdChefe = pessoaIdChefe;
            this.PessoaIdChefeSubstituto = pessoaIdChefeSubstituto;
            this.TipoFuncaoUnidadeId = tipoFuncaoUnidadeId;
            this.SituacaoUnidadeId = situacaoUnidadeId;
            this.TipoUnidadeId = tipoUnidadeId;
            this.UfId = ufId;
            this.CodSiorg = codSiorg;
            this.CodSgc = codSgc;
        }
    }
}
