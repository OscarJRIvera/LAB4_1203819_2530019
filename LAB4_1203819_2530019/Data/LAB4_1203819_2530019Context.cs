using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LAB4_1203819_2530019.Models;

namespace LAB4_1203819_2530019.Data
{
    public class LAB4_1203819_2530019Context : DbContext
    {
        public LAB4_1203819_2530019Context (DbContextOptions<LAB4_1203819_2530019Context> options)
            : base(options)
        {
        }

        public DbSet<LAB4_1203819_2530019.Models.Tarea> Tarea { get; set; }
    }
}
