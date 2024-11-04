using MediatR;
using Microsoft.AspNetCore.Mvc;
using Susep.SISRH.Application.Queries.Abstractions;
using Susep.SISRH.Domain.AggregatesModel.PessoaAggregate;
using Susep.SISRH.Domain.Exceptions;
using SUSEP.Framework.Data.Abstractions.UnitOfWorks;
using SUSEP.Framework.Result.Concrete;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Susep.SISRH.Application.Commands.Pessoa
{
    public class CadastrarPessoaCommandHandler : IRequestHandler<CadastrarPessoaCommand, IActionResult>
    {
        private IPessoaRepository PessoaRepository { get; }
        private IPessoaQuery PessoaQuery { get; }
        private IUnitOfWork UnitOfWork { get; }

        public CadastrarPessoaCommandHandler(
            IPessoaRepository pessoaRepository,
            IPessoaQuery pessoaQuery,
            IUnitOfWork unitOfWork)
        {
            PessoaRepository = pessoaRepository;
            PessoaQuery = pessoaQuery;
            UnitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Handle(CadastrarPessoaCommand request, CancellationToken cancellationToken)
        {
            ApplicationResult<Int64> result = new ApplicationResult<Int64>(request);
            try
            {
                await validarCpf(request);

                //Monta o objeto com os dados do assunto
                var pessoa = Domain.AggregatesModel.PessoaAggregate.Pessoa.Criar(
                Int64.Parse(request.Cpf.Replace(".", "").Replace("-", "")),
                request.Nome,
                request.Email,
                request.UnidadeId,
                request.CargaHorariaDb,
                request.TipoFuncaoId,
                request.SituacaoPessoaId,
                request.TipoVinculoId,
                request.Cpf.Replace(".", "").Replace("-", ""),
                request.MatriculaSiape,
                request.DataNascimento);

                await validarPessoa(pessoa);

                //Adiciona o assunto no banco de dados
                await PessoaRepository.AdicionarAsync(pessoa);
                UnitOfWork.Commit(false);

                result.Result = pessoa.PessoaId;
                result.SetHttpStatusToOk("Pessoa cadastrada com sucesso.");
                return result;
            }
            catch (SISRHDomainException e)
            {
                result.SetHttpStatusToBadRequest(e.Message);
                return result;
            }

            
        }

        private async Task validarCpf(CadastrarPessoaCommand pessoa)
        {
            var cpf_number = pessoa.Cpf.Replace(".", "").Replace("-", "");
            if (cpf_number.Length != 11 || cpf_number.Contains("_")){
                throw new SISRHDomainException("CPF com formato invalido.");
            }
        }

        private async Task validarPessoa(Susep.SISRH.Domain.AggregatesModel.PessoaAggregate.Pessoa pessoa)
        {

            await pessoaComValorDuplicado(pessoa);
        }

        private async Task pessoaComValorDuplicado(Susep.SISRH.Domain.AggregatesModel.PessoaAggregate.Pessoa pessoa)
        {
            if (pessoa != null)
            {
                var valorDuplicado = await PessoaQuery.ValorDuplicadoAsync(pessoa.PessoaId, pessoa.Cpf);
                if (valorDuplicado.Result)
                {
                    throw new SISRHDomainException("Pessoa com CPF duplicado.");
                }
            }
        }

    }
}