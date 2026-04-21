Feature: Comparar planes gratuito y premium
  Como visitante del sitio web
  Quiero comparar los planes ofrecidos por FrutiLogic
  Para elegir la opción que más me conviene

  Scenario: Consultar tabla comparativa de planes
    Given que el visitante ingresa a la sección de planes
    When revisa la tabla de comparación entre el plan gratuito y el plan premium
    Then puede identificar las diferencias y elegir el plan adecuado
