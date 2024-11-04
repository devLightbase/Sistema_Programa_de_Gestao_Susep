import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { PerfilEnum } from 'src/app/modules/programa-gestao/enums/perfil.enum';
import { IDadosCombo } from 'src/app/shared/models/dados-combo.model';
import { IPessoa } from '../../manutencao-pessoa/models/pessoa.model';
import { PessoaDataService } from '../../manutencao-pessoa/services/pessoa.service';
import { IUnidade } from '../models/unidade.model';
import { UnidadeDataServices } from '../service/unidade.service';

@Component({
  selector: 'app-manutencao-unidades-detalhes',
  templateUrl: './manutencao-unidades-detalhes.component.html',
  styleUrls: ['./manutencao-unidades-detalhes.component.css']
})
export class ManutencaoUnidadesDetalhesComponent implements OnInit {

  PerfilEnum = PerfilEnum;

  form: FormGroup;

  unidade: IUnidade;

  unidadePai: IUnidade;
  temPai: boolean;

  chefe: IPessoa;
  chefeNome: String;

  chefeSubstituto: IPessoa;
  chefeSubstitutoNome: String;

  constructor(
    public router: Router,
    private route: ActivatedRoute,
    private formBuilder: FormBuilder,
    private unidadeDataService: UnidadeDataServices,
    private pessoaDataService: PessoaDataService
  ) { }

  ngOnInit() {
    this.chefeNome = "";
    this.chefeSubstitutoNome = "";
    this.temPai = false;
    const id = this.route.snapshot.params.id;
    if (id) {
      this.unidadeDataService.ObterPorId(id).subscribe(result => {
        this.unidade = result.retorno;
        if (this.unidade.unidadeIdPai) {
          this.unidadeDataService.ObterPorId(this.unidade.unidadeIdPai).subscribe(result => {
          this.unidadePai = result.retorno;
          this.temPai = true
          });
        }
        if (this.unidade.pessoaIdChefe) {
          this.pessoaDataService.ObterPessoa(this.unidade.pessoaIdChefe.toString()).subscribe(result => {
            this.chefe = result.retorno;
            this.chefeNome = this.chefe.nome
          });
        }
        if (this.unidade.pessoaIdChefeSubstituto) {
          this.pessoaDataService.ObterPessoa(this.unidade.pessoaIdChefeSubstituto.toString()).subscribe(result => {
            this.chefeSubstituto = result.retorno;
            this.chefeSubstitutoNome = this.chefeSubstituto.nome
          });
        }

      });
    }
  }

}
