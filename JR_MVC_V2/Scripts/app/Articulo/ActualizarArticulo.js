// Función de actualización al guardar
function ActualizarArticulo(url) {
    var Articulo = {
        Id: document.getElementById("txtid").value.trim(),
        Nombre: document.getElementById("txtnombre").value.trim(),
        Descripcion: document.getElementById("txtdescripcion").value.trim(),
        Precio: document.getElementById("txtprecio").value.trim(),
        Stock: document.getElementById("txtstock").value.trim(),
    };

    jQuery.ajax({
        url: url,
        type: "POST",
        data: JSON.stringify(Articulo), // Enviar el objeto directamente
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.resultado != 0) {
                var articuloId = Articulo.Id;
                if (articuloId > 0) {
                    var filaIndex = articuloId - 1;
                    var fila = tabladata_art.row(filaIndex);
                    // Lógica adicional para actualizar la fila en la tabla
                }
                fila.data(Articulo).draw(false); // Actualizar la fila con los nuevos datos
                $('#tabladata_art').DataTable().ajax.reload();
                $("#FormModal").modal("hide"); // Cerrar el modal
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
}

// Al cerrar el modal, resetear los campos
$('#FormModal').on('hidden.bs.modal', function () {
    $('#txtid').val(0);
    $('#txtnombre').val('');
    $('#txtdescripcion').val('');
    $('#txtprecio').val('');
    $('#txtstock').val('');
});
