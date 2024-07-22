using System.Diagnostics;
using Dermatologia.Entities;
using Dermatologia.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Dermatologia.Controllers
{
    public class CitaController : Controller
    {
        private readonly ILogger<CitaController> _logger;
        private readonly ApplicationDbContext _context;

        public CitaController(ILogger<CitaController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult CitaList()
        {
            List<CitaModel> list = _context.Citas.Select(c => new CitaModel()
            {
                Id = c.Id,
                NombreDeCita = c.NombreDeCita,
                Telefono = c.Telefono,
                DoctorDeCita = c.DoctorDeCita,
                FechaDeCita = c.FechaDeCita,
                
            }).ToList();

            return View(list);
        }

        [HttpGet]
        public IActionResult CitaEdit(Guid Id)
        {
            Cita? citaActualizar = _context.Citas.Where(c => c.Id == Id).FirstOrDefault();
            if (citaActualizar == null)
            {
                return RedirectToAction("CitaList");
            }

            CitaModel model = new CitaModel
            {
                Id = citaActualizar.Id,
                NombreDeCita = citaActualizar.NombreDeCita,
                Telefono = citaActualizar.Telefono,
                DoctorDeCita = citaActualizar.DoctorDeCita,
                FechaDeCita = citaActualizar.FechaDeCita,
                
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult CitaEdit(CitaModel model)
        {
            if (ModelState.IsValid)
            {
                Cita citaActualizar = _context.Citas.Where(c => c.Id == model.Id).First();
                if (citaActualizar == null)
                {
                    return RedirectToAction("CitaList");
                }

                citaActualizar.NombreDeCita = model.NombreDeCita;
                citaActualizar.Telefono = model.Telefono;
                citaActualizar.DoctorDeCita = model.DoctorDeCita;
                citaActualizar.FechaDeCita = model.FechaDeCita;
                

                _context.Citas.Update(citaActualizar);
                _context.SaveChanges();

                return RedirectToAction("CitaList");
            }

            return View(model);
        }

        public IActionResult CitaAdd()
        {
            CitaModel cita = new CitaModel();

            cita.ListaDoctores = 
                    _context.Doctores.Select(p => new SelectListItem()
                    { Value = p.Id.ToString(), Text = p.Nombre }
                    ).ToList();
            return View(cita);
        }

        [HttpPost]
        public IActionResult CitaAdd(CitaModel model)
        {
            if (!ModelState.IsValid)
            {
                model.ListaDoctores = 
                    _context.Doctores.Select(p => new SelectListItem()
                    { Value = p.Id.ToString(), Text = p.Nombre }
                    ).ToList();
                return View(model);
            }

            var citaEntity = new Cita();
            
                citaEntity.Id = Guid.NewGuid();
                citaEntity.NombreDeCita = model.NombreDeCita;
                citaEntity.Telefono = model.Telefono;
                
                citaEntity.FechaDeCita = model.FechaDeCita;
                citaEntity.DoctorId = model.DoctorId;
                
            

            _context.Citas.Add(citaEntity);
            _context.SaveChanges();

            return RedirectToAction("CitaList", "Cita");
        }

        public IActionResult CitaDeleted(Guid Id)
        {
            Cita? citaBorrar = _context.Citas.Where(c => c.Id == Id).FirstOrDefault();

            if (citaBorrar == null)
            {
                return RedirectToAction("CitaList", "Cita");
            }

            CitaModel model = new CitaModel
            {
                Id = citaBorrar.Id,
                NombreDeCita = citaBorrar.NombreDeCita,
                Telefono = citaBorrar.Telefono,
                DoctorDeCita = citaBorrar.DoctorDeCita,
                FechaDeCita = citaBorrar.FechaDeCita,
                
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult CitaDeleted(CitaModel cita)
        {
            bool exists = _context.Citas.Any(c => c.Id == cita.Id);
            if (!exists)
            {
                return View(cita);
            }

            Cita citaEntity = _context.Citas.Where(c => c.Id == cita.Id).First();
            _context.Citas.Remove(citaEntity);
            _context.SaveChanges();

            return RedirectToAction("CitaList", "Cita");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}