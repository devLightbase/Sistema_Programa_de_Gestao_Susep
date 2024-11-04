import { NgModule, LOCALE_ID } from '@angular/core';

import { BrowserModule } from '@angular/platform-browser';
import { NgbModule, NgbModalModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { NgBrazil } from 'ng-brazil'
import { registerLocaleData } from '@angular/common';
import localePt from '@angular/common/locales/pt';

import { AppComponent } from './app.component';
import { RoutingModule } from './app.routes';

import { App3rdPartyModule } from './app.3rdparty.module';
import { SharedModule } from './shared/shared.module';
import { ProgramaGestaoModule } from './modules/programa-gestao/programa-gestao.module';
import { PessoaModule } from './modules/pessoa/pessoa.module';
import { UnidadeModule } from './modules/unidade/unidade.module';
import { AssuntoModule } from './modules/assunto/assunto.module';
import { ObjetoModule } from './modules/objeto/objeto.module';
import { ManutencaoUnidadeComponent } from './modules/manutencao/components/manutencao-unidade/manutencao-unidade.component';
import { ManutencaoUnidadeEdicaoComponent } from './modules/manutencao/components/manutencao-unidade/edicao/manutencao-unidade-edicao.component';
import { ManutencaoPessoaPesquisaComponent } from './modules/manutencao/components/manutencao-pessoa/manutencao-pessoa-pesquisa/manutencao-pessoa-pesquisa.component';
import { ManutencaoPessoaEdicaoComponent } from './modules/manutencao/components/manutencao-pessoa/manutencao-pessoa-edicao/manutencao-pessoa-edicao.component';
import { SituacaoPessoaComponent } from './modules/dados-auxiliares/situacao-pessoa/situacao-pessoa.component';
import { SituacaoPessoaEdicaoComponent } from './modules/dados-auxiliares/situacao-pessoa/edicao/situacao-pessoa-edicao.component';
import { TipoFuncaoComponent } from './modules/dados-auxiliares/tipo-funcao/tipo-funcao.component';
import { TipoFuncaoEdicaoComponent } from './modules/dados-auxiliares/tipo-funcao/edicao/tipo-funcao-edicao.component';
import { TipoVinculoComponent } from './modules/dados-auxiliares/tipo-vinculo/tipo-vinculo.component';
import { TipoVinculoEdicaoComponent } from './modules/dados-auxiliares/tipo-vinculo/edicao/tipo-vinculo-edicao.component';
import { FeriadosComponent } from './modules/dados-auxiliares/feriados/feriados.component';
import { FeriadosEdicaoComponent } from './modules/dados-auxiliares/feriados/edicao/feriados-edicao.component';
import { CatalogoDominioComponent } from './modules/consultas/catalogo-dominio/catalogo-dominio.component';
import { ManutencaoUnidadesDetalhesComponent } from './modules/manutencao/components/manutencao-unidade/detalhar/manutencao-unidades-detalhes.component';
import { ManutencaoPessoaDetalhesComponent } from './modules/manutencao/components/manutencao-pessoa/manutencao-pessoa-detalhes/manutencao-pessoa-detalhes.component';
import { PgUnidadeComponent } from './modules/consultas/pg-unidade/pg-unidade.component';


registerLocaleData(localePt)

@NgModule({
  declarations: [
    AppComponent,
    ManutencaoUnidadeComponent,
    ManutencaoUnidadeEdicaoComponent,
    ManutencaoPessoaPesquisaComponent,
    ManutencaoPessoaEdicaoComponent,
    SituacaoPessoaComponent,
    SituacaoPessoaEdicaoComponent,
    TipoFuncaoComponent,
    TipoFuncaoEdicaoComponent,
    TipoVinculoComponent,
    TipoVinculoEdicaoComponent,
    FeriadosComponent,
    FeriadosEdicaoComponent,
    CatalogoDominioComponent,
    ManutencaoUnidadesDetalhesComponent,
    ManutencaoPessoaDetalhesComponent,
    PgUnidadeComponent,    
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),

    RouterModule,
    HttpClientModule,
    NgbModule,
    NgbModalModule,
    ReactiveFormsModule,
    HttpClientModule,
    FormsModule,

    NgBrazil,
    BrowserAnimationsModule,

    RoutingModule,

    App3rdPartyModule,
    SharedModule,
    ProgramaGestaoModule,
    PessoaModule,
    UnidadeModule,
    AssuntoModule,
    ObjetoModule,
  ],
  exports: [RouterModule],
  providers: [    
    { provide: LOCALE_ID, useValue: 'pt-BR' }
  ],
  entryComponents: [
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
