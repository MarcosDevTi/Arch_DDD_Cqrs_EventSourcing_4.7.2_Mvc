﻿using Arch.CqrsClient.Command.User;
using Arch.CqrsClient.Query.User;
using Arch.Infra.Data;
using Arch.Infra.Shared.Cqrs.Commands;
using Arch.Infra.Shared.Cqrs.Query;
using System.Linq;
using System.Text;

namespace Arch.CqrsHandlers.User
{
    public class UserHandler :
        ICommandHandler<RegisterUser>,
        IQueryHandler<LoginUser, Domain.Models.User>,
        IQueryHandler<UserExists, bool>
    {
        private readonly ArchCoreContext _archDbContext;

        public UserHandler(ArchCoreContext archDbContext)
        {
            _archDbContext = archDbContext;
        }

        public void Handle(RegisterUser command)
        {
            var user = new Domain.Models.User(command.Username, command.Password);

            _archDbContext.Users.Add(user);
            _archDbContext.SaveChanges();
        }

        public Domain.Models.User Handle(LoginUser query)
        {
            var user = _archDbContext.Users.FirstOrDefault(x => x.Username == query.Username);
            if (user == null) return null;
            if (!VerifyPasswordHash(query.Password, user.PasswordHash, user.PasswordSalt)) return null;
            return user;
        }

        public bool Handle(UserExists query) =>
            _archDbContext.Users.Any(x => x.Username == query.UserName);


        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i]) return false;
                }
            }
            return true;
        }
    }
}
