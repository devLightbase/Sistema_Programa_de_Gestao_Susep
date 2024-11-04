import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Guid } from 'src/app/shared/helpers/guid.helper';
import { IDadosPaginados } from 'src/app/shared/models/pagination.model';
import { ApplicationResult } from '../../../../shared/models/application-result.model';
import { ConfigurationService } from '../../../../shared/services/configuration.service';
import { DataService } from '../../../../shared/services/data.service';
import { IPgUnidade } from '../models/pg-unidade.model';
import { IProgramaGestaoModal } from '../models/programa-gestao-modal.model';
import { IPactosVigentesModal } from '../models/pactos-vigentes-modal.model';
import { IProgramaGestaoPesquisa } from '../models/programa-gestao-modal.pesquisa.model';
import { IPactosVigentesPesquisa } from '../models/pactos-vigentes-modal.pesquisa.model';
import { IPgUnidadePesquisa } from '../models/pg-unidade.pesquisa.model';
import { IContagem } from '../models/contagem.model';

@Injectable({
  providedIn: 'root', // <---- Adiciona isto ao serviço
})
export class PgUnidadeDataService {

  constructor(
    private service: DataService,
    private configuration: ConfigurationService) { }

  ObterPagina(dadosBusca: IPgUnidadePesquisa): Observable<ApplicationResult<IDadosPaginados<IPgUnidade>>> {
    const baseURI = this.configuration.getApiGatewayUrl();
    const params = this.service.toQueryParams(dadosBusca);
    const url = `${baseURI}catalogo/pg?${params}`;

    return this.service.get(url).pipe(map((response: any) => {
      return response;
    }));
  }
  
  ObterProgramaGestao(dadosBusca: IProgramaGestaoPesquisa): Observable<ApplicationResult<IDadosPaginados<IProgramaGestaoModal>>> {
    const baseURI = this.configuration.getApiGatewayUrl();
    const params = this.service.toQueryParams(dadosBusca);
    const url = `${baseURI}catalogo/pg/programa-gestao?${params}`;

    return this.service.get(url).pipe(map((response: any) => {
      return response;
    }));
  } 

  ObterPactosVigentes(dadosBusca: IPactosVigentesPesquisa): Observable<ApplicationResult<IDadosPaginados<IPactosVigentesModal>>> {
    const baseURI = this.configuration.getApiGatewayUrl();
    const params = this.service.toQueryParams(dadosBusca);
    const url = `${baseURI}catalogo/pg/pactos-vigentes?${params}`;

    return this.service.get(url).pipe(map((response: any) => {
      return response;
    }));
  }

  ObterContagem(dadosBusca: IPactosVigentesPesquisa): Observable<ApplicationResult<IContagem>> {
    const baseURI = this.configuration.getApiGatewayUrl();
    const params = this.service.toQueryParams(dadosBusca);
    const url = `${baseURI}catalogo/pg/contagem?${params}`;

    return this.service.get(url).pipe(map((response: any) => {
      return response;
    }));
  } 
}
