using System;
using FinalDemo.BL.Interface;
using System.Web;
using FinalDemo.Extension;
using FinalDemo.Models.DTO;
using FinalDemo.Models.ENUM;
using FinalDemo.Models.POCO;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using FinalDemo.Models;

namespace FinalDemo.BL.Operation
{
    /// <summary>
    /// Business logic class for handling attendance operations.
    /// Implements IDataHandler interface for DTOATT01.
    /// </summary>
    public class BLATT01 : IDataHandler<DTOATT01>
    {
        private ATT01 _objAtt01;
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
        public BLATT01()
        {
            _objResponse = new Response();
            _dbFactory = HttpContext.Current.Application["DbFactory"] as IDbConnectionFactory;

            if (_dbFactory == null)
            {
                throw new Exception("IDbConnectionFactory not found");
            }
        }

        /// <summary>
        /// Retrieves all attendance records.
        /// </summary>
        /// <returns>Response with attendance data and message.</returns>
        public Response GetAll()
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                _objResponse.Data = db.Select<ATT01>();
                _objResponse.IsError = _objResponse.Data == null;
                _objResponse.Message = _objResponse.IsError ? "Error [GetAll]: no data" : "Success [GetAll]: data";
                return _objResponse;
            }
        }

        /// <summary>
        /// Retrieves a specific attendance record by ID.
        /// </summary>
        /// <param name="id">Attendance ID.</param>
        /// <returns>Response with attendance data and message.</returns>
        public Response Get(int id)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                _objResponse.Data = db.SingleById<ATT01>(id);
                _objResponse.IsError = _objResponse.Data == null;
                _objResponse.Message = _objResponse.IsError ? "Error [Get]: no data" : "Success [Get]: data";
                return _objResponse;
            }
        }

        /// <summary>
        /// Checks if an attendance record exists based on its ID.
        /// </summary>
        /// <param name="id">Attendance ID.</param>
        /// <returns>Response indicating whether the attendance record exists or not.</returns>
        public Response IsAttendanceExist(int id)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                bool exists = db.Exists<ATT01>(a => a.ATT01F01 == id);
                _objResponse.Data = exists;
                _objResponse.IsError = !exists;
                _objResponse.Message = exists ? "Success [IsAttendanceExist]: attendance exists" : "Error [IsAttendanceExist]: attendance not exists";
                return _objResponse;
            }
        }

        /// <summary>
        /// Prepares for attendance deletion.
        /// </summary>
        /// <param name="id">Attendance ID.</param>
        /// <returns>Response with attendance data if it exists, else null.</returns>
        private Response PreDelete(int id)
        {
            if (!IsAttendanceExist(id).IsError)
            {
                return Get(id);
            }
            _objResponse.Data = null;
            return _objResponse;
        }

        /// <summary>
        /// Validates if the attendance exists before deleting.
        /// </summary>
        /// <param name="objAtt01">Attendance object.</param>
        /// <returns>Response with validation message.</returns>
        private Response ValidateOnDelete(ATT01 objAtt01)
        {
            _objResponse.IsError = objAtt01 == null;
            _objResponse.Message = objAtt01 == null ? "Attendance not found." : "Attendance found.";
            return _objResponse;
        }

        /// <summary>
        /// Deletes an attendance record by ID after validation.
        /// </summary>
        /// <param name="id">Attendance ID.</param>
        /// <returns>Response with the result of the delete operation.</returns>
        public Response Delete(int id)
        {
            Response attendance = PreDelete(id);
            Response validationResponse = ValidateOnDelete(attendance.Data);
            if (validationResponse.IsError)
                return validationResponse;

            using (var db = _dbFactory.OpenDbConnection())
            {
                db.DeleteById<ATT01>(id);
            }
            _objResponse.Data = null;
            _objResponse.IsError = false;
            _objResponse.Message = "Data Deleted";
            return _objResponse;
        }

        /// <summary>
        /// Prepares an attendance DTO for saving.
        /// </summary>
        /// <param name="objDTO">Attendance DTO.</param>
        public void PreSave(DTOATT01 objDTO)
        {
            _objAtt01 = objDTO.Convert<ATT01>();
            _id = Type == EnumType.E ? objDTO.ATT01F01 : 0;

            // Set timestamps for new or updated records
            if (Type == EnumType.A)
            {
                _objAtt01.ATT01F11 = DateTime.UtcNow; // CreatedDate
            }
            else if (Type == EnumType.E)
            {
                _objAtt01.ATT01F12 = DateTime.UtcNow; // UpdatedDate
            }
        }

        /// <summary>
        /// Validates the attendance data before saving.
        /// </summary>
        /// <returns>Response with validation results.</returns>
        public Response Validation()
        {
            // Add validation for required fields
            if (_objAtt01.ATT01F02 <= 0)
            {
                _objResponse.IsError = true;
                _objResponse.Message = "Employee ID is required";
            }

            if (_objAtt01.ATT01F03 <= 0)
            {
                _objResponse.IsError = true;
                _objResponse.Message = "Department ID is required";
            }

            if (_objAtt01.ATT01F04 == default)
            {
                _objResponse.IsError = true;
                _objResponse.Message = "Attendance Date is required";
            }
            if (Type == EnumType.E)
            {
                if (!(_id > 0))
                {
                    _objResponse.IsError = true;
                    _objResponse.Message = "Enter Correct Id";
                }
                else if (IsAttendanceExist(_id).IsError)
                {
                    _objResponse.IsError = true;
                    _objResponse.Message = "Attendance Not Found";
                }
            }

            // Additional validation logic can be added here
            if (string.IsNullOrEmpty(_objAtt01.ATT01F08))
            {
                _objResponse.IsError = true;
                _objResponse.Message = "Status is required.";
            }

            return _objResponse;
        }


        /// <summary>
        /// Saves a new or existing attendance record to the database.
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
                        int id = (int)db.Insert(_objAtt01, selectIdentity: true);
                        _objResponse.Message = $"Data Added [ID]:{id}";
                    }
                    else if (Type == EnumType.E)
                    {
                        db.Update(_objAtt01);
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