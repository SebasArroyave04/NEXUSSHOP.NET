using Microsoft.Data.SqlClient;
using prototipo_web.Models;
using Dapper;
using Microsoft.AspNetCore.Components.Web;

namespace prototipo_web.Repositorio 
{ 
    public interface IRepositorioUsuario
    {
        Task<bool> Registro(RegistrarUsuario usuario);
        Task<bool> ContactanosModel(ContactanosModel usuario);
        Task<bool> ValidarUsuario(LoginUsuario login);
    }

    public class RepositorioUsuario : IRepositorioUsuario
    {
        private readonly string cnx;
        public RepositorioUsuario(IConfiguration configuracion)
        {
            cnx = configuracion.GetConnectionString("DefaultConnection");
        }
        public async Task<bool> Registro(RegistrarUsuario usuario)
        {
            bool isInserted =false;
            try
            {
                var connection = new SqlConnection(cnx);
                isInserted = await connection.ExecuteAsync(@"INSERT INTO Registro (Identificacion, Nombres, Apellidos, FNacimiento, SSexo, Correo, Contrasena, CContrasena)
                VALUES (@Identificacion, @Nombres, @Apellidos, @FNacimiento, @SSexo, @Correo, @Contrasena, @CContrasena)", usuario) > 0;
            }
            catch (Exception ex)
            {

            }
            return isInserted;
        }

        public async Task<bool> ContactanosModel(ContactanosModel contacto)
        {
            bool isInserted =false;
            try
            {
                var connection = new SqlConnection(cnx);
                isInserted= await connection.ExecuteAsync(
                    @"INSERT INTO Contactanos (Nombre,Correo,Mensaje)
                VALUES (@Nombre,@Correo,@Mensaje)", contacto) > 0;
            }
            catch (Exception ex)
            {
                throw;
            } return isInserted;
        }

        public async Task<bool> ValidarUsuario(LoginUsuario login)
        {
            using var connection = new SqlConnection(cnx);
            string query = @"SELECT COUNT(1) FROM Registro WHERE Correo=@Correo AND Contrasena=@Contrasena";
            bool usuarioExiste = await connection.ExecuteScalarAsync<int>(query, new
            {
                login.Correo,
                login.Contrasena
            }) > 0;
            return usuarioExiste;
        }
    }
}