function authArticuloModel() {
    var nombre = document.getElementById("txtnombre").value.trim();
    var descripcion = document.getElementById("txtdescripcion").value.trim();
    var precio = document.getElementById("txtprecio").value.trim();
    var stock = document.getElementById("txtstock").value.trim();

    var errorNombre = document.getElementById("errorNombre");
    var errorDescripcion = document.getElementById("errorDescripcion");
    var errorPrecio = document.getElementById("errorPrecio");
    var errorStock = document.getElementById("errorStock");

    // Limpiar los mensajes de error previos, solo si los elementos existen
    if (errorNombre) errorNombre.classList.add("d-none");
    if (errorDescripcion) errorDescripcion.classList.add("d-none");
    if (errorPrecio) errorPrecio.classList.add("d-none");
    if (errorStock) errorStock.classList.add("d-none");

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

    // Validación de descripción
    if (descripcion === "") {
        if (errorDescripcion) {
            errorDescripcion.classList.remove("d-none");
        } else {
            console.warn("El elemento con id 'errorDescripcion' no existe.");
        }
        isValid = false;
    } else if (descripcion.length > 255) {
        if (errorDescripcion) {
            errorDescripcion.textContent = "La descripción no puede exceder los 255 caracteres.";
            errorDescripcion.classList.remove("d-none");
        }
        isValid = false;
    }

    // Validación de precio
    if (precio === "" || parseFloat(precio) < 0) {
        if (errorPrecio) {
            errorPrecio.classList.remove("d-none");
        } else {
            console.warn("El elemento con id 'errorPrecio' no existe.");
        }
        isValid = false;
    }

    // Validación de stock
    if (stock === "" || parseInt(stock) < 0) {
        if (errorStock) {
            errorStock.classList.remove("d-none");
        } else {
            console.warn("El elemento con id 'errorStock' no existe.");
        }
        isValid = false;
    }

    return isValid;
}

// Limpiar los mensajes de error cuando el modal se cierre
$('#FormModal').on('hidden.bs.modal', function () {
    // Limpiar valores de los campos
    document.getElementById("txtnombre").value = '';
    document.getElementById("txtdescripcion").value = '';
    document.getElementById("txtprecio").value = '';
    document.getElementById("txtstock").value = '';

    // Limpiar los mensajes de error
    var errorNombre = document.getElementById("errorNombre");
    var errorDescripcion = document.getElementById("errorDescripcion");
    var errorPrecio = document.getElementById("errorPrecio");
    var errorStock = document.getElementById("errorStock");

    if (errorNombre) errorNombre.classList.add("d-none");
    if (errorDescripcion) errorDescripcion.classList.add("d-none");
    if (errorPrecio) errorPrecio.classList.add("d-none");
    if (errorStock) errorStock.classList.add("d-none");
});
