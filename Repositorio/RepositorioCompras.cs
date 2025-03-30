using Microsoft.Data.SqlClient;
using prototipo_web.Models;
using System.Data;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace prototipo_web.Repositorio
{
    public interface IRepositorioCompras
    {
        ComprasModel ObtenerCompra(ComprasModel codigo);
        Task<ComprasModel> ObtenerProductoPorCodigo(string codigo);
        
        ComprasModel ObtenerProveedor(int identificacion);
    }

    public class RepositorioCompras: IRepositorioCompras
    {     
            private readonly string cnx;
            public RepositorioCompras(IConfiguration configuration)
            {
                cnx = configuration.GetConnectionString("DefaultConnection");
            }

            public ComprasModel ObtenerCompra(ComprasModel codigo)
            {
                using (IDbConnection db = new SqlConnection(cnx))
                {
                    string query = "SELECT codigo,nombre ,precio, unidades FROM inventario WHERE codigo =@codigo";
                    ComprasModel await = db.QuerySingleOrDefault<ComprasModel>(query, new { codigo = codigo });

                    return await; ;
                }
            }

        public async Task<ComprasModel> ObtenerProductoPorCodigo(string codigo)
        {
            using var connection = new SqlConnection(cnx);
            var query = "SELECT codigo,nombre ,precio, unidades FROM inventario WHERE codigo =@codigo";
            return await connection.QueryFirstOrDefaultAsync<ComprasModel>(query, new { codigo = codigo });
        }

        public ComprasModel ObtenerProveedor (int Identificacion)
        {
            using (IDbConnection db = new SqlConnection(cnx))
            {
                string query = "SELECT Nombres FROM Proveedores WHERE Identificacion = @Identificacion";
                ComprasModel model = db.QuerySingleOrDefault<ComprasModel>(query, new { Identificacion });

                return model;
            }
        }

        
    }
}
