DROP TABLE IF EXISTS Arbre;
DROP TABLE IF EXISTS Prenom;
DROP TABLE IF EXISTS Pays;
DROP TABLE IF EXISTS Image;
DROP TABLE IF EXISTS Ville;
DROP TABLE IF EXISTS Personne;


CREATE TABLE [dbo].[Arbre]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[Nom] VARCHAR(100) NOT NULL
)

CREATE TABLE [dbo].[Prenom]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[Prenom] VARCHAR(100) NOT NULL UNIQUE
)


CREATE TABLE [dbo].[Pays]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[Code] INT,
	[Alpha2] VARCHAR(2) UNIQUE,
	[Alpha3] VARCHAR(3) UNIQUE,
	[Nom_en_gb] VARCHAR(50),
	[Nom_fr_fr] VARCHAR(50) NOT NULL,
	[Nationalite] VARCHAR(70) NOT NULL
)

CREATE TABLE [dbo].[Image]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[Image] IMAGE NOT NULL
)

CREATE TABLE [dbo].[Ville]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[Nom] VARCHAR(200) UNIQUE NOT NULL,
	[Latitude] DECIMAL(10,7),
	[Longitude] DECIMAL(10,7),
	[Pays] INT FOREIGN KEY REFERENCES [Pays]([Id])
)

CREATE TABLE [dbo].[Sexe]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[Sexe] VARCHAR(30) UNIQUE NOT NULL
)

CREATE TABLE [dbo].[Description]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[Description] VARCHAR(2000) NOT NULL
)

CREATE TABLE [dbo].[Personne]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[Numero] INT NOT NULL UNIQUE,
	[Nom_usage] VARCHAR(200) NOT NULL,
	[Nom] VARCHAR(200) NOT NULL,
	[Date_naissance] DATE,
	[Date_deces] DATE,
	[Id_ville] INT,
	[Id_img_principale] INT,
	[Id_pere] INT,
	[Id_mere] INT,
	[Id_sexe] INT,
	CONSTRAINT fk_perso_ville FOREIGN KEY(Id_ville) REFERENCES Ville(Id),
	CONSTRAINT fk_perso_img FOREIGN KEY(Id_img_principale) REFERENCES Image(Id),
	CONSTRAINT fk_perso_pere FOREIGN KEY(Id_pere) REFERENCES Personne(Id),
	CONSTRAINT fk_perso_mere FOREIGN KEY(Id_mere) REFERENCES Personne(Id),
	CONSTRAINT fk_perso_sexe FOREIGN KEY(Id_sexe) REFERENCES Sexe(Id)
)

CREATE TABLE [dbo].[Description_Personne]
(
	[Id_description] INT,
	[Id_personne] INT,
	CONSTRAINT pk_descper PRIMARY KEY(Id_description, Id_personne),
	CONSTRAINT fk_descper_prenom FOREIGN KEY(Id_description) REFERENCES Description(Id),
	CONSTRAINT fk_descper_personne FOREIGN KEY(Id_personne) REFERENCES Personne(Id)
)

CREATE TABLE [dbo].[Arbre_Personne]
(
	[Id_arbre] INT,
	[Id_personne] INT,
	CONSTRAINT pk_arper PRIMARY KEY(Id_arbre, Id_personne),
	CONSTRAINT fk_arper_arbre FOREIGN KEY(Id_arbre) REFERENCES Arbre(Id),
	CONSTRAINT fk_arper_personne FOREIGN KEY(Id_personne) REFERENCES Personne(Id)
)

CREATE TABLE [dbo].[Image_Personne]
(
	[Id_image] INT,
	[Id_personne] INT,
	CONSTRAINT pk_imgper PRIMARY KEY(Id_image, Id_personne),
	CONSTRAINT fk_imgper_image FOREIGN KEY(Id_image) REFERENCES Image(Id),
	CONSTRAINT fk_imgper_personne FOREIGN KEY(Id_personne) REFERENCES Personne(Id)
)

CREATE TABLE [dbo].[Nationalite]
(
	[Id_pays] INT NOT NULL,
	[Id_personne] INT NOT NULL,
	CONSTRAINT pk_nationalite PRIMARY KEY(Id_pays, Id_personne),
	CONSTRAINT fk_nationalite_pays FOREIGN KEY(Id_pays) REFERENCES Pays(Id),
	CONSTRAINT fk_nationalite_personne FOREIGN KEY(Id_personne) REFERENCES Personne(Id)
)

CREATE TABLE [dbo].[Prenom_Personne]
(
	[Id_prenom] INT NOT NULL,
	[Id_personne] INT NOT NULL,
	CONSTRAINT pk_preper PRIMARY KEY(Id_prenom, Id_personne),
	CONSTRAINT fk_preper_prenom FOREIGN KEY(Id_prenom) REFERENCES Prenom(Id),
	CONSTRAINT fk_preper_personne FOREIGN KEY(Id_personne) REFERENCES Personne(Id)
)