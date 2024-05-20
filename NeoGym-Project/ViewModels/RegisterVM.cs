using System.ComponentModel.DataAnnotations;

namespace NeoGym_Project.ViewModels
{
    public class RegisterVM
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.Password)]

        public string Password { get; set; }
        [DataType(DataType.Password),Compare(nameof(DataType.Password))]

        public string ConfirmPassword { get; set; }
    }
}
