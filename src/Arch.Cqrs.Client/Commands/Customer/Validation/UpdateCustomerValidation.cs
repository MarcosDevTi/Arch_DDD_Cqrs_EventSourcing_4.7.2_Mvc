namespace Arch.CqrsClient.Command.Customer.Validation
{
    public class UpdateCustomerValidation : CustomerCommandValidation<UpdateCustomer>
    {
        public UpdateCustomerValidation()
        {
            ValidateFirstName();
            ValidateLastName();
            ValidateBirthDate();
        }
    }
}
