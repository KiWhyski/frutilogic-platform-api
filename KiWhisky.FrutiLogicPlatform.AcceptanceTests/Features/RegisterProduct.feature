Feature: Crear producto

Como administrador del sistema
Quiero poder crear un nuevo producto
Para que esté disponible en el catálogo

    Scenario: Crear producto exitosamente
        Given el producto no existe con el nombre "Laptop X123"
        When envío una solicitud para crear el producto con los siguientes datos:
          | Nombre      | Precio  | Stock |
          | Laptop X123 | 1200.00 | 10    |
        Then la respuesta debe tener código 201
        And el cuerpo de la respuesta debe contener el producto con nombre "Laptop X123"