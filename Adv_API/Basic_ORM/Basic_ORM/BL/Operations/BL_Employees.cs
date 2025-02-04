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
    /// <summary>
    /// BL_Employee is a business logic class of employee
    /// </summary>
    public class BL_Employee : IDataHandler<DTO_Emp01>
    {
        // following are field which are necessary and used manytime in operations
        private Emp01 _objEmp01;
        private int _id;
        private Response _objResponse;
        private readonly IDbConnectionFactory _dbFactory;

        public EnmType Type { get; set; }

        public BL_Employee()
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
        /// <summary>
        /// GetAll method fetches all the employeees from the db
        /// </summary>
        /// <returns>List of employees</returns>
        public List<Emp01> GetAll()
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                return db.Select<Emp01>();
            }
        }
        /// <summary>
        /// Get method fetch only 1 employee based on id
        /// </summary>
        /// <param name="id">unique identity of the entity</param>
        /// <returns>return row [data of employee]</returns>
        public Emp01 Get(int id)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                return db.SingleById<Emp01>(id);
            }
        }
        /// <summary>
        /// method checks employee exists or not
        /// </summary>
        /// <param name="id">unique identity of employee</param>
        /// <returns>return [bool data , is error , message ]</returns>
        private Response IsEmployeeExist(int id)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                _objResponse.Data= db.Exists<Emp01>(e => e.Id == id);
                if(_objResponse.Data == true)
                {
                    _objResponse.IsError = false;
                    _objResponse.Message = "Success : employee exists";
                }
                else
                {
                    _objResponse.IsError = true;
                    _objResponse.Message = "Error : employee not exists";
                }
                return _objResponse;
            }
        }
        /// <summary>
        /// checks id exists or not in table
        /// </summary>
        /// <param name="id">unique identity of employee</param>
        /// <returns>return row</returns>
        private Emp01 PreDelete(int id)
        {
            if (!IsEmployeeExist(id).IsError)
            {
                return Get(id);
            }
            return null;
        }
        /// <summary>
        /// checks employee id their or not based on pre delete
        /// </summary>
        /// <param name="objEmp01">obj of employee data</param>
        /// <returns>[null , error , message]</returns>
        private Response ValidateOnDelete(Emp01 objEmp01)
        {
            if (objEmp01 == null)
                return new Response { IsError = true, Message = "Employee not found." };

            return new Response { IsError = false };
        }
        /// <summary>
        /// use pre delete and validation delete to delete employee 
        /// </summary>
        /// <param name="id">unique identity of employee</param>
        /// <returns>[null , isError , message]</returns>
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
            _objResponse.Data = null;
            _objResponse.IsError = false;
            _objResponse.Message = "Data Deleted";
            return _objResponse;
        }
        /// <summary>
        /// Pre save perform dto to poco , and id management
        /// </summary>
        /// <param name="objDTO">dto model from gui</param>
        public void PreSave(DTO_Emp01 objDTO)
        {
            objDTO.Name = objDTO.Name.ToLower();
            objDTO.Department = objDTO.Department.ToLower();
            _objEmp01 = objDTO.Convert<Emp01>();
            if (Type == EnmType.E)
            {
                _id = objDTO.Id;
            }
            //else if(Type == EnmType.A )
            //{
            //    _id = objDTO.Id;
            //    if (!IsEmployeeExist(_id).IsError)
            //    {
            //        _objResponse.Data = null;
            //        _objResponse.IsError = true;
            //        _objResponse.Message = "Error: Can't add employee, already their";
            //    }
            //}
            else
            {
                _id = 0;
            }
        }
        /// <summary>
        /// validates before save
        /// </summary>
        /// <returns>[data , IsError  ,Message]</returns>
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
        /// <summary>
        /// Store record into table
        /// </summary>
        /// <returns>[data , isError , Message]</returns>
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
        /// <summary>
        /// use single orm method to fetch first record
        /// </summary>
        /// <returns>[first record , isError, message]</returns>
        public Response FirstEmployee()
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                var emp = db.Select<Emp01>().FirstOrDefault();
                if (emp != null)
                {
                    _objResponse.Data = emp;
                    _objResponse.IsError = false;
                    _objResponse.Message = "Success : First employee";
                    return _objResponse;
                }

                _objResponse.IsError = true;
                _objResponse.Message = "Error : No employees";
                return _objResponse;
            }

        }/// <summary>
        /// return last record from table
        /// </summary>
        /// <returns>[data , isError , Message]</returns>
        public Response LastEmployee()
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                var emp = db.Select<Emp01>().LastOrDefault();
                if (emp != null)
                {
                    _objResponse.Data = emp;
                    _objResponse.IsError = false;
                    _objResponse.Message = "Success : Last employee";
                    return _objResponse;
                }

                _objResponse.IsError = true;
                _objResponse.Message = "Error : No employees";
                return _objResponse;

            }
        }
        /// <summary>
        /// find employee having heighest salary in whole table
        /// </summary>
        /// <returns>[rich employee data , is error, message]</returns>
        public Response RichestEmployee()
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                var emp = db.Single(db.From<Emp01>().OrderByDescending(e => e.Salary));
                if (emp != null)
                {

                    _objResponse.Data = emp;
                    _objResponse.IsError = false;
                    _objResponse.Message = "Success : Get richest employee";
                    return _objResponse;
                }
            }
            _objResponse.IsError = true;
            _objResponse.Message = "Error : Failed to get richest employee";
            return _objResponse;
        }
        /// <summary>
        /// Return matching record based on ch
        /// </summary>
        /// <param name="ch">starting charactor of name</param>
        /// <returns>matched record , is error , message</returns>
        public Response EmployeeWhereNameStartsWith(char ch)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                var emp = db.Select(db.From<Emp01>().Where(e => e.Name.StartsWith(ch.ToString())));
                if (emp.Count > 0)
                {

                    _objResponse.Data = emp;
                    _objResponse.IsError = false;
                    _objResponse.Message = "Success : Get employee where employee name starts with : " + ch;
                    return _objResponse;
                }
                _objResponse.IsError = true;
                _objResponse.Message = "Error : Failed to employee where employee name starts with : " + ch;
                return _objResponse;
            }
        }
        /// <summary>
        /// insights : count of total employee in department , min max avg pay per employee in that department
        /// </summary>
        /// <param name="dpt">nameof department</param>
        /// <returns>insights , is error, message</returns>
        public Response DepartmentInsigts(string dpt)
        {
            dpt = dpt.ToLower();
            using (var db = _dbFactory.OpenDbConnection())
            {
                long count = db.Count<Emp01>(e => e.Department == dpt);
                int maxPay = db.Scalar<int>(db.From<Emp01>().Where(e => e.Department == dpt).Select(e => Sql.Max(e.Salary)));
                int avgPay = db.Scalar<int>($"SELECT AVG(Salary) FROM Emp01 WHERE Department = \"{dpt}\"");
                int minPay = db.Scalar<int>($"SELECT MIN(Salary) FROM  Emp01 WHERE Department = \"{dpt}\"");
                var res = $"Count : {count}, Max Pay : {maxPay}, Min Pay : {minPay}, Avg Pay : {avgPay}";
                if (count > 0 && maxPay >0 && avgPay > 0 && minPay > 0)
                {
                    _objResponse.Data = res; _objResponse.IsError = false;
                    _objResponse.Message = "Success : Got insights";
                }
                else
                {

                _objResponse.IsError = true;
                _objResponse.Message = "Error : Can't get insights";
                }

            }
            return _objResponse;
        }


    }
}
