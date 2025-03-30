using Dapper;
using Microsoft.Data.SqlClient;
using prototipo_web.Models;
using System.Data;

namespace prototipo_web.Repositorio
{
    public interface IRepositorioHome 
    {
       
        ProductosModel DetalleProducto(int id);
        Producto GetProductoById(int productoId);
        IEnumerable<ProductosModel> Listarproductos();
        Task<bool> Productos(ProductosModel model);
    }

    public class ReposiitorioHome : IRepositorioHome
    {
        private readonly string cnx;

        public ReposiitorioHome(IConfiguration configuration)
        {
            cnx = configuration.GetConnectionString("DefaultConnection");
        }

        public IEnumerable<ProductosModel> Listarproductos()
        {
            using (IDbConnection db = new SqlConnection(cnx)) 
            {
                string sqlQuery = "SELECT * FROM inventario";
                var productos = db.Query<ProductosModel>(sqlQuery).ToList();
                return productos;
            }
        }

        public async Task<bool> Productos(ProductosModel model)
        {
            bool isInserted = false;
            try
            {
                var connection = new SqlConnection(cnx);
                isInserted = await connection.ExecuteAsync(
                    @"INSERT INTO inventario(codigo, nombre, descripcion, precio, unidades, estado, urlimage, marca, modelo, accesorios) 
                        VALUES(@codigo, @nombre, @descripcion, @precio, @unidades, @estado, @urlimage, @marca, @modelo, @accesorios) ", model) > 0;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
            return isInserted;
        }

        public ProductosModel DetalleProducto(int codigo)
        {
            using (IDbConnection db = new SqlConnection(cnx))
            {
                string sqlQuery = "SELECT codigo, nombre, descripcion, precio, urlimage, marca, modelo, unidades FROM inventario WHERE codigo = @codigo";
                  ProductosModel producto = db.QuerySingleOrDefault<ProductosModel>(sqlQuery, new { codigo });
                return producto;
            }
        }

        public Producto GetProductoById(int productoId)
        {
            throw new NotImplementedException();
        }
       
    }
}

