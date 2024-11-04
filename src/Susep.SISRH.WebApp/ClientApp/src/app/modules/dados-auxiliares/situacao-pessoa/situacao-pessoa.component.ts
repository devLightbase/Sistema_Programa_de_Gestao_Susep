import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ISituacao } from './models/situacao.model';
import { IDadosPaginados } from '../../../shared/models/pagination.model';
import { SituacaoDataService } from './services/situacao.service';
import { FormGroup, FormBuilder } from '@angular/forms';
import { ISituacaoPesquisa } from './models/situacao.pesquisa.model';
import { BehaviorSubject } from 'rxjs';
import { PerfilEnum } from '../../programa-gestao/enums/perfil.enum';

@Component({
  selector: 'app-situacao-pessoa',
  templateUrl: './situacao-pessoa.component.html',
  styleUrls: ['./situacao-pessoa.component.css']
})
export class SituacaoPessoaComponent implements OnInit {
  PerfilEnum = PerfilEnum;

  dadosEncontrados: IDadosPaginados<ISituacao>;
  dadosUltimaPesquisa: ISituacaoPesquisa = {};
  paginacao = new BehaviorSubject<IDadosPaginados<ISituacao>>(null);

  situacao: ISituacao;

  form: FormGroup;

  constructor(
    public router: Router,
    private formBuilder: FormBuilder,
    private situacaoDataService: SituacaoDataService
  ) { }

  ngOnInit() {
    this.form = this.formBuilder.group({
      descricao: [""]
    });
    this.pesquisar(1);
  }

  // excluirItem(id) {
  //   const SituacaoPessoaId = id;
  //   this.situacaoDataService.Excluir(SituacaoPessoaId).subscribe(
  //     r => {
  //       this.router.navigateByUrl(`/situacao-pessoa/pesquisa`);
  //     });
  // }

  excluirItem(id: string){
    this.situacaoDataService.ObterPorId(id).subscribe(result => {
      this.situacao = result.retorno;
      this.situacao.Situacao = 0
      this.situacaoDataService.AtualizarSituacao(this.situacao).subscribe(result => {
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
  
      this.situacaoDataService.ObterPagina(this.dadosUltimaPesquisa)
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