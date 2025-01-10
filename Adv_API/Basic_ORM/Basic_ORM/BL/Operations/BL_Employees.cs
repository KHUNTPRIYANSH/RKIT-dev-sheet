using Basic_ORM.BL.Interface;
using Basic_ORM.Models;
using Basic_ORM.Models.DTO;
using Basic_ORM.Models.Enum;
using Basic_ORM.Models.POCO;
using Basic_ORM.Extensions;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace Basic_ORM.BL.Operations
{
    public class BL_Employee : IDataHandler<DTO_Emp01>
    {
        private Emp01 _objEmp01;
        private int _id;
        private Response _objResponse;
        private readonly IDbConnectionFactory _dbFactory;

        public EnmType Type { get; set; }

        public BL_Employee()
        {
            _objResponse = new Response();
            _dbFactory = HttpContext.Current.Application["DbFactory"] as IDbConnectionFactory;

            if (_dbFactory == null)
            {
                throw new Exception("IDbConnectionFactory not found");
            }
        }

        public List<Emp01> GetAll()
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                return db.Select<Emp01>();
            }
        }

        public Emp01 Get(int id)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                return db.SingleById<Emp01>(id);
            }
        }

        private bool IsEmployeeExist(int id)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                return db.Exists<Emp01>(id);
            }
        }

        private Emp01 PreDelete(int id)
        {
            if (IsEmployeeExist(id))
            {
                return Get(id);
            }
            return null;
        }

        private Response ValidateOnDelete(Emp01 objEmp01)
        {
            if (objEmp01 == null)
                return new Response { IsError = true, Message = "Employee not found." };

            return new Response { IsError = false };
        }

        public Response Delete(int id)
        {
            var employee = PreDelete(id);
            var validationResponse = ValidateOnDelete(employee);
            if (validationResponse.IsError)
                return validationResponse;

            using (var db = _dbFactory.OpenDbConnection())
            {
                db.DeleteById<Emp01>(id);
            }
            _objResponse.Message = "Data Deleted";
            return _objResponse;
        }

        public void PreSave(DTO_Emp01 objDTO)
        {
            _objEmp01 = objDTO.Convert<Emp01>();
            if (Type == EnmType.E && objDTO.Id > 0)
            {
                _id = objDTO.Id;
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
                else if (!IsEmployeeExist(_id))
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
