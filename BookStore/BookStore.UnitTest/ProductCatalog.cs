using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BookStore.Domain.Abstract;
using Moq;
using BookStore.Domain.Entites;
using BookStore.WebUI.Controllers;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BookStore.WebUI.HtmlHelper;
using BookStore.WebUI.Models;

namespace BookStore.UnitTest {

    [TestClass]
    
public class ProductCatalog
{
    [TestMethod]
    public void Can_Paginate()
    {
        //Arrange
        Mock<IBookRepository> mock = new Mock<IBookRepository>();
        mock.Setup(b => b.Books).Returns(new Book[]

      {
            new Book { ISBN = 1 , Title="Book1" },
            new Book { ISBN = 2 , Title="Book2" },
            new Book { ISBN = 3 , Title="Book3" },
            new Book { ISBN = 4 , Title="Book4" },
            new Book { ISBN = 5 , Title="Book5" }

        }
            );

        BookController controller = new BookController(mock.Object);
        controller.pagesize = 3;
        //Act
        BookListViewModel result1 = (BookListViewModel)controller.List(null, 1).Model;
        //IEnumerable<Book> result = (IEnumerable <Book>) controller.List(1).Model;
        //Assert

        Book[] BookArray = result1.Books.ToArray();
        Assert.IsTrue(BookArray.Length == 3);
        Assert.AreEqual(BookArray[0].Title, "Book1");
        Assert.AreEqual(BookArray[1].Title, "Book2");
        Assert.AreEqual(BookArray[2].Title, "Book3");

    }


    //[TestMethod]
    //public void Can_Generate_Page_Links()
    //{
    //    //Arrange
    //    HtmlHelper myhelper = null;
    //    PagingInfo pageinfoo = new PagingInfo
    //    {

    //        currentPage = 2,
    //        itemperpage = 15,
    //        totalitem = 5

    //    };

    //    Func<int, string> pageurldelegate = i => "Page" + i;

    //    string expctedresult = "<a class=\"btn btn-default\"href=\"Page1\">1</a>"
    //                         + "<a class=\"btn btn-default btn-primary selected\"href=\"Page2\">2</a>"
    //                         + "<a class=\"btn btn-default\"href=\"Page3\">3</a> ";

    //    //Act 
    //    String result = myhelper.Pagelinks(pageinfoo, pageurldelegate).ToString();

    //    //Assert 

    //    Assert.AreEqual(expctedresult, result);

    //}

    [TestMethod]

    public void Can_send_Pageination_view_model()
    {

        //Arrang

        Mock<IBookRepository> mock = new Mock<IBookRepository>();
        mock.Setup(p => p.Books).Returns(
      new Book[] {
                new Book { ISBN = 1, Title = "Book1" },
                new Book { ISBN = 2, Title = "Book2" },
                new Book { ISBN = 3, Title = "Book3" },
                new Book { ISBN = 4, Title = "Book4" },
                new Book { ISBN = 5, Title = "Bool5" }
      }
            );
        BookController contrller = new BookController(mock.Object);
        contrller.pagesize = 3;

        //Act
        BookListViewModel result = (BookListViewModel)contrller.List(null, 2).Model;


        //Assert
        PagingInfo pageinfo = result.PagingInfo;
        Assert.AreEqual(pageinfo.currentPage, 2);
        Assert.AreEqual(pageinfo.itemperpage, 3);
        Assert.AreEqual(pageinfo.Totalpage, 2);
        Assert.AreEqual(pageinfo.totalitem, 5);

    }
    [TestMethod]
    public void Filter()
    {
        //Arrange
        Mock<IBookRepository> mock = new Mock<IBookRepository>();
        mock.Setup(p => p.Books).Returns(
      new Book[] {
                 new Book { ISBN = 1, Title = "Book1"  , Specialization =  "Css" },
                 new Book { ISBN = 2, Title = "Book2"  , Specialization =  "is"  },
                 new Book { ISBN = 3, Title = "Book3"  , Specialization =  "is"  },
                 new Book { ISBN = 4, Title = "Book4"  , Specialization  = "is"  },
                 new Book { ISBN = 5, Title = "Bool5"  , Specialization =  "is"  }
                 }

            );
        BookController contrller = new BookController(mock.Object);
        contrller.pagesize = 3;

        //Act
        Book[] result = ((BookListViewModel)contrller.List("is", 2).Model).Books.ToArray();

        //Assert
        Assert.AreEqual(result.Length, 1);
        // Assert.IsTrue(result[0].Title ="MIS" && result[0].Specialization = "is");
        Assert.IsTrue(result[0].Title == "Bool5" && result[0].Specialization == "is");
    }

    [TestMethod]

    public void can_create_speciliztion()
    {

        //Arrange
        Mock<IBookRepository> mock = new Mock<IBookRepository>();
        mock.Setup(p => p.Books).Returns(
      new Book[] {
                 new Book { ISBN = 1, Title = "Book1"  , Specialization =  "Css" },
                 new Book { ISBN = 2, Title = "Book2"  , Specialization =  "is"  },
                 new Book { ISBN = 3, Title = "Book3"  , Specialization =  "is"  },
                 new Book { ISBN = 4, Title = "Book4"  , Specialization  = "is"  },
                 new Book { ISBN = 5, Title = "Bool5"  , Specialization =  "is"  }
                 }

            );
        NavController contrller = new NavController(mock.Object);
        //contrller.pagesize = 3;

        //Act
        string[] result = ((IEnumerable<string>)contrller.Menu().Model).ToArray();



        //Assert
        Assert.AreEqual(result.Length, 2);
        // Assert.IsTrue(result[0].Title ="MIS" && result[0].Specialization = "is");
        Assert.IsTrue(result[0] == "Css" && result[1] == "is");

    }

    [TestMethod]

    public void Indicates_Selected_spc()
    {


        //Arrange
        Mock<IBookRepository> mock = new Mock<IBookRepository>();
        mock.Setup(p => p.Books).Returns(
      new Book[] {
                 new Book { ISBN = 1, Title = "Book1"  , Specialization =  "Css" },
                 new Book { ISBN = 2, Title = "Book2"  , Specialization =  "is"  },
                 new Book { ISBN = 3, Title = "Book3"  , Specialization =  "is"  },
                 new Book { ISBN = 4, Title = "Book4"  , Specialization  = "is"  },
                 new Book { ISBN = 5, Title = "Bool5"  , Specialization =  "is"  }
                 }

            );
        NavController contrller = new NavController(mock.Object);
        //contrller.pagesize = 3;

        //Act
        string result = contrller.Menu("is").ViewBag.Selecedsepc;



        //Assert
        Assert.AreEqual("is", result);



    }

    [TestMethod]

    public void Generate_spc_Specific_Book_Count()
    {


        //Arrange
        Mock<IBookRepository> mock = new Mock<IBookRepository>();
        mock.Setup(p => p.Books).Returns(
      new Book[] {
                 new Book { ISBN = 1, Title = "Book1"  , Specialization =  "is" },
                 new Book { ISBN = 2, Title = "Book2"  , Specialization =  "Cs"  },
                 new Book { ISBN = 3, Title = "Book3"  , Specialization =  "is"  },
                 new Book { ISBN = 4, Title = "Book4"  , Specialization  = "Cs"  },
                 new Book { ISBN = 5, Title = "Bool5"  , Specialization =  "is"  }
                 }

            );
        BookController contrller = new BookController(mock.Object);
        //contrller.pagesize = 3;

        //Act
        int result1 = ((BookListViewModel)contrller.List("is").Model).PagingInfo.totalitem;

        int result2 = ((BookListViewModel)contrller.List("Cs").Model).PagingInfo.totalitem;

        //Assert
        Assert.AreEqual(result1, 3);
        Assert.AreEqual(result2, 2);


    }
} 
}