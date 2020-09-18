

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[LogMeusJogos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Acao] [varchar](255) NULL,
	[MatriculaUsuario] [int] NULL,
	[Chaves] [varchar](255) NULL,
	[NomeColuna] [varchar](100) NULL,
	[Propriedade] [varchar](100) NULL,
	[ValorOriginal] [text] NULL,
	[ValorAtual] [text] NULL,
	[DtcOcorrencia] [datetime] NULL,
	[Tabela] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 100) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


