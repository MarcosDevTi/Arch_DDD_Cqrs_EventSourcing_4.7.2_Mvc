using Arch.Infra.Shared.Cqrs.Commands;

namespace Arch.CqrsClient.Command.Customer
{
    public class InsertVolumeCustomersDapper : ICommand
    {
        public InsertVolumeCustomersDapper(int insertsCount)
        {
            InsertsCount = insertsCount;
        }
        public int InsertsCount { get; set; }
    }

    public class InsertOpenCloseDapper500 : ICommand
    {
        public InsertOpenCloseDapper500(int insertsCount)
        {
            InsertsCount = insertsCount;
        }
        public int InsertsCount { get; set; }
    }
}
