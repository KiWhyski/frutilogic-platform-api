Feature: Configurar alertas de reposición
  Como dueño de licorería o proveedor
  Quiero definir umbrales de stock mínimo
  Para evitar quiebres de stock

  Scenario: Guardar umbral de stock mínimo
    Given que un producto está registrado
    When se define un valor de stock mínimo para el producto
    Then el sistema guarda el umbral y activa la monitorización

  Scenario: Generar alerta por stock bajo
    Given que el producto tiene un stock mínimo configurado
    When la cantidad disponible cae por debajo del umbral
    Then el sistema envía una alerta indicando la necesidad de reposición
