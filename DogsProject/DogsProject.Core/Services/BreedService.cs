using DogsProject.Core.Contracts;
using DogsProject.Infrastructure;
using DogsProject.Infrastructure.Data.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsProject.Core.Services
{
    public class BreedService : IBreedService
    {
        private readonly ApplicationDbContext _context;

        public BreedService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Breed?> GetBreedByIdAsync(int id)
        {
            return await _context.Breeds.FindAsync(id);
        }

        public List<Breed> GetBreeds()
        {
            List<Breed> breeds = _context.Breeds.ToList();
            return breeds;
        }

        public List<Dog> GetDogsByBreed(int breedId)
        {
            return _context.Dogs
                .Where(x => x.BreedId == breedId).ToList();
        }
    }
}
