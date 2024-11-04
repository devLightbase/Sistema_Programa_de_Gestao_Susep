import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { IPessoa } from '../models/pessoa.model';
import { IDadosPaginados } from '../../../../../shared/models/pagination.model';
import { PessoaDataService } from '../services/pessoa.service';
import { FormGroup, FormBuilder } from '@angular/forms';
import { IPessoaPesquisa } from '../models/pessoa.pesquisa.model';
import { BehaviorSubject } from 'rxjs';
import { UnidadeDataService } from '../../../../unidade/services/unidade.service';
import { IDadosCombo } from '../../../../../shared/models/dados-combo.model';
import { DominioDataService } from '../../../../../shared/services/dominio.service';
import { IDominio } from '../../../../../shared/models/dominio.model';
import { PlanoTrabalhoSituacaoCandidatoEnum } from '../../../../programa-gestao/enums/plano-trabalho-situacao-candidato.enum';
import { PerfilEnum } from 'src/app/modules/programa-gestao/enums/perfil.enum';

@Component({
  selector: 'app-manutencao-pessoa-pesquisa',
  templateUrl: './manutencao-pessoa-pesquisa.component.html',
  styleUrls: ['./manutencao-pessoa-pesquisa.component.css']
})
export class ManutencaoPessoaPesquisaComponent implements OnInit {
  
  planoTrabalhoSituacaoCandidatoEnum = PlanoTrabalhoSituacaoCandidatoEnum;

  PerfilEnum = PerfilEnum;
  form: FormGroup;
  dadosEncontrados: IDadosPaginados<IPessoa>;
  dadosUltimaPesquisa: IPessoaPesquisa = {};
  paginacao = new BehaviorSubject<IDadosPaginados<IPessoa>>(null);
  unidadesAtivasCombo: IDadosCombo[];
  situacaoPlanoTrabalhoCombo: IDominio[];
  pessoa: IPessoa;

  constructor(
    public router: Router,
    private formBuilder: FormBuilder,
    private pessoaDataService: PessoaDataService,
    private unidadeDataService: UnidadeDataService,
    private dominioDataService: DominioDataService,
  ) { }

  ngOnInit() {

    this.montarComboUnidades();

    this.configurarForm();

    this.pesquisar(1);
  }

  configurarForm() {
    this.form = this.formBuilder.group({
      nome: [''],
      unidadeId: ['', []],
      catalogoDominioId: ['', []],
    });
  }  

  montarComboUnidades() {
    this.unidadeDataService.ObterTodasAtivasDadosCombo().subscribe(
      appResult => {
        this.unidadesAtivasCombo = appResult.retorno;
      }
    );

  }

  pesquisar(pagina: number) {

    this.dadosUltimaPesquisa = this.form.value;
    this.dadosUltimaPesquisa.page = pagina;

    this.pessoaDataService.ObterPagina(this.dadosUltimaPesquisa)
      .subscribe(
        resultado => {
          this.dadosEncontrados = resultado.retorno;
          this.paginacao.next(this.dadosEncontrados);
        }
      );
  }

  excluirItem(id: string){
    this.pessoaDataService.ObterPessoa(id).subscribe(result => {
      this.pessoa = result.retorno;
      this.pessoa.situacaoPessoaId = 3
      this.pessoa.cargaHorariaDb = this.pessoa.cargaHoraria
      this.pessoaDataService.AtualizarPessoa(this.pessoa).subscribe(result => {
        if (result.retorno) {
          window.location.reload()
        }
      });

    });
  }

  onSubmit() {
    this.pesquisar(1);
  }

}

  

