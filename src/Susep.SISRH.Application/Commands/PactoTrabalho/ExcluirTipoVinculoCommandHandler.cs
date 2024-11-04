using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Susep.SISRH.Application.Queries.Abstractions;
using Susep.SISRH.Domain.AggregatesModel.TipoVinculoAggregate;
using SUSEP.Framework.Data.Abstractions.UnitOfWorks;
using SUSEP.Framework.Result.Concrete;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Susep.SISRH.Domain.Exceptions;
using System.Linq;

namespace Susep.SISRH.Application.Commands.TipoVinculo
{
    public class ExcluirTipoVinculoCommandHandler : IRequestHandler<ExcluirTipoVinculoCommand, IActionResult>
    {
        private ITipoVinculoRepository TipoVinculoRepository { get; }
        private IUnitOfWork UnitOfWork { get; }

        public ExcluirTipoVinculoCommandHandler(
            ITipoVinculoRepository tipoVinculoRepository,
            IUnitOfWork unitOfWork)
        {
            TipoVinculoRepository = tipoVinculoRepository;
            UnitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Handle(ExcluirTipoVinculoCommand request, CancellationToken cancellationToken)
        {
            ApplicationResult<Int64> result = new ApplicationResult<Int64>(request);

            //Monta o objeto com os dados do catalogo
            var tipovinculo = await TipoVinculoRepository.ObterAsync(request.TipoVinculoId);

            //Excluir o pacto de trabalho no banco de dados
            TipoVinculoRepository.Excluir(tipovinculo);
            UnitOfWork.Commit(false);

            result.Result = tipovinculo.TipoVinculoId;
            result.SetHttpStatusToOk("Tipo vinculo excluído com sucesso.");
            return result;
        }
    }
}
