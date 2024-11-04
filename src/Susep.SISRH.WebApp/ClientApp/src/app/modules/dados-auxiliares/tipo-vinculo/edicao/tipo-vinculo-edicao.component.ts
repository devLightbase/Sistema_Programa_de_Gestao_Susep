import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { PerfilEnum } from 'src/app/modules/programa-gestao/enums/perfil.enum';
import { ITipoVinculo } from '../models/tipo-vinculo.model';
import { TipoVinculoDataService } from '../services/tipo-vinculo.service';

@Component({
  selector: 'app-tipo-vinculo-edicao',
  templateUrl: './tipo-vinculo-edicao.component.html',
  styleUrls: ['./tipo-vinculo-edicao.component.css']
})
export class TipoVinculoEdicaoComponent implements OnInit {

  PerfilEnum = PerfilEnum;

  form: FormGroup;

  tipovinculo: ITipoVinculo;

  constructor(
    public router: Router,
    private route: ActivatedRoute,
    private formBuilder: FormBuilder,
    private tipoVinculoDataService: TipoVinculoDataService
  ) { }

  ngOnInit() {

    this.form = this.formBuilder.group({
      // TipoVinculoId: [0],
      Descricao: ['', [Validators.required]],
      Situacao: [1]
    });

    const id = this.route.snapshot.params.id;
    if (id) {
      this.tipoVinculoDataService.ObterPorId(id).subscribe(result => {
        this.tipovinculo = result.retorno;

        this.form.patchValue({
          TipoFuncaoId: this.tipovinculo.TipoVinculoId,
          Descricao: this.tipovinculo.Descricao,
          Situacao: this.tipovinculo.Situacao
        });
      });
    }
  }

  salvar() {
    if (this.form.valid) {
      const dados : ITipoVinculo = this.form.value;

      if (this.tipovinculo) {
        dados.TipoVinculoId = this.tipovinculo.TipoVinculoId;
        this.tipoVinculoDataService.Atualizar(dados).subscribe(result => {
          if (result.retorno) {
            this.router.navigateByUrl('/tipo-vinculo');
          }
        });
      }
      else {
        this.tipoVinculoDataService.Cadastrar(dados).subscribe(result => {
          if (result.retorno) {
            this.router.navigateByUrl('/tipo-vinculo');
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

