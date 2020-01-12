﻿namespace Arch.Infra.Shared.Cqrs.Commands
{
    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        void Handle(TCommand command);
    }
}
