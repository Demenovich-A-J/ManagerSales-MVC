using System.ComponentModel.DataAnnotations;

namespace ManagerSales.Web.GUI.Models
{
    public class LoginViewModel
    {
        [Display(Name = "Логин")]
        public string Login { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
    }
}