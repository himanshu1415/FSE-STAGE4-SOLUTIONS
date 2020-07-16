using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalCheckLan.Models
{
    public class MovieItemDbcontext:DbContext
    {
        public MovieItemDbcontext(DbContextOptions<MovieItemDbcontext> options) : base(options)
        {
        }
        public virtual DbSet<MovieItem> GetMovieItems { get; set; }

    }
}
