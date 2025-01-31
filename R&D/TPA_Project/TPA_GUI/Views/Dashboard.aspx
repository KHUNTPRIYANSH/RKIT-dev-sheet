<script type="text/javascript">
    function getUserDetails() {
        var token = sessionStorage.getItem("JWTToken");

        fetch("http://localhost:44377/api/auth/get-user", {
            method: "GET",
            headers: {
                "Authorization": "Bearer " + token
            }
        })
            .then(response => response.json())
            .then(data => {
                console.log("User Details:", data);
                document.getElementById("userInfo").innerHTML =
                    "Username: " + data.Username + "<br>" +
                    "Role: " + data.Role;
            })
            .catch(error => console.error("Error fetching user details:", error));
    }
</script>

<button onclick="getUserDetails()">Get User Info</button>
<div id="userInfo"></div>
