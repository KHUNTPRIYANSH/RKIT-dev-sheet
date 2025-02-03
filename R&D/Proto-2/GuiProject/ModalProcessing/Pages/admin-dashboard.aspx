<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="admin-dashboard.aspx.cs" Inherits="ModalProcessing.Pages.admin_dashboard" %>
<!DOCTYPE html>
<html>
<head>
    <title>Admin Dashboard</title>
    <script>
        let dto = JSON.parse(sessionStorage.getItem("userDTO"));

        if (dto) {
            document.write(`<h2>Welcome, Admin ${dto.Username}</h2>`);

            // Load Sidebar
            let sidebar = `<ul>${dto.SidebarOptions.map(item => `<li>${item}</li>`).join("")}</ul>`;
            document.write(sidebar);

            // Load Allowed Sections
            let allowedSections = `<h3>Allowed Sections</h3><ul>${dto.AllowedSections.map(item => `<li>${item}</li>`).join("")}</ul>`;
            document.write(allowedSections);

            // Load Async Scripts
            if (dto.AsyncScripts && Array.isArray(dto.AsyncScripts)) {
                dto.AsyncScripts.forEach(script => {
                    let scriptTag = document.createElement("script");
                    scriptTag.src = script;
                    document.head.appendChild(scriptTag);
                });
            }

            // Mark Setup as Complete and Send Back to API
            dto.IsSetupComplete = true;
            fetch("http://localhost:58530/api/config/update", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(dto)
            });
        }
    </script>
</head>
<body></body>
</html>
