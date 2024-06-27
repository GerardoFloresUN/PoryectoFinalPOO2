using System.Diagnostics;
using Dermatologia.Entities;
using Dermatologia.Models;
using Microsoft.AspNetCore.Mvc;

namespace Dermatologia.Controllers
{
    public class DoctorController : Controller
    {
        private readonly ILogger<DoctorController> _logger;
        private readonly ApplicationDbContext _context;

        public DoctorController(ILogger<DoctorController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult DoctorList()
        {
            List<DoctorModel> list = _context.Doctores.Select(d => new DoctorModel()
            {
                Id = d.Id,
                Nombre = d.Nombre,
                Especialidad = d.Especialidad,
                Experiencia = d.Experiencia,
                Educacion = d.Educacion,
                
            }).ToList();

            return View(list);
        }

        [HttpGet]
        public IActionResult DoctorEdit(Guid Id)
        {
            Doctor? doctorActualizar = _context.Doctores.Where(d => d.Id == Id).FirstOrDefault();
            if (doctorActualizar == null)
            {
                return RedirectToAction("DoctorList");
            }

            DoctorModel model = new DoctorModel
            {
                Id = doctorActualizar.Id,
                Nombre = doctorActualizar.Nombre,
                Especialidad = doctorActualizar.Especialidad,
                Experiencia = doctorActualizar.Experiencia,
                Educacion = doctorActualizar.Educacion,
                
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult DoctorEdit(DoctorModel model)
        {
            if (ModelState.IsValid)
            {
                Doctor doctorActualizar = _context.Doctores.Where(d => d.Id == model.Id).First();
                if (doctorActualizar == null)
                {
                    return RedirectToAction("DoctorList");
                }

                doctorActualizar.Nombre = model.Nombre;
                doctorActualizar.Especialidad = model.Especialidad;
                doctorActualizar.Experiencia = model.Experiencia;
                doctorActualizar.Educacion = model.Educacion;
                

                _context.Doctores.Update(doctorActualizar);
                _context.SaveChanges();

                return RedirectToAction("DoctorList");
            }

            return View(model);
        }

        public IActionResult DoctorAdd()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DoctorAdd(DoctorModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var doctorEntity = new Doctor();
            
                doctorEntity.Id = Guid.NewGuid();
                doctorEntity.Nombre = model.Nombre;
                doctorEntity.Especialidad = model.Especialidad;
                doctorEntity.Experiencia = model.Experiencia;
                doctorEntity.Educacion = model.Educacion;
                
            

            _context.Doctores.Add(doctorEntity);
            _context.SaveChanges();

            return RedirectToAction("DoctorList", "Doctor");
        }

        public IActionResult DoctorDeleted(Guid Id)
        {
            Doctor? doctorBorrar = _context.Doctores.Where(d => d.Id == Id).FirstOrDefault();

            if (doctorBorrar == null)
            {
                return RedirectToAction("DoctorList", "Doctor");
            }

            DoctorModel model = new DoctorModel
            {
                Id = doctorBorrar.Id,
                Nombre = doctorBorrar.Nombre,
                Especialidad = doctorBorrar.Especialidad,
                Experiencia = doctorBorrar.Experiencia,
                Educacion = doctorBorrar.Educacion,
                
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult DoctorDeleted(DoctorModel doctor)
        {
            bool exists = _context.Doctores.Any(d => d.Id == doctor.Id);
            if (!exists)
            {
                return View(doctor);
            }

            Doctor doctorEntity = _context.Doctores.Where(d => d.Id == doctor.Id).First();
            _context.Doctores.Remove(doctorEntity);
            _context.SaveChanges();

            return RedirectToAction("DoctorList", "Doctor");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}