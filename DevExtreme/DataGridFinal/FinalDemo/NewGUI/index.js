$(function () {
  initializeUserNameField();
  initializePasswordField();
  initializeConfirmPasswordField();
  initializeRoleDropdown();
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
 * Initializes the confirm password input field with validation.
 * @calledFrom Page load event.
 */
function initializeConfirmPasswordField() {
  $("#confirm-password")
    .dxTextBox({
      value: "",
      mode: "password",
      placeholder: "Confirm Password",
      showClearButton: true,
    })
    .dxValidator({
      validationRules: [
        { type: "required", message: "Confirm Password is required" },
        {
          type: "compare",
          comparisonTarget: function () {
            return $("#password").dxTextBox("instance").option("value");
          },
          message: "Passwords do not match!",
        },
      ],
    });
}

/**
 * Initializes the role dropdown selection.
 * @calledFrom Page load event.
 */
function initializeRoleDropdown() {
  $("#role").dxSelectBox({
    items: [
      { key: 3, name: "Admin" },
      { key: 2, name: "Editor" },
      { key: 1, name: "User" },
    ],
    valueExpr: "key",
    displayExpr: "name",
  });
}

/**
 * Initializes the submit button for user registration.
 * @calledFrom Page load event.
 */
function initializeSubmitButton() {
  $("#submit-button").dxButton({
    text: "Sign Up",
    type: "default",
    useSubmitBehavior: false,
    onClick: handleSignUp,
  });
}

/**
 * Handles the sign-up process.
 * Validates the input fields and sends a user registration request.
 */
function handleSignUp() {
  var validationGroup = DevExpress.validationEngine.validateGroup();
  if (!validationGroup.isValid) {
    return;
  }

  let dataObj = collectUserInputValues();
  let api = "https://localhost:44310/add_user";
  sendSignUpRequest(api, dataObj);
}

/**
 * Collects input values from the registration form fields.
 * @returns {Object} - An object containing user registration details.
 */
function collectUserInputValues() {
  return {
    R01F01: 0,
    R01F02: $("#userName").dxTextBox("instance").option("value"),
    R01F03: $("#password").dxTextBox("instance").option("value"),
    R01F04: $("#role").dxSelectBox("instance").option("value"),
  };
}

/**
 * Sends a user registration request to the server.
 * @param {string} api - The API endpoint for user registration.
 * @param {Object} dataObj - The user registration details.
 */
function sendSignUpRequest(api, dataObj) {
  $.ajax({
    url: api,
    type: "POST",
    data: JSON.stringify(dataObj),
    contentType: "application/json",
    success: handleSignUpSuccess,
    error: handleSignUpError,
  });
}

/**
 * Handles the success response of the sign-up request.
 * @param {Object} data - The response object from the API.
 */
function handleSignUpSuccess(data) {
  alert("User added successfully!");
  console.log(data);
  let uid = data.Message.split(":")[1];
  localStorage.setItem("uid", uid);
  window.location.href = "/pages/login/login.html";
}

/**
 * Handles the error response of the sign-up request.
 * @param {Object} xhr - The error object returned from the API.
 */
function handleSignUpError(xhr) {
  alert("Error: " + xhr.responseText);
}
