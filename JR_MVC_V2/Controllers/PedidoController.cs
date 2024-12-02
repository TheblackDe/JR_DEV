using CP_Entidades;
using CP_Negocio;
using System;
using System.Web.Mvc;

namespace JR_MVC_V2.Controllers
{
    // Controlador para gestionar los pedidos
    public class PedidoController : Controller
    {
        private readonly CapaNegocioTotales _capaNegocioTotales;
        private readonly CapaNegocioPedidos _capaNegocioPedidos;

        public PedidoController()
        {
            _capaNegocioTotales = new CapaNegocioTotales();
            _capaNegocioPedidos = new CapaNegocioPedidos();
        }

        public ActionResult Pedidos()
        {
            return View();
        }

        [HttpPost]
        public JsonResult InsertarPedido(Pedido pedido)
        {
            try
            {
                string mensaje;
                int resultado = _capaNegocioPedidos.InsertarPedido(pedido, out mensaje);

                if (resultado > 0)
                {
                    return Json(new { resultado = true, mensaje = "Pedido insertado correctamente." });
                }
                else
                {
                    return Json(new { resultado = false, mensaje });
                }
            }
            catch (Exception ex)
            {
                
                // Retornar error en caso de excepciones
                return Json(new { resultado = false, mensaje = ex.Message });
            }
        }
    }
}
