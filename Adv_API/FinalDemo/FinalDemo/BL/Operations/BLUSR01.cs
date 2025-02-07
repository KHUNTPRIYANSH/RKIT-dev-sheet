using FinalDemo.BL.Interface;
using FinalDemo.Extension;
using FinalDemo.Helpers;
using FinalDemo.Models;
using FinalDemo.Models.DTO;
using FinalDemo.Models.ENUM;
using FinalDemo.Models.POCO;
using Org.BouncyCastle.Ocsp;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using System.Web;

namespace FinalDemo.BL.Operations
{
    /// <summary>
    /// Business logic handler for the user (USR01) entity.
    /// Implements IDataHandler interface for operations related to users.
    /// </summary>
    public class BLUSR01 : IDataHandler<DTOUSR01>
    {
        // Fields
        private USR01 _objUsr01;
        private int _id;
        private Response _objResponse;
        private readonly IDbConnectionFactory _dbFactory;

        /// <summary>
        /// Property to set the operation type (Add, Edit, etc.)
        /// </summary>
        public EnumType Type { get; set; }

        /// <summary>
        /// Constructor initializes the response and database connection factory.
        /// </summary>
        /// <exception cref="Exception">Thrown if IDbConnectionFactory is not found in the application context.</exception>
        public BLUSR01()
        {
            _objResponse = new Response();
            _dbFactory = HttpContext.Current.Application["DbFactory"] as IDbConnectionFactory;

            if (_dbFactory == null)
            {
                throw new Exception("IDbConnectionFactory not found");
            }
        }

        /// <summary>
        /// Retrieves all users from the database.
        /// </summary>
        /// <returns>A Response object containing the list of users.</returns>
        public Response GetAll()
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                _objResponse.Data = db.Select<USR01>();
                if (_objResponse.Data == null)
                {
                    _objResponse.IsError = true;
                    _objResponse.Message = "Error [GetAll]: no data";
                }
                else
                {
                    _objResponse.IsError = false;
                    _objResponse.Message = "Success [GetAll]: data";
                }
                return _objResponse;
            }
        }

        /// <summary>
        /// Retrieves a user by its ID.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>A Response object containing the user details.</returns>
        public Response Get(int id)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                _objResponse.Data = db.SingleById<USR01>(id);
                if (_objResponse.Data == null)
                {
                    _objResponse.IsError = true;
                    _objResponse.Message = "Error [Get]: no data";
                }
                else
                {
                    _objResponse.IsError = false;
                    _objResponse.Message = "Success [Get]: data";
                }
                return _objResponse;
            }
        }

        /// <summary>
        /// Checks if a user exists based on their ID.
        /// </summary>
        /// <param name="id">The ID of the user to check for existence.</param>
        /// <returns>A Response object indicating if the user exists.</returns>
        public Response IsUserExist(int id)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                _objResponse.Data = db.Exists<USR01>(u => u.R01F01 == id);
                if ((bool)_objResponse.Data)
                {
                    _objResponse.IsError = false;
                    _objResponse.Message = "Success [IsUserExist]: user exists";
                }
                else
                {
                    _objResponse.IsError = true;
                    _objResponse.Message = "Error [IsUserExist]: user does not exist";
                }
                return _objResponse;
            }
        }

        /// <summary>
        /// Pre-delete checks for user based on ID.
        /// </summary>
        /// <param name="id">The ID of the user to check for deletion constraints.</param>
        /// <returns>A Response object with deletion eligibility check.</returns>
        private Response PreDelete(int id)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                int count = (int)db.Count<USR01>(u => u.R01F04 == EnmRoleType.Admin);
                if (!IsUserExist(id).IsError)
                {
                    Response temp = Get(id);

                    if (temp.Data.R01F04 == EnmRoleType.Admin && count > 1)
                    {
                        return temp;
                    }
                }
            }
            _objResponse.Data = null;
            return _objResponse;
        }

        /// <summary>
        /// Validates user before deletion.
        /// </summary>
        /// <param name="objUsr01">The user object to validate.</param>
        /// <returns>A Response object indicating if the user is valid for deletion.</returns>
        private Response ValidateOnDelete(USR01 objUsr01)
        {
            if (objUsr01 == null)
            {
                _objResponse.IsError = true;
                _objResponse.Message = "User not found.";
            }
            else
            {
                _objResponse.IsError = false;
                _objResponse.Message = "User found.";
            }
            return _objResponse;
        }

        /// <summary>
        /// Deletes a user by ID.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>A Response object with the result of the deletion.</returns>
        public Response Delete(int id)
        {
            Response user = PreDelete(id);
            Response validationResponse = ValidateOnDelete(user.Data as USR01);
            if (validationResponse.IsError)
                return validationResponse;

            using (var db = _dbFactory.OpenDbConnection())
            {
                db.DeleteById<USR01>(id);
            }
            _objResponse.Data = null;
            _objResponse.IsError = false;
            _objResponse.Message = "User deleted successfully.";
            return _objResponse;
        }

        /// <summary>
        /// Prepares the DTO object before saving.
        /// Encrypts the password and converts it to a POCO object.
        /// </summary>
        /// <param name="objDTO">The DTO object to prepare.</param>
        public void PreSave(DTOUSR01 objDTO)
        {
            // Encrypt the password from DTO before converting to POCO
            objDTO.R01F02 = objDTO.R01F02.Trim().ToLower();
            _objUsr01 = objDTO.Convert<USR01>();
            _objUsr01.R01F03 = EncryptionHelper.GetEncryptPassword(_objUsr01.R01F03);

            if (Type == EnumType.E)
            {
                _id = objDTO.R01F01;
                _objUsr01.R01F07 = DateTime.Now; // Update timestamp
            }
            else
            {
                _objUsr01.R01F01 = 0; // For auto-increment ID
                _objUsr01.R01F06 = DateTime.Now; // Set creation timestamp
                _objUsr01.R01F07 = DateTime.Now; // Set updated timestamp
            }
        }

        /// <summary>
        /// Validates the user before saving or updating.
        /// </summary>
        /// <returns>A Response object indicating the result of validation.</returns>
        public Response Validation()
        {
            if (Type == EnumType.E)
            {
                if (!(_id > 0))
                {
                    _objResponse.IsError = true;
                    _objResponse.Message = "Enter a valid ID.";
                }
                else if (IsUserExist(_id).IsError)
                {
                    _objResponse.IsError = true;
                    _objResponse.Message = "User not found.";
                }
            }
            return _objResponse;
        }

        /// <summary>
        /// Saves the user data to the database.
        /// Inserts new users or updates existing users based on the operation type.
        /// </summary>
        /// <returns>A Response object with the result of the save operation.</returns>
        public Response Save()
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    if (Type == EnumType.A)
                    {
                        int id = (int)db.Insert(_objUsr01, selectIdentity: true);
                        _objResponse.Message = $"Data Added [ID]:{id}";
                    }
                    else if (Type == EnumType.E)
                    {
                        db.Update(_objUsr01);
                        _objResponse.Message = "User updated successfully.";
                    }
                }
            }
            catch (Exception ex)
            {
                _objResponse.IsError = true;
                _objResponse.Message = ex.Message;
            }
            return _objResponse;
        }
    }
}
