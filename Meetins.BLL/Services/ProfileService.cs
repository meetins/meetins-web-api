﻿using Meetins.BLL.DTOs;
using Meetins.BLL.Interfaces;
using Meetins.DAL.Entities;
using Meetins.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meetins.BLL.Services
{
    public class ProfileService : IProfileService
    {
        private IUnitOfWork _db;

        public ProfileService(IUnitOfWork unitOfWork)
        {
            _db = unitOfWork;
        }

        public async Task<ProfileDto> GetUserProfile(Guid userId)
        {
            UserEntity user = await _db.Users.GetUserById(userId);

            var profileDto = new ProfileDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Gender = user.Gender,
                UserIcon = user.UserIcon,
                DateRegister = user.DateRegister
            };

            return profileDto;
        }

    }
}