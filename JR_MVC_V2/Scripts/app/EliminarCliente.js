$(document).ready(function () {
    // Acción al hacer clic en el botón de eliminar
    $("#tabla tbody").on("click", '.btn-eliminar', function () {
        // Obtener la fila seleccionada
        var fila = $(this).closest("tr");
        if (typeof tabladata !== 'undefined') {
            var data = tabladata.row(fila).data(); // Obtener los datos de la fila seleccionada
            var nombre = data["Nombre"];
            var apellido = data["Apellido"];
            var idCliente = data["Id"]; // ID del cliente

            // Mostrar alerta de confirmación con los datos del cliente
            swal({
                title: "¿Estás seguro?",
                text: `¡Estás a punto de eliminar al usuario:\n${nombre} ${apellido}!`,
                type: "warning",
                showCancelButton: true,
                confirmButtonClass: "btn-danger",
                confirmButtonText: "Sí, eliminarlo",
                cancelButtonText: "No, cancelar",
                closeOnConfirm: false,
                closeOnCancel: false
            },
                function (isConfirm) {
                    if (isConfirm) {
                        // Solicitud AJAX para eliminar el cliente
                        jQuery.ajax({
                            url: urlClienteEliminar, // URL de la API para eliminar el usuario
                            type: "POST",
                            data: JSON.stringify({ id: idCliente }), // Enviar el ID del cliente
                            dataType: "json",
                            contentType: "application/json; charset=utf-8",
                            success: function (data) {
                                if (data.resultado) {
                                    $('#tabla').DataTable().ajax.reload(); // Recargar la tabla
                                    swal("¡Eliminado!", `El usuario ${nombre} ${apellido} ha sido eliminado.`, "success");
                                } else {
                                    $("#mensajeError").text(data.mensaje);
                                    $("#mensajeError").show();
                                }
                            },
                            error: function (xhr, status, error) {
                                $("#mensajeError").text("Error al realizar la solicitud: " + error);
                                $("#mensajeError").show();
                            }
                        });
                    } else {
                        swal("Cancelado", `El usuario ${nombre} ${apellido} está a salvo.`, "error");
                    }
                });
        }
    });
});
