import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { PerfilEnum } from 'src/app/modules/programa-gestao/enums/perfil.enum';
import { IDadosCombo } from 'src/app/shared/models/dados-combo.model';
import { IUnidade } from '../models/unidade.model';
import { UnidadeDataServices } from '../service/unidade.service';

@Component({
  selector: 'app-manutencao-unidade-edicao',
  templateUrl: './manutencao-unidade-edicao.component.html',
  styleUrls: ['./manutencao-unidade-edicao.component.css']
})
export class ManutencaoUnidadeEdicaoComponent implements OnInit {

  PerfilEnum = PerfilEnum;

  form: FormGroup;

  unidade: IUnidade;
  unidades: IDadosCombo[];
  pessoas: IDadosCombo[];
  uf: IDadosCombo[];

  constructor(
    public router: Router,
    private route: ActivatedRoute,
    private formBuilder: FormBuilder,
    private unidadeDataService: UnidadeDataServices
  ) { }

  ngOnInit() {

    this.form = this.formBuilder.group({
      sigla: ['', [Validators.required]],
      nome: ['', [Validators.required]],
      // unidadeId: [6666666, []],
      unidadeIdPai: [null, []],
      tipoUnidadeId: [0, []],
      tipoFuncaoUnidadeId: [15, []],
      situacaoUnidadeId: [1, []],
      ufId: ["", []],
      nivel: [1, [Validators.required]],
      email: ["", []],
      pessoaIdChefe: [null, []],
      pessoaIdChefeSubstituto: [null, []],
      codSiorg: [0, []],
      codSgc: [0, []]
    });

    this.unidadeDataService.ObterDadosCombo().subscribe(result => {
      this.unidades = result.retorno;
    });

    this.unidadeDataService.ObterPessoasCombo().subscribe(result => {
      this.pessoas = result.retorno;
    });

    this.unidadeDataService.ObterUFCombo().subscribe(result => {
      this.uf = result.retorno;
    });

    const id = this.route.snapshot.params.id;
    if (id) {
      this.unidadeDataService.ObterPorId(id).subscribe(result => {
        this.unidade = result.retorno;

        this.form.patchValue({
          unidadeId: this.unidade.unidadeId,
          sigla: this.unidade.sigla,
          nome: this.unidade.nome,
          unidadeIdPai: this.unidade.unidadeIdPai,
          tipoUnidadeId: this.unidade.tipoUnidadeId,
          tipoFuncaoUnidadeId: this.unidade.tipoFuncaoUnidadeId,
          situacaoUnidadeId: this.unidade.situacaoUnidadeId,
          ufId: this.unidade.ufId,
          nivel: this.unidade.nivel,
          email: this.unidade.email,
          pessoaIdChefe: this.unidade.pessoaIdChefe,
          pessoaIdChefeSubstituto: this.unidade.pessoaIdChefeSubstituto,
          codSiorg: this.unidade.codSiorg,
          codSgc: this.unidade.codSgc
        });
      });
    }
  }

  salvar() {
    if (this.form.valid) {
      const dados: IUnidade = this.form.value;

      if (this.unidade) {
        dados.unidadeId = this.unidade.unidadeId;
        this.unidadeDataService.AtualizarUnidade(dados).subscribe(result => {
          if (result.retorno) {
            this.router.navigateByUrl('/manutencao-unidade');
          }
        });
      }
      else {
        this.unidadeDataService.CadastrarUnidade(dados).subscribe(result => {
          if (result.retorno) {
            this.router.navigateByUrl('/manutencao-unidade');
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
