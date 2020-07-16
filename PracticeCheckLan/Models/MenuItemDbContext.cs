using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticeCheckLan.Models
{
    public class MenuItemDbContext:DbContext
    {
        public MenuItemDbContext(DbContextOptions<MenuItemDbContext> options):base(options)
        {
        }

    }
}
