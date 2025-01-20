using FinalDemo.Models;
using FinalDemo.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalDemo.BL.Interface
{
    public interface IUserHandler
    {
        List<UserDto> GetAllUsers();

        UserDto GetUserById(int userId);

        Responce UpdateUser(int userId , UserDto userDto);

        Responce DeleteUser(int userId);
    }
}