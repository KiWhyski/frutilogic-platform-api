Feature: Consultar información detallada sobre FrutiLogic
  Como visitante del sitio web
  Quiero entender cómo FrutiLogic resuelve problemas de inventario
  Para tomar una decisión informada sobre el uso de la aplicación

  Scenario: Revisar funcionalidades destacadas
    Given que el visitante ingresa a la sección de funcionalidades
    When navega por la descripción de cada módulo
    Then el visitante conoce las principales funcionalidades de la aplicación

  Scenario: Revisar descripción ampliada de funcionalidades
    Given que el visitante necesita más detalle sobre el uso de la aplicación
    When accede al contenido detallado de cada funcionalidad
    Then el visitante conoce qué acciones puede realizar con la aplicación
