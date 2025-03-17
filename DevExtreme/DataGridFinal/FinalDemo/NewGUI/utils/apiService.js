const apiHandler = {
  baseUrl: "https://localhost:44310",

  request: function (url, method, data = null) {
      return $.ajax({
          url: this.baseUrl + url,
          method: method,
          headers: { "Authorization": "Bearer " + localStorage.getItem("utoken") },
          contentType: "application/json",
          data: data ? JSON.stringify(data) : null,
      }).fail((jqXHR, textStatus, errorThrown) => {
          console.error("API Error:", textStatus, errorThrown, jqXHR.responseText);
      });
  },

  getAll: function (endpoint) {
      return this.request(endpoint, "GET");
  },

  getById: function (endpoint, id) {
      return this.request(`${endpoint}?id=${id}`, "GET");
  },

  create: function (endpoint, data) {
      return this.request(endpoint, "POST", data);
  },

  update: function (endpoint, id, data) {
      data["id"] = id; // Ensure ID is sent in the request body
      return this.request(endpoint, "PUT", data);
  },

  delete: function (endpoint, id) {
      return this.request(`${endpoint}?id=${id}`, "DELETE");
  }
};
