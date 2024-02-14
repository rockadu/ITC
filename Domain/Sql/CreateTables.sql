CREATE DATABASE App;

USE App;

CREATE TABLE Unidade (
    Codigo INT PRIMARY KEY IDENTITY(1,1),
    Chave VARCHAR(8) NOT NULL,
    Nome VARCHAR(64) NOT NULL,
    Ativa BIT NOT NULL
)

CREATE TABLE Setor (
    Codigo INT PRIMARY KEY IDENTITY(1,1),
    Chave VARCHAR(8) NOT NULL,
    Nome VARCHAR(64) NOT NULL,
    Ativo BIT NOT NULL,
    CodigoUnidade INT NOT NULL,
    CONSTRAINT FK_SETOR_UNID FOREIGN KEY (CodigoUnidade) REFERENCES Unidade(Codigo)
)

CREATE TABLE Cargo (
    Codigo INT PRIMARY KEY IDENTITY(1,1),
    Chave VARCHAR(8) NOT NULL,
    Nome VARCHAR(64) NOT NULL,
    Ativo BIT NOT NULL
)

CREATE TABLE Permissao (
    Codigo INT PRIMARY KEY IDENTITY(1,1),
    Chave VARCHAR(8) NOT NULL,
    Nome VARCHAR(64) NOT NULL
)

CREATE TABLE Perfil (
    Codigo INT PRIMARY KEY IDENTITY(1,1),
    Nome VARCHAR(8) NOT NULL,
    Descricao VARCHAR(64) NOT NULL
)

CREATE TABLE PerfilPermissoes (
    CodigoPerfil INT NOT NULL,
    CodigoPermissao INT NOT NULL,
    CONSTRAINT FK_PERFPERM_PERF FOREIGN KEY (CodigoPerfil) REFERENCES Perfil(Codigo),
    CONSTRAINT FK_PERFPERM_PERM FOREIGN KEY (CodigoPermissao) REFERENCES Permissao(Codigo)
)

CREATE TABLE TemaSistema(
    Codigo INT IDENTITY(1,1) PRIMARY KEY,
    Categoria VARCHAR(64) NOT NULL,
    Valor VARCHAR(128) NOT NULL,
)

CREATE TABLE Usuario (
    Codigo INT PRIMARY KEY IDENTITY(1,1),
    Nome VARCHAR(128) NOT NULL,
    Apelido VARCHAR(128),
    Email VARCHAR(128),
    Senha VARCHAR(128),
    Foto VARCHAR(2048),
    CodigoCargo INT,
    CodigoSetor INT,
    CodigoSuperiorImediato INT,
    EstiloBody VARCHAR(256),
    EstiloHtml VARCHAR(128),
    Ativo BIT NOT NULL,
    CONSTRAINT FK_USU_CARGO FOREIGN KEY (CodigoCargo) REFERENCES Cargo(Codigo),
    CONSTRAINT FK_USU_SETOR FOREIGN KEY (CodigoSetor) REFERENCES Setor(Codigo),
    CONSTRAINT FK_USU_SUPIM FOREIGN KEY (CodigoSuperiorImediato) REFERENCES Usuario(Codigo)
)

CREATE TABLE UsuarioPerfis (
    CodigoUsuario INT NOT NULL,
    CodigoPerfil INT NOT NULL,
    CONSTRAINT FK_USUPERF_USU FOREIGN KEY (CodigoUsuario) REFERENCES Usuario(Codigo),
    CONSTRAINT FK_USUPERF_PERF FOREIGN KEY (CodigoPerfil) REFERENCES Perfil(Codigo)
)

CREATE TABLE UsuarioPermissoes (
    CodigoUsuario INT NOT NULL,
    CodigoPermissao INT NOT NULL,
    CONSTRAINT FK_USUPERM_USU FOREIGN KEY (CodigoUsuario) REFERENCES Usuario(Codigo),
    CONSTRAINT FK_USUPERM_PERM FOREIGN KEY (CodigoPermissao) REFERENCES Permissao(Codigo)
)

INSERT INTO Unidade (Chave, Nome, Ativa) VALUES ('MTZ', 'Matriz', 1);
INSERT INTO Setor (Chave, Nome, Ativo, CodigoUnidade) VALUES ('TI', 'Tecnologia da Informação', 1, 1);
INSERT INTO Permissao (Chave, Nome) VALUES ('root', 'Root');
/* MeuPrimeiroMilhão */
INSERT INTO Usuario (Nome, Apelido, Email, Senha, Ativo) VALUES ('Admin', 'Admin', 'contato@precept-consulting.com.br', 'BE3D405865A608D6839D5BDF216D376B', 1)