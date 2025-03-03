using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FinalDemo.BL.Interface;
using FinalDemo.Models;
using Google.Protobuf.WellKnownTypes;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using FinalDemo.Models.DTO;
using FinalDemo.Models.POCO;
using FinalDemo.Models.ENUM;
using FinalDemo.Extension;

namespace FinalDemo.BL.Operation
{
    /// <summary>
    /// Business logic operations for managing EMP01 entities.
    /// </summary>
    public class BLEMP01 : IDataHandler<DTOEMP01>
    {
        // following are fields which are necessary and used many times in operations
        private EMP01 _objEmp01;
        private int _id;
        private Response _objResponse;
        private readonly IDbConnectionFactory _dbFactory;

        /// <summary>
        /// Type of operation (Add/Edit).
        /// </summary>
        public EnumType Type { get; set; }

        /// <summary>
        /// Constructor for BLEMP01. Initializes response object and database connection factory.
        /// </summary>
        public BLEMP01()
        {
            // Creating object of response [data, isError, msg]
            _objResponse = new Response();
            // Strong reference of connection to _dbFactory
            _dbFactory = HttpContext.Current.Application["DbFactory"] as IDbConnectionFactory;

            // Checking if connection is available or not
            if (_dbFactory == null)
            {
                throw new Exception("IDbConnectionFactory not found");
            }
        }

        /// <summary>
        /// Retrieves all EMP01 entities from the database.
        /// </summary>
        /// <returns>Response containing the result of the operation.</returns>
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

        /// <summary>
        /// Retrieves a specific EMP01 entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the EMP01 entity to retrieve.</param>
        /// <returns>Response containing the result of the operation.</returns>
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

        /// <summary>
        /// Checks if an employee exists by their ID.
        /// </summary>
        /// <param name="id">The ID of the employee to check.</param>
        /// <returns>Response indicating whether the employee exists.</returns>
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

        /// <summary>
        /// Pre-deletion checks to validate if an employee can be deleted.
        /// </summary>
        /// <param name="id">The ID of the employee to check.</param>
        /// <returns>Response with employee data or null.</returns>
        private Response PreDelete(int id)
        {
            if (IsEmployeeExist(id).IsError == false)
            {
                return Get(id);
            }
            _objResponse.Data = null;
            return _objResponse;
        }

        /// <summary>
        /// Validates whether the employee exists before deletion.
        /// </summary>
        /// <param name="objEmp01">The EMP01 object to validate.</param>
        /// <returns>Response indicating whether the employee was found.</returns>
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
                _objResponse.Message = "Employee found.";
            }
            return _objResponse;
        }

        /// <summary>
        /// Deletes an employee based on the ID.
        /// </summary>
        /// <param name="id">The ID of the employee to delete.</param>
        /// <returns>Response with the result of the delete operation.</returns>
        public Response Delete(int id)
        {
            Response employee = PreDelete(id);
            Response validationResponse = ValidateOnDelete(employee.Data as EMP01);
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

        /// <summary>
        /// Prepares an EMP01 object for saving by converting the DTO to POCO.
        /// </summary>
        /// <param name="objDTO">The DTO object to convert and prepare.</param>
        public void PreSave(DTOEMP01 objDTO)
        {
            objDTO.P01F02 = objDTO.P01F02.ToLower();
            objDTO.P01F03 = objDTO.P01F03.ToLower();
            objDTO.P01F06 = objDTO.P01F06.ToLower();
            _objEmp01 = objDTO.Convert<EMP01>();
            if (Type == EnumType.E)
            {
                _id = objDTO.P01F01;
            }
            else
            {
                // As we are using auto-increment, we set ID = 0 for all other operations
                _id = 0;
            }
        }

        /// <summary>
        /// Validates the data before saving or updating.
        /// </summary>
        /// <returns>Response indicating validation results.</returns>
        public Response Validation()
        {
            if (Type == EnumType.E)
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
        /// Saves or updates the EMP01 entity depending on the operation type.
        /// </summary>
        /// <returns>Response with the result of the save operation.</returns>
        public Response Save()
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    if (Type == EnumType.A)
                    {
                        int id = (int)db.Insert(_objEmp01, selectIdentity: true);
                        _objResponse.Message = $"Data Added [ID]:{id}";
                    }
                    if (Type == EnumType.E)
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
