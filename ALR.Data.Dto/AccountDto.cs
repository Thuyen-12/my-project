using System.ComponentModel.DataAnnotations;

namespace ALR.Data.Dto
{
    public class AccountDto
    {
        [Required]
        public string Account { get; set; }
        [Required]
        public string Password { get; set; }
       
    }
}
