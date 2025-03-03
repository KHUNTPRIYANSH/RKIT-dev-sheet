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
using Google.Protobuf.WellKnownTypes;

namespace FinalDemo.BL.Operation
{
    /// <summary>
    /// Business logic class for handling department operations.
    /// Implements IDataHandler interface for DTODEPT01.
    /// </summary>
    public class BLDEPT01 : IDataHandler<DTODEPT01>
    {
        private DEPT01 _objDept01;
        private int _id;
        private Response _objResponse;
        private readonly IDbConnectionFactory _dbFactory;

        /// <summary>
        /// Enum type (Add or Edit).
        /// </summary>
        public EnumType Type { get; set; }

        /// <summary>
        /// Constructor initializes the response object and database connection factory.
        /// </summary>
        public BLDEPT01()
        {
            _objResponse = new Response();
            _dbFactory = HttpContext.Current.Application["DbFactory"] as IDbConnectionFactory;

            if (_dbFactory == null)
            {
                throw new Exception("IDbConnectionFactory not found");
            }
        }

        /// <summary>
        /// Retrieves all department records.
        /// </summary>
        /// <returns>Response with department data and message.</returns>
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

        /// <summary>
        /// Retrieves a specific department by ID.
        /// </summary>
        /// <param name="id">Department ID.</param>
        /// <returns>Response with department data and message.</returns>
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

        /// <summary>
        /// Checks if a department exists based on its ID.
        /// </summary>
        /// <param name="id">Department ID.</param>
        /// <returns>Response indicating whether the department exists or not.</returns>
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

        /// <summary>
        /// Prepares for department deletion.
        /// </summary>
        /// <param name="id">Department ID.</param>
        /// <returns>Response with department data if it exists, else null.</returns>
        private Response PreDelete(int id)
        {
            if (!IsDepartmentExist(id).IsError)
            {
                return Get(id);
            }
            _objResponse.Data = null;
            return _objResponse;
        }

        /// <summary>
        /// Validates if the department exists before deleting.
        /// </summary>
        /// <param name="objDept01">Department object.</param>
        /// <returns>Response with validation message.</returns>
        private Response ValidateOnDelete(DEPT01 objDept01)
        {
            _objResponse.IsError = objDept01 == null;
            _objResponse.Message = objDept01 == null ? "Department not found." : "Department found.";
            return _objResponse;
        }

        /// <summary>
        /// Deletes a department by ID after validation.
        /// </summary>
        /// <param name="id">Department ID.</param>
        /// <returns>Response with the result of the delete operation.</returns>
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

        /// <summary>
        /// Prepares a department DTO for saving (lowercases fields as needed).
        /// </summary>
        /// <param name="objDTO">Department DTO.</param>
        public void PreSave(DTODEPT01 objDTO)
        {      
            objDTO.T01F02 = objDTO.T01F02.ToLower();
            _objDept01 = objDTO.Convert<DEPT01>();
            _id = Type == EnumType.E ? objDTO.T01F01 : 0;
        }

        /// <summary>
        /// Validates the department data before saving.
        /// </summary>
        /// <returns>Response with validation results.</returns>
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

        /// <summary>
        /// Saves a new or existing department to the database.
        /// </summary>
        /// <returns>Response with success or failure message.</returns>
        public Response Save()
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    if (Type == EnumType.A)
                    {
                        _objDept01.T01F01 = 0;
                        int id = (int)db.Insert(_objDept01, selectIdentity: true);
                        _objResponse.Message = $"Data Added [ID]:{id}";
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
