import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { PerfilEnum } from 'src/app/modules/programa-gestao/enums/perfil.enum';
import { ISituacao } from '../models/situacao.model';
import { SituacaoDataService } from '../services/situacao.service';

@Component({
  selector: 'app-situacao-pessoa-edicao',
  templateUrl: './situacao-pessoa-edicao.component.html',
  styleUrls: ['./situacao-pessoa-edicao.component.css']
})
export class SituacaoPessoaEdicaoComponent implements OnInit {

  PerfilEnum = PerfilEnum;

  form: FormGroup;

  situacao: ISituacao;

  constructor(
    public router: Router,
    private route: ActivatedRoute,
    private formBuilder: FormBuilder,
    private situacaoDataService: SituacaoDataService
  ) { }

  ngOnInit() {

    this.form = this.formBuilder.group({
      // SituacaoPessoaId: [0],
      Descricao: ['', [Validators.required]],
      Situacao: [1]
    });

    const id = this.route.snapshot.params.id;
    if (id) {
      this.situacaoDataService.ObterPorId(id).subscribe(result => {
        this.situacao = result.retorno;

        this.form.patchValue({
          SituacaoPessoaId: this.situacao.SituacaoPessoaId,
          Descricao: this.situacao.Descricao,
          Situacao: this.situacao.Situacao
        });
      });
    }
  }

  salvar() {
    if (this.form.valid) {
      const dados : ISituacao = this.form.value;

      if (this.situacao) {
        dados.SituacaoPessoaId = this.situacao.SituacaoPessoaId;
        this.situacaoDataService.AtualizarSituacao(dados).subscribe(result => {
          if (result.retorno) {
            this.router.navigateByUrl('/situacao-pessoa');
          }
        });
      }
      else {
        this.situacaoDataService.CadastrarSituacao(dados).subscribe(result => {
          if (result.retorno) {
            this.router.navigateByUrl('/situacao-pessoa');
          }
        });
      }
    }
    else {
      this.getFormValidationErrors(this.form)
    }
  }

  getFormValidationErrors(form) {
    Object.keys(form.controls).forEach(field => {
      const control = form.get(field);
      control.markAsDirty({ onlySelf: true });
    });
  }

}

