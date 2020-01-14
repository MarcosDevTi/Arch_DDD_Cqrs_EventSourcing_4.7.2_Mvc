using Arch.Infra.Shared.Cqrs.Contracts;
using System.ComponentModel.DataAnnotations;

namespace Arch.CqrsClient.Command.User
{
    public class RegisterUser : CommandAction
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [StringLength(8, MinimumLength = 4, ErrorMessage = "You must specify password between 4 and 8")]
        public string Password { get; set; }

        public override bool IsValid() => true;
    }
}
