using BookStore.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Concerte
{
  public   class EF2dbContext : DbContext
    {


       public DbSet<Book> Books { get; set; }

                 
    }
}
