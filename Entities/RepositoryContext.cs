﻿using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions<RepositoryContext> options) :
            base(options)
        {                    
        }

        public DbSet<Company> MyCompany { get; set; }
        public DbSet<Employee> MyEmployee { get; set; }
    }
}