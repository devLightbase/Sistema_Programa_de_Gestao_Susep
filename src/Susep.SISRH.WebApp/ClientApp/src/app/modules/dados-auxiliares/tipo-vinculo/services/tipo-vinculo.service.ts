import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Guid } from 'src/app/shared/helpers/guid.helper';
import { IDadosPaginados } from 'src/app/shared/models/pagination.model';
import { ApplicationResult } from '../../../../shared/models/application-result.model';
import { ConfigurationService } from '../../../../shared/services/configuration.service';
import { DataService } from '../../../../shared/services/data.service';
import { ITipoVinculo } from '../models/tipo-vinculo.model';
import { ITipoVinculoPesquisa } from '../models/tipo-vinculo.pesquisa.model';

@Injectable({
  providedIn: 'root', // <---- Adiciona isto ao serviço
})
export class TipoVinculoDataService {

  constructor(
    private service: DataService,
    private configuration: ConfigurationService) { }

  ObterPagina(dadosBusca: ITipoVinculoPesquisa): Observable<ApplicationResult<IDadosPaginados<ITipoVinculo>>> {
    const baseURI = this.configuration.getApiGatewayUrl();
    const params = this.service.toQueryParams(dadosBusca);
    const url = `${baseURI}pessoa/tipo-vinculo?${params}`;

    return this.service.get(url).pipe(map((response: any) => {
      return response;
    }));
  }

  Cadastrar(dados: ITipoVinculo): Observable<ApplicationResult<boolean>> {
    const baseURI = this.configuration.getApiGatewayUrl();
    const url = `${baseURI}pessoa/tipo-vinculo`;

    return this.service.post(url, dados).pipe(map((response: any) => {
      return response;
    }));
  }

  ObterPorId(id: Guid): Observable<ApplicationResult<ITipoVinculo>> {
    const baseURI = this.configuration.getApiGatewayUrl();
    const url = `${baseURI}pessoa/tipo-vinculo/${id}`;

    return this.service.get(url).pipe(map((response: any) => {
      return response;
    }));
  }

  Atualizar(dados: ITipoVinculo): Observable<ApplicationResult<boolean>> {
    const baseURI = this.configuration.getApiGatewayUrl();
    const url = `${baseURI}pessoa/tipo-vinculo`;

    return this.service.put(url, dados).pipe(map((response: any) => {
      return response;
    }));
  }

  Excluir(id: number): Observable<ApplicationResult<ITipoVinculo>> {
    const baseURI = this.configuration.getApiGatewayUrl();
    const url = `${baseURI}pessoa/tipo-vinculo/${id}`;
    return this.service.delete(url).pipe(map((response: any) => {
      return response;
    }));
  }  
}
