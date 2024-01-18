using System.ComponentModel.DataAnnotations;

namespace ExamApp.ViewModels
{
    public class RegisterVM
    {
        [Required]
        [MaxLength(40)]
        [MinLength(3)]
        public string Name { get; set; }
        [Required]
        [MaxLength(40)]
        [MinLength(3)]
        public string Surname { get; set; }
        [Required]
        [MaxLength(40)]
        [MinLength(3)]
        public string Username { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }

    }
}
