using Dapper;
using Microsoft.Data.SqlClient;
using prototipo_web.Models;
using System.Data;
namespace prototipo_web.Repositorio
{
    public interface IRepositorioPDF
    {
        List<RegistrarUsuario> HacerPDF(RegistrarUsuario Hacer);
        List<ProveedoresModel> HacerPDF1(ProveedoresModel Hacer);
        List<ContactanosModel> HacerPDF2(ContactanosModel Hacer);
        List<ProductosModel> HacerPDF3(ProductosModel Hacer);
    }

    public class RepositorioPDF : IRepositorioPDF
    {
        private readonly string cnx;
        private readonly IConfiguration configuration;

        public RepositorioPDF(IConfiguration configuration)
        {
            cnx = configuration.GetConnectionString("DefaultConnection");

        }
        public List<RegistrarUsuario> HacerPDF(RegistrarUsuario Hacer)
        {
            using (IDbConnection db = new SqlConnection(cnx))
            {
                string sqlQuery = "SELECT * FROM Registro";
                var productos = db.Query<RegistrarUsuario>(sqlQuery).ToList();
                return productos;
            }
        }

        public List<ProveedoresModel> HacerPDF1(ProveedoresModel Hacer)
        {
            using (IDbConnection db = new SqlConnection(cnx))
            {
                string sqlQuery = "SELECT * FROM Proveedores";
                var productos = db.Query<ProveedoresModel>(sqlQuery).ToList();
                return productos;
            }
        }

        public List<ContactanosModel> HacerPDF2(ContactanosModel Hacer)
        {
            using (IDbConnection db = new SqlConnection(cnx))
            {
                string sqlQuery = "SELECT * FROM Contactanos";
                var productos = db.Query<ContactanosModel>(sqlQuery).ToList();
                return productos;
            }
        }

        public List<ProductosModel> HacerPDF3(ProductosModel Hacer)
        {
            using (IDbConnection db = new SqlConnection(cnx))
            {
                string sqlQuery = "SELECT * FROM inventario";
                var productos = db.Query<ProductosModel>(sqlQuery).ToList();
                return productos;
            }
        }
    }
}
