using MediatR;
using Microsoft.AspNetCore.Mvc;
using Susep.SISRH.Application.Queries.Abstractions;
using Susep.SISRH.Domain.AggregatesModel.SituacaoAggregate;
using Susep.SISRH.Domain.Exceptions;
using SUSEP.Framework.Data.Abstractions.UnitOfWorks;
using SUSEP.Framework.Result.Concrete;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Susep.SISRH.Application.Commands.Situacao
{
    public class CadastrarSituacaoCommandHandler : IRequestHandler<CadastrarSituacaoCommand, IActionResult>
    {
        private ISituacaoRepository SituacaoRepository { get; }
        private IPessoaQuery PessoaQuery { get; }
        private IUnitOfWork UnitOfWork { get; }

        public CadastrarSituacaoCommandHandler(
            ISituacaoRepository situacaoRepository,
            IPessoaQuery pessoaQuery,
            IUnitOfWork unitOfWork)
        {
            SituacaoRepository = situacaoRepository;
            PessoaQuery = pessoaQuery;
            UnitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Handle(CadastrarSituacaoCommand request, CancellationToken cancellationToken)
        {
            ApplicationResult<Int64> result = new ApplicationResult<Int64>(request);
            try
            {
                //Monta o objeto com os dados da situacao
                var situacao = Domain.AggregatesModel.SituacaoAggregate.Situacao.Criar(
                request.SituacaoPessoaId,
                request.Descricao,
                request.situacao);

                await situacaoComValorDuplicado(situacao);

                //Adiciona a situacao no banco de dados
                await SituacaoRepository.AdicionarAsync(situacao);
                UnitOfWork.Commit(false);

                result.Result = situacao.SituacaoPessoaId;
                result.SetHttpStatusToOk("Situação cadastrada com sucesso.");
                return result;
            }
            catch (SISRHDomainException e)
            {
                result.SetHttpStatusToBadRequest(e.Message);
                return result;
            }
        }

        private async Task situacaoComValorDuplicado(Susep.SISRH.Domain.AggregatesModel.SituacaoAggregate.Situacao situacao)
        {
            if (situacao != null)
            {
                var valorDuplicado = await PessoaQuery.SituacaoValorDuplicadoAsync(situacao.Descricao);
                if (valorDuplicado.Result)
                {
                    throw new SISRHDomainException("Situação com Descrição duplicada.");
                }
            }
        }
    }
}
