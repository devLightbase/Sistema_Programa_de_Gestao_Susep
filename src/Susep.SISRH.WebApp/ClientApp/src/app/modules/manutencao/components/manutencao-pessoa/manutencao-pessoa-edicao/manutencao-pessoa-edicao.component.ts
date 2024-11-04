import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { PerfilEnum } from 'src/app/modules/programa-gestao/enums/perfil.enum';
import { IDadosCombo } from 'src/app/shared/models/dados-combo.model';
import { IPessoa } from '../models/pessoa.model';
import { PessoaDataService } from '../services/pessoa.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-manutencao-pessoa-edicao',
  templateUrl: './manutencao-pessoa-edicao.component.html',
  styleUrls: ['./manutencao-pessoa-edicao.component.css']
})
export class ManutencaoPessoaEdicaoComponent implements OnInit {

  PerfilEnum = PerfilEnum;

  form: FormGroup;

  pessoa: IPessoa;
  unidades: IDadosCombo[];
  funcoes: IDadosCombo[];
  vinculos: IDadosCombo[];
  situacoes: IDadosCombo[];

  constructor(
    public router: Router,
    private route: ActivatedRoute,
    private formBuilder: FormBuilder,
    private toastr: ToastrService,
    private pessoaDataService: PessoaDataService
  ) { }


  ngOnInit() {
    this.form = this.formBuilder.group({
      pessoaId: [0, []],
      nome: ['', [Validators.required]],
      unidadeId: ['', [Validators.required]],
      cargaHorariaDb: [0, [Validators.required]],
      tipoFuncaoId: [null, []],
      cpf: ["", [Validators.required]],
      email: ["", []],
      matriculaSiape: ["-", []],
      dataNascimento: [null, []],
      situacaoPessoaId: [1, []],
      tipoVinculoId: [1, []]
    });

    this.pessoaDataService.ObterComboUnidades().subscribe(result => {
      this.unidades = result.retorno;
    });

    this.pessoaDataService.ObterComboFuncoes().subscribe(result => {
      this.funcoes = result.retorno;
    });

    this.pessoaDataService.ObterComboVinculos().subscribe(result => {
      this.vinculos = result.retorno;
    });

    this.pessoaDataService.ObterComboSituacao().subscribe(result => {
      this.situacoes = result.retorno;
    });


    const id = this.route.snapshot.params.id;
    if (id) {
      this.pessoaDataService.ObterPessoa(id).subscribe(result => {
        this.pessoa = result.retorno;

        this.form.patchValue({
          pessoaId: this.pessoa.pessoaId,
          nome: this.pessoa.nome,
          unidadeId: this.pessoa.unidadeId,
          cargaHorariaDb: result.retorno.cargaHoraria,
          tipoFuncaoId: this.pessoa.tipoFuncaoId,
          cpf: this.mascaraCpf(this.pessoa.cpf),
          email: this.pessoa.email,
          matriculaSiape: this.pessoa.matriculaSiape,
          dataNascimento: this.pessoa.dataNascimento,
          situacaoPessoaId: this.pessoa.situacaoPessoaId,
          tipoVinculoId: this.pessoa.tipoVinculoId
        });
      });
    }
  }

  escapeRegExp(string) {
    return string.replace(/[.*+?^${}()|[\]\\]/g, '\\$&'); // $& means the whole matched string
  }

  replaceAll(str, find, replace) {
    return str.replace(new RegExp(this.escapeRegExp(find), 'g'), replace);
  }

  mascaraCpf(valor) {
    return valor.replace(/(\d{3})(\d{3})(\d{3})(\d{2})/g, "\$1.\$2.\$3\-\$4");
  }

  salvar() {
    if (this.form.valid) {
      var dados: IPessoa = this.form.value;
      if (dados.tipoFuncaoId == "null") {
        dados.tipoFuncaoId = null
      }
      if (this.pessoa) {
        dados.pessoaId = this.pessoa.pessoaId;
        this.pessoaDataService.AtualizarPessoa(dados).subscribe(result => {
          if (result.retorno) {
            this.router.navigateByUrl('/manutencao-pessoa');
          }
        });
      }
      else {
        this.pessoaDataService.ObterPessoaDuplicada(this.replaceAll(this.replaceAll(dados.cpf, ".", ""), "-", "")).subscribe(duplicada => {
          if (duplicada.retorno) {
            this.toastr.error("CPF já existente na base de dados, cadastre o usuário com outro cpf", "Erro")
          } else {
            this.pessoaDataService.CadastrarPessoa(dados).subscribe(result => {
              if (result.retorno) {
                this.router.navigateByUrl('/manutencao-pessoa');
              }
            });
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

  cpfcnpjmask = function (rawValue) {
    var numbers = rawValue.match(/\d/g);
    var numberLength = 0;
    if (numbers) {
      numberLength = numbers.join('').length;
    }
    return [/[0-9]/, /[0-9]/, /[0-9]/, '.', /[0-9]/, /[0-9]/, /[0-9]/, '.', /[0-9]/, /[0-9]/, /[0-9]/, '-', /[0-9]/, /[0-9]/];
  }
}
