using System.ComponentModel.DataAnnotations;

namespace Task20_consumewebapioftask11_.Models
{
    public class UpdateEmailStatus
    {
        [Required(ErrorMessage = "Email is required.")]
            [EmailAddress(ErrorMessage = "Invalid email address.")]
            public string Email { get; set; }

            [Required(ErrorMessage = "EmailConfirmed is required.")]
            public bool EmailConfirmed { get; set; }
        

    }
}
