import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { IFeriado } from './models/feriados.model';
import { IDadosPaginados } from '../../../shared/models/pagination.model';
import { FeriadosDataService } from './services/feriados.service';
import { FormGroup, FormBuilder } from '@angular/forms';
import { IFeriadoPesquisa } from './models/feriados.pesquisa.model';
import { BehaviorSubject } from 'rxjs';
import { PerfilEnum } from '../../programa-gestao/enums/perfil.enum';

@Component({
  selector: 'app-feriados',
  templateUrl: './feriados.component.html',
  styleUrls: ['./feriados.component.css']
})
export class FeriadosComponent implements OnInit {
  PerfilEnum = PerfilEnum;

  dadosEncontrados: IDadosPaginados<IFeriado>;
  dadosUltimaPesquisa: IFeriadoPesquisa = {};
  paginacao = new BehaviorSubject<IDadosPaginados<IFeriado>>(null);

  feriado: IFeriado;

  form: FormGroup;

  constructor(
    public router: Router,
    private formBuilder: FormBuilder,
    private feriadosDataService: FeriadosDataService
  ) { }

  ngOnInit() {
    this.form = this.formBuilder.group({
      data: [null],
      descricao: [null]
    });
    this.pesquisar(1);
  }

  dataAtualFormatada(dataa){
    var data = new Date(dataa);
    return data.toLocaleDateString();
}

  // excluirItem(id) {
  //   this.feriadosDataService.Excluir(id).subscribe(
  //     r => {
  //       this.router.navigateByUrl(`/feriados/pesquisa`);
  //     });
  // }

  excluirItem(id: string){
    this.feriadosDataService.ObterPorId(id).subscribe(result => {
      this.feriado = result.retorno;
      this.feriado.Situacao = 0
      this.feriadosDataService.Atualizar(this.feriado).subscribe(result => {
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
  
      this.feriadosDataService.ObterPagina(this.dadosUltimaPesquisa)
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