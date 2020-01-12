using System;
using System.ComponentModel.DataAnnotations;

namespace Arch.Cqrs.Client.Command.Customer
{
    public abstract class CustomerCommand : Infra.Shared.Cqrs.Commands.Command
    {
        [Required(ErrorMessage = "The FirstName is Required")]
        [MinLength(2), MaxLength(100)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "The LastName is Required")]
        [MinLength(2), MaxLength(100)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "The E-mail is Required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "The BirthDate is Required")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        public DateTime BirthDate { get; set; }

        public int Score { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
    }
}
