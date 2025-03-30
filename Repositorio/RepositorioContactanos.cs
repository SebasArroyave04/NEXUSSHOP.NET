using Dapper;
using Microsoft.Data.SqlClient;
using prototipo_web.Models;

namespace prototipo_web.Repositorio
{

    public interface IrepositorioContacto
    {
        Task<bool> Contacto(ContactanosModel contacto);
    }
    public class RepositorioContactanos : IrepositorioContacto
    {
        private readonly string cnx;

        public RepositorioContactanos(IConfiguration configuration)
        {
            cnx = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<bool> Contacto(ContactanosModel contacto)
        {
            bool isInserted = false;
            try
            {
                var connection = new SqlConnection(cnx);
                isInserted = await connection.ExecuteAsync(
                @"INSERT INTO Contactanos (Nombre, Correo, Mensaje)
                VALUES (@Nombre, @Correo, @Mensaje)", contacto) > 0;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
            return isInserted;
        }
    }
}
