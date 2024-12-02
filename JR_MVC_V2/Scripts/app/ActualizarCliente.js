// Función de actualización al guardar
function ActualizarCliente(url) {
    var Cliente = {
        Id: document.getElementById("txtid").value.trim(),
        Nombre: document.getElementById("txtnombre").value.trim(),
        Apellido: document.getElementById("txtapellido").value.trim(),
        Estado: document.getElementById("selectEstado").value.trim() === "1" ? true : false,  // Corregido el error en 'selectEstado'
        FechaNacimiento: document.getElementById("txtfechanacimiento").value.trim()
    };

    jQuery.ajax({
        url: url,
        type: "POST",
        data: JSON.stringify(Cliente), // Enviar el objeto directamente
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            // Verificar si la operación fue exitosa
            if (data.resultado != 0) {
                var clienteId = Cliente.Id;
                // Actualizar la fila correspondiente en la tabla
                if (clienteId > 0) {
                    var filaIndex = clienteId - 1;
                    var fila = tabladata.row(filaIndex);
                    // Lógica adicional
                }
                // Buscar la fila correspondiente al cliente
                fila.data(Cliente).draw(false); // Actualizar la fila con los nuevos datos
                $("#FormModal").modal("hide"); // Cerrar el modal

            } else {
                // Mostrar mensaje de error si no se actualiza el cliente
                $("#mensajeError").text(data.mensaje);
                $("#mensajeError").show();
            }
        },
        error: function (xhr, status, error) {
            $("#mensajeError").text("Error al realizar la solicitud: " + error);
            $("#mensajeError").show();
        }
    });
}

// Al cerrar el modal, resetear los campos
$('#FormModal').on('hidden.bs.modal', function () {
    $('#txtid').val(0);
    $('#txtnombre').val('');
    $('#txtapellido').val('');
    $('#txtfechanacimiento').val('');
    $('#campoSelect').hide();
});
