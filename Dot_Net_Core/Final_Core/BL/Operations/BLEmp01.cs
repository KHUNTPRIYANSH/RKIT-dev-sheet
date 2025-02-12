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
    /// <summary>
    /// Business logic class for handling employee operations.
    /// Implements IDataHandler for DTOEmp01 operations.
    /// </summary>
    public class BLEmp01 : IDataHandler<DTOEmp01>
    {
        #region Fields

        private readonly IDbConnectionFactory _dbFactory;
        private Emp01 _objEmp01;
        private int _id;
        private Response _objResponse;

        #endregion

        #region Properties

        /// <summary>
        /// Type of operation to perform (Add, Edit, etc.).
        /// </summary>
        public EnmType Type { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes the BLEmp01 class with the database connection factory.
        /// </summary>
        /// <param name="dbFactory">Database connection factory.</param>
        public BLEmp01(IDbConnectionFactory dbFactory)
        {
            _dbFactory = dbFactory ?? throw new ArgumentNullException(nameof(dbFactory));
            _objResponse = new Response();
        }

        #endregion

        #region CRUD Operations

        /// <summary>
        /// Retrieves all employees from the database.
        /// </summary>
        /// <returns>A list of employees.</returns>
        public List<Emp01> GetAll()
        {
            using var db = _dbFactory.OpenDbConnection();
            return db.Select<Emp01>();
        }

        /// <summary>
        /// Retrieves a single employee by their ID.
        /// </summary>
        /// <param name="id">The ID of the employee to retrieve.</param>
        /// <returns>The employee with the specified ID.</returns>
        public Emp01 Get(int id)
        {
            using var db = _dbFactory.OpenDbConnection();
            return db.SingleById<Emp01>(id);
        }

        /// <summary>
        /// Deletes an employee by their ID.
        /// </summary>
        /// <param name="id">The ID of the employee to delete.</param>
        /// <returns>A response indicating the result of the deletion.</returns>
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

        #endregion

        #region Helper Methods

        /// <summary>
        /// Checks if an employee exists in the database by their ID.
        /// </summary>
        /// <param name="id">The ID of the employee to check.</param>
        /// <returns>A response indicating whether the employee exists or not.</returns>
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

        /// <summary>
        /// Prepares for deletion by checking if the employee exists.
        /// </summary>
        /// <param name="id">The ID of the employee to check.</param>
        /// <returns>The employee object if found, otherwise null.</returns>
        private Emp01 PreDelete(int id)
        {
            var response = IsEmployeeExist(id);
            return !response.IsError ? Get(id) : null;
        }

        /// <summary>
        /// Validates the employee before deletion.
        /// </summary>
        /// <param name="objEmp01">The employee to validate.</param>
        /// <returns>A response indicating the validation result.</returns>
        private Response ValidateOnDelete(Emp01 objEmp01)
        {
            return objEmp01 == null
                ? new Response { IsError = true, Message = "Employee not found." }
                : new Response { IsError = false };
        }

        #endregion

        #region Pre-Save and Validation

        /// <summary>
        /// Prepares the employee data for saving or updating.
        /// </summary>
        /// <param name="objDTO">The DTO representing the employee data.</param>
        public void PreSave(DTOEmp01 objDTO)
        {
            objDTO.P01F02 = objDTO.P01F02.ToLower();
            objDTO.P01F09 = objDTO.P01F09.ToLower();
            _objEmp01 = objDTO.Convert<Emp01>();
            _id = Type == EnmType.E ? objDTO.P01F01 : 0;
        }

        /// <summary>
        /// Validates the employee data before saving or updating.
        /// </summary>
        /// <returns>A response indicating the validation result.</returns>
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

        #endregion

        #region Save Operations

        /// <summary>
        /// Saves or updates the employee data based on the operation type.
        /// </summary>
        /// <returns>A response indicating the result of the save operation.</returns>
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

        #endregion

        #region Employee Retrieval Methods

        /// <summary>
        /// Retrieves the first employee from the database.
        /// </summary>
        /// <returns>A response containing the first employee or an error message.</returns>
        public Response FirstEmployee()
        {
            using var db = _dbFactory.OpenDbConnection();
            var emp = db.Select<Emp01>().FirstOrDefault();
            return emp != null ? new Response { Data = emp, IsError = false, Message = "Success: First employee" }
                               : new Response { IsError = true, Message = "Error: No employees" };
        }

        /// <summary>
        /// Retrieves the last employee from the database.
        /// </summary>
        /// <returns>A response containing the last employee or an error message.</returns>
        public Response LastEmployee()
        {
            using var db = _dbFactory.OpenDbConnection();
            var emp = db.Select<Emp01>().LastOrDefault();
            return emp != null ? new Response { Data = emp, IsError = false, Message = "Success: Last employee" }
                               : new Response { IsError = true, Message = "Error: No employees" };
        }

        /// <summary>
        /// Retrieves the richest employee from the database based on salary.
        /// </summary>
        /// <returns>A response containing the richest employee or an error message.</returns>
        public Response RichestEmployee()
        {
            using var db = _dbFactory.OpenDbConnection();
            var emp = db.Single(db.From<Emp01>().OrderByDescending(e => e.P01F08));
            return emp != null ? new Response { Data = emp, IsError = false, Message = "Success: Richest employee" }
                               : new Response { IsError = true, Message = "Error: Failed to get richest employee" };
        }

        #endregion
    }
}
