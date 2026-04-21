Feature: Recuperar contraseña mediante correo electrónico
  Como usuario
  Quiero restablecer mi contraseña con un enlace por correo
  Para acceder a mi cuenta si la olvido

  Scenario: Recuperación exitosa con correo registrado
    Given que el usuario indica que no recuerda su contraseña
    When solicita recuperación usando un correo registrado
    Then el sistema envía un enlace de restablecimiento

  Scenario: Intento con correo no registrado
    Given que el usuario ingresa una dirección no registrada
    When solicita recuperación de contraseña
    Then el sistema notifica que no existe una cuenta con ese correo
