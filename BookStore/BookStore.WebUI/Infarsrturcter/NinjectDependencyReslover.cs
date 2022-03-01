using Moq;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore.Domain.Abstract;
using BookStore.Domain;
using BookStore.Domain.Entites;
using BookStore.Domain.Concerte;
using System.Configuration;
using BookStore.WebUI.Infarsrturcter.Abstract;
using BookStore.WebUI.Infarsrturcter.Abstract.Concrete;

namespace BookStore.WebUI.Infarsrturcter
{
    public class NinjectDependencyReslover : IDependencyResolver
    {
        private IKernel kernel;
      public   NinjectDependencyReslover (IKernel kernelpram) { kernel = kernelpram ;
            AddBindings();

        }
         
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);

        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

      private void AddBindings()
          {

            //Mock<IBookRepository> mock = new Mock<IBookRepository>();
            //mock.Setup(b => b.Books).Returns( 
            //    new List<Book> {
            //    new Book { Title ="Sql Server" ,Price=12},
            //    new Book {  Title = "Asp Mvc",Price=12},
            //    new Book {  Title = "Book CSharp" , Price = 23}
            //    }      );

            EmailSettings emailsettings = new EmailSettings()
            {
                WriteAsFile = bool.Parse(ConfigurationManager.AppSettings["Email.WriteAsFile"] ?? "false")
            };

             
            kernel.Bind<IBookRepository>().To<EFBookRespostriy>();

            kernel.Bind<IOrderProcessor>().To<EmailOrderProcessor>().WithConstructorArgument("setting", emailsettings);
            kernel.Bind<IAuthProvider>().To<FormsAutProvider>();

          }

 
    }
 
}