import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Guid } from 'src/app/shared/helpers/guid.helper';
import { IDadosPaginados } from 'src/app/shared/models/pagination.model';
import { ApplicationResult } from '../../../../shared/models/application-result.model';
import { ConfigurationService } from '../../../../shared/services/configuration.service';
import { DataService } from '../../../../shared/services/data.service';
import { ITipoFuncao } from '../models/tipo-funcao.model';
import { ITipoFuncaoPesquisa } from '../models/tipo-funcao.pesquisa.model';

@Injectable({
  providedIn: 'root', // <---- Adiciona isto ao serviço
})
export class TipoFuncaoDataService {

  constructor(
    private service: DataService,
    private configuration: ConfigurationService) { }

  ObterPagina(dadosBusca: ITipoFuncaoPesquisa): Observable<ApplicationResult<IDadosPaginados<ITipoFuncao>>> {
    const baseURI = this.configuration.getApiGatewayUrl();
    const params = this.service.toQueryParams(dadosBusca);
    const url = `${baseURI}pessoa/tipo-funcao?${params}`;

    return this.service.get(url).pipe(map((response: any) => {
      return response;
    }));
  }

  Cadastrar(dados: ITipoFuncao): Observable<ApplicationResult<boolean>> {
    const baseURI = this.configuration.getApiGatewayUrl();
    const url = `${baseURI}pessoa/tipo-funcao`;

    return this.service.post(url, dados).pipe(map((response: any) => {
      return response;
    }));
  }

  ObterPorId(id: Guid): Observable<ApplicationResult<ITipoFuncao>> {
    const baseURI = this.configuration.getApiGatewayUrl();
    const url = `${baseURI}pessoa/tipo-funcao/${id}`;

    return this.service.get(url).pipe(map((response: any) => {
      return response;
    }));
  }

  Atualizar(dados: ITipoFuncao): Observable<ApplicationResult<boolean>> {
    const baseURI = this.configuration.getApiGatewayUrl();
    const url = `${baseURI}pessoa/tipo-funcao`;

    return this.service.put(url, dados).pipe(map((response: any) => {
      return response;
    }));
  }

  Excluir(id: number): Observable<ApplicationResult<ITipoFuncao>> {
    const baseURI = this.configuration.getApiGatewayUrl();
    const url = `${baseURI}pessoa/tipo-funcao/${id}`;
    return this.service.delete(url).pipe(map((response: any) => {
      return response;
    }));
  }  
}
