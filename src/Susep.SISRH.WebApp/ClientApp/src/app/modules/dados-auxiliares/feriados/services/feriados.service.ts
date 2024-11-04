import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Guid } from 'src/app/shared/helpers/guid.helper';
import { IDadosPaginados } from 'src/app/shared/models/pagination.model';
import { ApplicationResult } from '../../../../shared/models/application-result.model';
import { ConfigurationService } from '../../../../shared/services/configuration.service';
import { DataService } from '../../../../shared/services/data.service';
import { IFeriado } from '../models/feriados.model';
import { IFeriadoPesquisa } from '../models/feriados.pesquisa.model';
import { IDadosCombo } from 'src/app/shared/models/dados-combo.model';

@Injectable({
  providedIn: 'root', // <---- Adiciona isto ao serviço
})
export class FeriadosDataService {

  constructor(
    private service: DataService,
    private configuration: ConfigurationService) { }

  ObterPagina(dadosBusca: IFeriadoPesquisa): Observable<ApplicationResult<IDadosPaginados<IFeriado>>> {
    const baseURI = this.configuration.getApiGatewayUrl();
    const params = this.service.toQueryParams(dadosBusca);
    const url = `${baseURI}pessoa/feriados?${params}`;

    return this.service.get(url).pipe(map((response: any) => {
      return response;
    }));
  }

  Cadastrar(dados: IFeriado): Observable<ApplicationResult<boolean>> {
    const baseURI = this.configuration.getApiGatewayUrl();
    const url = `${baseURI}pessoa/feriados`;

    return this.service.post(url, dados).pipe(map((response: any) => {
      return response;
    }));
  }

  ObterPorId(id: Guid): Observable<ApplicationResult<IFeriado>> {
    const baseURI = this.configuration.getApiGatewayUrl();
    const url = `${baseURI}pessoa/feriados/${id}`;

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

  Atualizar(dados: IFeriado): Observable<ApplicationResult<boolean>> {
    const baseURI = this.configuration.getApiGatewayUrl();
    const url = `${baseURI}pessoa/feriados`;

    return this.service.put(url, dados).pipe(map((response: any) => {
      return response;
    }));
  }

  Excluir(id: number): Observable<ApplicationResult<IFeriado>> {
    const baseURI = this.configuration.getApiGatewayUrl();
    const url = `${baseURI}pessoa/feriados/${id}`;
    return this.service.delete(url).pipe(map((response: any) => {
      return response;
    }));
  }  
}
