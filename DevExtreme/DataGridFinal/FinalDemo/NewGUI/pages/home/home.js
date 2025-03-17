/** @type {string} API base URL */
const API_URL = "https://localhost:44310/update_user";
/** @type {JQuery} Reference to the menu list element */
const $menuList = $("#menu-list");
/** @type {JQuery} Reference to the holding frame element */
const $holdingFrame = $("#holding-frame");
/** @type {JQuery} Reference to the profile section element */
const $profileSection = $("#profile-section");
/** @type {JQuery} Reference to the page title element */
const $pageTitle = $("#page-title");
/** @type {JQuery} Reference to the welcome message container */
const $welcomeContainer = $("#welcome-container");
/** @type {boolean} Flag to track if welcome message should be shown */
let showWelcomeMessage = true;

// Initialize module when DOM is ready
$(document).ready(initializeUserInterface);

/**
 * Initializes user interface based on stored credentials
 * @called_from - Document ready event
 */
function initializeUserInterface() {
  const userName = localStorage.getItem("uname");
  const userRole = localStorage.getItem("urole");

  // Display welcome message only if it's the first load
  if (showWelcomeMessage) {
    displayWelcomeMessage(userName);
  }

  buildNavigationMenu(userRole);
}

/**
 * Displays a styled welcome message
 * @param {string} userName - Current user's name
 * @called_from - initializeUserInterface
 */
function displayWelcomeMessage(userName) {
  $welcomeContainer
    .html(
      `
        <div id="welcome-message">
          <h1>Welcome, ${userName}!</h1>
          <p>We're glad to have you here. Start exploring the system.</p>
        </div>
      `
    )
    .show();
}

/**
 * Constructs navigation menu based on user role
 * @param {string} userRole - Current user's role
 * @called_from - initializeUserInterface
 */
function buildNavigationMenu(userRole) {
  $menuList.append(
    '<li onclick="ProfileManager.openProfile()">Open Profile</li>'
  );

  const adminMenuItems = [
    {
      text: "Departments",
      page: "../../../components/departments/departments.html",
    },
    {
      text: "Employees",
      page: "../../../components/employees/employees.html",
    },
    { text: "Users", page: "../../../components/users/users.html" },
    {
      text: "Attendance",
      page: "../../../components/attendance/attendance.html",
    },
  ];

  const userMenuItems = [
    {
      text: "Departments",
      page: "../../../components/departments/departments.html",
    },
    {
      text: "Attendance",
      page: "../../../components/attendance/attendance.html",
    },
  ];

  const menuItems =
    userRole === "Admin" || userRole === "Editor"
      ? adminMenuItems
      : userMenuItems;

  menuItems.forEach((item) => {
    $menuList.append(
      `<li onclick="ProfileManager.loadPage('${item.page}')">Show ${item.text}</li>`
    );
  });

  // Add click handler to remove welcome message permanently
  $menuList.find("li").on("click", () => {
    showWelcomeMessage = false;
    $welcomeContainer.hide();
  });
}

/**
 * Displays user profile editing interface
 * @called_from - Menu item click
 */
function openProfile() {
  $holdingFrame.hide();
  $pageTitle.text("Profile").show();
  $profileSection.show().html(createProfileFormTemplate());

  initializeFormControls(
    localStorage.getItem("uname"),
    localStorage.getItem("urole")
  );
}

/**
 * Creates profile form HTML template
 * @returns {string} Form HTML structure
 * @called_from - openProfile
 */
function createProfileFormTemplate() {
  return `
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
      `;
}

/**
 * Initializes profile form controls
 * @param {string} userName - Current username
 * @param {string} userRole - Current user role
 * @called_from - openProfile
 */
function initializeFormControls(userName, userRole) {
  const canEditName = userRole !== "User";
  const canEditRole = userRole === "Admin";

  $("#profileUserName").dxTextBox({
    value: userName,
    stylingMode: "filled",
    showClearButton: true,
    disabled: !canEditName,
  });

  $("#profileRole").dxSelectBox({
    items: ["Admin", "Editor", "User"],
    value: userRole,
    stylingMode: "filled",
    disabled: !canEditRole,
  });

  $("#saveProfileBtn").dxButton({
    text: "Save Changes",
    type: "success",
    visible: userRole !== "User",
    onClick: handleProfileUpdate,
  });
}

/**
 * Handles profile update form submission
 * @called_from - Save button click
 */
function handleProfileUpdate() {
  const updatedName = $("#profileUserName")
    .dxTextBox("instance")
    .option("value");
  const updatedRole = $("#profileRole").dxSelectBox("instance").option("value");

  if (validateProfileChanges(updatedName, updatedRole)) {
    updateUserProfile(updatedName, updatedRole);
  }
}

/**
 * Validates profile changes against user permissions
 * @param {string} newName - Proposed new username
 * @param {string} newRole - Proposed new role
 * @returns {boolean} True if changes are valid
 * @called_from - handleProfileUpdate
 */
function validateProfileChanges(newName, newRole) {
  const currentRole = localStorage.getItem("urole");

  if (currentRole === "User") {
    showStatusMessage("You are not allowed to edit your profile.", "error");
    return false;
  }

  if (currentRole === "Editor" && newRole !== currentRole) {
    showStatusMessage("Editors can only update their username.", "error");
    return false;
  }

  return true;
}

/**
 * Updates user profile through API
 * @param {string} updatedName - New username
 * @param {string} updatedRole - New user role
 * @called_from - handleProfileUpdate
 */
function updateUserProfile(updatedName, updatedRole) {
  const userId = localStorage.getItem("uid");
  const token = localStorage.getItem("utoken");
  const userPass = localStorage.getItem("upass");

  $.ajax({
    url: API_URL,
    type: "PUT",
    contentType: "application/json",
    headers: { Authorization: `Bearer ${token}` },
    data: JSON.stringify({
      R01F01: userId,
      R01F02: updatedName,
      R01F03: userPass,
      R01F04: updatedRole,
    }),
    success: () => handleProfileUpdateSuccess(updatedName, updatedRole),
    error: (xhr) =>
      showStatusMessage(`Error updating profile: ${xhr.responseText}`, "error"),
  });
}

/**
 * Handles successful profile update
 * @param {string} newName - Updated username
 * @param {string} newRole - Updated user role
 * @called_from - updateUserProfile
 */
function handleProfileUpdateSuccess(newName, newRole) {
  localStorage.setItem("uname", newName);
  localStorage.setItem("urole", newRole);
  showStatusMessage("Profile updated successfully!", "success");
  $("h4").text(`Welcome ${newName}`);
  logout();
}

/**
 * Displays status messages to the user
 * @param {string} message - Message to display
 * @param {string} type - Message type (success/error)
 * @called_from - Multiple locations
 */
function showStatusMessage(message, type) {
  DevExpress.ui.notify(message, type, 2000);
}

/**
 * Handles user logout process
 * @called_from - Menu item click
 */
function logout() {
  if (confirm("Are you sure you want to logout?")) {
    ["uid", "utoken", "urole", "uname"].forEach((key) =>
      localStorage.removeItem(key)
    );
    showStatusMessage("You have been logged out successfully.", "success");
    setTimeout(() => {
      window.location.href = "../../default.html";
    }, 1500);
  }
}

/**
 * Loads content into the main frame
 * @param {string} page - URL of the page to load
 * @called_from - Menu item click
 */
function loadPage(page) {
  $profileSection.hide();
  $pageTitle.hide();
  $holdingFrame.show().attr("src", page);
  $welcomeContainer.hide(); // Hide welcome message when a page is loaded
}

// Expose public methods
window.ProfileManager = {
  openProfile,
  loadPage,
  logout,
};
