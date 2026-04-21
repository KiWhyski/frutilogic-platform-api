Feature: Consultar precios y condiciones del plan premium
  Como visitante del sitio web
  Quiero conocer el precio y las condiciones de uso del plan premium
  Para decidir si es viable para mi negocio

  Scenario: Revisar precio y condiciones de cancelación
    Given que el visitante accede a la sección de planes
    When se muestra la información del plan premium
    Then el visitante ve el costo, las condiciones de uso y las políticas de cancelación

  Scenario: Consultar términos y condiciones
    Given que el visitante desea conocer los términos del servicio
    When ingresa a la sección de términos y condiciones
    Then entiende las reglas de uso y cancelación de la suscripción
