using DogsProject.Infrastructure;
using DogsProject.Infrastructure.Data.Entities;
using DogsProject.Models.Dog;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DogsProject.Controllers
{
    public class DogController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DogController(ApplicationDbContext context)
        {
            _context = context;
        }



        // GET: DogController
        public async Task<IActionResult> Index()
        {
            List<DogAllViewModel> dogs = await _context.Dogs
                .Select(dogFromDb => new DogAllViewModel
                {
                    Id = dogFromDb.Id,
                    Name = dogFromDb.Name,
                    Age = dogFromDb.Age,
                    Breed = dogFromDb.Breed,
                    Picture = dogFromDb.Picture,
                }).ToListAsync();

            return View(dogs);
        }

        // GET: DogController/Details/5
        [HttpGet]
        public async Task<ActionResult> Details(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            Dog? dogFromDb = await _context.Dogs.FindAsync(id);
            if (dogFromDb == null)
            {
                return NotFound();
            }
            DogDetailsViewModel dog = new DogDetailsViewModel()
            {
                Id = dogFromDb.Id,
                Name = dogFromDb.Name,
                Age = dogFromDb.Age,
                Breed = dogFromDb.Breed,
                Picture = dogFromDb.Picture,
            };
            return View(dog);
        }

        // GET: DogController/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: DogController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DogCreateViewModel model)
        {
            if(ModelState.IsValid)
            {
                Dog dog = new Dog
                {
                    Name = model.Name,
                    Age  = model.Age,
                    Breed = model.Breed,
                    Picture = model.Picture,
                };

                _context.Add(dog);
                await _context.SaveChangesAsync();

                return RedirectToAction("Success");
            }

            return View();
        }

        public IActionResult Success()
        {
            return View();
        }

        // GET: DogController/Edit/5
        [HttpGet]
        public async Task<ActionResult> Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            Dog? dogFromDb = await _context.Dogs.FindAsync  (id);
            if(dogFromDb == null)
            {
                return NotFound();
            }

            DogEditViewModel dog = new DogEditViewModel()
            {
                Id = dogFromDb.Id,
                Name = dogFromDb.Name,
                Age = dogFromDb.Age,
                Breed = dogFromDb.Breed,
                Picture = dogFromDb.Picture

            };

            return View(dog);
        }

        // POST: DogController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, DogEditViewModel model)
        {
            if(ModelState.IsValid)
            {
                Dog dog = new Dog()
                {
                    Id = id,
                    Name = model.Name,
                    Age = model.Age,
                    Breed = model.Breed,
                    Picture = model.Picture

                };

                _context.Dogs.Update(dog);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: DogController/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            Dog? dogFromDb = await _context.Dogs.FindAsync(id);
            if(dogFromDb == null)
            {
                return NotFound();
            }

            DogDetailsViewModel dog = new DogDetailsViewModel()
            {
                Id = dogFromDb.Id,
                Name = dogFromDb.Name,
                Age = dogFromDb.Age,
                Breed = dogFromDb.Breed,
                Picture = dogFromDb.Picture
            };

            return View(dog);
        }

        // POST: DogController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            Dog? dog = await _context.Dogs.FindAsync(id);
            if(dog == null)
            {
                return NotFound();
            }

            _context.Dogs.Remove(dog);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Dog");
        }
    }
}
