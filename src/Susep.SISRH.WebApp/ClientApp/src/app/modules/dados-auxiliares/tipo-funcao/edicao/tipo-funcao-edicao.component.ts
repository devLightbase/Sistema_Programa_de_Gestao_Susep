import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { PerfilEnum } from 'src/app/modules/programa-gestao/enums/perfil.enum';
import { ITipoFuncao } from '../models/tipo-funcao.model';
import { TipoFuncaoDataService } from '../services/tipo-funcao.service';

@Component({
  selector: 'app-tipo-funcao-edicao',
  templateUrl: './tipo-funcao-edicao.component.html',
  styleUrls: ['./tipo-funcao-edicao.component.css']
})
export class TipoFuncaoEdicaoComponent implements OnInit {

  PerfilEnum = PerfilEnum;

  form: FormGroup;

  tipofuncao: ITipoFuncao;

  constructor(
    public router: Router,
    private route: ActivatedRoute,
    private formBuilder: FormBuilder,
    private tipoFuncaoDataService: TipoFuncaoDataService
  ) { }

  ngOnInit() {

    this.form = this.formBuilder.group({
      // TipoFuncaoId: [0],
      Descricao: ['', [Validators.required]],
      CodigoFuncao: [''],
      IndicadorChefia: [true, [Validators.required]],
      Situacao: [1]
    });

    const id = this.route.snapshot.params.id;
    if (id) {
      this.tipoFuncaoDataService.ObterPorId(id).subscribe(result => {
        this.tipofuncao = result.retorno;

        this.form.patchValue({
          TipoFuncaoId: this.tipofuncao.TipoFuncaoId,
          Descricao: this.tipofuncao.Descricao,
          CodigoFuncao: this.tipofuncao.CodigoFuncao,
          IndicadorChefia: this.tipofuncao.IndicadorChefia,
          Situacao: this.tipofuncao.Situacao
        });
      });
    }
  }

  salvar() {
    if (this.form.valid) {
      const dados : ITipoFuncao = this.form.value;

      if (this.tipofuncao) {
        dados.TipoFuncaoId = this.tipofuncao.TipoFuncaoId;
        this.tipoFuncaoDataService.Atualizar(dados).subscribe(result => {
          if (result.retorno) {
            this.router.navigateByUrl('/tipo-funcao');
          }
        });
      }
      else {
        this.tipoFuncaoDataService.Cadastrar(dados).subscribe(result => {
          if (result.retorno) {
            this.router.navigateByUrl('/tipo-funcao');
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

