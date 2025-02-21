document.addEventListener("DOMContentLoaded", () => {
    console.log("Page loaded");

    // Load user preference
    let theme = sessionStorage.getItem("UserPreference") || "light";
    console.log("Current theme: ", theme);
    applyTheme(theme);

    // Simulate login (you would call this when the login action occurs)
    login();
});

// Function to toggle the theme
function toggleTheme() {
    let currentTheme = sessionStorage.getItem("UserPreference") || "light";
    let newTheme = currentTheme === "light" ? "dark" : "light";
    sessionStorage.setItem("UserPreference", newTheme);
    console.log("Theme toggled. New theme:", newTheme);
    applyTheme(newTheme);
}

// Function to apply the theme to the page
function applyTheme(theme) {
    console.log("Applying theme:", theme);
    if (theme === "dark") {
        document.body.classList.add("dark-mode");
    } else {
        document.body.classList.remove("dark-mode");
    }
}

// Function to handle login
async function login() {
    let username = "admin"; // Static for now
    let password = "admin123"; // Static for now
    console.log("Attempting login with username:", username);

    let response = await fetch("http://localhost:58530/api/auth/login", { 
                method: "POST", 
                headers: { "Content-Type": "application/json" }, 
                body: JSON.stringify({ username, password }) 
            });
    if (!response.ok) {
        console.log("Login failed with status:", response.status);
        alert("Invalid login!");
        return;
    }

    let dto = await response.json();
    console.log("Received DTO:", dto);
    if (dto && dto.AsyncScripts) {
        sessionStorage.setItem("userDTO", JSON.stringify(dto)); // Store DTO
        loadModules(dto); // Load modules based on the DTO
        loadSidebar(dto); // Load sidebar options based on the DTO
    } else {
        console.log("Invalid DTO received");
        alert("Invalid login!");
    }
}

// Function to load modules (scripts) based on DTO
function loadModules(dto) {
    console.log("Loading modules for user:", dto.Username);

    // Load async scripts (modules)
    let allScripts = [
        ...dto.AsyncScripts, // External scripts
        ...getSidebarModules(dto), // Sidebar modules
        ...getAllowedSectionModules(dto) // Allowed section modules
    ];

    // Load all scripts (combining sidebar, allowed sections, and external scripts)
    loadAsyncScripts(allScripts);

    // Update loaded modules and mark setup complete
    dto.LoadedModules = allScripts; // Store all scripts as loaded modules
    dto.IsSetupComplete = true;
    dto.UserPreference = sessionStorage.getItem("UserPreference");
    console.log("Updated DTO after setup:", dto);

    // Send updated DTO back to API
    updateUserConfig(dto);
}

// Function to get sidebar module scripts
function getSidebarModules(dto) {
    console.log("Loading sidebar modules for user:", dto.Username);

    // Map sidebar options to their corresponding script paths
    return dto.SidebarOptions.map(option => `/scripts/${dto.Role}/sidebarOptions/${option.toLowerCase()}.js`);
}

// Function to get allowed section module scripts
function getAllowedSectionModules(dto) {
    console.log("Loading allowed section modules for user:", dto.Username);

    // Map allowed sections to their corresponding script paths
    return dto.AllowedSections.map(section => `/scripts/${dto.Role}/allowedSections/${section.toLowerCase()}.js`);
}

// Function to load async scripts dynamically
function loadAsyncScripts(scripts) {
    scripts.forEach(scriptSrc => {
        let script = document.createElement("script");
        script.src = scriptSrc;
        script.onload = () => {
            console.log(`Script loaded and executed: ${scriptSrc}`);
        };
        script.onerror = () => {
            console.log(`Failed to load script: ${scriptSrc}`);
        };
        document.body.appendChild(script);
    });
}

// Function to send updated DTO back to the API
async function updateUserConfig(dto) {
    let response = await fetch("http://localhost:58530/api/config/update", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(dto)
    });

    if (response.ok) {
        console.log("DTO updated successfully");
    } else {
        console.log("Failed to update DTO");
    }
}
