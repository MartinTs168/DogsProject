﻿using DogsProject.Infrastructure.Data.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DogsProject.Core.Contracts
{
    public interface IDogService
    {
        Task<bool> CreateAsync(string name, int age, string breed, string picture);
        Task<bool> UpdateDog(int dogId, string name, int age, string breed, string picture);

        List<Dog> GetDogs();
        Task<Dog> GetDogByIdAsync(int dogId);
        Task<bool> RemoveByIdAsync(int dogId);
        List<Dog> GetDogs(string searchStringBreed, string searchStringName);
    }
}
