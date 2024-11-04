using MediatR;
using Microsoft.AspNetCore.Mvc;
using Susep.SISRH.Domain.AggregatesModel.FeriadoAggregate;
using SUSEP.Framework.Data.Abstractions.UnitOfWorks;
using SUSEP.Framework.Result.Concrete;
using Susep.SISRH.Application.Queries.Abstractions;
using System;
using System.Threading;
using System.Threading.Tasks;
using Susep.SISRH.Domain.Exceptions;
using System.Linq;

namespace Susep.SISRH.Application.Commands.Feriado
{
    public class AlterarFeriadoCommandHandler : IRequestHandler<AlterarFeriadoCommand, IActionResult>
    {
        private IFeriadoRepository FeriadoRepository { get; }
        private IPessoaQuery PessoaQuery { get; }
        private IUnitOfWork UnitOfWork { get; }

        public AlterarFeriadoCommandHandler(
            IFeriadoRepository feriadoRepository,
            IPessoaQuery pessoaQuery,
            IUnitOfWork unitOfWork)
        {
            FeriadoRepository = feriadoRepository;
            PessoaQuery = pessoaQuery;
            UnitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Handle(AlterarFeriadoCommand request, CancellationToken cancellationToken)
        {
            ApplicationResult<Int64> result = new ApplicationResult<Int64>(request);
            try
            {
                //Recupera o objeto feriado
                var feriado = await FeriadoRepository.ObterAsync(request.FeriadoId);
                var mesma_descricao = false;
                var mesma_data = false;
                
                if(feriado.Descricao == request.Descricao)
                {
                    mesma_descricao = true;
                }
                if(feriado.Data == request.Data)
                {
                    mesma_data = true;
                }

                //Monta o objeto com os dados do feriado
                feriado.Alterar(
                    request.FeriadoId,
                    request.Data.Date,
                    request.Fixo,
                    request.Descricao,
                    request.UfId,
                    request.Situacao);

                await feriadoComValorDuplicado(feriado, mesma_descricao, mesma_data);

                //Altera o feriado no banco de dados
                FeriadoRepository.Atualizar(feriado);
                UnitOfWork.Commit(false);

                result.Result = feriado.FeriadoId;
                result.SetHttpStatusToOk("Feriado alterado com sucesso.");
                return result;
            }

            catch (SISRHDomainException e)
            {
                result.SetHttpStatusToBadRequest(e.Message);
                return result;
            }
        }

        private async Task feriadoComValorDuplicado(Susep.SISRH.Domain.AggregatesModel.FeriadoAggregate.Feriado feriado, Boolean mesma_descricao, Boolean mesma_data)
        {
            if (feriado != null)
            {
                if (!mesma_descricao)
                {
                    var descricaoDuplicado = await PessoaQuery.DescricaoFeriadoDuplicadoAsync(feriado.Descricao);
                    if (descricaoDuplicado.Result)
                    {
                        throw new SISRHDomainException("Feriado com Descrição duplicada.");
                    }
                }

                if (!mesma_data)
                {
                    var dataDuplicado = await PessoaQuery.DataFeriadoDuplicadoAsync(feriado.Data);
                    if (dataDuplicado.Result)
                    {
                        throw new SISRHDomainException("Feriado com Data duplicada.");
                    }
                }                
            }
        }
    }
}
