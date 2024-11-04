import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ICatalogoDominio } from './models/catalogo-dominio.model';
import { IDadosPaginados } from '../../../shared/models/pagination.model';
import { CatalogoDominioDataService } from './services/catalogo-dominio.service';
import { FormGroup, FormBuilder } from '@angular/forms';
import { ICatalogoDominioPesquisa } from './models/catalogo-dominio.pesquisa.model';
import { BehaviorSubject } from 'rxjs';
import { PerfilEnum } from '../../programa-gestao/enums/perfil.enum';

@Component({
  selector: 'app-catalogo-dominio',
  templateUrl: './catalogo-dominio.component.html',
  styleUrls: ['./catalogo-dominio.component.css']
})
export class CatalogoDominioComponent implements OnInit {
  PerfilEnum = PerfilEnum;

  dadosEncontrados: IDadosPaginados<ICatalogoDominio>;
  dadosUltimaPesquisa: ICatalogoDominioPesquisa = {};
  paginacao = new BehaviorSubject<IDadosPaginados<ICatalogoDominio>>(null);

  form: FormGroup;

  constructor(
    public router: Router,
    private formBuilder: FormBuilder,
    private catalogoDominioDataService: CatalogoDominioDataService
  ) { }

  ngOnInit() {
    this.form = this.formBuilder.group({
      classificacao: [""],
      descricao: [""]
    });
    this.pesquisar(1);
  }

  pesquisar(pagina: number) {

    if (this.form.valid) {
      this.dadosUltimaPesquisa = this.form.value;
      this.dadosUltimaPesquisa.page = pagina;
  
      this.catalogoDominioDataService.ObterPagina(this.dadosUltimaPesquisa)
        .subscribe(resultado => {
          this.dadosEncontrados = resultado.retorno
        });
    }

  }

  changePage(pagina: number) {
    this.pesquisar(pagina);
  }

  onSubmit() {
    this.pesquisar(1);
  }

}