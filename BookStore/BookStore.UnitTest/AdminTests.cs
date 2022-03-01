using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using BookStore.WebUI.Infarsrturcter.Abstract;
using BookStore.Domain.Abstract;
using BookStore.Domain.Entites;
using BookStore.WebUI.Controllers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BookStore.UnitTest
{
    [TestClass]
    public class AdminTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            //Arrange
            Mock<IBookRepository> mock = new Mock<IBookRepository>();

            mock.Setup(m => m.Books).Returns(new Book[]
            {
                new Book {ISBN = 1 , Title="Book1" },
                new Book {ISBN = 2 , Title="Book2" },
                new Book {ISBN = 3 , Title="Book3" }

            });

            AdminController target = new AdminController(mock.Object);


            //Act
            Book[] Result = ((IEnumerable<Book>)target.Index().ViewData.Model).ToArray();



            //Assert
            Assert.AreEqual(Result.Length, 3);
            Assert.AreEqual("Book1", Result[0].Title);
            Assert.AreEqual("Book3", Result[2].Title);
           


        }

        [TestMethod]
        public void Can_Edid_Book ()
        {
            Mock<IBookRepository> mock = new Mock<IBookRepository>();
            mock.Setup(b => b.Books).Returns(
                new Book[]
                {

                    new Book {ISBN= 1 , Title="Book1" },
                    new Book {ISBN= 2 , Title="Book2" },
                    new Book {ISBN =3 , Title="Book3" }

                }

                );
            AdminController target = new AdminController(mock.Object);

            //Act
            Book b1 = target.Edit(1).ViewData.Model as Book;
            Book b2 = target.Edit(2).ViewData.Model as Book;
            Book b3 = target.Edit(3).ViewData.Model as Book;

            //Assert 
            Assert.AreEqual("Book1",b1.Title);
            Assert.AreEqual(2, b2.ISBN);
            Assert.AreEqual("Book3", b3.Title);


        }


        [TestMethod]
        public void Can_Not_Edid_Book()
        {
            Mock<IBookRepository> mock = new Mock<IBookRepository>();
            mock.Setup(b => b.Books).Returns(
                new Book[]
                {

                    new Book {ISBN= 1 , Title="Book1" },
                    new Book {ISBN= 2 , Title="Book2" },
                    new Book {ISBN =3 , Title="Book3" }

                }

                );
            AdminController target = new AdminController(mock.Object);

            //Act
            
            Book b4= target.Edit(4).ViewData.Model as Book;

            //Assert 
            Assert.IsNull(b4);
       


        }

        [TestMethod]
        public void Can_Save_Valid_Changes ()
        {
            //Arrange
            Mock<IBookRepository> mock = new Mock<IBookRepository>();
            AdminController target = new AdminController(mock.Object);
            Book book = new Book
            {
                Title = "TestBook"

            };
         ActionResult result= target.Edit(book);


            //Act


            //Assert
            mock.Verify(b => b.SaveBook(book));
            Assert.IsNotInstanceOfType(result, typeof(ViewResult));


        }



        [TestMethod]

        public void Can_Save_Valid_Change()
        {
            //Arrange
            Mock<IBookRepository> mock = new Mock<IBookRepository>();
            AdminController target = new AdminController(mock.Object);
            Book book = new Book
            {
                Title = "TestBook"

            };
            ActionResult result = target.Edit(book);


            //Act


            //Assert
            mock.Verify(b => b.SaveBook(book));
            Assert.IsNotInstanceOfType(result, typeof(ViewResult));


        }



    }
}
