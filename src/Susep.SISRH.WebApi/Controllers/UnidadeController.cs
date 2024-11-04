using Microsoft.AspNetCore.Mvc;
using Susep.SISRH.Application.Queries.Abstractions;
using Susep.SISRH.Application.Requests;
using Susep.SISRH.Application.ViewModels;
using SUSEP.Framework.Result.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Susep.SISRH.Application.Commands.Unidade;
using MediatR;


namespace Susep.SISRH.WebApi.Controllers
{
    /// <summary>
    /// Operações com as unidades organizacionais
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    public class UnidadeController : ControllerBase
    {
        private readonly IUnidadeQuery UnidadeQuery;
        private readonly IDominioQuery DominioQuery;
        private readonly IEstruturaOrganizacionalQuery EstruturaOrganizacionalQuery;
        private IMediator Mediator { get; }

        /// <summary>
        /// Contrutor da classe
        /// </summary>
        /// <param name="unidadeQuery"></param>
        /// <param name="dominioQuery"></param>
        /// <param name="estruturaOrganizacionalQuery"></param>     
        /// <param name="mediator"></param>     
        public UnidadeController(IUnidadeQuery unidadeQuery, IDominioQuery dominioQuery, IEstruturaOrganizacionalQuery estruturaOrganizacionalQuery, IMediator mediator)
        {
            UnidadeQuery = unidadeQuery;
            DominioQuery = dominioQuery;
            EstruturaOrganizacionalQuery = estruturaOrganizacionalQuery;
            Mediator = mediator;
        }

        /// <summary>
        /// Obtém todas as unidades ativas para preenchimento de combos
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("todasativas"), Produces("application/json", Type = typeof(IApplicationResult<IEnumerable<DadosComboViewModel>>))]
        public async Task<IActionResult> GetTodasAtivasDadosCombo()
            => await UnidadeQuery.ObterAtivasDadosComboAsync();

        /// <summary>
        /// Obtém todas as unidades ativas com todos os detalhes de cada unidade
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet(), Produces("application/json", Type = typeof(IApplicationResult<IEnumerable<UnidadeViewModel>>))]
        public async Task<IActionResult> GetAll([FromQuery] UnidadeFiltroRequest request)
        {
            return await UnidadeQuery.ObterAtivasTotal(request);
        }
        /// <summary>
        /// Obtém as unidades ativas para preenchimento de combos
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("ativas"), Produces("application/json", Type = typeof(IApplicationResult<IEnumerable<DadosComboViewModel>>))]
        public async Task<IActionResult> GetAtivasDadosCombo([FromRoute]UsuarioLogadoRequest request)
            => await EstruturaOrganizacionalQuery.ObterUnidadesAtivasDadosComboAsync(request);

        /// <summary>
        /// Obtém as unidades ativas para preenchimento de combos
        /// </summary>
        /// <returns></returns>
        [HttpGet("combo"), Produces("application/json", Type = typeof(IApplicationResult<IEnumerable<DadosComboViewModel>>))]
        public async Task<IActionResult> GetDadosCombo()
            => await UnidadeQuery.ObterUnidadesCombo();


        /// <summary>
        /// Obtém as unidades que possuem pelo menos um plano de trabalho cadastrado para preenchimento de combos
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("complanotrabalho"), Produces("application/json", Type = typeof(IApplicationResult<IEnumerable<DadosComboViewModel>>))]
        public async Task<IActionResult> GetComPlanoTrabalhoDadosCombo([FromRoute]UsuarioLogadoRequest request)
            => await EstruturaOrganizacionalQuery.ObterUnidadesComPlanoTrabalhoDadosComboAsync(request);

        /// <summary>
        /// Obtém as unidades que não possuem catalago cadastrado
        /// </summary>
        /// <returns></returns>
        [HttpGet("semcatalogocadastrado"), Produces("application/json", Type = typeof(IApplicationResult<IEnumerable<DadosComboViewModel>>))]
        public async Task<IActionResult> GetSemCatalogoCadastro()
            => await UnidadeQuery.ObterSemCatalogoCadastradoComboAsync();

        /// <summary>
        /// Obtém as unidades que não possuem catalago cadastrado
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("comcatalogocadastrado"), Produces("application/json", Type = typeof(IApplicationResult<IEnumerable<DadosComboViewModel>>))]
        public async Task<IActionResult> GetComCatalogoCadastro([FromRoute] UsuarioLogadoRequest request)
            => await EstruturaOrganizacionalQuery.ObterComCatalogoCadastradoComboAsync(request);


        /// <summary>
        /// Obtém os dados de uma unidade
        /// </summary>
        /// <param name="unidadeId"></param>
        /// <returns></returns>
        [HttpGet("{unidadeId}"), Produces("application/json", Type = typeof(IApplicationResult<UnidadeViewModel>))]
        public async Task<IActionResult> GetById([FromRoute]Int64 unidadeId)
            => await UnidadeQuery.ObterPorChaveAsync(unidadeId);

        /// <summary>
        /// Obtém os dados de uma unidade
        /// </summary>
        /// <param name="unidadeId"></param>
        /// <returns></returns>
        [HttpGet("db/{unidadeId}"), Produces("application/json", Type = typeof(IApplicationResult<UnidadeViewModel>))]
        public async Task<IActionResult> GetUnidadeById([FromRoute] Int64 unidadeId)
            => await UnidadeQuery.ObterUnidadePorChaveAsync(unidadeId);


        /// <summary>
        /// Obtém as modalidades de execução de uma unidade
        /// </summary>
        /// <param name="unidadeId"></param>
        /// <returns></returns>
        [HttpGet("{unidadeId}/modalidadeexecucao"), Produces("application/json", Type = typeof(IApplicationResult<DadosComboViewModel>))]
        public async Task<IActionResult> GetModalidadeExecucaoById([FromRoute]Int64 unidadeId)
            => await DominioQuery.ObterDominioPorClassificacaoAsync(Domain.Enums.ClassificacaoCatalogoDominioEnum.ModalidadeExecucao);

        /// <summary>
        /// Obtém as pessoas de uma unidade
        /// </summary>
        /// <param name="unidadeId"></param>
        /// <returns></returns>
        [HttpGet("{unidadeId}/pessoas"), Produces("application/json", Type = typeof(IApplicationResult<DadosComboViewModel>))]
        public async Task<IActionResult> GetPessoasById([FromRoute]Int64 unidadeId)
            => await UnidadeQuery.ObterPessoasDadosComboAsync(unidadeId);

        /// <summary>
        /// Cadastra uma unidade
        /// </summary>
        /// <param name="command">Dados do item de catálogo a ser cadastrado</param>
        /// <returns></returns>
        [HttpPost(), Produces("application/json", Type = typeof(IApplicationResult<Guid>))]
        public async Task<IActionResult> PostUnidade([FromBody] CadastrarUnidadeCommand command)
        {
            return await Mediator.Send(command);
        }

        /// <summary>
        /// Altera uma unidade
        /// </summary>
        /// <param name="command">Dados do item de catálogo a ser cadastrado</param>
        /// <returns></returns>
        [HttpPut(), Produces("application/json", Type = typeof(IApplicationResult<Guid>))]
        public async Task<IActionResult> PutUnidade([FromBody] AlterarUnidadeCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}