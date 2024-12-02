function GuardarArticulo(url) {
    // Obtener los valores de los campos del formulario
    var articulo = {
        Id: $("#txtid").val(),
        Nombre: $("#txtnombre").val(),
        Descripcion: $("#txtdescripcion").val(),
        Precio: $("#txtprecio").val(),
        Stock: $("#txtstock").val()
    };

    // Realizar la solicitud AJAX
    $.ajax({
        url: url,
        type: "POST",
        data: JSON.stringify(articulo),  // Convertir el objeto a JSON
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.resultado) {
                alert("Artículo guardado con éxito.");
                $("#FormModal").modal("hide");  // Ocultar el modal después de guardar
                location.reload();  // Recargar la página para mostrar los cambios
            } else {
                alert("Error: " + data.mensaje);
            }
        },
        error: function (xhr, status, error) {
            alert("Error al guardar el artículo: " + error);
        }
    });
}
