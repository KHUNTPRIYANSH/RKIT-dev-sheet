$(function () {
  initializeUserNameField();
  initializePasswordField();
  initializeSubmitButton();
});

/**
 * Initializes the username input field with validation.
 * @calledFrom Page load event.
 */
function initializeUserNameField() {
  $("#userName")
    .dxTextBox({
      value: "",
      placeholder: "User Name",
      showClearButton: true,
    })
    .dxValidator({
      validationRules: [
        { type: "required", message: "User Name is required" },
        {
          type: "pattern",
          pattern: "^[a-zA-Z ]*$",
          message: "Only alphabets and spaces are allowed",
        },
      ],
    });
}

/**
 * Initializes the password input field with validation.
 * @calledFrom Page load event.
 */
function initializePasswordField() {
  $("#password")
    .dxTextBox({
      value: "",
      mode: "password",
      placeholder: "Password",
      showClearButton: true,
    })
    .dxValidator({
      validationRules: [
        { type: "required", message: "Password is required" },
        {
          type: "stringLength",
          min: 8,
          message: "Password must be at least 8 characters long",
        },
      ],
    });
}

/**
 * Initializes the submit button for login authentication.
 * @calledFrom Page load event.
 */
function initializeSubmitButton() {
  $("#submit-button").dxButton({
    text: "Login",
    type: "default",
    useSubmitBehavior: false,
    onClick: handleLogin,
  });
}

/**
 * Handles the login process.
 * Validates the input fields and sends an authentication request.
 */
function handleLogin() {
  var validationGroup = DevExpress.validationEngine.validateGroup();
  if (!validationGroup.isValid) {
    return;
  }

  let dataObj = collectInputValues();
  let api = "/api/LoginPanel/api/auth/login";

  sendLoginRequest(api, dataObj);
}

/**
 * Collects input values from username and password fields.
 * @returns {Object} - An object containing username and password.
 */
function collectInputValues() {
  return {
    UserName: $("#userName").dxTextBox("instance").option("value"),
    Password: $("#password").dxTextBox("instance").option("value"),
  };
}

/**
 * Sends a login request to the server.
 * @param {string} api - The API endpoint for authentication.
 * @param {Object} dataObj - The login credentials (username and password).
 */
function sendLoginRequest(api, dataObj) {
  apiHandler
    .request(api, "POST", dataObj)
    .done(handleLoginSuccess)
    .fail(handleLoginError);
}

/**
 * Handles the success response of the login request.
 * @param {Object} res - The response object from the API.
 */
function handleLoginSuccess(res) {
  alert("User login successfully!");
  console.log(res);
  localStorage.setItem("uid", res.ID);
  localStorage.setItem("utoken", res.Token);
  localStorage.setItem("urole", res.Role);
  localStorage.setItem("uname", res.UserName);
  localStorage.setItem("upass", res.Password);

  window.location.href = "../../pages/home/home.html";
}

/**
 * Handles the error response of the login request.
 * @param {Object} xhr - The error object returned from the API.
 */
function handleLoginError(xhr) {
  alert("Error: " + xhr.responseText);
}
