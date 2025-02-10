using Final_Core.BL.Interfaces;
using Final_Core.Data;
using Final_Core.Models.DTO;
using Final_Core.Models.Enum;
using Final_Core.Models.POCO;
using Final_Core.Models;
using ServiceStack.Data;
using System.Data;
using ServiceStack.OrmLite;
using Final_Core.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Final_Core.BL.Operations
{
    public class BLEmp01 : IDataHandler<DTOEmp01>
    {
        private readonly IDbConnectionFactory _dbFactory;
        private Emp01 _objEmp01;
        private int _id;
        private Response _objResponse;

        public EnmType Type { get; set; }

        public BLEmp01(IDbConnectionFactory dbFactory)
        {
            _dbFactory = dbFactory ?? throw new ArgumentNullException(nameof(dbFactory));
            _objResponse = new Response();
        }

        public List<Emp01> GetAll()
        {
            using var db = _dbFactory.OpenDbConnection();
            return db.Select<Emp01>();
        }

        public Emp01 Get(int id)
        {
            using var db = _dbFactory.OpenDbConnection();
            return db.SingleById<Emp01>(id);
        }

        private Response IsEmployeeExist(int id)
        {
            using var db = _dbFactory.OpenDbConnection();
            bool exists = db.Exists<Emp01>(e => e.P01F01 == id);
            return new Response
            {
                IsError = !exists,
                Message = exists ? "Success: Employee exists" : "Error: Employee not found",
                Data = exists
            };
        }

        private Emp01 PreDelete(int id)
        {
            var response = IsEmployeeExist(id);
            return !response.IsError ? Get(id) : null;
        }

        private Response ValidateOnDelete(Emp01 objEmp01)
        {
            return objEmp01 == null
                ? new Response { IsError = true, Message = "Employee not found." }
                : new Response { IsError = false };
        }

        public Response Delete(int id)
        {
            var employee = PreDelete(id);
            var validationResponse = ValidateOnDelete(employee);
            if (validationResponse.IsError)
                return validationResponse;

            using var db = _dbFactory.OpenDbConnection();
            db.DeleteById<Emp01>(id);
            return new Response { IsError = false, Message = "Data Deleted" };
        }

        public void PreSave(DTOEmp01 objDTO)
        {
            objDTO.P01F02 = objDTO.P01F02.ToLower();
            objDTO.P01F09 = objDTO.P01F09.ToLower();
            _objEmp01 = objDTO.Convert<Emp01>();
            _id = Type == EnmType.E ? objDTO.P01F01 : 0;
        }

        public Response Validation()
        {
            if (Type == EnmType.E)
            {
                if (_id <= 0)
                {
                    return new Response { IsError = true, Message = "Enter a valid ID" };
                }

                var existResponse = IsEmployeeExist(_id);
                if (existResponse.IsError)
                {
                    return new Response { IsError = true, Message = "Employee Not Found" };
                }
            }
            return new Response { IsError = false };
        }

        public Response Save()
        {
            try
            {
                using var db = _dbFactory.OpenDbConnection();
                if (Type == EnmType.A)
                {
                    int id = (int)db.Insert(_objEmp01, selectIdentity: true);
                    return new Response { IsError = false, Message = $"Data Added {id}" };
                }
                if (Type == EnmType.E)
                {
                    db.Update(_objEmp01);
                    return new Response { IsError = false, Message = "Data Updated" };
                }
            }
            catch (Exception ex)
            {
                return new Response { IsError = true, Message = ex.Message };
            }
            return new Response { IsError = true, Message = "Unknown Error" };
        }

        public Response FirstEmployee()
        {
            using var db = _dbFactory.OpenDbConnection();
            var emp = db.Select<Emp01>().FirstOrDefault();
            return emp != null ? new Response { Data = emp, IsError = false, Message = "Success: First employee" }
                               : new Response { IsError = true, Message = "Error: No employees" };
        }

        public Response LastEmployee()
        {
            using var db = _dbFactory.OpenDbConnection();
            var emp = db.Select<Emp01>().LastOrDefault();
            return emp != null ? new Response { Data = emp, IsError = false, Message = "Success: Last employee" }
                               : new Response { IsError = true, Message = "Error: No employees" };
        }

        public Response RichestEmployee()
        {
            using var db = _dbFactory.OpenDbConnection();
            var emp = db.Single(db.From<Emp01>().OrderByDescending(e => e.P01F08));
            return emp != null ? new Response { Data = emp, IsError = false, Message = "Success: Richest employee" }
                               : new Response { IsError = true, Message = "Error: Failed to get richest employee" };
        }
    }
}

