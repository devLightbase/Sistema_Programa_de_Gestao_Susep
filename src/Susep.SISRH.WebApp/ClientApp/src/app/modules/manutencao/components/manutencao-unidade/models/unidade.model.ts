export interface IUnidade
{
  unidadeId: number;
  sigla: string;
  nome: string;
  unidadeIdPai: number;
  tipoUnidadeId: number;
  situacaoUnidadeId: number;
  ufId: string;
  nivel: number;
  tipoFuncaoUnidadeId: number;
  email: string;
  pessoaIdChefe: number,
  pessoaIdChefeSubstituto: number,
  codSiorg: number,
  codSgc: number
}