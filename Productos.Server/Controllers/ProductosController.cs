using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Productos.Server.Models;

namespace Productos.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase  // abtenemos acceso a la Bd desde cualquier parte 
    {
        private readonly ProductosContext _context;

        public ProductosController(ProductosContext context)
        {
            _context = context;

        }

        [HttpPost]
        [Route("/AGREGAR_PRODUCTO")]

        public async Task<IActionResult>CrearProducto(Producto producto)//metodo para guardar un producto en la bd 
        {
            await _context.Productos.AddAsync(producto);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        [Route("/VER_LISTADO_DE_PRODUCTOS")]

        public async Task<ActionResult<IEnumerable<Producto>>> ListaProductos()
        {
            var productos = await _context.Productos.ToListAsync();
            return Ok(productos);
        }
        [HttpGet]
        [Route("/VER_PRODUCTO")]

        public async Task<IActionResult>VerProducto(int id)
        {
            Producto producto = await _context.Productos.FindAsync(id);

            if (producto == null)
            {
                return NotFound(" No hay producto regstrado con ese Id");
            }

            return Ok(producto);
        }

        [HttpPut]
        [Route("/MODIFICAR_PRODUCTO")]

        public async Task<IActionResult> ActualizarProducto(int id, Producto producto)
        {
            var productoExistente = await _context.Productos.FindAsync(id);
            productoExistente!.Nombre = producto.Nombre;
            productoExistente.Descripcion = producto.Descripcion;
            productoExistente.Precio = producto.Precio;

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        [Route("/BORRAR_PRODUCTO")]

        public async Task<IActionResult> EliminarProducto(int id)
        {
            var productoBorrado = await _context.Productos.FindAsync(id);
            _context.Productos.Remove(productoBorrado!);

            await _context.SaveChangesAsync();

            return Ok();
        }


    }
}
