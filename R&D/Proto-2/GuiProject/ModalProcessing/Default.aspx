<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ModalProcessing.Default" %>

<!DOCTYPE html>
<html>
<head>
    <title>Login Page</title>
    <script>
        async function login() {
            let username = document.getElementById("username").value;
            let password = document.getElementById("password").value;

            let response = await fetch("http://localhost:58530/api/auth/login", { 
                method: "POST", 
                headers: { "Content-Type": "application/json" }, 
                body: JSON.stringify({ username, password }) 
            });
            console.log("Res [default] : " + response)

            let dto = await response.json();
            console.log("DTO [default] : " + dto)
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
    <h2>Login</h2>
    <input type="text" id="username" placeholder="Username">
    <input type="password" id="password" placeholder="Password">
    <button onclick="login()">Login</button>
</body>
</html>