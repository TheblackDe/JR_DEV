using CP_Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CP_Negocio;

namespace JR_MVC_V2.Controllers
{
    // Controlador que maneja las operaciones relacionadas con los artículos
    public class ArticuloController : Controller
    {
        // Acción para mostrar la vista de artículos
        public ActionResult Articulos()
        {
            return View();
        }

        // Acción para obtener la lista de artículos en formato JSON
        [HttpGet]
        public JsonResult ArticulosList()
        {
            List<Articulo> articulos = new CapaNegocioArticulos().Listar(); // Obtener la lista de artículos
            return Json(new { data = articulos }, JsonRequestBehavior.AllowGet);
        }

        // Acción para registrar un artículo
        [HttpPost]
        public JsonResult RegistrarArticulo(Articulo articulo)
        {
            object resultado = null;
            string mensaje = string.Empty;

      
            // Registrar el artículo
            resultado = new CapaNegocioArticulos().Registrar(articulo, out mensaje);

            // Retornar el resultado
            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        // Acción para actualizar un artículo
        [HttpPost]
        public JsonResult ActualizarArticulo(Articulo articulo)
        {
            object resultado = null;
            string mensaje = string.Empty;

            // Actualizar el artículo
            resultado = new CapaNegocioArticulos().Editar(articulo, out mensaje);

            // Retornar el resultado
            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        // Acción para eliminar un artículo
        [HttpPost]
        public JsonResult EliminarArticulo(int Id)
        {
            bool resultado = false;
            string mensaje = string.Empty;

            // Mostrar el ID recibido para depuración
            System.Diagnostics.Debug.WriteLine("Id: " + Id);

            // Eliminar el artículo
            resultado = new CapaNegocioArticulos().Eliminar(Id, out mensaje);

            // Retornar el resultado
            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }
    }
}
