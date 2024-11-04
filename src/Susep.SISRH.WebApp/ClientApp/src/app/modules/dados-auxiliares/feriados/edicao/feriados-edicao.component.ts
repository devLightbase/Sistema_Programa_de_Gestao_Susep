import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { PerfilEnum } from 'src/app/modules/programa-gestao/enums/perfil.enum';
import { IFeriado } from '../models/feriados.model';
import { FeriadosDataService } from '../services/feriados.service';
import { IDadosCombo } from 'src/app/shared/models/dados-combo.model';

@Component({
  selector: 'app-feriados-edicao',
  templateUrl: './feriados-edicao.component.html',
  styleUrls: ['./feriados-edicao.component.css']
})
export class FeriadosEdicaoComponent implements OnInit {

  PerfilEnum = PerfilEnum;

  form: FormGroup;

  feriado: IFeriado;

  uf: IDadosCombo[];

  constructor(
    public router: Router,
    private route: ActivatedRoute,
    private formBuilder: FormBuilder,
    private feriadosDataService: FeriadosDataService
  ) { }

  ngOnInit() {

    this.form = this.formBuilder.group({
      // FeriadoId: [0],
      Data: [null, [Validators.required]],
      Descricao: [null, [Validators.required]],
      Fixo: [true, [Validators.required]],
      UfId: [null],
      Situacao: [1],
    });

    this.feriadosDataService.ObterUFCombo().subscribe(result => {
      this.uf = result.retorno;
    });

    const id = this.route.snapshot.params.id;
    if (id) {
      this.feriadosDataService.ObterPorId(id).subscribe(result => {
        this.feriado = result.retorno;

        this.form.patchValue({
          TipoFuncaoId: this.feriado.FeriadoId,
          Data: this.feriado.Data,
          Descricao: this.feriado.Descricao,
          Fixo: this.feriado.Fixo,
          UfId: this.feriado.UfId,
          Situacao: this.feriado.Situacao
        });
      });
    }
  }

  salvar() {
    if (this.form.valid) {
      const dados : IFeriado = this.form.value;

      if (this.feriado) {
        dados.FeriadoId = this.feriado.FeriadoId;
        this.feriadosDataService.Atualizar(dados).subscribe(result => {
          if (result.retorno) {
            this.router.navigateByUrl('/feriados');
          }
        });
      }
      else {
        this.feriadosDataService.Cadastrar(dados).subscribe(result => {
          if (result.retorno) {
            this.router.navigateByUrl('/feriados');
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

