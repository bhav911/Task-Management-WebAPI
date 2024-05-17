using MVCCrud.Repository.Interface;
using MVCCrud.Repository.Services;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace MVCCrud
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IStudentInterface, StudentsServices>();
            container.RegisterType<ITeacherInterface, TeachersServices>();
            
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}