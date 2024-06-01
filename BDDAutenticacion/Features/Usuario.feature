Feature: Usuario
Para garantizar el correcto funcionamiento de la API de Usuarios
Como desarrollador
Quiero asegurarme de que las respuestas de la API sean correctas


@ListaUsuarios
Scenario: Obtener la lista de usuarios con un token válido
	Given que la API está disponible
	And tengo un token válido
	When hago una solicitud GET a "/api/usuario/"
	Then el código de respuesta debe ser 200
	And la respuesta debe contener una lista de usuarios


@UsuarioId
Scenario: Obtener un usuario con un token válido y Id
	Given que la API está disponible
	And tengo un token válido
	When hago una solicitud GET a "/api/usuario/5"
	Then el código de respuesta debe ser 200
	And la respuesta debe contener el correo "cliente01@yopmail.com"
	And la respuesta debe contener el alias "adminPlat"
	And la respuesta debe contener la contraseña "1234"
	And la la respuesta debe contener el rol_id 2