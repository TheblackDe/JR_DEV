using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CP_Entidades;
using CP_Negocio;

namespace JR_MVC_V2.Controllers
{
    // Controlador principal de la aplicación que gestiona clientes, registros, y estadísticas
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Clientes()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ClientesList()
        {
            List<Cliente> clientes = new CapaNegocioClientes().Listar();
            return Json(new { data = clientes }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult RegistrarCliente(Cliente cliente)
        {
            object resultado = new CapaNegocioClientes().Registrar(cliente, out string mensaje);
            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ActualizarCliente(Cliente cliente)
        {
            object resultado = new CapaNegocioClientes().Editar(cliente, out string mensaje);
            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarCliente(int Id)
        {
            bool resultado = new CapaNegocioClientes().Eliminar(Id, out string mensaje);
            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult BuscarCliente(string nombre, string apellido)
        {
            try
            {
                Cliente cliente = new CapaNegocioClientes().BuscarClientePorNombreApellido(nombre, apellido);
                if (cliente != null)
                {
                    return Json(new
                    {
                        success = true,
                        data = new { cliente.Id, cliente.Nombre, cliente.Apellido, cliente.FechaNacimiento }
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, message = "Cliente no encontrado" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult Totales()
        {
            try
            {
                var totales = new CapaNegocioTotales().ObtenerTotales();
                return Json(new { success = true, totales = totales }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message, data = new Totales() }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult ObtenerHistorialVentas(DateTime? fechaInicio = null, DateTime? fechaFin = null, int? idPedido = null)
        {
            try
            {
                var historialVentas = new CapaNegocioHistorialVenta().ObtenerHistorialVentas(fechaInicio, fechaFin, idPedido);
                return Json(new { success = true, data = historialVentas }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message, data = new List<HistorialVenta>() }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
