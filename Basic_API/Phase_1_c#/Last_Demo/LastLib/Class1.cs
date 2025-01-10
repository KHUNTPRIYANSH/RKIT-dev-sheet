using System;

namespace LastLib
{
    public class CurrencyConverter
    {
        #region Conversion Rate

        // Current conversion rate from INR to USD (for demonstration purposes).
        // In a real-world application, we can fetch this rate from a live API.
        private const float ConversionRate = 0.012f; // 1 INR = 0.012 USD (example rate)

        #endregion

        #region Public Method

        /// <summary>
        /// Converts the given amount in INR to USD and prints the result.
        /// </summary>
        /// <param name="amountInRupees">The amount in INR (Indian Rupees) to convert.</param>
        public void ConvertAndPrintAmount(float amountInRupees)
        {
            // Convert the amount from INR to USD
            float amountInUsd = ConvertRupeesToUsd(amountInRupees);

            // Print the result to the console
            Console.WriteLine($"{amountInRupees} INR is equivalent to {amountInUsd} USD.");
        }

        #endregion

        #region Private Method

        /// <summary>
        /// Converts the given amount in INR to USD using a predefined conversion rate.
        /// </summary>
        /// <param name="amountInRupees">The amount in INR to convert.</param>
        /// <returns>The equivalent amount in USD.</returns>
        private float ConvertRupeesToUsd(float amountInRupees)
        {
            return amountInRupees * ConversionRate;
        }

        #endregion

    }
}
