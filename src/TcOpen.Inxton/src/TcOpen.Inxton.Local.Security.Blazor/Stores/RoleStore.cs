using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
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
    public class RoleStore :
        IRoleStore<IdentityRole>
    {
        public IdentityErrorDescriber ErrorDescriber { get; set; }

        public IList<BlazorRole> Roles {
            get 
            {
                return _unitOfWork.RoleRepository.GetRecords("*").ToList();
            }
        }

        private readonly IUnitOfWork _unitOfWork;
        public RoleStore(IUnitOfWork unitOfWork, IdentityErrorDescriber errorDescriber = null)
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
        public Task<IdentityResult> CreateAsync(IdentityRole role, CancellationToken cancellationToken)
        {
            if (cancellationToken != null)
                cancellationToken.ThrowIfCancellationRequested();

            if (role == null)
                throw new ArgumentNullException(nameof(role));

            var roleEntity = new BlazorRole
            {
                Name = role.Name,
                NormalizedName = role.NormalizedName,
                ConcurrencyStamp = role.ConcurrencyStamp,
                _EntityId = role.Id
            };

            _unitOfWork.RoleRepository.Create(role.Id, roleEntity);

            return Task.FromResult(IdentityResult.Success);
        }

        public Task<IdentityResult> DeleteAsync(IdentityRole role, CancellationToken cancellationToken)
        {
            if (cancellationToken != null)
                cancellationToken.ThrowIfCancellationRequested();

            if (role == null)
                throw new ArgumentNullException(nameof(role));

            _unitOfWork.RoleRepository.Delete(role.NormalizedName);

            return Task.FromResult(IdentityResult.Success);
        }


        public Task<IdentityRole> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (string.IsNullOrWhiteSpace(roleId))
                throw new ArgumentNullException(nameof(roleId));

            IdentityRole identityRole = null;
          
            var roleData = Roles.FirstOrDefault(x => x._EntityId == roleId);
            if (roleData != null)
            {
                identityRole = new IdentityRole
                {
                    Name = roleData.Name,
                    ConcurrencyStamp = roleData.ConcurrencyStamp,
                    Id = roleData._EntityId
                };
            }
            
            

            return Task.FromResult(identityRole);
        }

        public Task<IdentityRole> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {

            cancellationToken.ThrowIfCancellationRequested();

            if (string.IsNullOrWhiteSpace(normalizedRoleName))
                throw new ArgumentNullException(nameof(normalizedRoleName));

            IdentityRole identityRole = null;
          
            var roleData = Roles.FirstOrDefault(x => x.NormalizedName == normalizedRoleName);
            if (roleData != null)
            {
                identityRole = new IdentityRole
                {
                    Name = roleData.Name,
                    ConcurrencyStamp = roleData.ConcurrencyStamp,
                    NormalizedName = roleData.Name.ToUpper(),
                    Id = roleData._EntityId

                };
            }
            

            

            return Task.FromResult(identityRole);
        }

        public Task<string> GetNormalizedRoleNameAsync(IdentityRole role, CancellationToken cancellationToken)
        {
            if (cancellationToken != null)
                cancellationToken.ThrowIfCancellationRequested();

            if (role == null)
                throw new ArgumentNullException(nameof(role));

            return Task.FromResult(role.NormalizedName);
        }

        public Task<string> GetRoleIdAsync(IdentityRole role, CancellationToken cancellationToken)
        {
            if (cancellationToken != null)
                cancellationToken.ThrowIfCancellationRequested();

            if (role == null)
                throw new ArgumentNullException(nameof(role));

            return Task.FromResult(role.Id);
        }

        public Task<string> GetRoleNameAsync(IdentityRole role, CancellationToken cancellationToken)
        {
            if (cancellationToken != null)
                cancellationToken.ThrowIfCancellationRequested();

            if (role == null)
                throw new ArgumentNullException(nameof(role));

            return Task.FromResult(role.Name);
        }

        public Task SetNormalizedRoleNameAsync(IdentityRole role, string normalizedName, CancellationToken cancellationToken)
        {
            if (cancellationToken != null)
                cancellationToken.ThrowIfCancellationRequested();

            if (role == null)
                throw new ArgumentNullException(nameof(role));

            role.NormalizedName = normalizedName;

            return Task.CompletedTask;
        }

        public Task SetRoleNameAsync(IdentityRole role, string roleName, CancellationToken cancellationToken)
        {
            if (cancellationToken != null)
                cancellationToken.ThrowIfCancellationRequested();

            if (role == null)
                throw new ArgumentNullException(nameof(role));

            role.Name = roleName;

            return Task.CompletedTask;
        }

        public Task<IdentityResult> UpdateAsync(IdentityRole role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _disposed = true;
        }
    }
}
