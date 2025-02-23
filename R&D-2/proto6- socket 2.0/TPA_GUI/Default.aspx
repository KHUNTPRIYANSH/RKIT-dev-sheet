<!DOCTYPE html>
<html>
<head>
    <title>Script Loader</title>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <style>
        
    </style>
    <script>
        let socket;
        const API_BASE_URL = 'http://localhost:5000';
        const WS_URL = 'ws://localhost:8181';

        $(document).ready(function () {
            initializeApplication();
            $("#logoutBtn").click(logout);
        });

        function initializeApplication() {
            checkAuthToken();
            setupWebSocketConnection();
        }

        function checkAuthToken() {
            const token = getToken();
            if (token) {
                console.log("🔑 Existing token found");
                fetchDTO(token);
            }
        }

        function setupWebSocketConnection() {
            socket = new WebSocket(WS_URL);

            socket.onopen = () => {
                console.log("✅ WebSocket Connected!");
                checkPendingOperations();
            };

            socket.onmessage = handleSocketMessage;
            socket.onerror = handleSocketError;
            socket.onclose = handleSocketClose;
        }

        function handleSocketMessage(event) {
            console.log("📩 WebSocket message:", event.data);

            if (event.data.startsWith("TOKEN:")) {
                const token = event.data.split(':')[1];
                handleNewToken(token);
            }
        }

        function handleSocketError(error) {
            console.error("❌ WebSocket Error:", error);
            setTimeout(setupWebSocketConnection, 2000);
        }

        function handleSocketClose() {
            console.log("❌ WebSocket Disconnected");
            setTimeout(setupWebSocketConnection, 5000);
        }

        function handleNewToken(token) {
            storeToken(token);
            fetchDTO(token);
        }

        function storeToken(token) {
            sessionStorage.setItem("AuthToken", token);
            document.cookie = `AuthToken=${token}; path=/;`;
            console.log("🔒 Token stored securely");
        }

        async function fetchDTO(token) {
            try {
                const response = await $.ajax({
                    url: `${API_BASE_URL}/api/GetDTO`,
                    method: "GET",
                    headers: {
                        "Authorization": `Bearer ${token}`,
                        "Content-Type": "application/json"
                    }
                });

                console.log("🎯 Received DTO:", response);
                sessionStorage.setItem("userDTO", JSON.stringify(response));
                await loadScriptsFromDTO(response);
                confirmDTOProcessing(response);
            } catch (error) {
                console.error("❌ DTO Fetch Error:", error.statusText);
            }
        }

        async function loadScriptsFromDTO(dto) {
            try {
                const scriptsToLoad = [
                    ...dto.asyncScripts,
                    ...getRoleModules(dto.role, 'sidebar', dto.sidebarOptions),
                    ...getRoleModules(dto.role, 'sections', dto.allowedSections)
                ];

                console.log("📂 Scripts to load:", scriptsToLoad);
                const loadedScripts = await loadScripts(scriptsToLoad);

                // Update DTO with successfully loaded modules
                dto.loadedModules = scriptsToLoad.filter((script, index) =>
                    loadedScripts[index].status === "fulfilled"
                );
                dto.isSetupComplete = true;

                console.log("✅ All scripts loaded successfully");
                confirmDTOProcessing(dto);
            } catch (error) {
                console.error("❌ Script Loading Error:", error);
            }
        }


        function getRoleModules(role, type, items) {
            // Removed timestamp parameter from URL
            return items.map(item => `/scripts/${role}/${type}/${item.toLowerCase()}.js`);
        }


        async function loadScripts(scripts) {
            return Promise.all(scripts.map(script =>
                new Promise((resolve) => {
                    const scriptElement = document.createElement('script');
                    scriptElement.src = script;
                    scriptElement.onload = () => resolve({ script, status: "fulfilled" });
                    scriptElement.onerror = () => resolve({ script, status: "rejected" });
                    document.head.appendChild(scriptElement);
                })
            ));
        }

        function confirmDTOProcessing(dto) {
            if (socket && socket.readyState === WebSocket.OPEN) {
                const message = `DTO_CONFIRMED:${getToken()}:${JSON.stringify(dto)}`;
                socket.send(message);
                console.log("✅ DTO processing confirmed");
            } else {
                console.error("❌ WebSocket not connected for confirmation");
                setTimeout(() => confirmDTOProcessing(dto), 1000);
            }
        }

        function getToken() {
            return sessionStorage.getItem("AuthToken") ||
                document.cookie.match(/AuthToken=([^;]+)/)?.[1];
        }

        function checkPendingOperations() {
            const token = getToken();
            if (token && !sessionStorage.getItem("dtoProcessed")) {
                fetchDTO(token);
            }
        }

        function logout() {
            sessionStorage.clear();
            document.cookie = "AuthToken=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;";
            console.log("🚪 User logged out");
            location.reload();
        }
    </script>
</head>
<body>
    <button id="logoutBtn">Logout</button>
    <h1>Application Dashboard</h1>
    <p>Check console and Sources tab for loaded scripts</p>
</body>
</html>