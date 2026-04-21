Feature: Actualizar contraseña de la cuenta
  Como usuario
  Quiero cambiar mi contraseña periódicamente
  Para mantener segura mi información

  Scenario: Cambio de contraseña exitoso
    Given que el usuario ha iniciado sesión
    When ingresa su contraseña actual y una nueva válida
    Then el sistema actualiza la contraseña y confirma el cambio

  Scenario: Contraseña actual incorrecta
    Given que el usuario intenta cambiar la contraseña
    When introduce una contraseña actual incorrecta
    Then el sistema muestra un mensaje de error y no realiza el cambio
