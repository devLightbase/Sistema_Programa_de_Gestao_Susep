using MediatR;
using Microsoft.AspNetCore.Mvc;
using Susep.SISRH.Application.Queries.Abstractions;
using Susep.SISRH.Domain.AggregatesModel.FeriadoAggregate;
using Susep.SISRH.Domain.Exceptions;
using SUSEP.Framework.Data.Abstractions.UnitOfWorks;
using SUSEP.Framework.Result.Concrete;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Susep.SISRH.Application.Commands.Feriado
{
    public class CadastrarFeriadoCommandHandler : IRequestHandler<CadastrarFeriadoCommand, IActionResult>
    {
        private IFeriadoRepository FeriadoRepository { get; }
        private IPessoaQuery PessoaQuery { get; }
        private IUnitOfWork UnitOfWork { get; }

        public CadastrarFeriadoCommandHandler(
            IFeriadoRepository feriadoRepository,
            IPessoaQuery pessoaQuery,
            IUnitOfWork unitOfWork)
        {
            FeriadoRepository = feriadoRepository;
            PessoaQuery = pessoaQuery;
            UnitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Handle(CadastrarFeriadoCommand request, CancellationToken cancellationToken)
        {
            ApplicationResult<Int64> result = new ApplicationResult<Int64>(request);

            //Monta o objeto com os dados do tipo vinculo
            var feriado = Domain.AggregatesModel.FeriadoAggregate.Feriado.Criar(
                request.FeriadoId,
                request.Data.Date,
                request.Fixo,
                request.Descricao,
                request.UfId,
                request.Situacao);
            
            try
            {
                await feriadoComValorDuplicado(feriado);
            }
            catch (SISRHDomainException e)
            {
                result.SetHttpStatusToBadRequest(e.Message);
                return result;
            }

            //Adiciona o tipo no banco de dados
            await FeriadoRepository.AdicionarAsync(feriado);
            UnitOfWork.Commit(false);

            result.Result = feriado.FeriadoId;
            result.SetHttpStatusToOk("Feriado cadastrado com sucesso.");
            return result;
        }
        private async Task feriadoComValorDuplicado(Susep.SISRH.Domain.AggregatesModel.FeriadoAggregate.Feriado feriado)
        {
            if (feriado != null)
            {
                var descricaoDuplicado = await PessoaQuery.DescricaoFeriadoDuplicadoAsync(feriado.Descricao);
                if (descricaoDuplicado.Result)
                {
                    throw new SISRHDomainException("Feriado com Descrição duplicada.");
                }

                var dataDuplicado = await PessoaQuery.DataFeriadoDuplicadoAsync(feriado.Data);
                if (dataDuplicado.Result)
                {
                    throw new SISRHDomainException("Feriado com Data duplicada.");
                }
            }
        }
    }
}
