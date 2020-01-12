using Arch.Infra.Shared.Cqrs.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arch.Cqrs.Client.Command.Customer
{
    public class InsertVolumeCustomersDapper: ICommand
    {
        public InsertVolumeCustomersDapper(int insertsCount)
        {
            InsertsCount = insertsCount;
        }
        public int InsertsCount { get; set; }
    }

    public class InsertOpenCloseDapper500: ICommand
    {
        public InsertOpenCloseDapper500(int insertsCount)
        {
            InsertsCount = insertsCount;
        }
        public int InsertsCount { get; set; }
    }
}
