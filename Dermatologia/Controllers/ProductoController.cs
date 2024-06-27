using System.Diagnostics;
using Dermatologia.Entities;
using Dermatologia.Models;
using Microsoft.AspNetCore.Mvc;

namespace Dermatologia.Controllers
{
    public class ProductoController : Controller
    {
        private readonly ILogger<ProductoController> _logger;
        private readonly ApplicationDbContext _context;

        public ProductoController(ILogger<ProductoController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult ProductoList()
        {
            List<ProductoModel> list = _context.Productos.Select(p => new ProductoModel()
            {
                Id = p.Id,
                NombreProducto = p.NombreProducto,
                Descripcion = p.Descripcion,
                Precio = p.Precio,
                Disponibilidad = p.Disponibilidad,
               
            }).ToList();

            return View(list);
        }

        [HttpGet]
        public IActionResult ProductoEdit(Guid Id)
        {
            Producto? productoActualizar = _context.Productos.Where(p => p.Id == Id).FirstOrDefault();
            if (productoActualizar == null)
            {
                return RedirectToAction("ProductoList");
            }

            ProductoModel model = new ProductoModel
            {
                Id = productoActualizar.Id,
                NombreProducto = productoActualizar.NombreProducto,
                Descripcion = productoActualizar.Descripcion,
                Precio = productoActualizar.Precio,
                Disponibilidad = productoActualizar.Disponibilidad,
                
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult ProductoEdit(ProductoModel model)
        {
            if (ModelState.IsValid)
            {
                Producto productoActualizar = _context.Productos.Where(p => p.Id == model.Id).First();
                if (productoActualizar == null)
                {
                    return RedirectToAction("ProductoList");
                }

                productoActualizar.NombreProducto = model.NombreProducto;
                productoActualizar.Descripcion = model.Descripcion;
                productoActualizar.Precio = model.Precio;
                productoActualizar.Disponibilidad = model.Disponibilidad;
                
                _context.Productos.Update(productoActualizar);
                _context.SaveChanges();

                return RedirectToAction("ProductoList");
            }

            return View(model);
        }

        public IActionResult ProductoAdd()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ProductoAdd(ProductoModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var productoEntity = new Producto();
            
                productoEntity.Id = Guid.NewGuid();
                productoEntity.NombreProducto = model.NombreProducto;
                productoEntity.Descripcion = model.Descripcion;
                productoEntity.Precio = model.Precio;
                productoEntity.Disponibilidad = model.Disponibilidad;
                
            

            _context.Productos.Add(productoEntity);
            _context.SaveChanges();

            return RedirectToAction("ProductoList", "Producto");
        }

        public IActionResult ProductoDeleted(Guid Id)
        {
            Producto? productoBorrar = _context.Productos.Where(p => p.Id == Id).FirstOrDefault();

            if (productoBorrar == null)
            {
                return RedirectToAction("ProductoList", "Producto");
            }

            ProductoModel model = new ProductoModel
            {
                Id = productoBorrar.Id,
                NombreProducto = productoBorrar.NombreProducto,
                Descripcion = productoBorrar.Descripcion,
                Precio = productoBorrar.Precio,
                Disponibilidad = productoBorrar.Disponibilidad,
                
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult ProductoDeleted(ProductoModel producto)
        {
            bool exists = _context.Productos.Any(p => p.Id == producto.Id);
            if (!exists)
            {
                return View(producto);
            }

            Producto productoEntity = _context.Productos.Where(p => p.Id == producto.Id).First();
            _context.Productos.Remove(productoEntity);
            _context.SaveChanges();

            return RedirectToAction("ProductoList", "Producto");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}