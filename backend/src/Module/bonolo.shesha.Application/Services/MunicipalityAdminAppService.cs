//using Abp.Domain.Repositories;
//using Abp.UI;
//using bonolo.shesha.Domain.Domain;
//using Microsoft.AspNetCore.Identity;
//using Shesha;
//using Shesha.Authorization.Roles;
//using Shesha.Authorization.Users;
//using Shesha.Domain;
//using Shesha.DynamicEntities.Dtos;
//using Shesha.Persons;
//using Shesha.UserManagements;
//using Shesha.Users.Dto;
//using System;
//using System.Collections.Concurrent;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace bonolo.shesha.Common.Services
//{
//    public class MunicipalityAdminAppService : SheshaAppServiceBase
//    {
//        private readonly IRepository<MunicipalityAdmin, Guid> _municipalityAdminRepository;
//        private readonly UserManager<User> _userManager;
//        private readonly RoleManager<Role> _roleManager;

//        public MunicipalityAdminAppService(IRepository<MunicipalityAdmin, Guid> municipalityAdminRepository, UserManager<User> userManager, RoleManager<Role> roleManager)
//        {
//            _municipalityAdminRepository = municipalityAdminRepository;
//            _userManager = userManager;
//            _roleManager = roleManager;
//        }

//        public async Task<UserDto> CreateWithUserAccountAsync(CreatePersonAccountDto input)
//        {
//            if (string.IsNullOrWhiteSpace(input.FirstName) || string.IsNullOrWhiteSpace(input.LastName) || string.IsNullOrWhiteSpace(input.EmailAddress))
//                throw new UserFriendlyException("First Name, Last Name and Email are required.");

//            var user = new User
//            {
//                UserName = input.EmailAddress,
//                EmailAddress = input.EmailAddress,
//                Name = input.FirstName,
//                Surname = input.LastName,
//                IsActive = true
//            };

//            var result = await _userManager.CreateAsync(user, "Pass@123");
//            if (!result.Succeeded)
//                throw new UserFriendlyException(result.Errors.First().Description);

//            var roleName = "MunicipalityAdmin";
//            if (!await _roleManager.RoleExistsAsync(roleName))
//            {
//                await _roleManager.CreateAsync(new Role
//                {
//                    Name = roleName,
//                    DisplayName = "Municipality Admin",
//                    IsStatic = true,
//                });
//            }

//            await _userManager.AddToRoleAsync(user, roleName);

//            return ObjectMapper.Map<UserDto>(user);
//        }
//    }
//}
