using MediatR;
using Microsoft.AspNetCore.Mvc;
using Susep.SISRH.Application.Queries.Abstractions;
using Susep.SISRH.Domain.AggregatesModel.UnidadesAggregate;
using Susep.SISRH.Domain.Exceptions;
using SUSEP.Framework.Data.Abstractions.UnitOfWorks;
using SUSEP.Framework.Result.Concrete;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Susep.SISRH.Application.Commands.Unidade
{
    public class CadastrarUnidadeCommandHandler : IRequestHandler<CadastrarUnidadeCommand, IActionResult>
    {

        private IUnidadesRepository UnidadesRepository { get; }
        private IUnidadeQuery UnidadeQuery { get; }
        private IUnitOfWork UnitOfWork { get; }

        public CadastrarUnidadeCommandHandler(
            IUnidadesRepository unidadesRepository,
            IUnidadeQuery unidadeQuery,
            IUnitOfWork unitOfWork)
        {
            UnidadesRepository = unidadesRepository;
            UnidadeQuery = unidadeQuery;
            UnitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Handle(CadastrarUnidadeCommand request, CancellationToken cancellationToken)
        {
            ApplicationResult<Int64> result = new ApplicationResult<Int64>(request);

            var unidade = Domain.AggregatesModel.UnidadesAggregate.UnidadeDB.Criar(
                request.Unidadeid,
                request.Nome,
                request.Sigla,
                request.Nivel,
                request.UnidadeIdPai,
                request.Email,
                request.UfId,
                request.PessoaIdChefe,
                request.PessoaIdChefeSubstituto,
                request.TipoFuncaoUnidadeId,
                request.SituacaoUnidadeId,
                request.TipoUnidadeId,
                request.CodSiorg,
                request.CodSgc
                );

            try
            {
                await validarUnidade(unidade);
            }
            catch (SISRHDomainException e)
            {
                result.SetHttpStatusToBadRequest(e.Message);
                return result;
            }

            //Adiciona o unidade no banco de dados
            await UnidadesRepository.AdicionarAsync(unidade);
            UnitOfWork.Commit(false);

            result.Result = unidade.Unidadeid;
            result.SetHttpStatusToOk("Unidade cadastrada com sucesso.");
            return result;
        }

        private async Task validarUnidade(Susep.SISRH.Domain.AggregatesModel.UnidadesAggregate.UnidadeDB unidade)
        {
            await unidadeComValorDuplicado(unidade);
        }

        private async Task unidadeComValorDuplicado(Susep.SISRH.Domain.AggregatesModel.UnidadesAggregate.UnidadeDB unidade)
        {
            if (unidade != null)
            {
                var valorDuplicado = await UnidadeQuery.ValorDuplicadoAsync(unidade.Sigla, unidade.Nome, unidade.Unidadeid);
                if (valorDuplicado.Result)
                {
                    throw new SISRHDomainException("Unidade com Nome ou Sigla duplicado.");
                }
            }
        }

    }
}
