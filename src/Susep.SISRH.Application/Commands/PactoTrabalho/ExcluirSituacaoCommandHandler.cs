using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Susep.SISRH.Application.Queries.Abstractions;
using Susep.SISRH.Domain.AggregatesModel.SituacaoAggregate;
using SUSEP.Framework.Data.Abstractions.UnitOfWorks;
using SUSEP.Framework.Result.Concrete;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Susep.SISRH.Domain.Exceptions;
using System.Linq;

namespace Susep.SISRH.Application.Commands.Situacao
{
    public class ExcluirSituacaoCommandHandler : IRequestHandler<ExcluirSituacaoCommand, IActionResult>
    {
        private ISituacaoRepository SituacaoRepository { get; }
        private IUnitOfWork UnitOfWork { get; }

        public ExcluirSituacaoCommandHandler(
            ISituacaoRepository situacaoRepository,
            IUnitOfWork unitOfWork)
        {
            SituacaoRepository = situacaoRepository;
            UnitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Handle(ExcluirSituacaoCommand request, CancellationToken cancellationToken)
        {
            ApplicationResult<Int64> result = new ApplicationResult<Int64>(request);

            //Monta o objeto com os dados do catalogo
            var situacao = await SituacaoRepository.ObterAsync(request.SituacaoPessoaId);

            //Excluir o pacto de trabalho no banco de dados
            SituacaoRepository.Excluir(situacao);
            UnitOfWork.Commit(false);

            result.Result = situacao.SituacaoPessoaId;
            result.SetHttpStatusToOk("Situação excluída com sucesso.");
            return result;
        }
    }
}
