using MediatR;
using Microsoft.AspNetCore.Mvc;
using Susep.SISRH.Application.Queries.Abstractions;
using Susep.SISRH.Domain.AggregatesModel.TipoFuncaoAggregate;
using Susep.SISRH.Domain.Exceptions;
using SUSEP.Framework.Data.Abstractions.UnitOfWorks;
using SUSEP.Framework.Result.Concrete;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Susep.SISRH.Application.Commands.TipoFuncao
{
    public class CadastrarTipoFuncaoCommandHandler : IRequestHandler<CadastrarTipoFuncaoCommand, IActionResult>
    {
        private ITipoFuncaoRepository TipoFuncaoRepository { get; }
        private IPessoaQuery PessoaQuery { get; }
        private IUnitOfWork UnitOfWork { get; }

        public CadastrarTipoFuncaoCommandHandler(
            ITipoFuncaoRepository tipoFuncaoRepository,
            IPessoaQuery pessoaQuery,
            IUnitOfWork unitOfWork)
        {
            TipoFuncaoRepository = tipoFuncaoRepository;
            PessoaQuery = pessoaQuery;
            UnitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Handle(CadastrarTipoFuncaoCommand request, CancellationToken cancellationToken)
        {
            ApplicationResult<Int64> result = new ApplicationResult<Int64>(request);
            try
            {
                //Monta o objeto com os dados do tipo função
                var tipofuncao = Domain.AggregatesModel.TipoFuncaoAggregate.TipoFuncao.Criar(
                request.TipoFuncaoId,
                request.Descricao,
                request.CodigoFuncao,
                request.IndicadorChefia,
                request.Situacao);

                await tipoFuncaoComValorDuplicado(tipofuncao);

                //Adiciona o tipo no banco de dados
                await TipoFuncaoRepository.AdicionarAsync(tipofuncao);
                UnitOfWork.Commit(false);

                result.Result = tipofuncao.TipoFuncaoId;
                result.SetHttpStatusToOk("Tipo cadastrado com sucesso.");
                return result;
            }
            catch (SISRHDomainException e)
            {
                result.SetHttpStatusToBadRequest(e.Message);
                return result;
            }
        }

        private async Task tipoFuncaoComValorDuplicado(Susep.SISRH.Domain.AggregatesModel.TipoFuncaoAggregate.TipoFuncao tipofuncao)
        {
            if (tipofuncao != null)
            {
                var valorDuplicado = await PessoaQuery.TipoFuncaoValorDuplicadoAsync(tipofuncao.Descricao);
                if (valorDuplicado.Result)
                {
                    throw new SISRHDomainException("Tipo função com Descrição duplicada.");
                }
            }
        }
    }
}
