using ConsumeWebApiPlantillas.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace ConsumeWebApiPlantillas.Servicios
{
    public class Servicio_API : IServicio_API
    {
        private static string _baseurl;

        public Servicio_API()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            _baseurl = builder.GetSection("appsettings:baseurl").Value;
        }

        public async Task<List<Plantilla>> Lista()
        {
            List<Plantilla> lista = new List<Plantilla>();
            var Plantillas = new HttpClient();
            Plantillas.BaseAddress = new Uri(_baseurl);
            var response = await Plantillas.GetAsync("/api/Plantillas");

            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                lista = JsonConvert.DeserializeObject<List<Plantilla>>(json_respuesta);
            }
            return lista;
        }

        public async Task<bool> Guardar(Plantilla objeto)
        {
            bool respuesta = false;

            var Plantillas = new HttpClient();
            Plantillas.BaseAddress = new Uri(_baseurl);

            var content = new StringContent(JsonConvert.SerializeObject(objeto), Encoding.UTF8, "application/json");
            var response = await Plantillas.PostAsync("/api/Plantillas/", content);

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }
            return respuesta;
        }

        public async Task<Plantilla> obtener(int IdPlantilla)
        {
            Plantilla objeto = new Plantilla();

            var Plantillas = new HttpClient();
            var response = await Plantillas.GetAsync($"/api/Plantillas/{IdPlantilla}");

            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                objeto = JsonConvert.DeserializeObject<Plantilla>(json_respuesta);
            }
            return objeto;
        }

        public async Task<bool> Editar(Plantilla objeto)
        {
            bool respuesta = false;

            var Plantillas = new HttpClient();
            Plantillas.BaseAddress = new Uri(_baseurl);

            var content = new StringContent(JsonConvert.SerializeObject(objeto), Encoding.UTF8, "application/json");
            var response = await Plantillas.PutAsync("/api/Plantillas/", content);

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }
            return respuesta;
        }

        public async Task<bool> Eliminar(int IdPlantilla)
        {
            bool respuesta = false;

            var Plantillas = new HttpClient();
            Plantillas.BaseAddress = new Uri(_baseurl);
            var response = await Plantillas.DeleteAsync($"/api/Plantillas/{IdPlantilla}");

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }
            return respuesta;
        }

        public async Task<byte[]> ObtenerPDF()
        {
            byte[] pdfFile = null;

            var Plantillas = new HttpClient();
            Plantillas.BaseAddress = new Uri(_baseurl);
            var content = new StringContent("", Encoding.UTF8, "application/json");
            var response = await Plantillas.PostAsync("/api/PDF", content);

            if (response.IsSuccessStatusCode)
            {
                pdfFile = await response.Content.ReadAsByteArrayAsync();
            }
            return pdfFile;
        }

        public async Task<List<Plantilla>> plantillas()
        {
            List<Plantilla> objeto = new List<Plantilla>();

            var Plantillas = new HttpClient();
            Plantillas.BaseAddress = new Uri(_baseurl);
            var response = await Plantillas.GetAsync($"/api/Plantillas");

            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                objeto = JsonConvert.DeserializeObject<List<Plantilla>>(json_respuesta);
            }
            return objeto;
        }
    }
}
