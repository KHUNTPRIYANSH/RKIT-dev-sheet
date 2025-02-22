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
        });

        function toggleTheme() {
            let currentTheme = sessionStorage.getItem("UserPreference") || "light";
            let newTheme = currentTheme === "light" ? "dark" : "light";
            sessionStorage.setItem("UserPreference", newTheme);
            applyTheme(newTheme);
        }

        function applyTheme(theme) {
            if (theme === "dark") {
                document.body.classList.add("dark-mode");
            } else {
                document.body.classList.remove("dark-mode");
            }
        }

        async function login() {
            let username = document.getElementById("username").value;
            let password = document.getElementById("password").value;

            let response = await fetch("http://localhost:58530/api/auth/login", { 
                method: "POST", 
                headers: { "Content-Type": "application/json" }, 
                body: JSON.stringify({ username, password }) 
            });

            let dto = await response.json();
            if (dto && dto.Page) {
                sessionStorage.setItem("userDTO", JSON.stringify(dto)); // Store DTO
                window.location.href = dto.Page; // Redirect to specified page
            } else {
                alert("Invalid login!");
            }
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
