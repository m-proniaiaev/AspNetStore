using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Store.Core.Contracts.Domain;
using Store.Core.Contracts.Interfaces;
using Store.Core.Contracts.Interfaces.Models;
using Store.Core.Contracts.Interfaces.Services;
using Store.Core.Host.Authorization.CurrentUser;

namespace Store.Core.Services.Authorization.Users.Commands.Update
{
    public class ChangeUserPasswordCommand : IRequest<User>, IIdentity
    {
        public Guid Id { get; set; }
        public string Password { get; set; }
    }
    public class ChangeUserPasswordCommandHandler : IRequestHandler<ChangeUserPasswordCommand, User>
    {
        private readonly IUserService _userService;
        private readonly IHasher _hasher;
        private readonly ICurrentUserService _currentUser;

        public ChangeUserPasswordCommandHandler(IUserService userService, IHasher hasher, ICurrentUserService currentUser)
        {
            _userService = userService;
            _hasher = hasher;
            _currentUser = currentUser;
        }

        public async Task<User> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetUserAsync(request.Id, cancellationToken);
            
            if (user == null)
                throw new ArgumentException("There is no such user!");
            
            var (salt, hash) = _hasher.Hash(request.Password);
            
            if (salt == null || hash == null)
                throw new ArgumentException("Can't set provided password!");
            
            user.Salt = salt;
            user.Hash = hash;
            user.Edited = DateTime.Now;
            user.EditedBy = _currentUser.Id;

            await _userService.ChangeUserPassword(user, cancellationToken);
            
            var result = await _userService.GetUserAsync(user.Id, cancellationToken);

            if (result is null)
                throw new InvalidOperationException("Can't update password!");

            return result;
        }
    }
}