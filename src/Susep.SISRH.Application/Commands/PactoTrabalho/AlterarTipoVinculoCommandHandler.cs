using MediatR;
using Microsoft.AspNetCore.Mvc;
using Susep.SISRH.Domain.AggregatesModel.TipoVinculoAggregate;
using SUSEP.Framework.Data.Abstractions.UnitOfWorks;
using SUSEP.Framework.Result.Concrete;
using Susep.SISRH.Application.Queries.Abstractions;
using System;
using System.Threading;
using System.Threading.Tasks;
using Susep.SISRH.Domain.Exceptions;
using System.Linq;

namespace Susep.SISRH.Application.Commands.TipoVinculo
{
    public class AlterarTipoVinculoCommandHandler : IRequestHandler<AlterarTipoVinculoCommand, IActionResult>
    {
        private ITipoVinculoRepository TipoVinculoRepository { get; }
        private IPessoaQuery PessoaQuery { get; }
        private IUnitOfWork UnitOfWork { get; }

        public AlterarTipoVinculoCommandHandler(
            ITipoVinculoRepository tipoVinculoRepository,
            IPessoaQuery pessoaQuery,
            IUnitOfWork unitOfWork)
        {
            TipoVinculoRepository = tipoVinculoRepository;
            PessoaQuery = pessoaQuery;
            UnitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Handle(AlterarTipoVinculoCommand request, CancellationToken cancellationToken)
        {
            ApplicationResult<Int64> result = new ApplicationResult<Int64>(request);
            try
            {
                //Recupera o objeto tipo função
                var tipovinculo = await TipoVinculoRepository.ObterAsync(request.TipoVinculoId);

                var mesmo_nome = false;

                if (request.Descricao == tipovinculo.Descricao)
                {
                    mesmo_nome = true;
                }

                //Monta o objeto com os dados do tipo
                tipovinculo.Alterar(
                    request.TipoVinculoId,
                    request.Descricao,
                    request.Situacao);

                await tipoVinculoComValorDuplicado(tipovinculo, mesmo_nome);

                //Altera a situacao no banco de dados
                TipoVinculoRepository.Atualizar(tipovinculo);
                UnitOfWork.Commit(false);

                result.Result = tipovinculo.TipoVinculoId;
                result.SetHttpStatusToOk("Tipo alterado com sucesso.");
                return result;
            }
            catch (SISRHDomainException e)
            {
                result.SetHttpStatusToBadRequest(e.Message);
                return result;
            }
        }

        private async Task tipoVinculoComValorDuplicado(Susep.SISRH.Domain.AggregatesModel.TipoVinculoAggregate.TipoVinculo tipovinculo, Boolean mesmo_nome)
        {
            if (tipovinculo != null && !mesmo_nome)
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
