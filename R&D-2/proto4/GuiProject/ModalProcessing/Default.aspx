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
            //debugger;
            console.log("Full URL: ", window.location.href); // Debugging
            let token = getTokenFromUrl();
            let role = getRoleFromUrl();
            console.log("Token : " + token);
            console.log("Role : " + role);

            if (token && role) {
                processTokenAndRole(token, role); // Pass role here
            } else {
                let existingToken = getToken();
                if (existingToken) {
                    fetchUserDTO(existingToken);
                }
            }
        });

        function getTokenFromUrl() {
            let urlParams = new URLSearchParams(window.location.search);
            return urlParams.get("token");
        }

        function getRoleFromUrl() {
            // Decode the URL and replace &amp; with &
            let decodedUrl = decodeURIComponent(window.location.href).replace(/&amp;/g, '&');
            let urlParams = new URLSearchParams(decodedUrl.split('?')[1]); // Extract query parameters
            let role = urlParams.get("role");
            console.log("Extracted Role from URL: ", role); // Debugging
            return role;
        }

        // Pass role as a parameter
        function processTokenAndRole(token, role) {
            if (role === "Admin") {
                document.cookie = `authToken=${token}; path=/; max-age=7200`;
            } else {
                sessionStorage.setItem("authToken", token);
            }

            // Clear token from the URL
            window.history.replaceState({}, document.title, window.location.pathname);

            // Redirect based on role
            if (role === "Admin") {
                window.location.href = "Pages/admin-dashboard.aspx";
            } else {
                window.location.href = "Pages/user-dashboard.aspx";
            }
        }

        function getToken() {
            let cookieToken = document.cookie
                .split("; ")
                .find(row => row.startsWith("authToken="))
                ?.split("=")[1];

            let sessionToken = sessionStorage.getItem("authToken");
            return cookieToken || sessionToken;
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

        function login() {
            let username = document.getElementById("username").value;
            let password = document.getElementById("password").value;

            fetch("http://localhost:58530/api/auth/login", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify({ Username: username, Password: password })
            })
                .then(response => {
                    if (!response.ok) {
                        throw new Error("Login failed");
                    }
                    return response.json();
                })
                .then(data => {
                    if (data.Token && data.Role) {
                        // Redirect to the same page with token and role as query parameters
                        window.location.href = `Default.aspx?token=${data.Token}&role=${data.Role}`;
                    } else {
                        alert("Invalid response from server");
                    }
                })
                .catch(error => {
                    alert(error.message);
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