<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ModalProcessing.Default" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Login Page</title>
    <link href="styles.css" rel="stylesheet" />
    <script>
        document.addEventListener("DOMContentLoaded", () => {
            let theme = sessionStorage.getItem("UserPreference") || "light";
            applyTheme(theme);

            let token = getToken();
            if (token) {
                fetchUserDTO(token);
            }
        });

        function toggleTheme() {
            let currentTheme = sessionStorage.getItem("UserPreference") || "light";
            let newTheme = currentTheme === "light" ? "dark" : "light";
            sessionStorage.setItem("UserPreference", newTheme);
            applyTheme(newTheme);
        }

        function applyTheme(theme) {
            document.body.classList.toggle("dark-mode", theme === "dark");
        }

        function getToken() {
            let cookieToken = document.cookie
                .split("; ")
                .find(row => row.startsWith("authToken="))
                ?.split("=")[1];

            let sessionToken = sessionStorage.getItem("authToken");
            return cookieToken || sessionToken;
        }

        async function login() {
            let username = document.getElementById("username").value;
            let password = document.getElementById("password").value;

            let loginResponse = await fetch("http://localhost:58530/api/auth/login", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({ username, password })
            });

            if (!loginResponse.ok) {
                alert("Invalid login!");
                return;
            }

            let loginData = await loginResponse.json();
            let token = loginData.Token;
            let role = loginData.Role;

            if (role === "Admin") {
                // Set cookie without Secure and SameSite for debugging
                document.cookie = `authToken=${token}; path=/; max-age=7200`;

                try {
                    await waitForCookie("authToken", token, 100, 100);
                } catch (error) {
                    console.error("Failed to set cookie:", error);
                    alert("Failed to set cookie. Please try again.");
                    return;
                }
            } else {
                sessionStorage.setItem("authToken", token);
            }

            await fetchUserDTO(token);
        }

        async function fetchUserDTO(token) {
            let dtoResponse = await fetch("http://localhost:58530/api/data/getdto", {
                method: "GET",
                headers: {
                    "Authorization": `Bearer ${token}`
                }
            });

            if (!dtoResponse.ok) {
                alert("Failed to fetch user DTO!");
                return;
            }

            let dto = await dtoResponse.json();
            sessionStorage.setItem("userDTO", JSON.stringify(dto));

            if (dto && dto.Page) {
                window.location.href = dto.Page;
            } else {
                alert("Invalid DTO received!");
            }
        }

        function waitForCookie(cookieName, expectedValue, maxAttempts = 100, interval = 100) {
            return new Promise((resolve, reject) => {
                let attempts = 0;
                let intervalId = setInterval(() => {
                    let cookieValue = document.cookie
                        .split("; ")
                        .find(row => row.startsWith(`${cookieName}=`))
                        ?.split("=")[1];

                    console.log(`Attempt ${attempts + 1}: Cookie Value =`, cookieValue);

                    if (cookieValue === expectedValue) {
                        clearInterval(intervalId);
                        resolve();
                    } else if (attempts >= maxAttempts) {
                        clearInterval(intervalId);
                        reject(new Error("Cookie not set in time"));
                    }
                    attempts++;
                }, interval);
            });
        }
    </script>
</head>
<body>
    <div class="container">
        <h2>Login</h2>
        <input type="text" id="username" placeholder="Username">
        <input type="password" id="password" placeholder="Password">
        <button onclick="login()">Login</button>
        <button class="theme-toggle" onclick="toggleTheme()">Toggle Theme</button>
    </div>
</body>
</html>