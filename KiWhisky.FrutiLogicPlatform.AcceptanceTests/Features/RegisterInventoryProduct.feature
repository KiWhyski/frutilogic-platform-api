Feature: Registrar un nuevo producto en el inventario
  Como dueño de licorería o proveedor
  Quiero registrar productos con sus características
  Para mantener actualizado mi inventario

  Scenario: Registro exitoso de producto
    Given que el usuario está autenticado
    When completa todos los campos obligatorios de un producto
    Then el sistema valida la información y guarda el producto en el inventario

  Scenario: Intento de registro con datos incompletos
    Given que el usuario intenta registrar un producto
    When omite campos obligatorios
    Then el sistema impide el registro y solicita completar la información
