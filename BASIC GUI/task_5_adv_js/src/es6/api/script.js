async function getWeather() {
  const city = document.getElementById("cityInput").value;
  const apiKey = "9fee4dff51fbc026467d7c2ac54d10e1";
  // const apiUrl = `https://api.openweathermap.org/data/2.5/weather?q=Rajkot&appid=${apiKey}&units=metric`;
  const apiUrl = `https://api.openweathermap.org/data/2.5/weather?q=${city}&appid=${apiKey}&units=metric`;

  try {
    const response = await fetch(apiUrl);
    const data = await response.json();

    document.getElementById("weatherResult").innerHTML = `
            <h2>Weather in ${data.name}</h2>
            <p>Temperature: ${data.main.temp}°C</p>
            <p>Description: ${data.weather[0].description}</p>
            <p>Humidity: ${data.main.humidity}%</p>
            <p>Wind Speed: ${data.wind.speed} m/s</p>
        `;
  } catch (error) {
    console.error("Error fetching weather data:", error);
    document.getElementById("weatherResult").innerHTML =
      "Error fetching weather data. Please try again.";
  }
}

//+===========================================================

//+===========================================================

// console.log("What getWeather function returns by default : ", getWeather());
// in async function it will always return a fulfilled promise aka resolved
// to get rejected promise from the async function we can use throw keyword to throw error so promise will be rejected
// to handle error we can use try and catch // in promise we dont need it coz we have catch
// ex
async function temp() {
  // ! some code
  // ! throw new Error("to make promise rejected");
  const apiUrl = `https://api.openweathermap.org/data/2.5/weather?q=Rajkot&appid=9fee4dff51fbc026467d7c2ac54d10e1&units=metric`;
  const apiUrl2 = `https://api.openweathermap.org/data/2.5/weather?q=Surat&appid=9fee4dff51fbc026467d7c2ac54d10e1&units=metric`;

  const result = await fetch(apiUrl);
  console.log(result);
  console.log("below fetch 1 line");
  console.log("below fetch 1 line");
  console.log("below fetch 1 line");
  const result2 = await fetch(apiUrl);
  console.log(result2);
  console.log("below fetch 2 line");
  console.log("below fetch 2 line");
  console.log("below fetch 2 line");
}
// ! console.log("what temp function return : ", temp());
temp();
console.log("below call line");
console.log("below call line");
console.log("below call line");
console.log("below call line");