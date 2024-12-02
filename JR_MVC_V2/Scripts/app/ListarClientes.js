var tabladata;
$(document).ready(function () {
    // Inicialización de la tabla sin búsqueda global, pero manteniendo el resto
    tabladata = $("#tabla").DataTable({
        responsive: true,
        ordering: false,
        searching: true, // Deshabilitar la búsqueda global
        "ajax": {
            url: urlClientesList, // Usamos la variable definida previamente
            type: "GET",
            dataType: "json",
            error: function (xhr, status, error) {
                console.log("Hubo un error: " + error);
            }
        },
        "columns": [
            {
                "data": "Nombre",
                "render": function (nombre) {
                    return nombre;  // Mostramos el nombre tal cual
                }
            },
            { "data": "Apellido" },
            {
                "data": "FechaNacimiento",
                "render": function (fecha) {
                    var milisegundos = fecha.replace('/Date(', '').replace(')/', '');
                    var date = new Date(parseInt(milisegundos));

                    if (isNaN(date.getTime())) {
                        return 'Fecha inválida';
                    }
                    return date.toLocaleDateString(); // Formato de fecha
                }
            },
            {
                "data": "Estado",
                "render": function (valor) {
                    if (valor) {
                        // Si el cliente está activo
                        return '<span class="badge bg-success">Activo</span>';
                    } else {
                        // Si el cliente está desactivado
                        return '<span class="badge bg-danger">Desactivado</span>';
                    }
                }
            },
            {
                "defaultContent": '<button type="button" class="btn btn-primary btn-sm btn-editar"><i class="fas fa-pen"></i></button>' +
                    '<button type="button" class="btn btn-danger btn-sm ms-2 btn-eliminar" disabled><i class="fas fa-trash"></i></button>',
                "orderable": false,
                "searchable": false,
                "width": "90px",
                "render": function (data, type, row) {
                    // El botón de editar siempre está habilitado
                    var editarBtn = '<button type="button" class="btn btn-primary btn-sm btn-editar"><i class="fas fa-pen"></i></button>';

                    // El botón de eliminar solo está habilitado si el cliente está activo
                    var eliminarBtn = '<button type="button" class="btn btn-danger btn-sm ms-2 btn-eliminar" ';
                    if (row.Estado === false) {
                        eliminarBtn += 'disabled';
                    }
                    eliminarBtn += '><i class="fas fa-trash"></i></button>';

                    return editarBtn + eliminarBtn;
                }
            }
        ],
        "language": {
            "url": "https://cdn.datatables.net/plug-ins/1.13.4/i18n/es-ES.json"
        }
    });
});
