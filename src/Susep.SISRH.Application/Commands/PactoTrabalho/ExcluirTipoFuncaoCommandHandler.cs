using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Susep.SISRH.Application.Queries.Abstractions;
using Susep.SISRH.Domain.AggregatesModel.TipoFuncaoAggregate;
using SUSEP.Framework.Data.Abstractions.UnitOfWorks;
using SUSEP.Framework.Result.Concrete;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Susep.SISRH.Domain.Exceptions;
using System.Linq;

namespace Susep.SISRH.Application.Commands.TipoFuncao
{
    public class ExcluirTipoFuncaoCommandHandler : IRequestHandler<ExcluirTipoFuncaoCommand, IActionResult>
    {
        private ITipoFuncaoRepository TipoFuncaoRepository { get; }
        private IUnitOfWork UnitOfWork { get; }

        public ExcluirTipoFuncaoCommandHandler(
            ITipoFuncaoRepository tipoFuncaoRepository,
            IUnitOfWork unitOfWork)
        {
            TipoFuncaoRepository = tipoFuncaoRepository;
            UnitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Handle(ExcluirTipoFuncaoCommand request, CancellationToken cancellationToken)
        {
            ApplicationResult<Int64> result = new ApplicationResult<Int64>(request);

            //Monta o objeto com os dados do catalogo
            var tipofuncao = await TipoFuncaoRepository.ObterAsync(request.TipoFuncaoId);

            //Excluir o pacto de trabalho no banco de dados
            TipoFuncaoRepository.Excluir(tipofuncao);
            UnitOfWork.Commit(false);

            result.Result = tipofuncao.TipoFuncaoId;
            result.SetHttpStatusToOk("Tipo Função excluída com sucesso.");
            return result;
        }
    }
}
