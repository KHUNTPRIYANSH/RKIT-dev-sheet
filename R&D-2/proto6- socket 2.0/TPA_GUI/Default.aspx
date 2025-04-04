<!DOCTYPE html>
<html>
<head>
    <title>Script Loader</title>
    <!-- jQuery library for simplified AJAX calls and DOM manipulation -->
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <style>
        /* (Intentionally left blank for future styling) */
    </style>
    <script>
        // WebSocket and API configuration
        let socket; // Holds the WebSocket connection instance
        const API_BASE_URL = 'http://localhost:5000'; // Backend server URL
        const WS_URL = 'ws://localhost:8181'; // WebSocket server URL

        // Initial setup when page loads
        $(document).ready(function () {
            initializeApplication(); // Start the application flow
            $("#logoutBtn").click(logout); // Attach logout button click handler
        });

        // Main initialization sequence
        function initializeApplication() {
            checkAuthToken(); // Check if user already has a valid token
            setupWebSocketConnection(); // Establish WebSocket connection
        }

        // Check for existing authentication token
        function checkAuthToken() {
            const token = getToken(); // Get token from storage
            if (token) {
                console.log("🔑 Existing token found");
                fetchDTO(token); // Fetch user data if token exists
            }
        }

        // Set up WebSocket connection with event handlers
        function setupWebSocketConnection() {
            socket = new WebSocket(WS_URL); // Create new WebSocket connection

            // Connection established handler
            socket.onopen = () => {
                console.log("✅ WebSocket Connected!");
                checkPendingOperations(); // Check for pending actions
            };

            // Message received handler
            socket.onmessage = handleSocketMessage;

            // Error handler with automatic reconnection
            socket.onerror = handleSocketError;

            // Connection closed handler with automatic reconnection
            socket.onclose = handleSocketClose;
        }

        // Process incoming WebSocket messages
        function handleSocketMessage(event) {
            console.log("📩 WebSocket message:", event.data);

            // Handle token messages from server
            if (event.data.startsWith("TOKEN:")) {
                const token = event.data.split(':')[1];
                handleNewToken(token); // Process new authentication token
            }
        }

        // Handle WebSocket errors with retry logic
        function handleSocketError(error) {
            console.error("❌ WebSocket Error:", error);
            setTimeout(setupWebSocketConnection, 2000); // Retry connection after 2 seconds
        }

        // Handle WebSocket disconnections with retry logic
        function handleSocketClose() {
            console.log("❌ WebSocket Disconnected");
            setTimeout(setupWebSocketConnection, 5000); // Retry connection after 5 seconds
        }

        // Process new authentication token
        function handleNewToken(token) {
            storeToken(token); // Store the received token
            fetchDTO(token); // Fetch user data with new token
        }

        // Store authentication token securely
        function storeToken(token) {
            // Here if we want we can do storing based on role 
            // like as i have done previously if Admin store token in cokkie otherwise in session 
            sessionStorage.setItem("AuthToken", token); // Session-based storage
            document.cookie = `AuthToken=${token}; path=/;`; // Persistent cookie storage
            console.log("🔒 Token stored securely");
        }

        // Fetch user-specific data (DTO) from server
        async function fetchDTO(token) {
            try {
                debugger;
                const response = await $.ajax({
                    url: `${API_BASE_URL}/api/GetDTO`,
                    method: "GET",
                    headers: {
                        "Authorization": `Bearer ${token}`, // Send token in header
                        "Content-Type": "application/json"
                    }
                });

                console.log("🎯 Received DTO:", response);
                sessionStorage.setItem("userDTO", JSON.stringify(response)); // Store DTO
                await loadScriptsFromDTO(response); // Load required scripts
                confirmDTOProcessing(response); // Confirm completion to server [send updated dto to login api which is witing]
            } catch (error) {
                console.error("❌ DTO Fetch Error:", error.statusText);
            }
        }

        // Load scripts specified in the DTO
        async function loadScriptsFromDTO(dto) {
            try {
                //debugger;
                // Combine different script categories
                const scriptsToLoad = [
                    ...dto.asyncScripts, // Core application scripts [/<someFile>.js]
                    ...getRoleModules(dto.role, 'sidebar', dto.sidebarOptions), // Sidebar components [Admin/sidebar/<someFile>.js]
                    ...getRoleModules(dto.role, 'sections', dto.allowedSections) // Content sections [Admin/sections/<someFile>.js]
                ];

                console.log("📂 Scripts to load:", scriptsToLoad);
                const loadedScripts = await loadScripts(scriptsToLoad);

                // Track successfully loaded modules
                dto.loadedModules = scriptsToLoad.filter((script, index) =>
                    loadedScripts[index].status === "fulfilled"
                );
                dto.isSetupComplete = true; // Mark setup as complete

                console.log("✅ All scripts loaded successfully");
                confirmDTOProcessing(dto); // Notify server
            } catch (error) {
                console.error("❌ Script Loading Error:", error);
            }
        }

        // Generate script paths based on user role and component type
        function getRoleModules(role, type, items) {
            return items.map(item =>
                `/scripts/${role}/${type}/${item.toLowerCase()}.js` // Path pattern
            );
        }

        // Load individual scripts with success/failure tracking
        async function loadScripts(scripts) {
            return Promise.all(scripts.map(script =>
                new Promise((resolve) => {
                    const scriptElement = document.createElement('script');
                    scriptElement.src = script;

                    // Success handler
                    scriptElement.onload = () => resolve({
                        script,
                        status: "fulfilled"
                    });

                    // Error handler
                    scriptElement.onerror = () => resolve({
                        script,
                        status: "rejected"
                    });

                    document.head.appendChild(scriptElement); // Add to page
                })
            ));
        }

        // Confirm DTO processing completion to server
        function confirmDTOProcessing(dto) {
            if (socket && socket.readyState === WebSocket.OPEN) {
                const message = `DTO_CONFIRMED:${getToken()}:${JSON.stringify(dto)}`;
                socket.send(message); // Send confirmation message
                console.log("✅ DTO processing confirmed");
            } else {
                console.error("❌ WebSocket not connected for confirmation");
                setTimeout(() => confirmDTOProcessing(dto), 1000); // Retry confirmation
            }
        }

        // Retrieve authentication token from storage
        function getToken() {
            return sessionStorage.getItem("AuthToken") || // Check session storage
                document.cookie.match(/AuthToken=([^;]+)/)?.[1]; // Check cookies
        }

        // Check for pending operations after reconnection
        function checkPendingOperations() {
            const token = getToken();
            if (token && !sessionStorage.getItem("dtoProcessed")) {
                fetchDTO(token); // Resume interrupted workflow
            }
        }

        // Handle user logout
        function logout() {
            sessionStorage.clear(); // Clear session data
            document.cookie = "AuthToken=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;";
            console.log("🚪 User logged out");
            location.reload(); // Refresh page to reset state
        }
    </script>
</head>
<body>
    <!-- User interface elements -->
    <button id="logoutBtn">Logout</button>
    <h1>Application Dashboard</h1>
    <p>Check console and Sources tab for loaded scripts</p>
</body>
</html>