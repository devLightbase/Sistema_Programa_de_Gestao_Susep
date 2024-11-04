import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Guid } from 'src/app/shared/helpers/guid.helper';
import { IDadosPaginados } from 'src/app/shared/models/pagination.model';
import { ApplicationResult } from '../../../../shared/models/application-result.model';
import { ConfigurationService } from '../../../../shared/services/configuration.service';
import { DataService } from '../../../../shared/services/data.service';
import { ISituacao } from '../models/situacao.model';
import { ISituacaoPesquisa } from '../models/situacao.pesquisa.model';

@Injectable({
  providedIn: 'root', // <---- Adiciona isto ao serviço
})
export class SituacaoDataService {

  constructor(
    private service: DataService,
    private configuration: ConfigurationService) { }

  ObterPagina(dadosBusca: ISituacaoPesquisa): Observable<ApplicationResult<IDadosPaginados<ISituacao>>> {
    const baseURI = this.configuration.getApiGatewayUrl();
    const params = this.service.toQueryParams(dadosBusca);
    const url = `${baseURI}pessoa/situacao?${params}`;

    return this.service.get(url).pipe(map((response: any) => {
      return response;
    }));
  }

  CadastrarSituacao(dados: ISituacao): Observable<ApplicationResult<boolean>> {
    const baseURI = this.configuration.getApiGatewayUrl();
    const url = `${baseURI}pessoa/situacao`;

    return this.service.post(url, dados).pipe(map((response: any) => {
      return response;
    }));
  }

  ObterPorId(id: Guid): Observable<ApplicationResult<ISituacao>> {
    const baseURI = this.configuration.getApiGatewayUrl();
    const url = `${baseURI}pessoa/situacao/${id}`;

    return this.service.get(url).pipe(map((response: any) => {
      return response;
    }));
  }

  AtualizarSituacao(dados: ISituacao): Observable<ApplicationResult<boolean>> {
    const baseURI = this.configuration.getApiGatewayUrl();
    const url = `${baseURI}pessoa/situacao`;

    return this.service.put(url, dados).pipe(map((response: any) => {
      return response;
    }));
  }

  Excluir(id: number): Observable<ApplicationResult<ISituacao>> {
    const baseURI = this.configuration.getApiGatewayUrl();
    const url = `${baseURI}pessoa/situacao/${id}`;
    return this.service.delete(url).pipe(map((response: any) => {
      return response;
    }));
  }  
}
