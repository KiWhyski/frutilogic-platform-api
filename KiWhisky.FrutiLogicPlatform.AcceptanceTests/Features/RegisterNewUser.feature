Feature: Registrar un nuevo usuario en FrutiLogic
  Como nuevo usuario
  Quiero registrarme en la plataforma
  Para comenzar a usar FrutiLogic según mi rol

  Scenario: Registro exitoso como dueño de licorería
    Given que un nuevo usuario solicita registrarse como dueño de licorería
    When completa los datos requeridos y envía el formulario
    Then el sistema crea la cuenta con el rol de dueño de licorería
    And habilita las funcionalidades de gestión de inventario

  Scenario: Registro exitoso como proveedor
    Given que un nuevo usuario solicita registrarse como proveedor
    When completa los datos requeridos y envía el formulario
    Then el sistema crea la cuenta con el rol de proveedor
    And habilita las funcionalidades de gestión de proveedores
