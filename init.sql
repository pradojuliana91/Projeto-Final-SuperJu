USE [master];

GO
IF NOT EXISTS (SELECT * FROM sys.sql_logins WHERE name = 'superju_user')
BEGIN
	CREATE LOGIN [superju_user] WITH PASSWORD = 'superju_user123', CHECK_POLICY = OFF;
	ALTER SERVER ROLE [sysadmin] ADD MEMBER [superju_user];

END

GO
IF DB_ID('superju') IS NULL
BEGIN
    CREATE DATABASE [superju]; 
END

GO
USE [superju];	

GO
IF OBJECT_ID('dbo.PRODUTOS') IS NULL
BEGIN
	CREATE TABLE [dbo].[PRODUTOS] (
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[Nome] [nvarchar] (30) NOT NULL,
		[Descricao] [nvarchar] (100) NOT NULL,
		[Quantidade] [int] NOT NULL,
		[ValorCusto] [money] NOT NULL,
		[ValorVenda] [money] NOT NULL,
		CONSTRAINT PK_PRODUTOS PRIMARY KEY ([Id])
	);
END

GO
IF OBJECT_ID('dbo.CLIENTES') IS NULL
BEGIN
	CREATE TABLE [dbo].[CLIENTES] (
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[Nome] [nvarchar] (50) NOT NULL,
		[CPF] [nvarchar] (11) NOT NULL,
		[DataNascimento] [date] NOT NULL,
		[Telefone] [nvarchar] (11) NOT NULL,
		[Endereco] [nvarchar] (50) NOT NULL,
		[Complemento] [nvarchar] (50) NULL,
		[CEP] [nvarchar] (8) NOT NULL,
		[Bairro] [nvarchar] (30) NOT NULL,
		[Cidade] [nvarchar] (30) NOT NULL,
		[Estado] [nvarchar] (2) NOT NULL,
		CONSTRAINT PK_CLIENTES PRIMARY KEY ([Id])
	);
END

GO
IF OBJECT_ID('dbo.ENTRADAS_PRODUTO') IS NULL
BEGIN
	CREATE TABLE [dbo].[ENTRADAS_PRODUTO] (
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[NumeroNota] [nvarchar] (20) NOT NULL,
		[DataEntrada] [datetime] NOT NULL,	
		CONSTRAINT [PK_ENTRADAS_PRODUTO]  PRIMARY KEY ([Id])
	);
END

GO
IF OBJECT_ID('dbo.ENTRADAS_PRODUTO_ITEM') IS NULL
BEGIN
	CREATE TABLE [dbo].[ENTRADAS_PRODUTO_ITEM] (
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[EntradaProdutoId] [int] NOT NULL,
		[ProdutoId] [int] NOT NULL,
		[Quantidade] [int] NOT NULL,
		[ValorCusto] [money] NOT NULL,
		CONSTRAINT [PK_ENTRADAS_PRODUTO_ITEM] PRIMARY KEY ([Id])
	);
	ALTER TABLE [dbo].[ENTRADAS_PRODUTO_ITEM]  WITH CHECK ADD  CONSTRAINT [FK_ENTRADAS_PRODUTO_ITEM_ENTRADAS_PRODUTO] FOREIGN KEY([EntradaProdutoId]) REFERENCES [dbo].[ENTRADAS_PRODUTO]([Id]);
	ALTER TABLE [dbo].[ENTRADAS_PRODUTO_ITEM]  WITH CHECK ADD  CONSTRAINT [FK_ENTRADAS_PRODUTO_ITEM_PRODUTOS] FOREIGN KEY([ProdutoId]) REFERENCES [dbo].[PRODUTOS]([Id]);
END

GO
IF OBJECT_ID('dbo.FORMAS_PAGAMENTO') IS NULL
BEGIN
	CREATE TABLE [dbo].[FORMAS_PAGAMENTO] (
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[Nome] [nvarchar] (50) NOT NULL,
		[Descricao] [nvarchar] (100) NOT NULL,	
		CONSTRAINT [PK_FORMAS_PAGAMENTO] PRIMARY KEY ([Id])
	);
	INSERT INTO [dbo].[FORMAS_PAGAMENTO] VALUES ('Dinheiro','Pagamento em dinheiro');
	INSERT INTO [dbo].[FORMAS_PAGAMENTO] VALUES ('Cartão','Pagamento em cartão');
	INSERT INTO [dbo].[FORMAS_PAGAMENTO] VALUES ('Pix','Pagamento em pix');
END

GO
IF OBJECT_ID('dbo.PEDIDOS') IS NULL
BEGIN
	CREATE TABLE [dbo].[PEDIDOS] (
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[ClienteId] [int] NOT NULL,
		[FormaPagamentoId] [int] NOT NULL,
		[DataPedido] [datetime] NOT NULL,
		[ValorTotal] [money] NOT NULL,	
		CONSTRAINT [PK_PEDIDOS] PRIMARY KEY ([Id])
	);
	ALTER TABLE [dbo].[PEDIDOS]  WITH CHECK ADD  CONSTRAINT [FK_PEDIDOS_CLIENTES] FOREIGN KEY([ClienteId]) REFERENCES [dbo].[CLIENTES]([Id]);
	ALTER TABLE [dbo].[PEDIDOS]  WITH CHECK ADD  CONSTRAINT [FK_PEDIDOS_FORMAS_PAGAMENTO] FOREIGN KEY([FormaPagamentoId]) REFERENCES [dbo].[FORMAS_PAGAMENTO]([Id]);
END

GO
IF OBJECT_ID('dbo.PEDIDOS_ITEM') IS NULL
BEGIN
	CREATE TABLE [dbo].[PEDIDOS_ITEM] (
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[PedidoId] [int] NOT NULL,
		[ProdutoId] [int] NOT NULL,
		[Quantidade] [int] NOT NULL,
		[Valor] [money] NOT NULL,
		[ValorTotal] [money] NOT NULL,			
		CONSTRAINT [PK_PEDIDOS_ITEM] PRIMARY KEY ([Id])
	);
	ALTER TABLE [dbo].[PEDIDOS_ITEM]  WITH CHECK ADD  CONSTRAINT [FK_PEDIDOS_ITEM_PEDIDOS] FOREIGN KEY([PedidoId]) REFERENCES [dbo].[PEDIDOS]([Id]);
	ALTER TABLE [dbo].[PEDIDOS_ITEM]  WITH CHECK ADD  CONSTRAINT [FK_PEDIDOS_ITEM_PRODUTOS] FOREIGN KEY([ProdutoId]) REFERENCES [dbo].[PRODUTOS]([Id]);
END