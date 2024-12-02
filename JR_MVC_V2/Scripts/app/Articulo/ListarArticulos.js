var tabladata_art;
$(document).ready(function () {
    // Inicialización de la tabla para Artículos
    tabladata_art = $("#tabladata_art").DataTable({
        responsive: true,
        ordering: false,
        searching: true, // Mantener búsqueda activada
        "ajax": {
            url: urlArticulosList, // URL para obtener los datos de la tabla Artículos
            type: "GET",
            dataType: "json",
            error: function (xhr, status, error) {
                console.log("Hubo un error: " + error);
            }
        },
        "columns": [
            { "data": "Nombre" }, // Nombre del artículo
            { "data": "Descripcion" }, // Descripción del artículo
            {
                "data": "Precio",
                "render": function (precio) {
                    return "$" + parseFloat(precio).toFixed(2); // Mostrar el precio formateado
                }
            },
            {
                "data": "Stock", // Usamos el valor de "Stock" como "Cantidad"
                "render": function (stock) {
                    return parseInt(stock).toFixed(0); // Mostrar la cantidad como un número entero sin decimales
                },
                "title": "Stock"
            },
            {
                "data": "Stock",
                "render": function (stock) {
                    if (stock > 0) {
                        return '<span class="badge bg-success">En Stock</span>';
                    } else {
                        return '<span class="badge bg-danger">Sin Stock</span>';
                    }
                }
            },
            {
                "defaultContent": "",
                "orderable": false,
                "searchable": false,
                "width": "90px",
                "render": function (data, type, row) {
                    // Botón para editar siempre habilitado
                    var editarBtn = '<button type="button" class="btn btn-primary btn-sm btn-editar"><i class="fas fa-pen"></i></button>';
                    return editarBtn;
                }
            }
        ],
        "language": {
            "url": "https://cdn.datatables.net/plug-ins/1.13.4/i18n/es-ES.json"
        }
    });
});
