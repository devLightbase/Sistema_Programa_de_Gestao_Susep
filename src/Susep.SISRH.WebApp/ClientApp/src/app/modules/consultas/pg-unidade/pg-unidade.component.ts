import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { IPgUnidade } from './models/pg-unidade.model';
import { IProgramaGestaoModal } from './models/programa-gestao-modal.model';
import { IPactosVigentesModal } from './models/pactos-vigentes-modal.model';
import { IDadosPaginados } from '../../../shared/models/pagination.model';
import { PgUnidadeDataService } from './services/pg-unidade.service';
import { FormGroup, FormBuilder } from '@angular/forms';
import { IPgUnidadePesquisa } from './models/pg-unidade.pesquisa.model';
import { IProgramaGestaoPesquisa } from './models/programa-gestao-modal.pesquisa.model';
import { IPactosVigentesPesquisa } from './models/pactos-vigentes-modal.pesquisa.model';
import { IContagem } from './models/contagem.model';
import { BehaviorSubject } from 'rxjs';
import { PerfilEnum } from '../../programa-gestao/enums/perfil.enum';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-pg-unidade',
  templateUrl: './pg-unidade.component.html',
  styleUrls: ['./pg-unidade.component.css']
})

export class PgUnidadeComponent implements OnInit {

  PerfilEnum = PerfilEnum;

  @ViewChild('modalProgramaGestao', { static: true }) modalProgramaGestao;
  @ViewChild('modalPactosVigentes', { static: true }) modalPactosVigentes;
  
  dadosEncontrados: IDadosPaginados<IPgUnidade>;
  programaGestaoModal: IDadosPaginados<IProgramaGestaoModal>;
  pactosVigentesModal: IDadosPaginados<IPactosVigentesModal>;
  pgModalPesquisa: IProgramaGestaoPesquisa = {};
  pactosVigentesModalPesquisa: IPactosVigentesPesquisa = {};
  dadosUltimaPesquisa: IPgUnidadePesquisa = {};
  paginacao = new BehaviorSubject<IDadosPaginados<IPgUnidade>>(null);
  sigla: string;
  contagem: IContagem;

  form: FormGroup;

  constructor(
    public router: Router,
    private modalService: NgbModal,
    private formBuilder: FormBuilder,
    private pgUnidadeDataService: PgUnidadeDataService
  ) { }

  ngOnInit() {
    this.form = this.formBuilder.group({
      sigla: [""],
      descricao: [""]
    });

    this.pgUnidadeDataService.ObterContagem(this.dadosUltimaPesquisa)
    .subscribe(resultado => {
      this.contagem = resultado.retorno
    });

    this.pesquisar(1);
  }

  pesquisar(pagina: number) {
    if (this.form.valid) {
      this.dadosUltimaPesquisa = this.form.value;
      this.dadosUltimaPesquisa.page = pagina;

      this.pgUnidadeDataService.ObterPagina(this.dadosUltimaPesquisa)
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

  mostrarProgramaGestao (sigla: string, pagina: number) {
    this.pgModalPesquisa.sigla = sigla;
    this.pgModalPesquisa.page = pagina;
    this.sigla = sigla;
    this.pgUnidadeDataService.ObterProgramaGestao(this.pgModalPesquisa)
    .subscribe(resultado => {
      this.programaGestaoModal = resultado.retorno      
      this.modalService.open(this.modalProgramaGestao, { size: 'sm' });
    });
  }

  mostrarPactosVigentes (sigla: string, pagina: number) {
    this.pactosVigentesModalPesquisa.sigla = sigla;
    this.pactosVigentesModalPesquisa.page = pagina;
    this.sigla = sigla;
    this.pgUnidadeDataService.ObterPactosVigentes(this.pactosVigentesModalPesquisa)
    .subscribe(resultado => {
      this.pactosVigentesModal = resultado.retorno      
      this.modalService.open(this.modalPactosVigentes, { size: 'sm' });
    });
  }

  formatData(data){
    var dataformat = new Date(data)
    return dataformat.toLocaleDateString()
  }

  fecharModal() {
    this.modalService.dismissAll();
  }

}