Feature: Revisar testimonios de usuarios reales
  Como visitante del sitio web
  Quiero leer testimonios de usuarios de FrutiLogic
  Para confiar en la aplicación

  Scenario: Leer testimonios publicados
    Given que el visitante accede a la sección de testimonios
    When se muestran experiencias reales de usuarios
    Then el visitante obtiene referencias sobre el uso de la aplicación
