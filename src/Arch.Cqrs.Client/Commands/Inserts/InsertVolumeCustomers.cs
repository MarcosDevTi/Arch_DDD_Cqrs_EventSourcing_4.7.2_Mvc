using Arch.Infra.Shared.Cqrs.Commands;

namespace Arch.CqrsClient.Command.Customer
{
    public class InsertVolumeCustomers : ICommand
    {
        public InsertVolumeCustomers(int insertsCount)
        {
            InsertsCount = insertsCount;
        }
        public int InsertsCount { get; set; }
    }

    public class TrucateCustomers : ICommand
    {

    }
}
