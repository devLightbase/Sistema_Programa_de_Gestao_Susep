using MediatR;
using Microsoft.AspNetCore.Mvc;
using Susep.SISRH.Domain.AggregatesModel.PessoaAggregate;
using SUSEP.Framework.Data.Abstractions.UnitOfWorks;
using SUSEP.Framework.Result.Concrete;
using Susep.SISRH.Application.Queries.Abstractions;
using System;
using System.Threading;
using System.Threading.Tasks;
using Susep.SISRH.Domain.Exceptions;
using System.Linq;

namespace Susep.SISRH.Application.Commands.Pessoa
{
    public class AlterarPessoaCommandHandler : IRequestHandler<AlterarPessoaCommand, IActionResult>
    {
        private IPessoaRepository PessoaRepository { get; }
        private IPessoaQuery PessoaQuery { get; }
        private IUnitOfWork UnitOfWork { get; }

        public AlterarPessoaCommandHandler(
            IPessoaRepository pessoaRepository,
            IPessoaQuery pessoaQuery,
            IUnitOfWork unitOfWork)
        {
            PessoaRepository = pessoaRepository;
            PessoaQuery = pessoaQuery;
            UnitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Handle(AlterarPessoaCommand request, CancellationToken cancellationToken)
        {
            ApplicationResult<Int64> result = new ApplicationResult<Int64>(request);
            try
            {
                var pessoa = await PessoaRepository.ObterPessoaDBAsync(request.PessoaId);

                await validarCpf(request);

                pessoa.Alterar(
                    //Int64.Parse(request.Cpf),
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

                PessoaRepository.Atualizar(pessoa);
                UnitOfWork.Commit(false);

                result.Result = pessoa.PessoaId;
                result.SetHttpStatusToOk("Pessoa alterada com sucesso.");
                return result;
            }
            catch (SISRHDomainException e)
            {
                result.SetHttpStatusToBadRequest(e.Message);
                return result;
            }
        }

        private async Task validarCpf(AlterarPessoaCommand pessoa)
        {
            var cpf_number = pessoa.Cpf.Replace(".", "").Replace("-", "");
            if (cpf_number.Length != 11 || cpf_number.Contains("_"))
            {
                throw new SISRHDomainException("CPF com formato invalido.");
            }
        }
                
    }
}
