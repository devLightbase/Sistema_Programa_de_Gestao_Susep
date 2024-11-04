ALTER TABLE programagestao.itemcatalogo ALTER COLUMN entregasesperadas TYPE varchar(2000) USING entregasesperadas::varchar;
ALTER TABLE programagestao.itemcatalogo ALTER COLUMN titulo TYPE varchar(500) USING titulo::varchar;

ALTER TABLE programagestao.pactotrabalhoatividade ADD modalidadeexecucaoid int8 NULL;

ALTER TABLE dbo.unidade ADD pessoaidchefe bigint NULL;
ALTER TABLE dbo.unidade ADD pessoaidchefesubstituto bigint NULL;


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


