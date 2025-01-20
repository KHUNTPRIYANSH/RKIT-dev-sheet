using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Adv_Final.BL.Interface;
using Adv_Final.Extensions;
using Adv_Final.Models;
using Adv_Final.Models.Enum;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using Adv_Final.Models.DTO;
using Adv_Final.Models.POCO;

namespace Adv_Final.BL.Operation
{
    public class BLUSR01 : IDataHandler<DTOUSR01>
    {
        // Fields
        private USR01 _objUsr01;
        private int _id;
        private Response _objResponse;
        private readonly IDbConnectionFactory _dbFactory;

        public EnmType Type { get; set; }

        public BLUSR01()
        {
            _objResponse = new Response();
            _dbFactory = HttpContext.Current.Application["DbFactory"] as IDbConnectionFactory;

            if (_dbFactory == null)
            {
                throw new Exception("IDbConnectionFactory not found");
            }
        }

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

        private Response PreDelete(int id)
        {
            if (!IsUserExist(id).IsError)
            {
                return Get(id);
            }
            _objResponse.Data = null;
            return _objResponse;
        }

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


        public void PreSave(DTOUSR01 objDTO)
        {
            // Encrypt the password from DTO before converting to POCO
            //objDTO.R01F03 = Sha256Hashing.Hash(objDTO.R01F03);
            objDTO.R01F02 = objDTO.R01F02.Trim().ToLower();
            _objUsr01 = objDTO.Convert<USR01>();

            if (Type == EnmType.E)
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
        

        public Response Validation()
        {
            if (Type == EnmType.E)
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

        public Response Save()
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    if (Type == EnmType.A)
                    {
                        db.Insert(_objUsr01);
                        _objResponse.Message = "User added successfully.";
                    }
                    else if (Type == EnmType.E)
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
