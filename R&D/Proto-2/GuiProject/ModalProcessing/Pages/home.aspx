<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="home.aspx.cs" Inherits="ModalProcessing.Pages.home" %>

<!DOCTYPE html>
<html>
<head>
    <title>Home</title>
    <script>
        document.addEventListener("DOMContentLoaded", function () {
            let dtoData = sessionStorage.getItem("userDTO");

            if (!dtoData) {
                document.write("<h2>No user data found. Please log in.</h2>");
                return;
            }

            let dto;
            try {
                dto = JSON.parse(dtoData);
            } catch (e) {
                console.error("Error parsing userDTO:", e);
                document.write("<h2>Invalid user data. Please log in again.</h2>");
                return;
            }

            // Display Welcome Message
            document.write(`<h2>Welcome, ${dto.Username}</h2>`);

            // Load Sidebar
            let sidebarHtml = "<ul>";
            dto.SidebarOptions.forEach(item => {
                sidebarHtml += `<li>${item}</li>`;
            });
            sidebarHtml += "</ul>";
            document.write(sidebarHtml);

            // Load Allowed Sections
            let allowedSectionsHtml = "<h3>Allowed Sections</h3><ul>";
            dto.AllowedSections.forEach(item => {
                allowedSectionsHtml += `<li>${item}</li>`;
            });
            allowedSectionsHtml += "</ul>";
            document.write(allowedSectionsHtml);

            // Load Async Scripts
            if (Array.isArray(dto.AsyncScripts)) {
                dto.AsyncScripts.forEach(script => {
                    let scriptTag = document.createElement("script");
                    scriptTag.src = script;
                    document.head.appendChild(scriptTag);
                });
            }

            // Mark Setup as Complete & Send Update to API
            if (!dto.IsSetupComplete) {
                dto.IsSetupComplete = true;

                fetch("http://localhost:58530/api/config/update", {
                    method: "POST",
                    headers: { "Content-Type": "application/json" },
                    body: JSON.stringify(dto)
                }).catch(error => console.error("Error updating config:", error));
            }
        });
    </script>
</head>
<body></body>
</html>
