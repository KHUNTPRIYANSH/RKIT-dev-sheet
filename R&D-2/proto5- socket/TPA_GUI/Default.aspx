<!DOCTYPE html>
<html>
<head>
    <title>WebSocket JWT Exchange</title>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script>
        let socket;

        $(document).ready(function () {
            checkAuthToken();
            connectWebSocket();

            $("#logoutBtn").click(function () {
                logout();
            });
        });

        function checkAuthToken() {
            let token = getToken();
            if (token) {
                fetchDTO();
            }
        }

        function getToken() {
            return sessionStorage.getItem("AuthToken") || getCookie("AuthToken");
        }

        function connectWebSocket() {
            socket = new WebSocket("ws://localhost:8181");

            socket.onopen = function () {
                console.log("✅ WebSocket Connected!");
            };

            socket.onmessage = function (event) {
                const token = event.data;
                sessionStorage.setItem("AuthToken", token);
                document.cookie = `AuthToken=${token}; path=/;`;
                console.log("🔑 Token received and stored:", token);

                socket.send("Success");
                fetchDTO();
            };

            socket.onerror = function (error) {
                console.error("❌ WebSocket Error:", error);
            };

            socket.onclose = function () {
                console.log("❌ WebSocket Disconnected!");
            };
        }

        function fetchDTO() {
            let token = getToken();
            if (!token) {
                console.error("❌ No token found!");
                return;
            }

            $.ajax({
                url: "http://localhost:5000/api/dto",
                method: "GET",
                headers: { "Authorization": `Bearer ${token}` },
                success: function (dto) {
                    console.log("🎯 Received DTO:", dto);
                    sessionStorage.setItem("userDTO", JSON.stringify(dto));
                    loadModules(dto);
                    loadSidebar(dto);
                },
                error: function (xhr) {
                    console.error("❌ Failed to fetch DTO:", xhr.statusText);
                }
            });
        }

        function loadSidebar(dto) {
            console.log("📂 Loading sidebar for:", dto.username);

            if (!dto.sidebarOptions) return;

            let sidebarModules = dto.sidebarOptions.map(option => `/scripts/${dto.role}/sidebarOptions/${option.toLowerCase()}.js`);

            loadScripts(sidebarModules, () => {
                console.log("✅ All sidebar scripts loaded.");
                updateUserConfig(dto);
            });
        }

        function loadModules(dto) {
            console.log("🔄 Loading modules:", dto.username);
            let allScripts = [...dto.asyncScripts, ...getSidebarModules(dto), ...getAllowedSectionModules(dto)];

            loadScripts(allScripts, () => {
                console.log("✅ All modules loaded.");
                dto.loadedModules = allScripts;
                dto.isSetupComplete = true;
                updateUserConfig(dto);
            });
        }

        function loadScripts(scripts, callback) {
            let loaded = 0;
            let total = scripts.length;

            if (total === 0) {
                callback();
                return;
            }

            $.each(scripts, function (_, scriptSrc) {
                $.getScript(scriptSrc)
                    .done(() => {
                        console.log(`✅ Loaded: ${scriptSrc}`);
                        loaded++;
                        if (loaded === total) {
                            callback();
                        }
                    })
                    .fail(() => {
                        console.error(`❌ Failed: ${scriptSrc}`);
                        loaded++;
                        if (loaded === total) {
                            callback();
                        }
                    });
            });
        }

        function getSidebarModules(dto) {
            return dto.sidebarOptions.map(option => `/scripts/${dto.role}/sidebarOptions/${option.toLowerCase()}.js`);
        }

        function getAllowedSectionModules(dto) {
            return dto.allowedSections.map(section => `/scripts/${dto.role}/allowedSections/${section.toLowerCase()}.js`);
        }

        function updateUserConfig(dto) {
            if (socket && socket.readyState === WebSocket.OPEN) {
                socket.send(JSON.stringify(dto));
                console.log("✅ Updated DTO sent to server:", dto);
            } else {
                console.error("❌ WebSocket is not connected! Retrying...");
                setTimeout(() => updateUserConfig(dto), 2000);
            }
        }

        function getCookie(name) {
            let match = document.cookie.match(new RegExp('(^| )' + name + '=([^;]+)'));
            return match ? match[2] : null;
        }

        function logout() {
            sessionStorage.clear();
            document.cookie = "AuthToken=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;";
            console.log("🚪 Logged out! Session & cookies cleared.");
            location.reload();
        }
    </script>
</head>
<body>
    <h2>WebSocket JWT Exchange</h2>
    <button id="logoutBtn">Logout</button>
</body>
</html>
