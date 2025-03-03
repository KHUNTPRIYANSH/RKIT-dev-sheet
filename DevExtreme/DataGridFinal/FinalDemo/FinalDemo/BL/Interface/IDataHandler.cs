using FinalDemo.Models;
using FinalDemo.Models.ENUM;
using System;

namespace FinalDemo.BL.Interface
{
    /// <summary>
    /// The IDataHandler interface handles operations for adding, editing, and deleting entities of type T.
    /// It is a generic interface where T is the type of entity (e.g., User, Product, Order).
    /// </summary>
    public interface IDataHandler<T> where T : class
    {
        /// <summary>
        /// Enum type indicating the operation: A for Add, E for Edit, D for Delete.
        /// </summary>
        EnumType Type { get; set; }

        /// <summary>
        /// Pre-save method that handles validation or preparation before saving the data.
        /// </summary>
        /// <param name="objDTO">The DTO (Data Transfer Object) representing the data to be saved.</param>
        void PreSave(T objDTO);

        /// <summary>
        /// Saves the entity to the database.
        /// This method should be called after PreSave and Validation.
        /// </summary>
        /// <returns>A Response object indicating the result of the save operation.</returns>
        Response Save();

        /// <summary>
        /// Validates the entity before performing add, update, or delete operations.
        /// </summary>
        /// <returns>A Response object indicating whether the validation was successful.</returns>
        Response Validation();
    }
}
