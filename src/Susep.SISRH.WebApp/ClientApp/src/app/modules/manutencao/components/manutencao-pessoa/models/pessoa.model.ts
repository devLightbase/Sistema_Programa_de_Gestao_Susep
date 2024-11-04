import { IPlanoTrabalhoAtividadeCandidato, IPlanoTrabalhoAtividadeItem } from "../../../../programa-gestao/models/plano-trabalho.model";
import { Guid } from "../../../../../shared/helpers/guid.helper";

export interface IPessoa
{
  pessoaId?: number;
  nome?: string;
  unidadeId?: number;
  unidade?: string;
  cargaHorariaDb?: number;
  cargaHoraria?: number;
  tipoFuncaoId: any,
  cpf: string,
  email: string,
  matriculaSiape: string,
  dataNascimento: Date,
  situacaoPessoaId: number,
  tipoVinculoId: number
}