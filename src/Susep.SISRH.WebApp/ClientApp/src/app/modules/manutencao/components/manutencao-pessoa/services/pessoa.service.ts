import { Injectable } from '@angular/core';

import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';
import { DataService } from '../../../../../shared/services/data.service';
import { ConfigurationService } from '../../../../../shared/services/configuration.service';
import { ApplicationResult } from '../../../../../shared/models/application-result.model';
import { IDadosPaginados } from '../../../../../shared/models/pagination.model';
import { IPessoa } from '../models/pessoa.model';
import { IPessoaPesquisa } from '../models/pessoa.pesquisa.model';
import { IUsuario } from '../../../../../shared/models/perfil-usuario.model';
import { IDadosCombo } from '../../../../../shared/models/dados-combo.model';
import { IDashboard } from '../../../../dashboard/models/dashboard.model';

@Injectable({
  providedIn: 'root', // <---- Adiciona isto ao serviço
})

export class PessoaDataService {

  constructor(
    private service: DataService,
    private configuration: ConfigurationService) { }


  ObterPagina(dadosBusca: IPessoaPesquisa): Observable<ApplicationResult<IDadosPaginados<IPessoa>>> {
    const baseURI = this.configuration.getApiGatewayUrl();
    const params = this.service.toQueryParams(dadosBusca);
    const url = `${baseURI}pessoa?${params}`;

    return this.service.get(url).pipe(map((response: any) => {
      return response;
    }));
  }

  ObterDashboard(): Observable<ApplicationResult<IDashboard>> {
    const baseURI = this.configuration.getApiGatewayUrl();
    const url = `${baseURI}pessoa/dashboard`;

    return this.service.get(url).pipe(map((response: any) => {
      return response;
    }));
  }

  ObterPessoa(pessoaId: string): Observable<ApplicationResult<IPessoa>> {
    const baseURI = this.configuration.getApiGatewayUrl();
    const url = `${baseURI}pessoa/${pessoaId}`;

    return this.service.get(url).pipe(map((response: any) => {
      return response;
    }));
  }

  ObterPessoaDuplicada(cpf: string): Observable<ApplicationResult<IPessoa>> {
    const baseURI = this.configuration.getApiGatewayUrl();
    const url = `${baseURI}pessoa/duplicada/${cpf}`;

    return this.service.get(url).pipe(map((response: any) => {
      return response;
    }));
  }

  ObterFeriados(pessoaId: number, dataInicio: Date, dataFim: Date): Observable<ApplicationResult<Date[]>> {
    const baseURI = this.configuration.getApiGatewayUrl();
    const url = `${baseURI}pessoa/${pessoaId}/feriados/?dataInicio=${this.service.formatAsDate(dataInicio)}&dataFim=${this.service.formatAsDate(dataFim)}`;

    return this.service.get(url).pipe(map((response: any) => {
      return response;
    }));
  }

  ObterPerfil(): Observable<ApplicationResult<IUsuario>> {
    const baseURI = this.configuration.getApiGatewayUrl();
    const url = `${baseURI}pessoa/perfil`;

    return this.service.get(url).pipe(map((response: any) => {
      return response;
    }));
  }

  ObterComPactoTrabalhoDadosCombo(closeLoading = true): Observable<ApplicationResult<IDadosCombo[]>> {
    const baseURI = this.configuration.getApiGatewayUrl();
    const url = `${baseURI}pessoa/compactotrabalho`;

    return this.service.get(url, null, null, closeLoading).pipe(map((response: any) => {
      return response;
    }));
  }

  CadastrarPessoa(dados: IPessoa): Observable<ApplicationResult<boolean>> {
    const baseURI = this.configuration.getApiGatewayUrl();
    const url = `${baseURI}pessoa`;

    return this.service.post(url, dados).pipe(map((response: any) => {
      return response;
    }));
  }

  ObterComboUnidades(): Observable<ApplicationResult<IDadosCombo[]>> {
    const baseURI = this.configuration.getApiGatewayUrl();
    const url = `${baseURI}unidade/combo`;

    return this.service.get(url).pipe(map((response: any) => {
      return response;
    }));
  }

  ObterComboFuncoes(): Observable<ApplicationResult<IDadosCombo[]>> {
    const baseURI = this.configuration.getApiGatewayUrl();
    const url = `${baseURI}pessoa/combo/funcao`;

    return this.service.get(url).pipe(map((response: any) => {
      return response;
    }));
  }

  ObterComboVinculos(): Observable<ApplicationResult<IDadosCombo[]>> {
    const baseURI = this.configuration.getApiGatewayUrl();
    const url = `${baseURI}pessoa/combo/vinculos`;

    return this.service.get(url).pipe(map((response: any) => {
      return response;
    }));
  }

  ObterComboSituacao(): Observable<ApplicationResult<IDadosCombo[]>> {
    const baseURI = this.configuration.getApiGatewayUrl();
    const url = `${baseURI}pessoa/combo/situacao`;

    return this.service.get(url).pipe(map((response: any) => {
      return response;
    }));
  }

  AtualizarPessoa(dados: IPessoa): Observable<ApplicationResult<boolean>> {
    const baseURI = this.configuration.getApiGatewayUrl();
    const url = `${baseURI}pessoa`;

    return this.service.put(url, dados).pipe(map((response: any) => {
      return response;
    }));
  }

}
