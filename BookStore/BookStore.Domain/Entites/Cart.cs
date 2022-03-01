using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Entites
{
    public class Cart
    {
        private List<Cartline> lineCollection= new List<Cartline>() ;
        
        public void Addltem (Book book , int quantity = 1)
        {

            Cartline line = lineCollection
                .Where(b => b.Book.ISBN == book.ISBN)
                .FirstOrDefault();

            if (line == null)
            {
                lineCollection.Add(new Cartline { Book = book, Quantity = quantity });

            }
            else

                line.Quantity += quantity;

        }
        public void Removeline (Book book)
        {
            lineCollection.RemoveAll(b => b.Book.ISBN == book.ISBN);       
        }

        public decimal computeTotal ()
        {
            return lineCollection.Sum(b => b.Book.Price * b.Quantity);
        }
          
        public void Clear ()
        {
              lineCollection.Clear();

        }
        public IEnumerable<Cartline> lines { get { return lineCollection; }  }

    }

    public class Cartline
    {
        public Book Book { set; get; }
        public int Quantity { set; get; }
    }
}
