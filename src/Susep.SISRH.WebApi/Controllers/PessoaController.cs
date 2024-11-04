using MediatR;
using Microsoft.AspNetCore.Mvc;
using Susep.SISRH.Application.Queries.Abstractions;
using Susep.SISRH.Application.Queries.Concrete;
using Susep.SISRH.Application.Requests;
using Susep.SISRH.Application.ViewModels;
using Susep.SISRH.Domain.Enums;
using SUSEP.Framework.Result.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Susep.SISRH.Application.Commands.Pessoa;
using Susep.SISRH.Application.Commands.Situacao;
using Susep.SISRH.Application.Commands.TipoFuncao;
using Susep.SISRH.Application.Commands.TipoVinculo;
using Susep.SISRH.Application.Commands.Feriado;


namespace Susep.SISRH.WebApi.Controllers
{
    /// <summary>
    /// Operações com os planos de trabalho
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    public class PessoaController : ControllerBase
    {
        private IMediator Mediator { get; }
        private IMediator Mediator2 { get; }
        private readonly IPessoaQuery PessoaQuery;
        private readonly IEstruturaOrganizacionalQuery EstruturaOrganizacionalQuery;

        /// <summary>
        /// Contrutor da classe
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="mediator2"></param>
        /// <param name="pessoaQuery"></param>    
        /// <param name="estruturaOrganizacionalQuery"></param>     
        public PessoaController(
            IMediator mediator, 
            IMediator mediator2,
            IPessoaQuery pessoaQuery, 
            IEstruturaOrganizacionalQuery estruturaOrganizacionalQuery)
        {
            Mediator = mediator;
            Mediator2 = mediator2;
            PessoaQuery = pessoaQuery;
            EstruturaOrganizacionalQuery = estruturaOrganizacionalQuery;
        }


        /// <summary>
        /// Obtém o dashboad de acordo com a pessoa
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("dashboard"), Produces("application/json", Type = typeof(IApplicationResult<DashboardViewModel>))]
        public async Task<IActionResult> GetDashboard([FromQuery] UsuarioLogadoRequest request)
            => await PessoaQuery.ObterDashboardAsync(request);

        /// <summary>
        /// Obtém as pessoas cadastradas
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet(), Produces("application/json", Type = typeof(IApplicationResult<IEnumerable<PessoaViewModel>>))]
        public async Task<IActionResult> GetAll([FromQuery] PessoaFiltroRequest request)
            => await PessoaQuery.ObterPorFiltroAsync(request);


        /// <summary>
        /// Obtém o perfil do usuário logado pessoas cadastradas
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("perfil"), Produces("application/json", Type = typeof(IApplicationResult<IEnumerable<PessoaViewModel>>))]
        public async Task<IActionResult> GetPerfilAll([FromQuery] UsuarioLogadoRequest request)
            => await EstruturaOrganizacionalQuery.ObterPerfilPessoaAsync(request);


        /// <summary>
        /// Obtém uma pessoa por ID
        /// </summary>
        /// <param name="pessoaId"></param>
        /// <returns></returns>
        [HttpGet("{pessoaId}"), Produces("application/json", Type = typeof(IApplicationResult<PessoaViewModel>))]
        public async Task<IActionResult> GetByPessoaId([FromRoute] long pessoaId)
            => await PessoaQuery.ObterDetalhesPorChaveAsync(pessoaId);

        /// <summary>
        /// Obtém uma pessoa por ID
        /// </summary>
        /// <param name="pessoaId"></param>
        /// <returns></returns>
        [HttpGet("duplicada/{cpf}"), Produces("application/json", Type = typeof(IApplicationResult<PessoaViewModel>))]
        public async Task<IActionResult> GetPessoaDuplicada([FromRoute] String cpf)
            => await PessoaQuery.ValorDuplicadoAsync(0, cpf);

        /// <summary>
        /// Obtém os feriados por pessoa e período
        /// </summary>
        /// <param name="pessoaId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("{pessoaId}/feriados"), Produces("application/json", Type = typeof(IApplicationResult<IEnumerable<DateTime>>))]
        public async Task<IActionResult> GetFeriados([FromRoute] Int64 pessoaId, [FromQuery] FeriadoBuscaViewModel request)
            => await PessoaQuery.ObterDiasNaoUteisAsync(pessoaId, request.DataInicio, request.DataFim);

        /// <summary>
        /// Obtém as pessoas que possuem pelo menos um pacto de trabalho cadastrado para preenchimento de combos
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("compactotrabalho"), Produces("application/json", Type = typeof(IApplicationResult<IEnumerable<DadosComboViewModel>>))]
        public async Task<IActionResult> GetComPlanoTrabalhoDadosCombo([FromRoute]UsuarioLogadoRequest request)
            => await EstruturaOrganizacionalQuery.ObterPessoasComPactoTrabalhoDadosComboAsync(request);

        /// <summary>
        /// Obtém as pessoas que possuem pelo menos um pacto de trabalho cadastrado para preenchimento de combos
        /// <param name="request"></param>
        /// </summary>
        /// <returns></returns>
        [HttpGet("combo"), Produces("application/json", Type = typeof(IApplicationResult<IEnumerable<DadosComboViewModel>>))]
        public async Task<IActionResult> GetComboUnidades([FromRoute] UsuarioLogadoRequest request)
            => await EstruturaOrganizacionalQuery.ObterPessoasComboAsync(request);

        /// <summary>
        /// Obtém as pessoas que possuem pelo menos um pacto de trabalho cadastrado para preenchimento de combos
        /// <param name="request"></param>
        /// </summary>
        /// <returns></returns>
        [HttpGet("combo/unidade"), Produces("application/json", Type = typeof(IApplicationResult<IEnumerable<DadosComboViewModel>>))]
        public async Task<IActionResult> GetComboPessoasUnidades([FromRoute] UsuarioLogadoRequest request)
            => await EstruturaOrganizacionalQuery.ObterPessoasComboUnidadesAsync();

        /// <summary>
        /// Obtém as uf para preenchimento de combos
        /// <param name="request"></param>
        /// </summary>
        /// <returns></returns>
        [HttpGet("combo/uf"), Produces("application/json", Type = typeof(IApplicationResult<IEnumerable<DadosComboViewModel>>))]
        public async Task<IActionResult> GetComboUf([FromRoute] UsuarioLogadoRequest request)
            => await EstruturaOrganizacionalQuery.ObterUfComboAsync();

        /// <summary>
        /// Obtém Funções para preenchimento de combos
        /// <param name="request"></param>
        /// </summary>
        /// <returns></returns>
        [HttpGet("combo/funcao"), Produces("application/json", Type = typeof(IApplicationResult<IEnumerable<DadosComboViewModel>>))]
        public async Task<IActionResult> GetComboFuncoes([FromRoute] UsuarioLogadoRequest request)
            => await EstruturaOrganizacionalQuery.ObterComboFuncoesAsync();

        /// <summary>
        /// Obtém Vínculos para preenchimento de combos
        /// <param name="request"></param>
        /// </summary>
        /// <returns></returns>
        [HttpGet("combo/vinculos"), Produces("application/json", Type = typeof(IApplicationResult<IEnumerable<DadosComboViewModel>>))]
        public async Task<IActionResult> GetComboVinculos([FromRoute] UsuarioLogadoRequest request)
            => await EstruturaOrganizacionalQuery.ObterComboVinculosAsync();

        /// <summary>
        /// Obtém Vínculos para preenchimento de combos
        /// <param name="request"></param>
        /// </summary>
        /// <returns></returns>
        [HttpGet("combo/situacao"), Produces("application/json", Type = typeof(IApplicationResult<IEnumerable<DadosComboViewModel>>))]
        public async Task<IActionResult> GetComboSituacoes([FromRoute] UsuarioLogadoRequest request)
            => await EstruturaOrganizacionalQuery.ObterComboSituacoesAsync();

        /// <summary>
        /// Cadastra uma pessoa
        /// </summary>
        /// <param name="command">Dados do item de catálogo a ser cadastrado</param>
        /// <returns></returns>
        [HttpPost(), Produces("application/json", Type = typeof(IApplicationResult<Guid>))]
        public async Task<IActionResult> PostPessoa([FromBody] CadastrarPessoaCommand command)
        {
            return await Mediator.Send(command);
        }

        /// <summary>
        /// Altera uma pessoa
        /// </summary>
        /// <param name="command">Dados do item de catálogo a ser cadastrado</param>
        /// <returns></returns>
        [HttpPut(), Produces("application/json", Type = typeof(IApplicationResult<Guid>))]
        public async Task<IActionResult> PutPessoa([FromBody] AlterarPessoaCommand command)
        {
            return await Mediator.Send(command);
        }

        //------------------------------------ Situação Pesssoa

        /// <summary>
        /// Obtém as situacoes cadastrados
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("situacao"), Produces("application/json", Type = typeof(IApplicationResult<IEnumerable<SituacoesViewModel>>))]
        public async Task<IActionResult> GetAllSituacoes([FromQuery] SituacoesFiltroRequest request)
        {
            return await PessoaQuery.ObterPaginaSituacoesAsync(request);
        }

        /// <summary>
        /// Obtém a situacao por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("situacao/{id}"), Produces("application/json", Type = typeof(IApplicationResult<SituacoesViewModel>))]
        public async Task<IActionResult> GetSituacaoId([FromRoute] Int64 id)
        {
            return await PessoaQuery.ObterSituacaoPorIdAsync(id);
        }

        /// <summary>
        /// Cadastra uma situação
        /// </summary>
        /// <param name="command">Dados do item de catálogo a ser cadastrado</param>
        /// <returns></returns>
        [HttpPost("situacao"), Produces("application/json", Type = typeof(IApplicationResult<Int64>))]
        public async Task<IActionResult> PostSituacao([FromBody] CadastrarSituacaoCommand command)
        {
            return await Mediator.Send(command);
        }

        /// <summary>
        /// Altera uma situacao
        /// </summary>
        /// <param name="command">Dados do item de catálogo a ser cadastrado</param>
        /// <returns></returns>
        [HttpPut("situacao"), Produces("application/json", Type = typeof(IApplicationResult<Int64>))]
        public async Task<IActionResult> PutSituacao([FromBody] AlterarSituacaoCommand command)
        {
            return await Mediator.Send(command);
        }

        /// <summary>
        /// Exclui uma situacao
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpDelete("situacao/{SituacaoPessoaId}/"), Produces("application/json", Type = typeof(IApplicationResult<Int64>))]
        public async Task<IActionResult> DeleteSituacao([FromRoute] ExcluirSituacaoCommand command)
            => await Mediator.Send(command);

        //------------------------------------ Tipo Função

        /// <summary>
        /// Obtém os tipos de função cadastrados
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("tipo-funcao"), Produces("application/json", Type = typeof(IApplicationResult<IEnumerable<TipoFuncaoViewModel>>))]
        public async Task<IActionResult> GetAllTipoFuncao([FromQuery] TipoFuncaoFiltroRequest request)
        {
            return await PessoaQuery.ObterPaginaTipoFuncaoAsync(request);
        }

        /// <summary>
        /// Obtém os tipos de função por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("tipo-funcao/{id}"), Produces("application/json", Type = typeof(IApplicationResult<TipoFuncaoViewModel>))]
        public async Task<IActionResult> GetTipoFuncaoId([FromRoute] Int64 id)
        {
            return await PessoaQuery.ObterTipoFuncaoPorIdAsync(id);
        }

        /// <summary>
        /// Cadastra um tipo de função
        /// </summary>
        /// <param name="command">Dados do item de catálogo a ser cadastrado</param>
        /// <returns></returns>
        [HttpPost("tipo-funcao"), Produces("application/json", Type = typeof(IApplicationResult<Int64>))]
        public async Task<IActionResult> PostTipoFuncao([FromBody] CadastrarTipoFuncaoCommand command)
        {
            return await Mediator.Send(command);
        }

        /// <summary>
        /// Altera um tipo de função
        /// </summary>
        /// <param name="command">Dados do item de catálogo a ser cadastrado</param>
        /// <returns></returns>
        [HttpPut("tipo-funcao"), Produces("application/json", Type = typeof(IApplicationResult<Int64>))]
        public async Task<IActionResult> PutTipoFuncao([FromBody] AlterarTipoFuncaoCommand command)
        {
            return await Mediator.Send(command);
        }
        
        /// <summary>
        /// Exclui um tipo de função
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpDelete("tipo-funcao/{TipoFuncaoId}/"), Produces("application/json", Type = typeof(IApplicationResult<Int64>))]
        public async Task<IActionResult> DeleteTipoFuncao([FromRoute] ExcluirTipoFuncaoCommand command)
            => await Mediator.Send(command);

        //------------------------------------ Tipo Vinculo

        /// <summary>
        /// Obtém os tipos de vinculo cadastrados
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("tipo-vinculo"), Produces("application/json", Type = typeof(IApplicationResult<IEnumerable<TipoVinculoViewModel>>))]
        public async Task<IActionResult> GetAllTipoVinculo([FromQuery] TipoVinculoFiltroRequest request)
        {
            return await PessoaQuery.ObterPaginaTipoVinculoAsync(request);
        }

        /// <summary>
        /// Obtém os tipos de vinculo por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("tipo-vinculo/{id}"), Produces("application/json", Type = typeof(IApplicationResult<TipoVinculoViewModel>))]
        public async Task<IActionResult> GetTipoVinculoId([FromRoute] Int64 id)
        {
            return await PessoaQuery.ObterTipoVinculoPorIdAsync(id);
        }

        /// <summary>
        /// Cadastra um tipo de vinculo
        /// </summary>
        /// <param name="command">Dados do item de catálogo a ser cadastrado</param>
        /// <returns></returns>
        [HttpPost("tipo-vinculo"), Produces("application/json", Type = typeof(IApplicationResult<Int64>))]
        public async Task<IActionResult> PostTipoVinculo([FromBody] CadastrarTipoVinculoCommand command)
        {
            return await Mediator.Send(command);
        }

        /// <summary>
        /// Altera um tipo de vinculo
        /// </summary>
        /// <param name="command">Dados do item de catálogo a ser cadastrado</param>
        /// <returns></returns>
        [HttpPut("tipo-vinculo"), Produces("application/json", Type = typeof(IApplicationResult<Int64>))]
        public async Task<IActionResult> PutTipoVinculo([FromBody] AlterarTipoVinculoCommand command)
        {
            return await Mediator.Send(command);
        }

        /// <summary>
        /// Exclui um tipo de função
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpDelete("tipo-vinculo/{TipoVinculoId}/"), Produces("application/json", Type = typeof(IApplicationResult<Int64>))]
        public async Task<IActionResult> DeleteTipoVinculo([FromRoute] ExcluirTipoVinculoCommand command)
            => await Mediator.Send(command);

        //------------------------------------ Feriados

        /// <summary>
        /// Obtém os feriados cadastrados
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("feriados"), Produces("application/json", Type = typeof(IApplicationResult<IEnumerable<FeriadoViewModel>>))]
        public async Task<IActionResult> GetAllFeriado([FromQuery] FeriadoFiltroRequest request)
        {
            return await PessoaQuery.ObterPaginaFeriadoAsync(request);
        }

        /// <summary>
        /// Obtém os feriados por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("feriados/{id}"), Produces("application/json", Type = typeof(IApplicationResult<FeriadoViewModel>))]
        public async Task<IActionResult> GetFeriadoId([FromRoute] Int64 id)
        {
            return await PessoaQuery.ObterFeriadoPorIdAsync(id);
        }

        /// <summary>
        /// Cadastra um feriado
        /// </summary>
        /// <param name="command">Dados do item de catálogo a ser cadastrado</param>
        /// <returns></returns>
        [HttpPost("feriados"), Produces("application/json", Type = typeof(IApplicationResult<Int64>))]
        public async Task<IActionResult> PostFeriado([FromBody] CadastrarFeriadoCommand command)
        {
            return await Mediator.Send(command);
        }

        /// <summary>
        /// Altera um feriado
        /// </summary>
        /// <param name="command">Dados do item de catálogo a ser cadastrado</param>
        /// <returns></returns>
        [HttpPut("feriados"), Produces("application/json", Type = typeof(IApplicationResult<Int64>))]
        public async Task<IActionResult> PutFeriado([FromBody] AlterarFeriadoCommand command)
        {
            return await Mediator.Send(command);
        }

        /// <summary>
        /// Exclui um feriado
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpDelete("feriados/{FeriadoId}/"), Produces("application/json", Type = typeof(IApplicationResult<Int64>))]
        public async Task<IActionResult> DeleteFeriado([FromRoute] ExcluirFeriadoCommand command)
            => await Mediator.Send(command);
    }
}