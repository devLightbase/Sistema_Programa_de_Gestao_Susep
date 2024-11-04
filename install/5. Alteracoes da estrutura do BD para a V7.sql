ALTER TABLE programagestao.itemcatalogo ALTER COLUMN entregasesperadas TYPE varchar(2000) USING entregasesperadas::varchar;
ALTER TABLE programagestao.itemcatalogo ALTER COLUMN titulo TYPE varchar(500) USING titulo::varchar;

ALTER TABLE programagestao.pactotrabalhoatividade ADD modalidadeexecucaoid int8 NULL;

EXEC sys.sp_addextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'ProgramaGestao', @level1type=N'TABLE',@level1name=N'PactoTrabalhoAtividade', @level2type=N'COLUMN',
@level2name=N'modalidadeExecucaoId', @value=N'Registra a modalidade em que a atividade foi executada'
GO


ALTER TABLE dbo.unidade ADD pessoaidchefe bigint NULL;
ALTER TABLE dbo.unidade ADD pessoaidchefesubstituto bigint NULL;



EXEC sys.sp_addextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Unidade', @level2type=N'COLUMN',
@level2name=N'pessoaIdChefe', @value=N'Registra o ID da pessoa que é o chefe da unidade'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Unidade', @level2type=N'COLUMN',
@level2name=N'pessoaIdChefeSubstituto', @value=N'Registra o ID da pessoa que é o chefe substituto da unidade'
GO


CREATE TABLE dbo.situacaopessoa (
	situacaopessoaid int8 NOT NULL,
	spsdescricao varchar(50) NOT NULL,
	CONSTRAINT pk_situacaopessoa PRIMARY KEY (situacaopessoaid)
);



insert into dbo.situacaopessoa values (1 , 'Ativa');
insert into dbo.situacaopessoa values (4 , 'Cedida');
insert into dbo.situacaopessoa values (5 , 'Desligada');
insert into dbo.situacaopessoa values (2 , 'Falecida');
insert into dbo.situacaopessoa values (3 , 'Inativa');


CREATE TABLE dbo.tipovinculo (
	tipovinculoid bigint NOT NULL,
	tvndescricao varchar(50) NOT NULL,
	CONSTRAINT pk_tipovinculo PRIMARY KEY (tipovinculoid)
);
CREATE UNIQUE INDEX uq_tipovinculo_tvndescricao ON dbo.tipovinculo USING btree (tvndescricao);



ALTER TABLE dbo.pessoa ADD situacaopessoaid bigint NULL;
ALTER TABLE dbo.pessoa ADD tipovinculoid bigint NULL;


