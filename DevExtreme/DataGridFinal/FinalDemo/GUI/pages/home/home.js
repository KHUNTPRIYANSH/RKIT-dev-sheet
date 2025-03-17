$(document).ready(function () {
    // Retrieve stored user details from localStorage
    let userName = localStorage.getItem("uname");
    let userRole = localStorage.getItem("urole");

    // Display welcome message with user's name
    $("h4").text("Welcome " + userName);

    let menuList = $("#menu-list");

    // Add "Open Profile" option to the menu for all users
    menuList.append('<li onclick="openProfile()">Open Profile</li>');

    // Add menu options based on user role
    if (userRole === "Admin" || userRole === "Editor") {
        menuList.append("<li onclick=\"loadPage('../../../components/departments/departments.html')\">Show Departments</li>");
        menuList.append("<li onclick=\"loadPage('../../../components/employees/employees.html')\">Show Employees</li>");
        menuList.append("<li onclick=\"loadPage('../../../components/users/users.html')\">Show Users</li>");
        menuList.append("<li onclick=\"loadPage('../../../components/attendance/attendance.html')\">Show Attendance</li>");
    } else {
        menuList.append("<li onclick=\"loadPage('../../../components/departments/departments.html')\">Show Departments</li>");
        menuList.append("<li onclick=\"loadPage('../../../components/attendance/attendance.html')\">Show Attendance</li>");
    }
});

// Function to open the profile section
function openProfile() {
    $("#holding-frame").hide(); // Hide the iframe
    $("#page-title").text("Profile").show(); // Show page title
    $("#profile-section").show().html(`
        <form id="profile-form">
            <div class="form-group">
                <label for="profileUserName">User Name</label>
                <div id="profileUserName"></div>
            </div>
            <div class="form-group">
                <label for="profileRole">Role</label>
                <div id="profileRole"></div>
            </div>
            <div class="form-group">
                <div id="saveProfileBtn"></div>
            </div>
        </form>
    `);

    let userRole = localStorage.getItem("urole");
    let userName = localStorage.getItem("uname");

    let canEditName = userRole !== "User"; // Only non-User roles can edit name
    let canEditRole = userRole === "Admin"; // Only Admin can edit role

    // Initialize user name input field
    $("#profileUserName").dxTextBox({
        value: userName,
        stylingMode: "filled",
        showClearButton: true,
        disabled: !canEditName,
    });

    // Initialize role selection dropdown
    $("#profileRole").dxSelectBox({
        items: ["Admin", "Editor", "User"],
        stylingMode: "filled",
        value: userRole,
        disabled: !canEditRole,
    });

    const hide = userRole === "User" ? false : true; // Hide save button for regular users
    $("#saveProfileBtn").dxButton({
        text: "Save Changes",
        type: "success",
        visible: hide,
        onClick: function () {
            let updatedName = $("#profileUserName").dxTextBox("instance").option("value");
            let updatedRole = $("#profileRole").dxSelectBox("instance").option("value");
            updateUserProfile(updatedName, updatedRole);
        },
    });
}

// Function to update user profile via API call
function updateUserProfile(updatedName, updatedRole) {
    let userId = localStorage.getItem("uid");
    let token = localStorage.getItem("utoken");
    let currentRole = localStorage.getItem("urole");
    let userPass = localStorage.getItem("upass");

    // Restrict profile editing based on role
    if (currentRole === "User") {
        alert("You are not allowed to edit your profile.");
        return;
    } else if (currentRole === "Editor" && updatedRole !== currentRole) {
        alert("Editors can only update their username.");
        return;
    }

    // AJAX request to update user profile
    $.ajax({
        url: "https://localhost:44310/update_user",
        type: "PUT",
        contentType: "application/json",
        headers: {
            Authorization: "Bearer " + token,
        },
        data: JSON.stringify({
            R01F01: userId,
            R01F02: updatedName,
            R01F03: userPass,
            R01F04: updatedRole,
        }),
        success: function () {
            // Update localStorage with new details
            localStorage.setItem("uname", updatedName);
            localStorage.setItem("urole", updatedRole);
            alert("Profile updated successfully!");
            $("h4").text("Welcome " + updatedName);
            logout(); // Log the user out to apply changes
        },
        error: function (xhr) {
            alert("Error updating profile: " + xhr.responseText);
        },
    });
}

// Function to log the user out
function logout() {
    if (confirm("Are you sure you want to logout?")) {
        // Clear user-related data from localStorage
        localStorage.setItem("uid", "");
        localStorage.setItem("utoken", "");
        localStorage.setItem("urole", "");
        localStorage.setItem("uname", "");

        alert("You have been logged out successfully.");
        window.location.href = "../../default.html"; // Redirect to login page
    }
}

// Function to load different pages dynamically in the iframe
function loadPage(page) {
    $("#profile-section").hide();
    $("#page-title").hide();
    $("#holding-frame").show().attr("src", page);
}
