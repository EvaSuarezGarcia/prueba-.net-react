# prueba-.net-react

## DroneApp

Componentes:

-   **`ConsoleUI:`** Aplicación de consola que lee las instrucciones de un fichero y muestra los resultados por consola.
-   **`ConsoleUI.Tests:`** Tests de ConsoleUI.
-   **`DroneLibrary:`** Biblioteca de manejo de drones.
-   **`DroneLibrary.Tests:`** Tests de DroneLibrary.

### Cómo ejecutar

Todos los comandos a continuación asumen que el directorio actual es DroneApp.

-   Tests: `dotnet test [<PROYECTO>]`
    -   Si no se especifica proyecto, se ejecutarán los tests tanto de `ConsoleUI.Tests` como de `DroneLibrary.Tests`.
-   Programa principal: `dotnet run --project ConsoleUI -- <FICHERO>`
    -   El path al fichero puede ser absoluto o relativo al directorio desde el que se está ejecutando `dotnet run`.
    -   Ficheros de ejemplo en `ConsoleUI/SampleFiles/`:
        -   `sample.txt`: Instrucciones válidas.
        -   `sampleDroneOutsideArea.txt`: Intenta colocar un dron fuera del área de vuelo.
        -   `sampleLastDroneMissingActions.txt`: Define dos drones, pero el último no tiene acciones, por lo que solo se procesa el primer dron.
        -   `sampleWrongDronePath.txt`: Define tres drones, pero las acciones del segundo lo llevarían a salirse del área de vuelo, por lo que se envía de vuelta a su base antes de que pueda salirse.
        -   `sampleWrongFormat.txt`: Define un dron, pero no tiene el formato apropiado.
    -   También se puede debuggear desde VSCode con la configuración `ConsoleUI`, que usa como fichero de entrada `sample.txt`.

## Cards App

### Cómo ejecutar

Todos los comandos a continuación asumen que el directorio actual es cards-app.

-   Setup: `npm install`
-   Ejecutar la aplicación: `npm start`. Al abrir la aplicación por primera vez no habrá ninguna tarjeta, será necesario crear alguna para probar las funcionalidades.
-   Tests de Cypress: Dos opciones una vez está la aplicación levantada:
    -   `npm run cy:open` para utilizar la interfaz gráfica de Cypress.
    -   `npm run cy:run` para ejecutar los tests de forma headless.
