using ConsumeWebApiPlantillas.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ConsumeWebApiPlantillas.Servicios;

namespace ConsumeWebApiPlantillas.Controllers
{
    public class HomeController : Controller
    {
        private readonly IServicio_API _servicio_Api;

        public HomeController(IServicio_API servicioApi)
        {
            _servicio_Api = servicioApi;
        }

        public async Task<IActionResult> Index()
        {
            List<Plantilla> lista = await _servicio_Api.plantillas();
            return View(lista);
        }

        public async Task<IActionResult> Plantilla(int IdPlantilla = 0)
        {
            Plantilla modelo_plantilla;
            if (IdPlantilla == 0)
            {
                modelo_plantilla = new Plantilla();
                ViewBag.Accion = "Nueva Plantilla";
            }
            else
            {
                modelo_plantilla = await _servicio_Api.obtener(IdPlantilla);
                ViewBag.Accion = "Editar Plantilla";
            }
            return View(modelo_plantilla);
        }

        [HttpPost]
        public async Task<IActionResult> GuardarCambios(Plantilla ob_plantilla)
        {
            bool respuesta;
            if (ob_plantilla.id == 0)
            {
                respuesta = await _servicio_Api.Guardar(ob_plantilla);
            }
            else
            {
                respuesta = await _servicio_Api.Editar(ob_plantilla);
            }

            if (respuesta)
                return RedirectToAction("Index");
            else
                return NoContent();
        }

       public async Task<IActionResult> EditarPlantilla(int id)
    {
        var modelo_plantilla = await _servicio_Api.obtener(id);
        if (modelo_plantilla == null)
        {
            return NotFound();
        }

        ViewBag.Accion = "Editar Plantilla";
        return View("Plantilla", modelo_plantilla);
    }

        [HttpPost]
        public async Task<IActionResult> GuardarPlantilla(Plantilla plantilla)
        {
            bool respuesta;
            if (plantilla.id == 0)
            {
                respuesta = await _servicio_Api.Guardar(plantilla);
            }
            else
            {
                respuesta = await _servicio_Api.Editar(plantilla);
            }

            if (respuesta)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error al guardar la plantilla");
            }

            return View("Plantilla", plantilla);
        }

        [HttpGet]
        public async Task<IActionResult> Eliminar(int IdPlantilla)
        {
            var respuesta = await _servicio_Api.Eliminar(IdPlantilla);
            if (respuesta)
                return RedirectToAction("Index");
            else
                return NoContent();
        }

        public async Task<IActionResult> Visualizar()
        {
            var pdfFile = await _servicio_Api.ObtenerPDF();
            if (pdfFile == null)
            {
                return NotFound();
            }

            return File(pdfFile, "application/pdf", "Plantilla.pdf");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
