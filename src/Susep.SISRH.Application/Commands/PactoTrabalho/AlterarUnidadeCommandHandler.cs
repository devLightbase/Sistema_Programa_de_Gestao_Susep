using MediatR;
using Microsoft.AspNetCore.Mvc;
using Susep.SISRH.Domain.AggregatesModel.UnidadesAggregate;
using SUSEP.Framework.Data.Abstractions.UnitOfWorks;
using SUSEP.Framework.Result.Concrete;
using Susep.SISRH.Application.Queries.Abstractions;
using System;
using System.Threading;
using System.Threading.Tasks;
using Susep.SISRH.Domain.Exceptions;
using System.Linq;

namespace Susep.SISRH.Application.Commands.Unidade
{
    public class AlterarUnidadeCommandHandler : IRequestHandler<AlterarUnidadeCommand, IActionResult>
    {
        private IUnidadesRepository UnidadesRepository { get; }
        private IUnidadeQuery UnidadeQuery { get; }
        private IUnitOfWork UnitOfWork { get; }

        public AlterarUnidadeCommandHandler(
            IUnidadesRepository unidadesRepository,
            IUnidadeQuery unidadeQuery,
            IUnitOfWork unitOfWork)
        {
            UnidadesRepository = unidadesRepository;
            UnidadeQuery = unidadeQuery;
            UnitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Handle(AlterarUnidadeCommand request, CancellationToken cancellationToken)
        {
            ApplicationResult<Int64> result = new ApplicationResult<Int64>(request);

            //Recupera o objeto assunto
            var unidade = await UnidadesRepository.ObterAsync(request.Unidadeid);

            //Monta o objeto com os dados do assunto
            unidade.Alterar(
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
                request.CodSgc);

            try
            {
                await validarUnidade(unidade);
            }
            catch (SISRHDomainException e)
            {
                result.SetHttpStatusToBadRequest(e.Message);
                return result;
            }

            //Altera o assunto no banco de dados
            UnidadesRepository.Atualizar(unidade);
            UnitOfWork.Commit(false);

            result.Result = unidade.Unidadeid;
            result.SetHttpStatusToOk("Unidade alterada com sucesso.");
            return result;
        }

        private async Task validarUnidade(Susep.SISRH.Domain.AggregatesModel.UnidadesAggregate.UnidadeDB unidade)
        {
            await assuntoComValorDuplicado(unidade);
        }

        private async Task assuntoComValorDuplicado(Susep.SISRH.Domain.AggregatesModel.UnidadesAggregate.UnidadeDB unidade)
        {
            if (unidade != null)
            {
                var valorDuplicado = await UnidadeQuery.ValorDuplicadoAsync(unidade.Sigla, unidade.Nome, unidade.Unidadeid);
                if (valorDuplicado.Result)
                {
                    throw new SISRHDomainException("Unidade com valor duplicado.");
                }
            }
        }

    }
}
