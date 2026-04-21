Feature: Crear guía de conservación por tipo de licor
  Como proveedor
  Quiero registrar guías de conservación para mis productos
  Para asegurar condiciones adecuadas de almacenamiento

  Scenario: Registrar nueva guía de conservación
    Given que el proveedor ingresa la información requerida de una guía
    When confirma el registro
    Then el sistema guarda la guía asociada al tipo de licor

  Scenario: Validar campos obligatorios
    Given que la guía tiene información incompleta
    When el proveedor intenta registrarla
    Then el sistema impide el registro y solicita completar los datos
