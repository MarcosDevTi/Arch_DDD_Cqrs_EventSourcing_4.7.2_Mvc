using Arch.Infra.Shared.Cqrs.Commands;
using System;

namespace Arch.CqrsClient.Command.Customer.Generics
{
    public class ListUpdate : ICommand
    {
        public Guid Id { get; set; }
        public string Key { get; set; }
        public dynamic Value { get; set; }
        public string AssemblyModel { get; set; }
    }
}