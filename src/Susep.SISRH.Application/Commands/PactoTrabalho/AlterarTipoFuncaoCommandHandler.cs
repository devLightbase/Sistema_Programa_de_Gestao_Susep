using MediatR;
using Microsoft.AspNetCore.Mvc;
using Susep.SISRH.Domain.AggregatesModel.TipoFuncaoAggregate;
using SUSEP.Framework.Data.Abstractions.UnitOfWorks;
using SUSEP.Framework.Result.Concrete;
using Susep.SISRH.Application.Queries.Abstractions;
using System;
using System.Threading;
using System.Threading.Tasks;
using Susep.SISRH.Domain.Exceptions;
using System.Linq;

namespace Susep.SISRH.Application.Commands.TipoFuncao
{
    public class AlterarTipoFuncaoCommandHandler : IRequestHandler<AlterarTipoFuncaoCommand, IActionResult>
    {
        private ITipoFuncaoRepository TipoFuncaoRepository { get; }
        private IPessoaQuery PessoaQuery { get; }
        private IUnitOfWork UnitOfWork { get; }

        public AlterarTipoFuncaoCommandHandler(
            ITipoFuncaoRepository tipoFuncaoRepository,
            IPessoaQuery pessoaQuery,
            IUnitOfWork unitOfWork)
        {
            TipoFuncaoRepository = tipoFuncaoRepository;
            PessoaQuery = pessoaQuery;
            UnitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Handle(AlterarTipoFuncaoCommand request, CancellationToken cancellationToken)
        {
            ApplicationResult<Int64> result = new ApplicationResult<Int64>(request);
            try
            {
                //Recupera o objeto tipo função
                var tipofuncao = await TipoFuncaoRepository.ObterAsync(request.TipoFuncaoId);

                var mesmo_nome = false;

                if (request.Descricao == tipofuncao.Descricao)
                {
                    mesmo_nome = true;
                }

                //Monta o objeto com os dados do tipo
                tipofuncao.Alterar(
                    request.TipoFuncaoId,
                    request.Descricao,
                    request.CodigoFuncao,
                    request.IndicadorChefia,
                    request.Situacao);

                await tipoFuncaoComValorDuplicado(tipofuncao, mesmo_nome);

                //Altera a situacao no banco de dados
                TipoFuncaoRepository.Atualizar(tipofuncao);
                UnitOfWork.Commit(false);

                result.Result = tipofuncao.TipoFuncaoId;
                result.SetHttpStatusToOk("Tipo alterado com sucesso.");
                return result;
            }
            catch (SISRHDomainException e)
            {
                result.SetHttpStatusToBadRequest(e.Message);
                return result;
            }
        }

        private async Task tipoFuncaoComValorDuplicado(Susep.SISRH.Domain.AggregatesModel.TipoFuncaoAggregate.TipoFuncao tipofuncao, Boolean mesmo_nome)
        {
            if (tipofuncao != null && !mesmo_nome)
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
