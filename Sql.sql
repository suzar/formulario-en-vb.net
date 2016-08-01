Create Database Ex
use Ex 

--- Tablas ---

Create Table Genero(
ID_Genero	int not null identity (1,1), 
Genero		Varchar (20),
Constraint PK_Clave_Genero primary key (ID_Genero),
Check (Genero = 'MASCULINO' OR Genero = 'FEMENINO'))

Create Table Persona(
ID_Persona	int Not Null, 
Nombre		Varchar (25) not null, 
Edad		int not null,
ID_Genero	int not null,
Constraint PK_Clave_Persona primary key (ID_Persona),
Constraint FK_Clave_Genero	foreign key (ID_Genero) references genero (ID_Genero),
Check (Edad > 0 And Edad < 125))

--- Procedimientos Almacenados ---

--- Insertar ---

Create Procedure sp_Inserta_Persona 
@ID			int,
@Nombre		Varchar (25),
@Edad		int,
@Id_Genero	int
as
	begin 
		insert into Persona (ID_Persona,Nombre,Edad,ID_Genero) values
			(@ID,@Nombre,@Edad,@Id_Genero)
	End 
 
--- Consultar ---

Create Procedure sp_Consulta_Persona 
as
	begin 
		Select ID_Persona as ID, Nombre, Edad, Genero 
		from Persona inner join Genero
		on Persona.ID_Genero = Genero.ID_Genero
	end 

--- Buscar ---

Create procedure sp_Buscar_Persona 
@ID int
as
	Begin 
		Select ID_Persona as ID, Nombre, Edad, Genero 
		from Persona inner join Genero
		on Persona.ID_Genero = Genero.ID_Genero
		Where ID_Persona = @ID
	End

--- Eliminar ---

Create Procedure sp_Eliminar_Persona 
@ID int 
as
	begin 
		Delete 
		From Persona 
		where ID_Persona = @ID
	End

--- Actualizar ---

--- Actualizar Nombre ---

Create procedure sp_Actualiza_Nombre_Persona 
@ID		int,
@Nombre Varchar (25)
as
	Begin 
		Update  Persona 
		Set Nombre = @Nombre 
		Where ID_Persona = @ID
	End 

--- Actualizar Edad ---

Create procedure sp_Actualiza_Edad_Persona 
@ID		int,
@Edad int
as
	Begin 
		Update  Persona 
		Set Edad = @Edad 
		Where ID_Persona = @ID
	End 

--- Actualizar Genero ---

Create procedure sp_Actualiza_Genero_Persona 
@ID		int,
@Genero int
as
	Begin 
		Update  Persona 
		Set ID_Genero = @Genero 
		Where ID_Persona = @ID
	End 

--- Disparador para evitar duplicados ---

Create trigger tr_Duplicados_Persona 
on Persona 
for insert,Update
as
	begin
		
		Declare @ID int
		Declare @Nombre varchar (25)
		Declare @Edad int
		Declare @Genero int 

		Select @ID = P.ID_Persona,
		@Nombre = P.Nombre,
		@Edad = P.Edad,
		@Genero = P.ID_Genero
		From Persona P

		If Exists (Select * 
		From Persona 
		Where ID_Persona != Persona.ID_Persona
		And @Nombre = Persona.Nombre
		And @Edad = Persona.Edad
		And @Genero = Persona.ID_Genero)

		Begin 
			
			Print 'La persona ya se encuentra registrada'
			RollBack Transaction 
		End 

		Else 
			Begin
				Print 'Registro insertado correctamente'

				Update Persona
				Set Nombre = UPPER (Nombre)
			END

		Execute sp_Consulta_Persona
	END