Feature: Cerrar sesión manualmente
  Como usuario
  Quiero cerrar sesión desde cualquier dispositivo
  Para proteger mi cuenta si la dejo abierta

  Scenario: Cierre de sesión exitoso
    Given que el usuario tiene una sesión activa
    When selecciona la opción de cerrar sesión
    Then el sistema invalida la sesión y redirige al inicio de sesión

  Scenario: Bloqueo de acceso tras cierre de sesión
    Given que el usuario cerró su sesión
    When intenta acceder a un recurso que requiere autenticación
    Then el sistema bloquea el acceso y solicita iniciar sesión nuevamente
