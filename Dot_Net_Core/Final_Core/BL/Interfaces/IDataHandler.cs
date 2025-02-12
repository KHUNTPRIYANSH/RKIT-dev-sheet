using Final_Core.Models.Enum;
using Final_Core.Models;

namespace Final_Core.BL.Interfaces
{
    /// <summary>
    /// Interface for handling data operations of a specific type.
    /// </summary>
    /// <typeparam name="T">The type of data to handle.</typeparam>
    public interface IDataHandler<T> where T : class
    {
        /// <summary>
        /// Gets or sets the type of operation (e.g., Edit, Add).
        /// </summary>
        EnmType Type { get; set; }

        /// <summary>
        /// Performs pre-save operations on the data object before saving.
        /// For example, mapping fields or setting certain properties.
        /// </summary>
        /// <param name="objDto">The data object to be saved.</param>
        void PreSave(T objDto);

        /// <summary>
        /// Validates the data before saving.
        /// Ensures that the data meets required conditions.
        /// </summary>
        /// <returns>A response indicating whether the validation was successful.</returns>
        Response Validation();

        /// <summary>
        /// Saves the data to the database.
        /// This operation may involve inserting or updating records.
        /// </summary>
        /// <returns>A response indicating the result of the save operation.</returns>
        Response Save();
    }
}
