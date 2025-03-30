using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using prototipo_web.Models;

namespace prototipo_web.Repositorio
{
    public interface IRepositorioProveedor
    {
        Task<bool> Proveedores(ProveedoresModel proveedores);
    }

    public class RepositroioProveedor : IRepositorioProveedor
    {
        private readonly string cnx;
        public RepositroioProveedor(IConfiguration configuration)
        {
            cnx = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<bool> Proveedores(ProveedoresModel proveedores)
        {
            bool isInserted = false;

            try
            {
                var connection = new SqlConnection(cnx);
                isInserted = await connection.ExecuteAsync
                    (@"INSERT INTO Proveedores (Nombres, Identificacion, Empresa, CorreoEmpresa, RUT, Direccion, Telefono, Correoprov, Comentarios)
                        VALUES (@Nombres, @Identificacion, @Empresa, @CorreoEmpresa, @RUT, @Direccion, @Telefono, @Correoprov, @Comentarios)", proveedores) > 0;
            }
            catch (Exception ex) { }

            return isInserted;
        }
    }
}