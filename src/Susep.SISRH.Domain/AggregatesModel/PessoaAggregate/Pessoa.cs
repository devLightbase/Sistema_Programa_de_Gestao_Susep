using Susep.SISRH.Domain.AggregatesModel.FeriadoAggregate;
using Susep.SISRH.Domain.AggregatesModel.PactoTrabalhoAggregate;
using Susep.SISRH.Domain.AggregatesModel.PlanoTrabalhoAggregate;
using Susep.SISRH.Domain.AggregatesModel.UnidadeAggregate;
using SUSEP.Framework.SeedWorks.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Susep.SISRH.Domain.AggregatesModel.PessoaAggregate
{
    /// <summary>
    /// Representa as pessoas 
    /// </summary>
    public class Pessoa : Entity
    {

        public Int64 PessoaId { get; private set; }

        public String Nome { get; private set; }
        public String Email { get; private set; }
        public String Cpf { get; private set; }
        public String MatriculaSiape { get; private set; }
        public Int64 UnidadeId { get; private set; }
        public Int64? TipoFuncaoId { get; private set; }
        public Int32? CargaHorariaDb { get; private set; }

        public Int32 CargaHoraria
        {
            get => CargaHorariaDb.HasValue && CargaHorariaDb.Value > 0 ?
                   CargaHorariaDb.Value : 8;
        }
        public Int64? SituacaoPessoaId { get; private set; }
        public Int64? TipoVinculoId { get; private set; }
        public DateTime? DataNascimento { get; private set; }

        public Unidade Unidade { get; private set; }
        public IEnumerable<PessoaModalidadeExecucao> ModalidadesExecucao { get; private set; }
        public IEnumerable<PactoTrabalho> PactosTrabalho { get; private set; }
        public List<PlanoTrabalhoAtividadeCandidato> Candidaturas { get; private set; }

        public static Pessoa Criar(Int64 pessoaId, String nome, String email, Int64 unidadeId, Int32? cargaHorariaDb, Int64? tipoFuncaoId, Int64? situacaoPessoaId, Int64? tipoVinculoId,
            String cpf, String matriculaSiape, DateTime? dataNascimento)
        {
            return new Pessoa()
            {
                PessoaId = pessoaId,
                Nome = nome,
                Email = email,
                UnidadeId = unidadeId,
                CargaHorariaDb = cargaHorariaDb,
                TipoFuncaoId = tipoFuncaoId,
                SituacaoPessoaId = situacaoPessoaId,
                TipoVinculoId = tipoVinculoId,
                Cpf = cpf,
                MatriculaSiape = matriculaSiape,
                DataNascimento = dataNascimento,
            };
        }

        public void Alterar( String nome, String email, Int64 unidadeId, Int32? cargaHorariaDb, Int64? tipoFuncaoId, Int64? situacaoPessoaId, Int64? tipoVinculoId,
            String cpf, String matriculaSiape, DateTime? dataNascimento)
        {
            //this.PessoaId = pessoaId;
            this.Nome = nome;
            this.Email = email;
            this.UnidadeId = unidadeId;
            this.CargaHorariaDb = cargaHorariaDb;
            this.TipoFuncaoId = tipoFuncaoId;
            this.SituacaoPessoaId = situacaoPessoaId;
            this.TipoVinculoId = tipoVinculoId;
            this.Cpf = cpf;
            this.MatriculaSiape = matriculaSiape;
            this.DataNascimento = dataNascimento;
        }
    }
}
