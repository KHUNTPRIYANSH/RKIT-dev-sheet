$(document).ready(function () {
  //! $(selector).load("URL","DATA" , CallBackFunction);
  //* URL [required] = api link / file path to load data
  //* DATA = key value pair from site to server
  //* callback = function which run after data loaded have three parameters
  // 1. responseTxt = contain result txt if call succeeds
  // 2. statusTxt = contains status of call
  // 1. xhr = XmlHttpRequest object

  $("#ajaxLoadTxtBtn").click(function () {
    $("#dataParagraph").load("./data/data.txt");
  });
  $("#ajaxLoadHtmlBtn").click(function () {
    $("#dataParagraph").load("./data/htmldata.html");
  });
  $("#ajaxLoadHtmlImgBtn").click(function () {
    $("#dataParagraph").load("./data/htmldata.html img");
  });
  $("#ajaxLoadBtnWithCallback").click(function () {
    const city = "Rajkot";
    const apiUrl = `https://api.openweathermap.org/data/2.5/weather?q=${city}&appid=9fee4dff51fbc026467d7c2ac54d10e1&units=metric`;
    $("#dataParagraph").load(apiUrl, (responseTxt, statusTxt, xhr) => {
      if (statusTxt === "success") {
        alert(
          `All data fetched !!! \n\nresponseTxt : ${responseTxt}\n\nstatusTxt : ${statusTxt}\n\nxhr : ${xhr.status}`
        );
      } else {
        alert(`Error fetching data: ${xhr.status} - ${xhr.statusText}`);
      }
    });
  });

  //! $.get("URL","DATA" , CallBackFunction , "Datatype");
  //* URL [required] = api link / file path to load data
  //* DATA = key value pair from site to server
  //* callback = function which run after data loaded have three parameters
  // 1. responseTxt = contain result txt if call succeeds
  // 2. statusTxt = contains status of call
  // 1. xhr = XmlHttpRequest object
  //* DATA Type = html, script, text ,json, xml

  $("#cityForm").submit(function (event) {
    event.preventDefault(); // Prevent form from refreshing the page
    const cityName = $("#cityInput").val(); // Get the city name from input
    const apiUrl = `https://api.openweathermap.org/data/2.5/weather?q=${cityName}&appid=9fee4dff51fbc026467d7c2ac54d10e1&units=metric`;

    console.log("Request URL: ", apiUrl);

    // Using $.get() to fetch weather data
    $.get(apiUrl, function (data, statusText, xhr) {
      console.log("GET Response data:", data);
      console.log("GET Response status:", statusText);
      console.log("GET Response xhr:", xhr);

      if (data && data.main) {
        const city = data.name;
        const temperature = data.main.temp;
        const weather = data.weather[0].description;

        $("#weatherData")
          .html(
            `
          <strong>Weather Details:</strong><br>
          City: ${city}<br>
          Temperature: ${temperature} °C<br>
          Description: ${weather}
        `
          )
          .css({ background: "linear-gradient(to bottom, #2f80ed, #56ccf2)" });

        $("#cityInput").val("");
      } else {
        //! Handle invalid city
        $("#weatherData")
          .html(
            `
          <strong>Error:</strong> Could not fetch valid data for this city.
        `
          )
          .css({ background: "crimson" });
      }
    }).fail(function (error) {
      //! Handle errors
      console.log("Error fetching data:", error);
      $("#weatherData")
        .html(
          `
        <strong>Error:</strong> Unable to fetch weather data. Please check the city name.
      `
        )
        .css({ background: "crimson" });
    });
  });

  //! $.post("URL","DATA" , CallBackFunction , "Datatype");
  //* URL [required] = api link to send data to server
  //* DATA = key value pair from site to server
  //* callback = function which runs after the request has been processed
  // 1. responseTxt = contains result txt if the request is successful
  // 2. statusTxt = contains status of the call
  // 1. xhr = XmlHttpRequest object
  //* DATA Type = html, script, text, json

  // Handle POST request form submission
  $("#postForm").submit(function (event) {
    event.preventDefault(); // Prevent form from submitting and reloading the page
    console.log("Form submitted"); // Debugging statement to confirm submission is captured

    const postData = $("#postDataInput").val(); // Get the data from input
    const apiUrl = "https://jsonplaceholder.typicode.com/posts"; // Example POST endpoint

    console.log("POST Request Data: ", postData); // Debugging to see what data is being captured

    // Check if input data is not empty
    if (postData.trim() === "") {
      alert("Please enter some data to send.");
      return;
    }

    // Using $.post() to send data to the server
    $.post(apiUrl, { data: postData }, function (response, statusText, xhr) {
      console.log("POST Response:", response); // Debugging to see the response
      console.log("POST Status:", statusText);
      console.log("POST XHR:", xhr);

      if (response) {
        // Display the response from the server
        $("#postDataResult")
          .html(
            `<strong>Data posted successfully:</strong><br>Response: ${JSON.stringify(
              response
            )}`
          )
          .css({ background: "linear-gradient(to top, #11998e, #38ef7d)" });
      } else {
        $("#postDataResult")
          .html("<strong>Error:</strong> Could not send the data.")
          .css({ background: "crimson" });
      }
    }).fail(function (error) {
      // Handle errors
      console.log("Error posting data:", error);
      $("#postDataResult")
        .html(`<strong>Error:</strong> Unable to send data. Please try again.`)
        .css({ background: "crimson" });
    });
  });

  //! Demo for $.ajax() using Random User API
  $("#ajaxDemoBtn").click(function () {
    const apiUrl = `https://randomuser.me/api/`; // URL to fetch random user details
    $.ajax({
      url: apiUrl, // The URL for the request
      method: "GET", // HTTP method (GET, POST, PUT, DELETE, etc.)
      dataType: "json", // The type of data expected from the server
      success: function (response) {
        const user = response.results[0]; // Get the first user from the response
        const userDetails = `
                <h2>Random User Details:</h2><br>
                <div class="container">
                <img src="${user.picture.large}" alt="User Picture" style="width: 100%; height: 100%;"><br>
                <div class='right'>
                <h3>
                Name: ${user.name.first} ${user.name.last}<br>
                </h3> 
                <h3>
                Gender: ${user.gender}<br>
                </h3> 
                <h3>
                Email: ${user.email}<br>
                </h3> 
                <h3>
                Location: ${user.location.city}, ${user.location.country}<br>
                </h3> 
                <h3>
                Phone: ${user.phone}
                </h3> 
                </div>
                </div>
              `;
        $("#ajaxResponseData")
          .html(userDetails)
          .css({ background: "linear-gradient(to top, #11998e, #38ef7d)" });
      },
      error: function (xhr, status, error) {
        console.error("Error in Ajax request:", error);
        $("#ajaxResponseData")
          .html(`<strong>Error:</strong> Could not fetch data.`)
          .css({ background: "crimson" });
      },
      complete: function () {
        console.log("AJAX Request Complete.");
      },
    });
  });
});
