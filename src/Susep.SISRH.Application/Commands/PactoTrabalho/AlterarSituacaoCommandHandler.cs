using MediatR;
using Microsoft.AspNetCore.Mvc;
using Susep.SISRH.Domain.AggregatesModel.SituacaoAggregate;
using SUSEP.Framework.Data.Abstractions.UnitOfWorks;
using SUSEP.Framework.Result.Concrete;
using Susep.SISRH.Application.Queries.Abstractions;
using System;
using System.Threading;
using System.Threading.Tasks;
using Susep.SISRH.Domain.Exceptions;
using System.Linq;

namespace Susep.SISRH.Application.Commands.Situacao
{
    public class AlterarSituacaoCommandHandler : IRequestHandler<AlterarSituacaoCommand, IActionResult>
    {
        private ISituacaoRepository SituacaoRepository { get; }
        private IPessoaQuery PessoaQuery { get; }
        private IUnitOfWork UnitOfWork { get; }

        public AlterarSituacaoCommandHandler(
            ISituacaoRepository situacaoRepository,
            IPessoaQuery pessoaQuery,
            IUnitOfWork unitOfWork)
        {
            SituacaoRepository = situacaoRepository;
            PessoaQuery = pessoaQuery;
            UnitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Handle(AlterarSituacaoCommand request, CancellationToken cancellationToken)
        {
            ApplicationResult<Int64> result = new ApplicationResult<Int64>(request);
            try
            {
                //Recupera o objeto situacao
                var situacao = await SituacaoRepository.ObterAsync(request.SituacaoPessoaId);
                var mesmo_nome = false;

                if(request.Descricao == situacao.Descricao)
                {
                    mesmo_nome = true;
                }

                //Monta o objeto com os dados da situacao
                situacao.Alterar(
                    request.SituacaoPessoaId,
                    request.Descricao,
                    request.situacao);

                await situacaoComValorDuplicado(situacao, mesmo_nome);

                //Altera a situacao no banco de dados
                SituacaoRepository.Atualizar(situacao);
                UnitOfWork.Commit(false);

                result.Result = situacao.SituacaoPessoaId;
                result.SetHttpStatusToOk("Situação alterada com sucesso.");
                return result;
            }
            catch (SISRHDomainException e)
            {
                result.SetHttpStatusToBadRequest(e.Message);
                return result;
            }
        }

        private async Task situacaoComValorDuplicado(Susep.SISRH.Domain.AggregatesModel.SituacaoAggregate.Situacao situacao, Boolean mesmo_nome)
        {
            if (situacao != null && !mesmo_nome)
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
