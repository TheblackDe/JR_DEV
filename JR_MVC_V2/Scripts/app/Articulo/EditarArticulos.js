$(document).ready(function () {
    // Cuando se hace clic en el botón de editar
    $("#tabladata_art tbody").on("click", '.btn-editar', function () {
        var fila = $(this).closest("tr");

        // Verificar si la tabla ya está inicializada
        if (typeof tabladata_art !== 'undefined') {
            var data = tabladata_art.row(fila).data(); // Obtener los datos de la fila seleccionada

            // Asignar los valores del artículo al formulario
            $('#txtnombre').val(data["Nombre"]);
            $('#txtdescripcion').val(data["Descripcion"]);
            $('#txtprecio').val(data["Precio"]);
            $('#txtstock').val(data["Stock"]);
            $('#txtid').val(data["Id"]);

            // Mostrar el modal
            $('#FormModal').modal('show');
        }
    });

    // Limpiar los datos cuando se cierra el modal
    $('#FormModal').on('hidden.bs.modal', function () {
        // Limpiar los campos del formulario
        $('#txtnombrearticulo').val('');
        $('#txtdescripcion').val('');
        $('#txtprecio').val('');
        $('#txtstock').val('');
        $('#txtidarticulo').val('');
    });
});
