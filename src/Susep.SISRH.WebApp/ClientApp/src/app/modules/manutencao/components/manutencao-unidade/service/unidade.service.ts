import { Injectable } from '@angular/core';

import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';
import { DataService } from '../../../../../shared/services/data.service';
import { ConfigurationService } from '../../../../../shared/services/configuration.service';
import { ApplicationResult } from '../../../../../shared/models/application-result.model';
import { IUnidade } from '../models/unidade.model';
import { IDadosCombo } from '../../../../../shared/models/dados-combo.model';
import { IDadosPaginados } from '../../../../../shared/models/pagination.model';
import { IUnidadePesquisa } from '../models/unidade.pesquisa.model';

@Injectable({
  providedIn: 'root', // <---- Adiciona isto ao serviço
})
export class UnidadeDataServices {

  constructor(
    private service: DataService,
    private configuration: ConfigurationService) { }

  ObterTodasAtivasDadosCombo(): Observable<ApplicationResult<IDadosCombo[]>> {
    const baseURI = this.configuration.getApiGatewayUrl();
    const url = `${baseURI}unidade/todasativas`;

    return this.service.get(url).pipe(map((response: any) => {
      return response;
    }));
  }

  ObterPagina(dadosBusca: IUnidadePesquisa): Observable<ApplicationResult<IDadosPaginados<IUnidade>>> {
    const baseURI = this.configuration.getApiGatewayUrl();
    const params = this.service.toQueryParams(dadosBusca);
    const url = `${baseURI}unidade?${params}`;

    return this.service.get(url).pipe(map((response: any) => {
      return response;
    }));
  }

  ObterTodasUnidadesAtivasDetalhes(): Observable<ApplicationResult<IDadosPaginados<IUnidade>>> {
    const baseURI = this.configuration.getApiGatewayUrl();
    const url = `${baseURI}unidade/todasunidadesativas`;

    return this.service.get(url).pipe(map((response: any) => {
      return response;
    }));
  }

  ObterAtivasDadosCombo(): Observable<ApplicationResult<IDadosCombo[]>> {
    const baseURI = this.configuration.getApiGatewayUrl();
    const url = `${baseURI}unidade/ativas`;

    return this.service.get(url).pipe(map((response: any) => {
      return response;
    }));
  }

  ObterDadosCombo(): Observable<ApplicationResult<IDadosCombo[]>> {
    const baseURI = this.configuration.getApiGatewayUrl();
    const url = `${baseURI}unidade/combo`;

    return this.service.get(url).pipe(map((response: any) => {
      return response;
    }));
  }

  ObterPessoasCombo(): Observable<ApplicationResult<IDadosCombo[]>> {
    const baseURI = this.configuration.getApiGatewayUrl();
    const url = `${baseURI}pessoa/combo/unidade`;

    return this.service.get(url).pipe(map((response: any) => {
      return response;
    }));
  }

  ObterUFCombo(): Observable<ApplicationResult<IDadosCombo[]>> {
    const baseURI = this.configuration.getApiGatewayUrl();
    const url = `${baseURI}pessoa/combo/uf`;

    return this.service.get(url).pipe(map((response: any) => {
      return response;
    }));
  }

  ObterComCatalogoCadastrado(): Observable<ApplicationResult<IDadosCombo[]>> {
    const baseURI = this.configuration.getApiGatewayUrl();
    const url = `${baseURI}unidade/comcatalogocadastrado`;

    return this.service.get(url).pipe(map((response: any) => {
      return response;
    }));
  }

  ObterSemCatalogoCadastrado(): Observable<ApplicationResult<IDadosCombo[]>> {
    const baseURI = this.configuration.getApiGatewayUrl();
    const url = `${baseURI}unidade/semcatalogocadastrado`;

    return this.service.get(url).pipe(map((response: any) => {
      return response;
    }));
  }


  ObterComPlanotrabalhoDadosCombo(closeLoading = true): Observable<ApplicationResult<IDadosCombo[]>> {
    const baseURI = this.configuration.getApiGatewayUrl();
    const url = `${baseURI}unidade/complanotrabalho`;

    return this.service.get(url, null, null, closeLoading).pipe(map((response: any) => {
      return response;
    }));
  }

  ObterItem(unidadeId: number): Observable<ApplicationResult<IUnidade>> {
    const baseURI = this.configuration.getApiGatewayUrl();
    const url = `${baseURI}unidade/${unidadeId}`;

    return this.service.get(url).pipe(map((response: any) => {
      return response;
    }));
  }

  ObterModalidadesExecucao(unidadeId: number): Observable<ApplicationResult<IDadosCombo[]>> {
    const baseURI = this.configuration.getApiGatewayUrl();
    const url = `${baseURI}unidade/${unidadeId}/modalidadeexecucao`;

    return this.service.get(url).pipe(map((response: any) => {
      return response;
    }));
  }

  ObterPessoas(unidadeId: number): Observable<ApplicationResult<IDadosCombo[]>> {
    const baseURI = this.configuration.getApiGatewayUrl();
    const url = `${baseURI}unidade/${unidadeId}/pessoas`;

    return this.service.get(url).pipe(map((response: any) => {
      return response;
    }));
  }

  CadastrarUnidade(dados: IUnidade): Observable<ApplicationResult<boolean>> {
    const baseURI = this.configuration.getApiGatewayUrl();
    const url = `${baseURI}unidade`;

    return this.service.post(url, dados).pipe(map((response: any) => {
      return response;
    }));
  }

  ObterPorId(id): Observable<ApplicationResult<IUnidade>> {
    const baseURI = this.configuration.getApiGatewayUrl();
    const url = `${baseURI}unidade/db/${id}`;

    return this.service.get(url).pipe(map((response: any) => {
      return response;
    }));
  }

  AtualizarUnidade(dados: IUnidade): Observable<ApplicationResult<boolean>> {
    const baseURI = this.configuration.getApiGatewayUrl();
    const url = `${baseURI}unidade`;

    return this.service.put(url, dados).pipe(map((response: any) => {
      return response;
    }));
  }

}
