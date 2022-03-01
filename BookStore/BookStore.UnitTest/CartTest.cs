using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BookStore.Domain.Entites;
using System.Linq;
using Moq;
using BookStore.Domain.Abstract;
using BookStore.WebUI.Controllers;
using System.Web.Mvc;
using BookStore.WebUI.Models;

namespace BookStore.UnitTest
{
    [TestClass]
    public class CartTest
    {
        [TestMethod]
        public void Can_AddNew_lines()
        {
            //Arrange
            Book b1 = new Book { ISBN = 1, Title = "Asp.Net" };
            Book b2 = new Book { ISBN = 2, Title = "Oracle" };
            //Act
            Cart target = new Cart();
            target.Addltem(b1);
            target.Addltem(b2,3);
            Cartline[] reult = target.lines.ToArray();

            //Assert

            Assert.AreEqual(reult[0].Book, b1);
            Assert.AreEqual(reult[1].Book, b2);
        }
         
            [TestMethod]
            public void Can_Add_Qty_for_Existing_lines()
            {
                //Arrange
                Book b1 = new Book { ISBN = 1, Title = "Asp.Net" };
                Book b2 = new Book { ISBN = 2, Title = "Oracle" };
                //Act
                Cart target = new Cart();
                target.Addltem(b1);
                 target.Addltem(b2, 3);
            target.Addltem(b1,5);
            Cartline[] reult = target.lines.OrderBy(c => c.Book.ISBN).ToArray();

            //Assert
            Assert.AreEqual(reult.Length, 2);
                Assert.AreEqual(reult[0].Quantity, 6);
                Assert.AreEqual(reult[1].Quantity, 3);
            }


        [TestMethod]
        public void Can_Remove_line()
        {
            //Arrange
            Book b1 = new Book { ISBN = 1, Title = "Asp.Net" };
            Book b2 = new Book { ISBN = 2, Title = "Oracle" };
            Book b3 = new Book { ISBN = 3, Title = "C#" };
            //Act
            Cart target = new Cart();
            target.Addltem(b1);
            target.Addltem(b2, 3);
            target.Addltem(b3, 5);
            target.Addltem(b2, 1);

            target.Removeline(b2);
           // Cartline[] reult = target.lines.OrderBy(c => c.Book.ISBN).ToArray();

            //Assert
            Assert.AreEqual( target.lines.Where(c1=>c1.Book==b2).Count(),0);
            Assert.AreEqual(target.lines.Count(), 2);
        

        }



        [TestMethod]
        public void Calculate_cart_Total()
        {
            //Arrange
            Book b1 = new Book { ISBN = 1, Title = "Asp.Net" ,Price=100};
            Book b2 = new Book { ISBN = 2, Title = "Oracle" , Price = 50};
            Book b3 = new Book { ISBN = 3, Title = "C#" ,Price = 70};
            //Act
            Cart target = new Cart();
            target.Addltem(b1);
            target.Addltem(b2, 2);
            target.Addltem(b3);

            decimal result = target.computeTotal();
            //Assert
            Assert.AreEqual(result ,  270M);
             


        }




        [TestMethod]
        public void Can_Clear_Contents()
        {
            //Arrange
            Book b1 = new Book { ISBN = 1, Title = "Asp.Net", Price = 100 };
            Book b2 = new Book { ISBN = 2, Title = "Oracle", Price = 50 };
            Book b3 = new Book { ISBN = 3, Title = "C#", Price = 70 };
            //Act
            Cart target = new Cart();
            target.Addltem(b1);
            target.Addltem(b2, 2);
            target.Addltem(b3,5);
            target.Addltem(b2, 1);
            target.Clear();

            //Assert
            Assert.AreEqual(target.lines.Count(),0);

        }

        //[TestMethod]
        //public void can_Add_To_Cart ()
        //{
        //    //Arrange
        //    Mock<IBookRepository> mock = new Mock<IBookRepository>();
        //    mock.Setup(m => m.Books).Returns(new Book[] {

        //        new Book {ISBN = 1 , Title = "Asp.Net Mvc", Specialization="Programming" }

        //    }.AsQueryable()
        //        );
        //    Cart cart = new Cart();
        //    CartController target = new CartController(mock.Object);
        //    //Act
        //     target.AddToCart(cart, 1, null);
        //     RedirectToRouteResult result =  target.AddToCart(cart, 2, "myUrl");
        //    //Assert
        //    Assert.AreEqual(result.RouteValues["action"], "Index");
        //    Assert.AreEqual(result.RouteValues["returnU"], "myUrl");
        //}



        [TestMethod]
        public void can_Add_To_Cart()
        {
            //Arrange
            Mock<IBookRepository> mock = new Mock<IBookRepository>();
            mock.Setup(m => m.Books).Returns(new Book[] {

                new Book {ISBN = 1 , Title = "Asp.Net Mvc", Specialization="Programming" }

            }.AsQueryable()
                );
            Cart cart = new Cart();
            CartController target = new CartController(mock.Object,null);
            //Act
            target.AddToCart(cart, 1, null);
            //Assert
            Assert.AreEqual(cart.lines.Count(), 1);
            Assert.AreEqual(cart.lines.ToArray()[0].Book.Title, "Asp.Net Mvc");
        }
         
        //
        [TestMethod]
        public void Adding_Book_To_Cart_Goes_To_Screen()
        {
            //Arrange
            Mock<IBookRepository> mock = new Mock<IBookRepository>();
            mock.Setup(m => m.Books).Returns(new Book[] {

                new Book {ISBN = 1 , Title = "Asp.Net Mvc", Specialization="Programming" }

            }.AsQueryable()
                );
            Cart cart = new Cart();
            CartController target = new CartController(mock.Object,null);
            //Act
            RedirectToRouteResult result = target.AddToCart(cart, 2, "myUrl");
            //Assert
            Assert.AreEqual( result.RouteValues["action"],"Index");
            Assert.AreEqual(result.RouteValues["returnU"], "myUrl");
        }

        [TestMethod]
        
        public void can_view_cart_Content ()
        {
            //Arrange
            Cart cart = new Cart();
            CartController target = new CartController(null,null);
            //Act
            CartIndexViewModel result = (CartIndexViewModel)target
                .Index(cart, "myUrl").ViewData.Model;
            //Assert

            Assert.AreEqual(result.Cart, cart);
            Assert.AreEqual(result.ReturnUrl, "myUrl");
           
        }


        [TestMethod]

        public void Cannot_Checkout_Empty_Cart()
        {
            //Arrange
           
             
            
            Mock<IOrderProcessor> mock = new Mock<IOrderProcessor>();
            Cart cart = new Cart();
            ShppingDetails shppingDetails = new ShppingDetails();
            CartController target = new CartController(null, mock.Object);


            //Act
            ViewResult result = target.Checkout(cart, shppingDetails);


           

            //Assert
            Assert.AreEqual("", result.ViewName);
            Assert.AreEqual(false, result.ViewData.ModelState.IsValid);
            
          
            


        }



        [TestMethod]

        public void Cannot_Checkout_Invalid_ShippingDeta()
        {
            //Arrange



            Mock<IOrderProcessor> mock = new Mock<IOrderProcessor>();
            Cart cart = new Cart();
            cart.Addltem(new Book(), 1);
            ShppingDetails shppingDetails = new ShppingDetails();
            CartController target = new CartController(null, mock.Object);
            target.ModelState.AddModelError("error", "error");
            //Act
            ViewResult result = target.Checkout(cart, shppingDetails);


           

            //Assert
            mock.Verify(m => m.ProcessorOrder(It.IsAny<Cart>(), It.IsAny<ShppingDetails>()), Times.Never());
            Assert.AreEqual("", result.ViewName);
            Assert.AreEqual(false, result.ViewData.ModelState.IsValid);
             

        }



        [TestMethod]

        public void Cannot_Checkout_And_Sumbit_Order()
        {
            //Arrange



            Mock<IOrderProcessor> mock = new Mock<IOrderProcessor>();
            Cart cart = new Cart();
            cart.Addltem(new Book(), 1);
            ShppingDetails shppingDetails = new ShppingDetails();
            CartController target = new CartController(null, mock.Object);
             
            //Act
            ViewResult result = target.Checkout(cart, shppingDetails);




            //Assert
            mock.Verify(m => m.ProcessorOrder(It.IsAny<Cart>(),
                It.IsAny<ShppingDetails>()), Times.Once());
            Assert.AreEqual("Completed", result.ViewName);
            Assert.AreEqual(true, result.ViewData.ModelState.IsValid);
            

        }
    }
}
