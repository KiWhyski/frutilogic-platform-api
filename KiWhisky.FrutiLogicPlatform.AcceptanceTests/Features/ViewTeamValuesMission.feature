Feature: Mostrar valores y equipo detrás de la aplicación
  Como visitante del sitio web
  Quiero conocer la misión, visión y el equipo de FrutiLogic
  Para sentir confianza al usar la aplicación

  Scenario: Consultar misión y visión
    Given que el visitante accede a la página principal
    When visualiza la sección "Sobre nosotros"
    Then conoce la misión y visión del equipo detrás de la aplicación

  Scenario: Conocer integrantes del equipo
    Given que el visitante accede a la sección del equipo
    When se listan los miembros con su rol
    Then el visitante identifica quiénes integran el equipo de desarrollo
