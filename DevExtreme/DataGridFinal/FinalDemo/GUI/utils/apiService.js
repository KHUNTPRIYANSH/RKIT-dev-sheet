// apiService.js - Handles all API communication

/**
 * Fetches data from the specified API endpoint
 * @param {string} url - API endpoint URL
 * @param {string} token - Authentication token
 * @returns {Promise} - Promise with response data
 */
export function fetchData(url, token) {
    return $.ajax({
      url: url,
      method: "GET",
      headers: { Authorization: "Bearer " + token },
      dataType: "json",
    });
  }
  
  /**
   * Posts data to the specified API endpoint
   * @param {string} url - API endpoint URL
   * @param {object} data - Data to send
   * @param {string} token - Authentication token
   * @returns {Promise} - Promise with response
   */
  export function postData(url, data, token) {
    return $.ajax({
      url: url,
      method: "POST",
      headers: { Authorization: "Bearer " + token },
      contentType: "application/json",
      data: JSON.stringify(data),
    });
  }
  
  /**
   * Updates data at the specified API endpoint
   * @param {string} url - API endpoint URL
   * @param {object} data - Updated data to send
   * @param {string} token - Authentication token
   * @returns {Promise} - Promise with response
   */
  export function putData(url, data, token) {
    return $.ajax({
      url: url,
      method: "PUT",
      headers: { Authorization: "Bearer " + token },
      contentType: "application/json",
      data: JSON.stringify(data),
    });
  }
  
  /**
   * Deletes data from the specified API endpoint
   * @param {string} url - API endpoint URL
   * @param {string} token - Authentication token
   * @returns {Promise} - Promise with response
   */
  export function deleteData(url, token) {
    return $.ajax({
      url: url,
      method: "DELETE",
      headers: { Authorization: "Bearer " + token },
    });
  }