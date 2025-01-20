using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Adv_Final.BL.Interface;
using Adv_Final.Extensions;
using Adv_Final.Models;
using Adv_Final.Models.Enum;
using Google.Protobuf.WellKnownTypes;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using Adv_Final.Models.DTO;
using Adv_Final.Models.POCO;

namespace Adv_Final.BL.Operation
{
    public class BLEMP01 : IDataHandler<DTOEMP01>
    {
        // following are field which are necessary and used manytime in operations
        private EMP01 _objEmp01;
        private int _id;
        private Response _objResponse;
        private readonly IDbConnectionFactory _dbFactory;

        public EnmType Type { get; set; }

        public BLEMP01()
        {
            // creating obj of response [data , isError , msg]
            _objResponse = new Response();
            // strong ref of connection to _dbFactory
            _dbFactory = HttpContext.Current.Application["DbFactory"] as IDbConnectionFactory;

            // checking if connection is their or not
            if (_dbFactory == null)
            {
                throw new Exception("IDbConnectionFactory not found");
            }
        }

        public Response GetAll()
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                _objResponse.Data = db.Select<EMP01>();
                if (_objResponse.Data == null)
                {
                    _objResponse.IsError = true;
                    _objResponse.Message = "Error [GetAll] : no data";
                }
                else
                {
                    _objResponse.IsError = false;
                    _objResponse.Message = "Success [GetAll] : data";
                }
                return _objResponse;
            }
        }
        public Response Get(int id)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                _objResponse.Data = db.SingleById<EMP01>(id);
                if (_objResponse.Data == null)
                {
                    _objResponse.IsError = true;
                    _objResponse.Message = "Error [Get] : no data";
                }
                else
                {
                    _objResponse.IsError = false;
                    _objResponse.Message = "Success [Get] : data";
                }
                return _objResponse;
            }
        }
        public Response IsEmployeeExist(int id)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                _objResponse.Data = db.Exists<EMP01>(e => e.P01F01 == id);
                if (_objResponse.Data == true)
                {
                    _objResponse.Data = true;
                    _objResponse.IsError = false;
                    _objResponse.Message = "Success [IsEmployeeExist]: employee exists";
                }
                else
                {
                    _objResponse.Data = false;
                    _objResponse.IsError = true;
                    _objResponse.Message = "Error [IsEmployeeExist]: employee not exists";
                }
                return _objResponse;
            }
        }
        private Response PreDelete(int id)
        {
            if (IsEmployeeExist(id).IsError == false)
            {
                return Get(id);
            }
            _objResponse.Data = null;
            return _objResponse;
        }
        private Response ValidateOnDelete(EMP01 objEmp01)
        {
            if (objEmp01 == null)
            {
                _objResponse.IsError = true;
                _objResponse.Message = "Employee not found.";
            }
            else
            {
            _objResponse.IsError = false;
            _objResponse.Message = "Employee  found.";

            }
            return _objResponse;
        }
        public Response Delete(int id)
        {
            Response employee = PreDelete(id);
            Response validationResponse = ValidateOnDelete(employee.Data);
            if (validationResponse.IsError)
                return validationResponse;

            using (var db = _dbFactory.OpenDbConnection())
            {
                db.DeleteById<EMP01>(id);
            }
            _objResponse.Data = null;
            _objResponse.IsError = false;
            _objResponse.Message = "Data Deleted";
            return _objResponse;
        }
        public void PreSave(DTOEMP01 objDTO)
        {
            objDTO.P01F02 = objDTO.P01F02.ToLower();
            objDTO.P01F03 = objDTO.P01F03.ToLower();
            objDTO.P01F06 = objDTO.P01F06.ToLower();
            _objEmp01 = objDTO.Convert<EMP01>();
            if (Type == EnmType.E)
            {
                _id = objDTO.P01F01;
            }
            else
            {
                // as we using auto increment we set id = 0 for all other operations
                _id = 0;
            }
        }
        public Response Validation()
        {
            if (Type == EnmType.E)
            {
                if (!(_id > 0))
                {
                    _objResponse.IsError = true;
                    _objResponse.Message = "Enter Correct Id";
                }
                else if (IsEmployeeExist(_id).IsError)
                {
                    _objResponse.IsError = true;
                    _objResponse.Message = "Employee Not Found";
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
                        db.Insert(_objEmp01);
                        _objResponse.Message = "Data Added";
                    }
                    if (Type == EnmType.E)
                    {
                        db.Update(_objEmp01);
                        _objResponse.Message = "Data Updated";
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