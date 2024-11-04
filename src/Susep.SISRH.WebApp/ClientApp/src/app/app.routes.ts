import { ModuleWithProviders } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";

import { AuthGuard } from "./shared/helpers/authorization.guard.helper";

import { HomeComponent } from "./shared/components/home/home.component";
import { LoginComponent } from "./shared/components/login/login.component";

import { CatalogoPesquisaComponent } from "./modules/programa-gestao/components/catalogo/catalogo-pesquisa.component";
import { CatalogoCadastroComponent } from "./modules/programa-gestao/components/catalogo/cadastro/catalogo-cadastro.component";
import { CatalogoEdicaoComponent } from "./modules/programa-gestao/components/catalogo/edicao/catalogo-edicao.component";
import { ItemCatalogoCadastroComponent } from "./modules/programa-gestao/components/item-catalogo/cadastro/item-catalogo-cadastro.component";
import { ItemCatalogoPesquisaComponent } from "./modules/programa-gestao/components/item-catalogo/item-catalogo-pesquisa.component";
import { ItemCatalogoDetalhesComponent } from "./modules/programa-gestao/components/item-catalogo/detalhes/item-catalogo-detalhes.component";
import { PlanoTrabalhoPesquisaComponent } from "./modules/programa-gestao/components/plano-trabalho/plano-trabalho-pesquisa.component";
import { PlanoTrabalhoCadastroComponent } from "./modules/programa-gestao/components/plano-trabalho/cadastro/plano-trabalho-cadastro.component";
import { PlanoTrabalhoDetalhesComponent } from "./modules/programa-gestao/components/plano-trabalho/detalhes/plano-trabalho-detalhes.component";
import { PactoTrabalhoPesquisaComponent } from "./modules/programa-gestao/components/pacto-trabalho/pacto-trabalho-pesquisa.component";
import { PactoTrabalhoCadastroComponent } from "./modules/programa-gestao/components/pacto-trabalho/cadastro/pacto-trabalho-cadastro.component";
import { PactoTrabalhoDetalhesComponent } from "./modules/programa-gestao/components/pacto-trabalho/detalhes/pacto-trabalho-detalhes.component";
import { AtividadesPactoAtualComponent } from "./modules/programa-gestao/components/atividades-servidor/pacto-atual/atividades-pacto-atual.component";
import { AtividadesServidorHistoricoComponent } from "./modules/programa-gestao/components/atividades-servidor/historico/atividades-servidor-historico.component";
import { PessoaPesquisaComponent } from "./modules/pessoa/components/pessoa-pesquisa.component";
import { PessoaEdicaoComponent } from "./modules/pessoa/components/edicao/pessoa-edicao.component";
import { UnidadePesquisaComponent } from "./modules/unidade/components/unidade-pesquisa.component";
import { UnidadeEdicaoComponent } from "./modules/unidade/components/edicao/unidade-edicao.component";
import { PerfilEnum } from "./modules/programa-gestao/enums/perfil.enum";
import { PlanoHabilitacaoComponent } from "./modules/programa-gestao/components/atividades-servidor/plano-habilitacao/plano-habilitacao.component";
import { DashboardComponent } from "./modules/dashboard/components/dashboard.component";
import { AssuntoPesquisaComponent } from "./modules/assunto/components/assunto-pesquisa.component";
import { AssuntoEdicaoComponent } from "./modules/assunto/components/edicao/assunto-edicao.component";
import { ModoExibicaoGuard } from "./shared/helpers/modo-exibicao.guard.helper";
import { ObjetoPesquisaComponent } from "./modules/objeto/components/objeto-pesquisa.component";
import { ObjetoEdicaoComponent } from "./modules/objeto/components/edicao/objeto-edicao.component";
import { AgendamentoPresencialComponent } from "./modules/programa-gestao/components/agendamento-presencial/agendamento-presencial.component";
import { ManutencaoUnidadeComponent } from "./modules/manutencao/components/manutencao-unidade/manutencao-unidade.component";
import { ManutencaoUnidadeEdicaoComponent } from "./modules/manutencao/components/manutencao-unidade/edicao/manutencao-unidade-edicao.component";
import { ManutencaoUnidadesDetalhesComponent } from "./modules/manutencao/components/manutencao-unidade/detalhar/manutencao-unidades-detalhes.component";
import { ManutencaoPessoaPesquisaComponent } from "./modules/manutencao/components/manutencao-pessoa/manutencao-pessoa-pesquisa/manutencao-pessoa-pesquisa.component";
import { ManutencaoPessoaEdicaoComponent } from "./modules/manutencao/components/manutencao-pessoa/manutencao-pessoa-edicao/manutencao-pessoa-edicao.component";
import { ManutencaoPessoaDetalhesComponent } from "./modules/manutencao/components/manutencao-pessoa/manutencao-pessoa-detalhes/manutencao-pessoa-detalhes.component";
import { SituacaoPessoaComponent } from "./modules/dados-auxiliares/situacao-pessoa/situacao-pessoa.component";
import { SituacaoPessoaEdicaoComponent } from "./modules/dados-auxiliares/situacao-pessoa/edicao/situacao-pessoa-edicao.component";
import { TipoFuncaoComponent } from "./modules/dados-auxiliares/tipo-funcao/tipo-funcao.component";
import { TipoFuncaoEdicaoComponent } from "./modules/dados-auxiliares/tipo-funcao/edicao/tipo-funcao-edicao.component";
import { TipoVinculoComponent } from "./modules/dados-auxiliares/tipo-vinculo/tipo-vinculo.component";
import { TipoVinculoEdicaoComponent } from "./modules/dados-auxiliares/tipo-vinculo/edicao/tipo-vinculo-edicao.component";
import { FeriadosComponent } from "./modules/dados-auxiliares/feriados/feriados.component";
import { FeriadosEdicaoComponent } from "./modules/dados-auxiliares/feriados/edicao/feriados-edicao.component";
import { CatalogoDominioComponent } from "./modules/consultas/catalogo-dominio/catalogo-dominio.component";
import { PgUnidadeComponent } from "./modules/consultas/pg-unidade/pg-unidade.component";


const routes: Routes = [
  { path: '', component: HomeComponent, data: { breadcrumb: 'Home' }, pathMatch: 'full' },
  { path: 'login', component: LoginComponent, data: { breadcrumb: 'Login' }, pathMatch: 'full' },  
  { path: 'dashboard', canActivate: [AuthGuard], component: DashboardComponent, data: { breadcrumb: 'Dashboard' }, pathMatch: 'full' },  
  {
    path: 'programagestao', canActivate: [AuthGuard], data: { breadcrumb: 'Programa de gestão' }, children: [
      { path: '', component: PlanoTrabalhoPesquisaComponent, data: { breadcrumb: 'Pesquisa' } },
      { path: 'pesquisa', component: PlanoTrabalhoPesquisaComponent, data: { breadcrumb: 'Pesquisa' } },
      { path: 'cadastro', component: PlanoTrabalhoCadastroComponent, data: { breadcrumb: 'Cadastro', roles: [PerfilEnum.Diretor, PerfilEnum.CoordenadorGeral, PerfilEnum.ChefeUnidade] } },
      { path: 'detalhar/:id', component: PlanoTrabalhoDetalhesComponent, data: { breadcrumb: 'Detalhes' } },     
      {
        path: 'catalogo', canActivate: [AuthGuard], data: { breadcrumb: 'Lista de atividade', roles: [PerfilEnum.GestorIndicadores] }, children: [      
          { path: '', component: CatalogoPesquisaComponent, data: { breadcrumb: 'Pesquisa' } },
          { path: 'pesquisa', component: CatalogoPesquisaComponent, data: { breadcrumb: 'Pesquisa' } },
          { path: 'cadastro', component: CatalogoCadastroComponent, data: { breadcrumb: 'Cadastro' } },
          { path: 'editar/:id', component: CatalogoEdicaoComponent, data: { breadcrumb: 'Editar' } },
          {
            path: 'item', canActivate: [AuthGuard], data: { breadcrumb: 'Atividade' }, children: [
              { path: '', component: ItemCatalogoPesquisaComponent, data: { breadcrumb: 'Pesquisa' } },
              { path: 'pesquisa', component: ItemCatalogoPesquisaComponent, data: { breadcrumb: 'Pesquisa' } },
              { path: 'cadastro', component: ItemCatalogoCadastroComponent, data: { breadcrumb: 'Cadastro' } },
              { path: 'editar/:id', component: ItemCatalogoCadastroComponent, data: { breadcrumb: 'Editar' } },
              { path: 'copiar/:id', component: ItemCatalogoCadastroComponent, data: { breadcrumb: 'Copiar' } },
              { path: 'detalhar/:id', component: ItemCatalogoDetalhesComponent, data: { breadcrumb: 'Detalhes' } },  
              { path: 'excluir/:id', component: ItemCatalogoDetalhesComponent, data: { breadcrumb: 'Excluir' } }             
            ]
          },
        ]
      },  
      {
        path: 'pactotrabalho', canActivate: [AuthGuard], data: { breadcrumb: 'Plano de trabalho' }, children: [      
          { path: '', component: PactoTrabalhoPesquisaComponent, data: { breadcrumb: 'Pesquisa' } },
          { path: 'pesquisa', component: PactoTrabalhoPesquisaComponent, data: { breadcrumb: 'Pesquisa' } },
          { path: 'cadastro', component: PactoTrabalhoCadastroComponent, data: { breadcrumb: 'Cadastro' } },
          { path: 'detalhar/:id', component: PactoTrabalhoDetalhesComponent, data: { breadcrumb: 'Detalhes' } },  
        ]
      },
      {
        path: 'atividade', canActivate: [AuthGuard], data: { breadcrumb: 'Atividades' }, children: [      
          { path: '', component: AtividadesPactoAtualComponent, data: { breadcrumb: 'Pacto atual' } },
          { path: 'atual', component: AtividadesPactoAtualComponent, data: { breadcrumb: 'Pacto atual' } },
          { path: 'atual/:id', component: AtividadesPactoAtualComponent, data: { breadcrumb: 'Pacto atual' } },
          { path: 'habilitacao', component: PlanoHabilitacaoComponent, data: { breadcrumb: 'Habilitação' } },
          { path: 'historico', component: AtividadesServidorHistoricoComponent, data: { breadcrumb: 'Meus planos de trabalho' } },
        ]
      },      
    ],
  },  
  {
    path: 'pessoa', canActivate: [AuthGuard], data: { breadcrumb: 'Pessoas' }, children: [      
      { path: '', component: PessoaPesquisaComponent, data: { breadcrumb: 'Pesquisa' } },
      { path: 'pesquisa', component: PessoaPesquisaComponent, data: { breadcrumb: 'Pesquisa' } },
      { path: 'editar/:id', component: PessoaEdicaoComponent, data: { breadcrumb: 'Editar' } },
    ]
  },
  {
    path: 'manutencao-unidade', canActivate: [AuthGuard], data: { breadcrumb: 'Unidades' }, children: [      
      { path: '', component: ManutencaoUnidadeComponent, data: { breadcrumb: 'Pesquisa' } },
      { path: 'cadastro', component: ManutencaoUnidadeEdicaoComponent, data: { breadcrumb: 'Cadastro' } },
      { path: 'editar/:id', component: ManutencaoUnidadeEdicaoComponent, data: { breadcrumb: 'Editar' } },
      { path: 'detalhes/:id', component: ManutencaoUnidadesDetalhesComponent, data: { breadcrumb: 'Detalhes' } },
    ]
  },
  {
    path: 'manutencao-pessoa', canActivate: [AuthGuard], data: { breadcrumb: 'Pessoas' }, children: [      
      { path: '', component: ManutencaoPessoaPesquisaComponent, data: { breadcrumb: 'Pesquisa' } },
      { path: 'cadastro', component: ManutencaoPessoaEdicaoComponent, data: { breadcrumb: 'Cadastro' } },
      { path: 'editar/:id', component: ManutencaoPessoaEdicaoComponent, data: { breadcrumb: 'Editar' } },
      { path: 'detalhes/:id', component: ManutencaoPessoaDetalhesComponent, data: { breadcrumb: 'Detalhes' } }
    ]
  },
  // {
  //   path: 'unidade', canActivate: [AuthGuard], data: { breadcrumb: 'Unidades' }, children: [      
  //     { path: '', component: UnidadePesquisaComponent, data: { breadcrumb: 'Pesquisa' } },
  //     { path: 'pesquisa', component: UnidadePesquisaComponent, data: { breadcrumb: 'Pesquisa' } },
  //     //{ path: 'editar/:id', component: UnidadeEdicaoComponent, data: { breadcrumb: 'Editar' } },
  //   ]
  // },
  {
    path: 'assunto', canActivate: [AuthGuard, ModoExibicaoGuard], data: { breadcrumb: 'Assuntos', roles: [PerfilEnum.GestorPessoas] }, children: [      
      { path: '', component: AssuntoPesquisaComponent, data: { breadcrumb: 'Assunto' } },
      { path: 'pesquisa', component: AssuntoPesquisaComponent, data: { breadcrumb: 'Pesquisa' } },
      { path: 'cadastro', component: AssuntoEdicaoComponent, data: { breadcrumb: 'Cadastro' } },
      { path: 'editar/:id', component: AssuntoEdicaoComponent, data: { breadcrumb: 'Editar' } },
    ]
  },
  {
    path: 'objeto', canActivate: [AuthGuard], data: { breadcrumb: 'Objetos', roles: [PerfilEnum.GestorPessoas] }, children: [      
      { path: '', component: ObjetoPesquisaComponent, data: { breadcrumb: 'Objeto' } },
      { path: 'pesquisa', component: ObjetoPesquisaComponent, data: { breadcrumb: 'Pesquisa' } },
      { path: 'cadastro', component: ObjetoEdicaoComponent, data: { breadcrumb: 'Cadastro' } },
      { path: 'editar/:id', component: ObjetoEdicaoComponent, data: { breadcrumb: 'Editar' } },
    ]
  },
  {
    path: 'situacao-pessoa', canActivate: [AuthGuard], data: { breadcrumb: 'Situações' }, children: [      
      { path: '', component: SituacaoPessoaComponent, data: { breadcrumb: 'Pesquisa' } },
      { path: 'pesquisa', component: SituacaoPessoaComponent, data: { breadcrumb: 'Pesquisa' } },
      { path: 'cadastro', component: SituacaoPessoaEdicaoComponent, data: { breadcrumb: 'Cadastro' } },
      { path: 'editar/:id', component: SituacaoPessoaEdicaoComponent, data: { breadcrumb: 'Editar' } },
    ]
  },
  {
    path: 'tipo-funcao', canActivate: [AuthGuard], data: { breadcrumb: 'Tipo Função' }, children: [      
      { path: '', component: TipoFuncaoComponent, data: { breadcrumb: 'Pesquisa' } },
      { path: 'pesquisa', component: TipoFuncaoComponent, data: { breadcrumb: 'Pesquisa' } },
      { path: 'cadastro', component: TipoFuncaoEdicaoComponent, data: { breadcrumb: 'Cadastro' } },
      { path: 'editar/:id', component: TipoFuncaoEdicaoComponent, data: { breadcrumb: 'Editar' } },
    ]
  },
  {
    path: 'tipo-vinculo', canActivate: [AuthGuard], data: { breadcrumb: 'Tipo Vinculo' }, children: [      
      { path: '', component: TipoVinculoComponent, data: { breadcrumb: 'Pesquisa' } },
      { path: 'pesquisa', component: TipoVinculoComponent, data: { breadcrumb: 'Pesquisa' } },
      { path: 'cadastro', component: TipoVinculoEdicaoComponent, data: { breadcrumb: 'Cadastro' } },
      { path: 'editar/:id', component: TipoVinculoEdicaoComponent, data: { breadcrumb: 'Editar' } },
    ]
  },
  {
    path: 'feriados', canActivate: [AuthGuard], data: { breadcrumb: 'Feriados' }, children: [      
      { path: '', component: FeriadosComponent, data: { breadcrumb: 'Pesquisa' } },
      { path: 'pesquisa', component: FeriadosComponent, data: { breadcrumb: 'Pesquisa' } },
      { path: 'cadastro', component: FeriadosEdicaoComponent, data: { breadcrumb: 'Cadastro' } },
      { path: 'editar/:id', component: FeriadosEdicaoComponent, data: { breadcrumb: 'Editar' } },
    ]
  },
  {
    path: 'catalogo-dominio', canActivate: [AuthGuard], data: { breadcrumb: 'Catálogo Domínio' }, children: [      
      { path: '', component: CatalogoDominioComponent, data: { breadcrumb: 'Pesquisa' } },
      { path: 'pesquisa', component: CatalogoDominioComponent, data: { breadcrumb: 'Pesquisa' } },
      // { path: 'cadastro', component: FeriadosEdicaoComponent, data: { breadcrumb: 'Cadastro' } },
      // { path: 'editar/:id', component: FeriadosEdicaoComponent, data: { breadcrumb: 'Editar' } },
    ]
  },
  {
    path: 'pg-unidade', canActivate: [AuthGuard], data: { breadcrumb: 'Pessoas no PG por Unidade' }, children: [      
      { path: '', component: PgUnidadeComponent, data: { breadcrumb: 'Pesquisa' } },
      { path: 'pesquisa', component: PgUnidadeComponent, data: { breadcrumb: 'Pesquisa' } },
      // { path: 'cadastro', component: FeriadosEdicaoComponent, data: { breadcrumb: 'Cadastro' } },
      // { path: 'editar/:id', component: FeriadosEdicaoComponent, data: { breadcrumb: 'Editar' } },
    ]
  },
  { path: 'agendamento', canActivate: [AuthGuard], component: AgendamentoPresencialComponent, data: { breadcrumb: 'Agendamento presencial' }, pathMatch: 'full' },  
  
];

export const RoutingModule: ModuleWithProviders = RouterModule.forRoot(routes);
