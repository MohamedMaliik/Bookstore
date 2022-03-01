using BookStore.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Abstract
{
   public  interface IOrderProcessor
    {

        void ProcessorOrder(Cart cart, ShppingDetails shppingDetails);
   
         
    }
}
