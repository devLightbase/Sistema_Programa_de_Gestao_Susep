import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ITipoVinculo } from './models/tipo-vinculo.model';
import { IDadosPaginados } from '../../../shared/models/pagination.model';
import { TipoVinculoDataService } from './services/tipo-vinculo.service';
import { FormGroup, FormBuilder } from '@angular/forms';
import { ITipoVinculoPesquisa } from './models/tipo-vinculo.pesquisa.model';
import { BehaviorSubject } from 'rxjs';
import { PerfilEnum } from '../../programa-gestao/enums/perfil.enum';

@Component({
  selector: 'app-tipo-vinculo',
  templateUrl: './tipo-vinculo.component.html',
  styleUrls: ['./tipo-vinculo.component.css']
})
export class TipoVinculoComponent implements OnInit {
  PerfilEnum = PerfilEnum;

  dadosEncontrados: IDadosPaginados<ITipoVinculo>;
  dadosUltimaPesquisa: ITipoVinculoPesquisa = {};
  paginacao = new BehaviorSubject<IDadosPaginados<ITipoVinculo>>(null);

  tipovinculo: ITipoVinculo;

  form: FormGroup;

  constructor(
    public router: Router,
    private formBuilder: FormBuilder,
    private tipoVinculoDataService: TipoVinculoDataService
  ) { }

  ngOnInit() {
    this.form = this.formBuilder.group({
      descricao: [""]
    });
    this.pesquisar(1);
  }

  // excluirItem(id) {
  //   this.tipoVinculoDataService.Excluir(id).subscribe(
  //     r => {
  //       this.router.navigateByUrl(`/tipo-vinculo/pesquisa`);
  //     });
  // }

  excluirItem(id: string){
    this.tipoVinculoDataService.ObterPorId(id).subscribe(result => {
      this.tipovinculo = result.retorno;
      this.tipovinculo.Situacao = 0
      this.tipoVinculoDataService.Atualizar(this.tipovinculo).subscribe(result => {
        if (result.retorno) {
          window.location.reload()
        }
      });

    });
  }

  pesquisar(pagina: number) {

    if (this.form.valid) {
      this.dadosUltimaPesquisa = this.form.value;
      this.dadosUltimaPesquisa.page = pagina;
  
      this.tipoVinculoDataService.ObterPagina(this.dadosUltimaPesquisa)
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