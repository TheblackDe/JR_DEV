$(document).ready(function () {
    // Acción al hacer clic en el botón de editar
    $("#tabla tbody").on("click", '.btn-editar', function () {
        var fila = $(this).closest("tr");
        if (typeof tabladata !== 'undefined') {
            var data = tabladata.row(fila).data();
            var fechaRaw = data["FechaNacimiento"];
            var fechaFormateada = '';

            // Procesar la fecha de nacimiento
            if (fechaRaw) {
                var milisegundos = fechaRaw.replace('/Date(', '').replace(')/', '');
                var fechaObj = new Date(parseInt(milisegundos));

                if (!isNaN(fechaObj.getTime())) {
                    fechaFormateada = fechaObj.toISOString().split('T')[0];
                } else {
                    fechaFormateada = '';
                }
            }

            // Rellenar el formulario con los datos del cliente
            $('#txtnombre').val(data["Nombre"]);
            $('#txtapellido').val(data["Apellido"]);
            $('#txtfechanacimiento').val(fechaFormateada);
            $('#txtid').val(data["Id"]);
            $('#campoSelect').show();
            $('#selectEstado').val(data["Estado"] ? "1" : "0");

            // Mostrar el modal
            $('#FormModal').modal('show');
        }
    });

    // Limpiar los datos cuando se cierra el modal
    $('#FormModal').on('hidden.bs.modal', function () {
        // Limpiar los campos del formulario
        $('#txtnombre').val('');
        $('#txtapellido').val('');
        $('#txtfechanacimiento').val('');
        $('#txtid').val('');
        $('#campoSelect').hide();
        $('#selectEstado').val(''); // Si necesitas reiniciar el select también
    });
});
