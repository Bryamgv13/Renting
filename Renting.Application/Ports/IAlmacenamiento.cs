using System.IO;
using System.Threading.Tasks;

namespace Renting.Application.Ports
{
    public interface IAlmacenamiento
    {
        Task SubirArchivoBlobAsync(string nombreArchivo, Stream contenido);
    }
}
