using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TcOpen.Inxton.Data;
using TcOpen.Inxton.Local.Security.Blazor.UnitOfWork;
using TcOpen.Inxton.Local.Security.Blazor.Users;

namespace TcOpen.Inxton.Local.Security.Blazor.Stores
{
    public class UserStore:
        IUserStore<User>,
        IUserPasswordStore<User>,
        IUserRoleStore<User>,
        IUserSecurityStampStore<User>,
        IUserClaimStore<User>,
        IQueryableUserStore<User>
    {
        public IdentityErrorDescriber ErrorDescriber { get; set; }

        public IQueryable<User> Users
        {
            get
            {
                return _unitOfWork.UserRepository.GetRecords("*").Select(x => new User(x)).AsQueryable();
            }
        }

        private IList<BlazorRole> _roleCollection
        {
            get
            {
                return _unitOfWork.RoleRepository.GetRecords("*").ToList();
            }
        }


    private readonly IUnitOfWork _unitOfWork;
        public UserStore(IUnitOfWork unitOfWork, IdentityErrorDescriber errorDescriber = null)
        {
            ErrorDescriber = errorDescriber;
            _unitOfWork = unitOfWork;
        }
        private bool _disposed;
        protected void ThrowIfDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
        }

      

        public void Dispose()
        {
            _disposed = true;
        }

        public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken = default)
        {

            cancellationToken.ThrowIfCancellationRequested();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return Task.FromResult(user.Id);
        }

        public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return Task.FromResult(user.UserName);
        }

        public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            user.UserName = userName;

            return Task.CompletedTask;
        }

        public Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return Task.FromResult(user.NormalizedUserName);
        }

        public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            user.NormalizedUserName = normalizedName;

            return Task.CompletedTask;
        }

        public Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var userEntity = new UserData
            {
                Username = user.UserName,
                Email = user.Email,
                HashedPassword = user.PasswordHash,
                SecurityStamp = user.SecurityStamp,
                Roles = new ObservableCollection<string>(user.Roles.ToList())
            };

            try
            {
                _unitOfWork.UserRepository.Create(user.NormalizedUserName, userEntity);
            }
            catch (DuplicateIdException)
            {
                return Task.FromResult(IdentityResult.Failed(new IdentityError { Description = $"User with username {user.UserName} already exists."}));
            }

            return Task.FromResult(IdentityResult.Success);
        }

        public Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var userData = _unitOfWork.UserRepository.Read(user.Id);
            userData.Roles = new ObservableCollection<string>(user.Roles.ToList());
           

            try
            {
                _unitOfWork.UserRepository.Update(user.NormalizedUserName, userData);
            }
            catch (UnableToLocateRecordId)
            {
                return Task.FromResult(IdentityResult.Failed(new IdentityError { Description = $"User with username {user.UserName} doesn't exists." }));
            }


            return Task.FromResult(IdentityResult.Success);
        }

        public Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            _unitOfWork.UserRepository.Delete(user.NormalizedUserName);

            return Task.FromResult(IdentityResult.Success);
        }

        public Task<User> FindByIdAsync(string entityId, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (string.IsNullOrWhiteSpace(entityId))
                throw new ArgumentNullException(nameof(entityId));
            User user;
            try
            {
                var userData = _unitOfWork.UserRepository.Read(entityId);
                user = new User(userData);
            }
            catch (UnableToLocateRecordId)
            {
                user = null;
            }
            

            return Task.FromResult(user);
        }

        public Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (string.IsNullOrWhiteSpace(normalizedUserName))
                throw new ArgumentNullException(nameof(normalizedUserName));

            User user;
            try
            {
                var userData = _unitOfWork.UserRepository.Read(normalizedUserName);
                user = new User(userData);
            }
            catch (UnableToLocateRecordId)
            {
                user = null;
            }
           
            
            return Task.FromResult(user);
        }

        public Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            user.PasswordHash = passwordHash;

            return Task.CompletedTask;
        }

        public Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return Task.FromResult(!string.IsNullOrWhiteSpace(user.PasswordHash));
        }

        public Task AddToRoleAsync(User user, string roleName, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            if (string.IsNullOrWhiteSpace(roleName))
                throw new ArgumentNullException(nameof(roleName));


            var role = _roleCollection.FirstOrDefault(x=>x.NormalizedName == roleName);
            if (role == null)
            {
                throw new InvalidOperationException(string.Format(System.Globalization.CultureInfo.CurrentCulture, "Role {0} does not exist.", roleName));
            }

            var roleList = new List<string>();
            roleList.Add(role._EntityId);

            user.Roles = roleList.ToArray();
           
            // _unitOfWork.UserRepository.Update(user.Id, user);
            //_unitOfWork.UserRepository.U
           
            

            return Task.CompletedTask;
        }

        public Task RemoveFromRoleAsync(User user, string normalizedRoleName, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            if (string.IsNullOrWhiteSpace(normalizedRoleName))
                throw new ArgumentNullException(nameof(normalizedRoleName));

            var role = _roleCollection.FirstOrDefault(x => x.NormalizedName == normalizedRoleName);
            if (role != null)
            {
                _unitOfWork.RoleRepository.Delete(role._EntityId);
            }
            

            return Task.CompletedTask;
        }

        public Task<IList<string>> GetRolesAsync(User user, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            IList<string> result = _unitOfWork.RoleRepository.GetRecords(user.Id).Select(x=> x.Name).ToList();


            return Task.FromResult(result);
        }

        public Task<bool> IsInRoleAsync(User user, string normalizedRoleName, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            if (string.IsNullOrWhiteSpace(normalizedRoleName))
                throw new ArgumentNullException(nameof(normalizedRoleName));

            var blazorRole = _roleCollection.FirstOrDefault(x => x.NormalizedName == normalizedRoleName);

            if (blazorRole == null) return Task.FromResult(false);



            var result = user.Roles.Contains(blazorRole._EntityId);
            return Task.FromResult(result);
        }
                


        public Task<IList<User>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task SetSecurityStampAsync(User user, string stamp, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            user.SecurityStamp = stamp;

            return Task.CompletedTask;
        }

        public Task<string> GetSecurityStampAsync(User user, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return Task.FromResult(user.SecurityStamp);
        }

        public Task<IList<Claim>> GetClaimsAsync(User user, CancellationToken cancellationToken = default)
        {
            IList<Claim> claims = new List<Claim>();
            return Task.FromResult(claims);
            //throw new NotImplementedException();
        }

        public Task AddClaimsAsync(User user, IEnumerable<Claim> claims, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task ReplaceClaimAsync(User user, Claim claim, Claim newClaim, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task RemoveClaimsAsync(User user, IEnumerable<Claim> claims, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IList<User>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        
    }
}
