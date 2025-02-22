<!DOCTYPE html>
<html>
<head>
    <title>WebSocket JWT Exchange</title>
    <script>
        let socket;

        function connectWebSocket() {
            socket = new WebSocket("ws://localhost:8181"); // API WebSocket URL

            socket.onopen = function () {
                console.log("✅ WebSocket Connected!");
            };

            socket.onmessage = function (event) {
                let token = event.data;
                document.cookie = "AuthToken=" + token + "; path=/; max-age=3600";
                console.log("🔑 Token received and stored: " + token);

                // Send success message back to API
                socket.send("Success");
            };

            socket.onerror = function (error) {
                console.log("❌ WebSocket Error: ", error);
            };

            socket.onclose = function () {
                console.log("❌ WebSocket Disconnected!");
            };
        }

        function checkCookie() {
            let token = document.cookie.split('; ').find(row => row.startsWith('AuthToken='));
            if (token) {
                token = token.split('=')[1];
                alert("Stored Token: " + token);
                document.cookie = `authToken=${token}; path=/; max-age=7200`;
            } else {
                alert("No token found.");
            }
        }
    </script>
</head>
<body onload="connectWebSocket()">
    <h2>WebSocket JWT Exchange</h2>
    <button onclick="checkCookie()">Check Token</button>
</body>
</html>
