import { Component, OnInit } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { IDadosPaginados } from '../../../../shared/models/pagination.model';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { PerfilEnum } from '../../../programa-gestao/enums/perfil.enum';
// Services
import { UnidadeDataServices } from './service/unidade.service';
//Models
import { IUnidade } from './models/unidade.model'
import { IUnidadePesquisa } from './models/unidade.pesquisa.model'


@Component({
  selector: 'app-manutencao-unidade',
  templateUrl: './manutencao-unidade.component.html',
  styleUrls: ['./manutencao-unidade.component.css']
})
export class ManutencaoUnidadeComponent implements OnInit {

  PerfilEnum = PerfilEnum;

  dadosEncontrados: IDadosPaginados<IUnidade>;
  dadosUltimaPesquisa: IUnidadePesquisa = {};
  paginacao = new BehaviorSubject<IDadosPaginados<IUnidade>>(null);
  unidade: IUnidade;

  form: FormGroup;

  constructor(
    public router: Router,
    private formBuilder: FormBuilder,
    private unidadeDataService: UnidadeDataServices
  ) { }

  ngOnInit() {
    this.form = this.formBuilder.group({
      nome: [null],
      sigla: [null]
    });
    this.pesquisar(1);
  }

  pesquisar(pagina: number) {
    if (this.form.valid) {
      this.dadosUltimaPesquisa = this.form.value;
      this.dadosUltimaPesquisa.page = pagina;

      this.unidadeDataService.ObterPagina(this.dadosUltimaPesquisa)
        .subscribe(resultado => {
          this.dadosEncontrados = resultado.retorno
        });
    }
  }

  excluirItem(id: number){
    this.unidadeDataService.ObterPorId(id).subscribe(result => {
      this.unidade = result.retorno;
      this.unidade.situacaoUnidadeId = 0
      this.unidadeDataService.AtualizarUnidade(this.unidade).subscribe(result => {
        if (result.retorno) {
          window.location.reload()
        }
      });

    });
  }

  changePage(pagina: number) {
    this.pesquisar(pagina);
  }

  onSubmit() {
    this.pesquisar(1);
  }
}
