import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ITipoFuncao } from './models/tipo-funcao.model';
import { IDadosPaginados } from '../../../shared/models/pagination.model';
import { TipoFuncaoDataService } from './services/tipo-funcao.service';
import { FormGroup, FormBuilder } from '@angular/forms';
import { ITipoFuncaoPesquisa } from './models/tipo-funcao.pesquisa.model';
import { BehaviorSubject } from 'rxjs';
import { PerfilEnum } from '../../programa-gestao/enums/perfil.enum';

@Component({
  selector: 'app-tipo-funcao',
  templateUrl: './tipo-funcao.component.html',
  styleUrls: ['./tipo-funcao.component.css']
})
export class TipoFuncaoComponent implements OnInit {
  
  PerfilEnum = PerfilEnum;

  dadosEncontrados: IDadosPaginados<ITipoFuncao>;
  dadosUltimaPesquisa: ITipoFuncaoPesquisa = {};
  paginacao = new BehaviorSubject<IDadosPaginados<ITipoFuncao>>(null);

  tipofuncao: ITipoFuncao;

  form: FormGroup;
  constructor(
    public router: Router,
    private formBuilder: FormBuilder,
    private tipoFuncaoDataService: TipoFuncaoDataService
  ) { }

  ngOnInit() {
    this.form = this.formBuilder.group({
      descricao: [""]
    });
    this.pesquisar(1);
  }

  // excluirItem(id) {
  //   const TipoFuncaoPessoaId = id;
  //   this.tipoFuncaoDataService.Excluir(TipoFuncaoPessoaId).subscribe(
  //     r => {
  //       this.router.navigateByUrl(`/tipo-funcao/pesquisa`);
  //     });
  // }

  excluirItem(id: string){
    this.tipoFuncaoDataService.ObterPorId(id).subscribe(result => {
      this.tipofuncao = result.retorno;
      this.tipofuncao.Situacao = 0
      this.tipoFuncaoDataService.Atualizar(this.tipofuncao).subscribe(result => {
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
  
      this.tipoFuncaoDataService.ObterPagina(this.dadosUltimaPesquisa)
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