$(document).ready(function () {
    function obtenerTotales() {
        $.ajax({
            url: urlT,
            type: 'GET',
            dataType: 'json',
            success: function (response) {
                if (response.success) {
                    $('#totalcliente').text(response.totales.TotalClientes);
                    $('#totalVenta').text(response.totales.TotalPedidos);
                    $('#totalproducto').text(response.totales.TotalArticulos);
                } else {
                    console.error('Error al obtener los totales:', response.message);
                }
            },
            error: function (xhr, status, error) {
                console.error('Error en la solicitud AJAX:', error);
                console.error('Detalles de la solicitud fallida:', xhr.responseText);
            }
        });
    }

    obtenerTotales();
});
