using DataAnnotationsExtensions.ClientValidation;

[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(SGB_Beta1.App_Start.RegisterClientValidationExtensions), "Start")]
 
namespace SGB_Beta1.App_Start {
    public static class RegisterClientValidationExtensions {
        public static void Start() {
            DataAnnotationsModelValidatorProviderExtensions.RegisterValidationExtensions();            
        }
    }
}