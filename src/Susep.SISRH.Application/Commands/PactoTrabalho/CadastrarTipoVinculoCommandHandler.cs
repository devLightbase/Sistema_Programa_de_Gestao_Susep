using MediatR;
using Microsoft.AspNetCore.Mvc;
using Susep.SISRH.Application.Queries.Abstractions;
using Susep.SISRH.Domain.AggregatesModel.TipoVinculoAggregate;
using Susep.SISRH.Domain.Exceptions;
using SUSEP.Framework.Data.Abstractions.UnitOfWorks;
using SUSEP.Framework.Result.Concrete;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Susep.SISRH.Application.Commands.TipoVinculo
{
    public class CadastrarTipoVinculoCommandHandler : IRequestHandler<CadastrarTipoVinculoCommand, IActionResult>
    {
        private ITipoVinculoRepository TipoVinculoRepository { get; }
        private IPessoaQuery PessoaQuery { get; }
        private IUnitOfWork UnitOfWork { get; }

        public CadastrarTipoVinculoCommandHandler(
            ITipoVinculoRepository tipoVinculoRepository,
            IPessoaQuery pessoaQuery,
            IUnitOfWork unitOfWork)
        {
            TipoVinculoRepository = tipoVinculoRepository;
            PessoaQuery = pessoaQuery;
            UnitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Handle(CadastrarTipoVinculoCommand request, CancellationToken cancellationToken)
        {
            ApplicationResult<Int64> result = new ApplicationResult<Int64>(request);
            try
            {
                //Monta o objeto com os dados do tipo vinculo
                var tipovinculo = Domain.AggregatesModel.TipoVinculoAggregate.TipoVinculo.Criar(
                request.TipoVinculoId,
                request.Descricao,
                request.Situacao);

                await tipoVinculoComValorDuplicado(tipovinculo);

                //Adiciona o tipo no banco de dados
                await TipoVinculoRepository.AdicionarAsync(tipovinculo);
                UnitOfWork.Commit(false);

                result.Result = tipovinculo.TipoVinculoId;
                result.SetHttpStatusToOk("Tipo cadastrado com sucesso.");
                return result;
            }
            catch (SISRHDomainException e)
            {
                result.SetHttpStatusToBadRequest(e.Message);
                return result;
            }
        }
        private async Task tipoVinculoComValorDuplicado(Susep.SISRH.Domain.AggregatesModel.TipoVinculoAggregate.TipoVinculo tipovinculo)
        {
            if (tipovinculo != null)
            {
                var valorDuplicado = await PessoaQuery.TipoVinculoValorDuplicadoAsync(tipovinculo.Descricao);
                if (valorDuplicado.Result)
                {
                    throw new SISRHDomainException("Tipo vinculo com Descrição duplicada.");
                }
            }
        }
    }
}
