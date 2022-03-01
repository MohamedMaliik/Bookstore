using BookStore.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Domain.Entites;

namespace BookStore.Domain.Concerte
{

   public  class EFBookRespostriy : IBookRepository

{
EF2dbContext context = new EF2dbContext();
public IEnumerable<Book> Books
        {
            get
            {
                return context.Books;                
            }
        }

        public void SaveBook(Book book)
        {
             
            
                Book dbEntity = context.Books.Find(book.ISBN);
                if(dbEntity==null)
                    context.Books.Add(book);

            else
            {
                dbEntity.Title = book.Title;
                dbEntity.Specialization = book.Specialization;
                dbEntity.Price = book.Price;
                dbEntity.Descrption = book.Descrption;
            }

            context.SaveChanges();
        }
        public  Book  DeleteBook (int ISBN)
        {
            Book dbEntity = context.Books.Find(ISBN);
            if (dbEntity != null)
            {
                context.Books.Remove(dbEntity);
                context.SaveChanges();
            }
            return dbEntity;
        }



    }
}
