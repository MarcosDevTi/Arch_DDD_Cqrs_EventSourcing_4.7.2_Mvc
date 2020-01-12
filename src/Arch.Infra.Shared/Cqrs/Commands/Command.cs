using System;
using Arch.Infra.Shared.Cqrs.Event;
using FluentValidation.Results;

namespace Arch.Infra.Shared.Cqrs.Commands
{
    public abstract class Command : Message
    {
        public ValidationResult ValidationResult { get; set; }
        public abstract bool IsValid();
    }
}
