async function getWeather() {
  const city = document.getElementById("cityInput").value;
  const apiKey = "9fee4dff51fbc026467d7c2ac54d10e1";
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
