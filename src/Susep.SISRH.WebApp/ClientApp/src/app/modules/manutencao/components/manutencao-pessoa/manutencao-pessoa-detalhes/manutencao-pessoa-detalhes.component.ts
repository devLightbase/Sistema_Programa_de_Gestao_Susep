import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ISituacao } from 'src/app/modules/dados-auxiliares/situacao-pessoa/models/situacao.model';
import { PerfilEnum } from 'src/app/modules/programa-gestao/enums/perfil.enum';
import { IDadosCombo } from 'src/app/shared/models/dados-combo.model';
import { IPessoa } from '../../manutencao-pessoa/models/pessoa.model';
import { PessoaDataService } from '../../manutencao-pessoa/services/pessoa.service';
import { IUnidade } from '../../manutencao-unidade/models/unidade.model';
import { UnidadeDataServices } from '../../manutencao-unidade/service/unidade.service';
import { SituacaoDataService } from 'src/app/modules/dados-auxiliares/situacao-pessoa/services/situacao.service'
import { ITipoVinculo } from 'src/app/modules/dados-auxiliares/tipo-vinculo/models/tipo-vinculo.model';
import { TipoVinculoDataService } from 'src/app/modules/dados-auxiliares/tipo-vinculo/services/tipo-vinculo.service';

@Component({
  selector: 'app-manutencao-pessoa-detalhes',
  templateUrl: './manutencao-pessoa-detalhes.component.html',
  styleUrls: ['./manutencao-pessoa-detalhes.component.css']
})
export class ManutencaoPessoaDetalhesComponent implements OnInit {
  PerfilEnum = PerfilEnum;

  form: FormGroup;

  pessoa: IPessoa;

  unidade: IUnidade;

  situacao: ISituacao;

  vinculo: ITipoVinculo;

  data: string;

  constructor(
    public router: Router,
    private route: ActivatedRoute,
    private formBuilder: FormBuilder,
    private unidadeDataService: UnidadeDataServices,
    private pessoaDataService: PessoaDataService,
    private situacaoDataService: SituacaoDataService,
    private tipoVinculoDataService: TipoVinculoDataService,
  ) { }

  ngOnInit() {
    const id = this.route.snapshot.params.id;
    this.data = ""
    if (id) {
      this.pessoaDataService.ObterPessoa(id).subscribe(result => {
        this.pessoa = result.retorno;
        if(this.pessoa.cpf.length == 11){
          this.pessoa.cpf = this.mascaraCpf(this.pessoa.cpf)
        }
        if(this.pessoa.dataNascimento){
          var dataformatada = new Date(this.pessoa.dataNascimento)
          this.data = dataformatada.toLocaleDateString()
        }
        if (this.pessoa.unidadeId) {
          this.unidadeDataService.ObterPorId(this.pessoa.unidadeId).subscribe(result => {
            this.unidade = result.retorno;
          });
        }
        if (this.pessoa.situacaoPessoaId) {
          this.situacaoDataService.ObterPorId(this.pessoa.situacaoPessoaId).subscribe(result => {
            this.situacao = result.retorno;
          });
        }
        if (this.pessoa.tipoVinculoId) {
          this.tipoVinculoDataService.ObterPorId(this.pessoa.tipoVinculoId).subscribe(result => {
            this.vinculo = result.retorno;
          });
        }
      });
    }
  }

  mascaraCpf(valor) {
    return valor.replace(/(\d{3})(\d{3})(\d{3})(\d{2})/g, "\$1.\$2.\$3\-\$4");
  }

}
