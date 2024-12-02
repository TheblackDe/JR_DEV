// Función para validar los datos del cliente
function authClienteModel() {
    var nombre = document.getElementById("txtnombre").value.trim();
    var apellido = document.getElementById("txtapellido").value.trim();
    var fechaNacimiento = document.getElementById("txtfechanacimiento").value;

    var errorNombre = document.getElementById("errorNombre");
    var errorApellido = document.getElementById("errorApellido");
    var errorFechaNacimiento = document.getElementById("errorFechaNacimiento");

    // Limpiar los mensajes de error previos, solo si los elementos existen
    if (errorNombre) errorNombre.classList.add("d-none");
    if (errorApellido) errorApellido.classList.add("d-none");
    if (errorFechaNacimiento) errorFechaNacimiento.classList.add("d-none");

    var isValid = true;

    // Validación de nombre
    if (nombre === "") {
        if (errorNombre) {
            errorNombre.classList.remove("d-none");
        } else {
            console.warn("El elemento con id 'errorNombre' no existe.");
        }
        isValid = false;
    }

    // Validación de apellido
    if (apellido === "") {
        if (errorApellido) {
            errorApellido.classList.remove("d-none");
        } else {
            console.warn("El elemento con id 'errorApellido' no existe.");
        }
        isValid = false;
    }

    // Validación de fecha de nacimiento
    if (fechaNacimiento === "") {
        if (errorFechaNacimiento) {
            errorFechaNacimiento.textContent = "La fecha de nacimiento es obligatoria.";
            errorFechaNacimiento.classList.remove("d-none");
        } else {
            console.warn("El elemento con id 'errorFechaNacimiento' no existe.");
        }
        isValid = false;
    } else {
        var currentDate = new Date();
        var birthDate = new Date(fechaNacimiento);
        if (birthDate > currentDate) {
            if (errorFechaNacimiento) {
                errorFechaNacimiento.textContent = "La fecha de nacimiento no puede ser futura.";
                errorFechaNacimiento.classList.remove("d-none");
            } else {
                console.warn("El elemento con id 'errorFechaNacimiento' no existe.");
            }
            isValid = false;
        }
    }

    return isValid;
}

// Limpiar los mensajes de error cuando el modal se cierre
$('#FormModal').on('hidden.bs.modal', function () {
    // Limpiar valores y ocultar errores
    document.getElementById("txtnombre").value = '';
    document.getElementById("txtapellido").value = '';
    document.getElementById("txtfechanacimiento").value = '';

    // Limpiar los mensajes de error
    var errorNombre = document.getElementById("errorNombre");
    var errorApellido = document.getElementById("errorApellido");
    var errorFechaNacimiento = document.getElementById("errorFechaNacimiento");

    if (errorNombre) errorNombre.classList.add("d-none");
    if (errorApellido) errorApellido.classList.add("d-none");
    if (errorFechaNacimiento) errorFechaNacimiento.classList.add("d-none");
});
