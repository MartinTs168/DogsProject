﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsProject.Infrastructure.Data.Entities
{
    public class Breed
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        public virtual IEnumerable<Dog> Dogs { get; set; } = null!;
    }
}
