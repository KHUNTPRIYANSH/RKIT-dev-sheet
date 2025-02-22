<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="home.aspx.cs" Inherits="ModalProcessing.Pages.home" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Home</title>
    <style>
        :root {
            --bg-light: #f4f4f4;
            --text-light: #333;
            --bg-dark: #1e1e1e;
            --text-dark: #fff;
            --sidebar-bg-light: #fff;
            --sidebar-bg-dark: #333;
            --btn-bg-light: #007bff;
            --btn-bg-dark: #ff9800;
        }

        body {
            font-family: Arial, sans-serif;
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
    </style>
    <script>
        document.addEventListener("DOMContentLoaded", function () {
            let dtoData = sessionStorage.getItem("userDTO");

            if (!dtoData) {
                document.body.innerHTML = "<h2>No user data found. Please log in.</h2>";
                return;
            }

            let dto;
            try {
                dto = JSON.parse(dtoData);
            } catch (e) {
                console.error("Error parsing userDTO:", e);
                document.body.innerHTML = "<h2>Invalid user data. Please log in again.</h2>";
                return;
            }

            // Apply User Preference (Theme)
            let theme = sessionStorage.getItem("UserPreference") || "light";
            if (theme === "dark") document.body.classList.add("dark-mode");

            // Load Sidebar
            let sidebar = document.createElement("div");
            sidebar.className = "sidebar";
            let sidebarHtml = `<h3>Menu</h3><ul>`;
            dto.SidebarOptions.forEach(item => {
                sidebarHtml += `<li>${item}</li>`;
            });
            sidebarHtml += "</ul>";
            sidebar.innerHTML = sidebarHtml;
            document.body.appendChild(sidebar);

            // Load Content
            let content = document.createElement("div");
            content.className = "content";
            content.innerHTML = `<h2>Welcome, ${dto.Username}</h2>`;

            // Load Allowed Sections
            let sectionsHtml = "<h3>Allowed Sections</h3><ul>";
            dto.AllowedSections.forEach(item => {
                sectionsHtml += `<li>${item}</li>`;
            });
            sectionsHtml += "</ul>";
            content.innerHTML += sectionsHtml;
            document.body.appendChild(content);

            // Load Async Scripts & Track Loaded Modules
            let loadedModules = [];
            if (Array.isArray(dto.AsyncScripts)) {
                dto.AsyncScripts.forEach(script => {
                    let scriptTag = document.createElement("script");
                    scriptTag.src = script;
                    scriptTag.onload = () => loadedModules.push(script);
                    document.head.appendChild(scriptTag);
                });
            }

            // Update DTO after Processing
            setTimeout(() => {
                dto.IsSetupComplete = true;
                dto.LoadedModules = loadedModules;
                dto.UserPreference = theme;
                sessionStorage.setItem("userDTO", JSON.stringify(dto)); // Save updates

                fetch("http://localhost:58530/api/config/update", {
                    method: "POST",
                    headers: { "Content-Type": "application/json" },
                    body: JSON.stringify(dto)
                }).catch(error => console.error("Error updating config:", error));
            }, 3000);
        });
    </script>
</head>
<body></body>
</html>
