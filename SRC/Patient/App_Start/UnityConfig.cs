using PatientContracts;
using PatientDL;
using PatientProvider;
using System.Web.Http;
using Unity;
using Unity.Lifetime;
using Unity.WebApi;

namespace patient
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IPatient, PatientProviderBLD>("BLD");
            container.RegisterType<IPatient, PatientDTO>("DTO");

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}