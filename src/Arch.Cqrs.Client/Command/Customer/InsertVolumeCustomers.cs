using Arch.Infra.Shared.Cqrs.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arch.Cqrs.Client.Command.Customer
{
    public class InsertVolumeCustomers: ICommand
    {
        public InsertVolumeCustomers(int insertsCount)
        {
            InsertsCount = insertsCount;
        }
        public int InsertsCount { get; set; }
    }

    public class TrucateCustomers: ICommand
    {

    }
}
