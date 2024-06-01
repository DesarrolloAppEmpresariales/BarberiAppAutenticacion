Feature: Usuario
Para garantizar el correcto funcionamiento de la API de Usuarios
Como desarrollador
Quiero asegurarme de que las respuestas de la API sean correctas


@mytag
Scenario: Obtener la lista de usuarios con un token válido
	Given que la API está disponible
	And tengo un token válido
	When hago una solicitud GET a "/api/usuario/"
	Then el código de respuesta debe ser 200
	And la respuesta debe contener una lista de usuarios