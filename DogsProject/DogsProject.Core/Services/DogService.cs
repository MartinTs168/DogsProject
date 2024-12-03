using DogsProject.Core.Contracts;
using DogsProject.Infrastructure;
using DogsProject.Infrastructure.Data.Entities;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsProject.Core.Services
{
    public class DogService : IDogService
    {
        private readonly ApplicationDbContext _context;

        public DogService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateAsync(string name, int age, string breed, string picture)
        {
            Dog item = new Dog
            {
                Name = name,
                Age = age,
                Breed = breed,
                Picture = picture
            };

            await _context.Dogs.AddAsync(item);
            return await _context.SaveChangesAsync() != 0;
        }

        public async Task<Dog> GetDogByIdAsync(int dogId)
        {
            return await _context.Dogs.FindAsync(dogId);
        }

        public List<Dog> GetDogs(string searchStringBreed, string searchStringName)
        {
            List<Dog> dogs = _context.Dogs.ToList();

            if (!String.IsNullOrEmpty(searchStringBreed) && !String.IsNullOrEmpty(searchStringName))
            {
                dogs = dogs.Where(d => d.Breed.Contains(searchStringBreed) && d.Name.Contains(searchStringName)).ToList();
            }

            else if (!String.IsNullOrEmpty(searchStringName))
            {
                dogs = dogs.Where(d => d.Name.Contains(searchStringName)).ToList();
            }
            else if (!String.IsNullOrEmpty(searchStringBreed))
            {
                dogs = dogs.Where(d => d.Breed.Contains(searchStringBreed)).ToList();
            }

            return dogs;
        }

        public List<Dog> GetDogs()
        {
            List<Dog> dogs = _context.Dogs.ToList();
            return dogs;
        }

        public async Task<bool> RemoveByIdAsync(int dogId)
        {
            var dog = await GetDogByIdAsync(dogId);
            if (dog == default(Dog))
            {
                return false;
            }

            _context.Remove(dog);
            return await _context.SaveChangesAsync() != 0;
        }

        public async Task<bool> UpdateDog(int dogId, string name, int age, string breed, string picture)
        {
            var dog = await GetDogByIdAsync(dogId);

            if (dog == default(Dog)) { return false; }

            dog.Name = name;
            dog.Age = age;
            dog.Breed = breed;
            dog.Picture = picture;
            _context.Update(dog);
            return await _context.SaveChangesAsync() != 0;
        }
    }
}
