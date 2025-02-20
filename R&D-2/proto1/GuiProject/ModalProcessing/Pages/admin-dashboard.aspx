<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="admin-dashboard.aspx.cs" Inherits="ModalProcessing.Pages.admin_dashboard" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Admin Dashboard</title>
    <style>
        :root {
            --bg-light: #f8f9fa;
            --text-light: #212529;
            --bg-dark: #121212;
            --text-dark: #e0e0e0;
            --sidebar-bg-light: #ffffff;
            --sidebar-bg-dark: #333;
            --btn-bg-light: #007bff;
            --btn-bg-dark: #20e8a9;
        }
        main {
            border: 3px double var(--bg-light);
            background: #ddd;
            display: flex;
            width: 100%;
            flex-direction: column;
            margin-top: 25px;
            padding: 17.5px;
        }
        body {
            font-family: 'Arial', sans-serif;
            transition: background 0.3s, color 0.3s;
            background-color: var(--bg-light);
            color: var(--text-light);
            margin: 0;
            display: flex;
        }
        .sidebar {
            width: 250px;
            height: 100vh;
            padding: 20px;
            background: var(--sidebar-bg-light);
            transition: background 0.3s;
            box-shadow: 2px 0 5px rgba(0, 0, 0, 0.1);
        }
        .content {
            flex-grow: 1;
            padding: 20px;
            display: flex;
            flex-direction: column;
        }
        ul {
            list-style: none;
            padding: 0;
        }
        ul li {
            padding: 10px;
            margin: 5px 0;
            background: var(--btn-bg-light);
            color: white;
            border-radius: 5px;
            text-align: center;
            cursor: pointer;
        }
        ul li:hover {
            opacity: 0.9;
        }
        .dark-mode {
            background-color: var(--bg-dark);
            color: var(--text-dark);
        }
        .dark-mode .sidebar {
            background: var(--sidebar-bg-dark);
        }
        .dark-mode ul li {
            background: var(--btn-bg-dark);
        }
        .theme-toggle {
            padding: 10px;
            background: var(--btn-bg-light);
            color: white;
            border: none;
            border-radius: 5px;
            cursor: pointer;
        }
        .dark-mode main {
            background: #353535;
        }
        .dark-mode .theme-toggle {
            background: var(--btn-bg-dark);
        }
    </style>
    <script>
        document.addEventListener("DOMContentLoaded", async function () {
            let dtoData = sessionStorage.getItem("userDTO");
            if (!dtoData) {
                console.warn("DTO missing in sessionStorage. Trying to restore from cookie...");
                const token = getTokenFromCookie();
                if (token) {
                    await restoreDTOFromToken(token);
                    dtoData = sessionStorage.getItem("userDTO");
                }
            }
            if (!dtoData) {
                document.body.innerHTML = "<h2>No user data found. Please log in.</h2>";
                return;
            }
            let dto = JSON.parse(dtoData);
            let theme = sessionStorage.getItem("UserPreference") || "light";
            if (theme === "dark") document.body.classList.add("dark-mode");

            let sidebar = document.createElement("div");
            sidebar.className = "sidebar";
            let sidebarHtml = `<h3>Admin Menu</h3><ul>`;
            dto.SidebarOptions.forEach(item => {
                sidebarHtml += `<li>${item}</li>`;
            });
            sidebarHtml += "</ul>";
            sidebar.innerHTML = sidebarHtml;
            document.body.appendChild(sidebar);

            let content = document.createElement("div");
            content.className = "content";
            content.innerHTML = `<h2>Welcome, Admin ${dto.Username}</h2>`;

            let sectionsHtml = "<h3>Allowed Sections</h3><ul>";
            dto.AllowedSections.forEach(item => {
                sectionsHtml += `<li>${item}</li>`;
            });
            sectionsHtml += "</ul>";
            content.innerHTML += sectionsHtml;

            let themeButton = document.createElement("button");
            themeButton.className = "theme-toggle";
            themeButton.innerText = "Toggle Theme";
            themeButton.onclick = function () {
                let isDarkMode = document.body.classList.toggle("dark-mode");
                sessionStorage.setItem("UserPreference", isDarkMode ? "dark" : "light");
            };
            content.appendChild(themeButton);
            document.body.appendChild(content);
            let loadedModules = [];
            if (Array.isArray(dto.AsyncScripts)) {
                dto.AsyncScripts.forEach(script => {
                    let scriptTag = document.createElement("script");
                    scriptTag.src = script;
                    scriptTag.Defer = true;

                    scriptTag.onload = () => loadedModules.push(script);
                    document.head.appendChild(scriptTag);
                });
            }
        });

        function getTokenFromCookie() {
            return document.cookie
                .split("; ")
                .find(row => row.startsWith("authToken="))
                ?.split("=")[1];
        }

        async function restoreDTOFromToken(token) {
            try {
                const response = await fetch("http://localhost:58530/api/data/getdto", {
                    method: "GET",
                    headers: { "Authorization": `Bearer ${token}` }
                });
                if (!response.ok) return;
                const dto = await response.json();
                sessionStorage.setItem("userDTO", JSON.stringify(dto));
            } catch (error) {
                console.error("Error restoring DTO:", error);
            }
        }
    </script>
</head>
<body></body>
</html>
