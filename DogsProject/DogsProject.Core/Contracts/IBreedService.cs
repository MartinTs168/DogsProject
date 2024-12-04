using DogsProject.Infrastructure.Data.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsProject.Core.Contracts
{
    public interface IBreedService
    {
        List<Breed> GetBreeds();
        Task<Breed?> GetBreedByIdAsync(int id);
        List<Dog> GetDogsByBreed(int breedId);
    }
}
