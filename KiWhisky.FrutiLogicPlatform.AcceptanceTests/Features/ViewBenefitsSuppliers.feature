Feature: Conocer beneficios para proveedores
  Como visitante del segmento de proveedores
  Quiero saber cómo la aplicación optimiza la demanda y las entregas
  Para evaluar si debo utilizar la aplicación

  Scenario: Ver beneficios específicos para proveedores
    Given que el visitante accede a la sección de beneficios
    When consulta el apartado dedicado a proveedores
    Then conoce los beneficios que ofrece la aplicación para gestionar la demanda y las entregas
