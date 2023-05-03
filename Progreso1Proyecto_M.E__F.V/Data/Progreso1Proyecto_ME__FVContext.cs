using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Progreso1Proyecto_M.E__F.V.Models;

namespace Progreso1Proyecto_M.E__F.V.Data
{
    public class Progreso1Proyecto_ME__FVContext : DbContext
    {
        public Progreso1Proyecto_ME__FVContext (DbContextOptions<Progreso1Proyecto_ME__FVContext> options)
            : base(options)
        {
        }

        public DbSet<Progreso1Proyecto_M.E__F.V.Models.Registro_M> Registro_M { get; set; } = default!;
    }
}
