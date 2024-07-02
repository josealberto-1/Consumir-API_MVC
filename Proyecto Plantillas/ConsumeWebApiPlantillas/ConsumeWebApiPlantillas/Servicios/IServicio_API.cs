using ConsumeWebApiPlantillas.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ConsumeWebApiPlantillas.Servicios
{
    public interface IServicio_API
    {
        Task<List<Plantilla>> Lista();
        Task<bool> Guardar(Plantilla objeto);
        Task<Plantilla> obtener(int IdPlantilla);
        Task<bool> Editar(Plantilla objeto);
        Task<bool> Eliminar(int IdPlantilla);
        Task<List<Plantilla>> plantillas();
        Task<byte[]> ObtenerPDF();  // Agrega esta línea
    }
}
