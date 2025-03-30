using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using prototipo_web.Models;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;

namespace prototipo_web.Repositorio
{
      
    public interface IcarritoServicio
    {   
        // limpiarCarro();
        void eliminarItemcarro(int productoId);
        void actualizarItemCarro(int ProductoId, int Cantidad);
        List<carroitem> ListarItemsCarro();
       
        void agregar(Producto _product, int cantidad);
        Producto save(int ProductoId, int Cantidad);
       
    }

    public class carritoServicio : IcarritoServicio
    {
        private readonly string cnx;

        private readonly productoSeleccionado _productoSeleccionados;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        public carritoServicio(productoSeleccionado productoSeleccionados, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _productoSeleccionados = _productoSeleccionados;
            cnx = configuration.GetConnectionString("DefaultConnection");
        }



        private productoSeleccionado obtenerItemsSesion()
        {
            // recupera la variable de session con los valores almacenados
            var session = _httpContextAccessor.HttpContext.Session;
            var cartJson = session.GetString("Carrito");
            return string.IsNullOrEmpty(cartJson)
                ? new productoSeleccionado()
                : JsonSerializer.Deserialize<productoSeleccionado>(cartJson);
        }

        private void guardarItemsSesion(productoSeleccionado cart)
        {
            /// crea la variable de secion si no existe
            var session = _httpContextAccessor.HttpContext.Session;
            session.SetString("Carrito", JsonSerializer.Serialize(cart));
        }

        public Producto save(int ProductoId, int Cantidad)
        {
            using (IDbConnection db = new SqlConnection(cnx))
            {
                string sqlquey = "SELECT * FROM inventario WHERE codigo = @ProductoId";
                Producto productos = db.QuerySingleOrDefault<Producto>(sqlquey, new { ProductoId, Cantidad });
                return productos;
            }
        }

        public void agregar (Producto _producto, int cantidad)
        {
            var cart = obtenerItemsSesion(); // obtiene los productos en la session

            // verifica que exista el producto
            var existingItem = cart.Items.FirstOrDefault(i => i.producto.codigo == _producto.codigo);

            if (existingItem != null)
            {
                existingItem.cantidad += cantidad; // aumenta la cantidad si ya existe
            }
            else
            {
                cart.Items.Add(new carroitem { producto = _producto, cantidad = cantidad });
            }
            guardarItemsSesion(cart); // Guarda el carrito actualizado en la sesion  
        }

        public void eliminarItemcarro(int productoId)
        {
            var cart = obtenerItemsSesion();
            var item = cart.Items.FirstOrDefault(i => i.producto.codigo == productoId);

            if (item != null)
            {
                cart.Items.Remove(item); // Elimina el producto del carrito
                guardarItemsSesion(cart); // Guarda el carito actualizado
            }
        }

        public decimal obtenertotal()
        {
            return _productoSeleccionados.TotalPrecio;
            // totalizar el precio
        }

        public void actualizarItemCarro(int ProductoId, int Cantidad)
        {
            var cart = obtenerItemsSesion();
            var existeItem = cart.Items.FirstOrDefault(i => i.producto.codigo == ProductoId);

            if (existeItem != null)
            { 
                existeItem.cantidad = Cantidad; // Actualiza la cantidad    
            }

            guardarItemsSesion(cart); // Guarda los cambios en la sesión 
        }

        public List<carroitem> ListarItemsCarro() 
        {
            return obtenerItemsSesion().Items;
        }
    }
}
