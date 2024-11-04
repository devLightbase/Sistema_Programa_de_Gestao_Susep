import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Guid } from 'src/app/shared/helpers/guid.helper';
import { IDadosPaginados } from 'src/app/shared/models/pagination.model';
import { ApplicationResult } from '../../../../shared/models/application-result.model';
import { ConfigurationService } from '../../../../shared/services/configuration.service';
import { DataService } from '../../../../shared/services/data.service';
import { ICatalogoDominio } from '../models/catalogo-dominio.model';
import { ICatalogoDominioPesquisa } from '../models/catalogo-dominio.pesquisa.model';

@Injectable({
  providedIn: 'root', // <---- Adiciona isto ao serviço
})
export class CatalogoDominioDataService {

  constructor(
    private service: DataService,
    private configuration: ConfigurationService) { }

  ObterPagina(dadosBusca: ICatalogoDominioPesquisa): Observable<ApplicationResult<IDadosPaginados<ICatalogoDominio>>> {
    const baseURI = this.configuration.getApiGatewayUrl();
    const params = this.service.toQueryParams(dadosBusca);
    const url = `${baseURI}catalogo/catalogo-dominio?${params}`;

    return this.service.get(url).pipe(map((response: any) => {
      return response;
    }));
  } 
}
