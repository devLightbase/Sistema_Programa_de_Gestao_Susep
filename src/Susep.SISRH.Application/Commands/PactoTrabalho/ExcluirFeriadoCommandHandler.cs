using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Susep.SISRH.Application.Queries.Abstractions;
using Susep.SISRH.Domain.AggregatesModel.FeriadoAggregate;
using SUSEP.Framework.Data.Abstractions.UnitOfWorks;
using SUSEP.Framework.Result.Concrete;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Susep.SISRH.Domain.Exceptions;
using System.Linq;

namespace Susep.SISRH.Application.Commands.Feriado
{
    public class ExcluirFeriadoCommandHandler : IRequestHandler<ExcluirFeriadoCommand, IActionResult>
    {
        private IFeriadoRepository FeriadoRepository { get; }
        private IUnitOfWork UnitOfWork { get; }

        public ExcluirFeriadoCommandHandler(
            IFeriadoRepository feriadoRepository,
            IUnitOfWork unitOfWork)
        {
            FeriadoRepository = feriadoRepository;
            UnitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Handle(ExcluirFeriadoCommand request, CancellationToken cancellationToken)
        {
            ApplicationResult<Int64> result = new ApplicationResult<Int64>(request);

            //Monta o objeto com os dados do catalogo
            var feriado = await FeriadoRepository.ObterAsync(request.FeriadoId);

            //Excluir o pacto de trabalho no banco de dados
            FeriadoRepository.Excluir(feriado);
            UnitOfWork.Commit(false);

            result.Result = feriado.FeriadoId;
            result.SetHttpStatusToOk("Feriado excluído com sucesso.");
            return result;
        }
    }
}
