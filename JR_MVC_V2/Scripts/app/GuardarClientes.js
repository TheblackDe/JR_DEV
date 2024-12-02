function GuardarCliente(url) {
    var cliente = {
        Id: $("#txtid").val(),
        Nombre: $("#txtnombre").val(),
        Apellido: $("#txtapellido").val(),
        Estado: $("#selectEstado").val() == 1 ? true : false,
        FechaNacimiento: $("#txtfechanacimiento").val()
    };

    // Realizar solicitud AJAX para guardar el cliente
    $.ajax({
        url: url,
        type: "POST",
        data: JSON.stringify(cliente),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.resultado) {
                alert("Cliente guardado con éxito.");
                $("#FormModal").modal("hide");
                location.reload(); // Recargar la página después de guardar
            } else {
                alert("Error: " + data.mensaje);
            }
        },
        error: function (xhr, status, error) {
            alert("Error al guardar el cliente: " + error);
        }
    });
}
