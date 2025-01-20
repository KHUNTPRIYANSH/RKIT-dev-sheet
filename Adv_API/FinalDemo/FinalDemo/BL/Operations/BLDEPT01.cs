using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FinalDemo.BL.Interface;
using FinalDemo.Extension;
using FinalDemo.Models;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using FinalDemo.Models.DTO;
using FinalDemo.Models.POCO;
using FinalDemo.Models.ENUM;

namespace FinalDemo.BL.Operation
{
    public class BLDEPT01 : IDataHandler<DTODEPT01>
    {
        private DEPT01 _objDept01;
        private int _id;
        private Response _objResponse;
        private readonly IDbConnectionFactory _dbFactory;

        public EnumType Type { get; set; }

        public BLDEPT01()
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
                _objResponse.Data = db.Select<DEPT01>();
                _objResponse.IsError = _objResponse.Data == null;
                _objResponse.Message = _objResponse.IsError ? "Error [GetAll]: no data" : "Success [GetAll]: data";
                return _objResponse;
            }
        }

        public Response Get(int id)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                _objResponse.Data = db.SingleById<DEPT01>(id);
                _objResponse.IsError = _objResponse.Data == null;
                _objResponse.Message = _objResponse.IsError ? "Error [Get]: no data" : "Success [Get]: data";
                return _objResponse;
            }
        }

        public Response IsDepartmentExist(int id)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                bool exists = db.Exists<DEPT01>(d => d.T01F01 == id);
                _objResponse.Data = exists;
                _objResponse.IsError = !exists;
                _objResponse.Message = exists ? "Success [IsDepartmentExist]: department exists" : "Error [IsDepartmentExist]: department not exists";
                return _objResponse;
            }
        }

        private Response PreDelete(int id)
        {
            if (!IsDepartmentExist(id).IsError)
            {
                return Get(id);
            }
            _objResponse.Data = null;
            return _objResponse;
        }

        private Response ValidateOnDelete(DEPT01 objDept01)
        {
            _objResponse.IsError = objDept01 == null;
            _objResponse.Message = objDept01 == null ? "Department not found." : "Department found.";
            return _objResponse;
        }

        public Response Delete(int id)
        {
            Response department = PreDelete(id);
            Response validationResponse = ValidateOnDelete(department.Data);
            if (validationResponse.IsError)
                return validationResponse;

            using (var db = _dbFactory.OpenDbConnection())
            {
                db.DeleteById<DEPT01>(id);
            }
            _objResponse.Data = null;
            _objResponse.IsError = false;
            _objResponse.Message = "Data Deleted";
            return _objResponse;
        }

        public void PreSave(DTODEPT01 objDTO)
        {
            objDTO.T01F02 = objDTO.T01F02.ToLower();
            _objDept01 = objDTO.Convert<DEPT01>();
            _id = Type == EnumType.E ? objDTO.T01F01 : 0;
        }

        public Response Validation()
        {
            if (Type == EnumType.E)
            {
                if (!(_id > 0))
                {
                    _objResponse.IsError = true;
                    _objResponse.Message = "Enter Correct Id";
                }
                else if (IsDepartmentExist(_id).IsError)
                {
                    _objResponse.IsError = true;
                    _objResponse.Message = "Department Not Found";
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
                    if (Type == EnumType.A)
                    {
                        db.Insert(_objDept01);
                        _objResponse.Message = "Data Added";
                    }
                    else if (Type == EnumType.E)
                    {
                        db.Update(_objDept01);
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
