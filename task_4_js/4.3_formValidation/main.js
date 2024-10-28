//! Selecting form and input elements

const signupForm = document.getElementById("signupForm");
const fullName = document.getElementById("fullName");
const email = document.getElementById("email");
const password = document.getElementById("password");
const mobileNumber = document.getElementById("mobileNumber");
const terms = document.getElementById("terms");

//! Selecting error message elements
const fullNameError = document.getElementById("fullNameError");
const emailError = document.getElementById("emailError");
const passwordError = document.getElementById("passwordError");
const mobileError = document.getElementById("mobileError");
const termsError = document.getElementById("termsError");
const successMessage = document.getElementById("successMessage");

//! Function to validate Full Name
function validateFullName() {
  const nameValue = fullName.value.trim();
  if (nameValue === "") {
    fullNameError.textContent = "Full Name is required.";
    return false;
  } else {
    fullNameError.textContent = "";
    return true;
  }
}

//! Function to validate Email
function validateEmail() {
  const emailValue = email.value.trim();
  const emailPattern = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
  if (emailValue === "") {
    emailError.textContent = "Email is required.";
    return false;
  } else if (!emailPattern.test(emailValue)) {
    emailError.textContent = "Please enter a valid email address.";
    return false;
  } else {
    emailError.textContent = "";
    return true;
  }
}

//! Function to validate Password
function validatePassword() {
  const passwordValue = password.value;
  if (passwordValue === "") {
    passwordError.textContent = "Password is required.";
    return false;
  } else if (passwordValue.length < 6) {
    passwordError.textContent = "Password must be at least 6 characters long.";
    return false;
  } else {
    passwordError.textContent = "";
    return true;
  }
}

//! Function to validate Mobile Number
function validateMobileNumber() {
  const mobileValue = mobileNumber.value.trim();
  const mobilePattern = /^[0-9]{10}$/; // Assuming 10-digit mobile numbers
  if (mobileValue === "") {
    mobileError.textContent = "Mobile Number is required.";
    return false;
  } else if (!mobilePattern.test(mobileValue)) {
    mobileError.textContent = "Please enter a valid 10-digit mobile number.";
    return false;
  } else {
    mobileError.textContent = "";
    return true;
  }
}

//! Function to validate Terms and Conditions
function validateTerms() {
  if (!terms.checked) {
    termsError.textContent = "You must agree to the terms and conditions.";
    return false;
  } else {
    termsError.textContent = "";
    return true;
  }
}

//! Adding event listeners for real-time validation
fullName.addEventListener("input", validateFullName);
email.addEventListener("input", validateEmail);
password.addEventListener("input", validatePassword);
mobileNumber.addEventListener("input", validateMobileNumber);
terms.addEventListener("change", validateTerms);

//! Form submission event
signupForm.addEventListener("submit", function (event) {
  event.preventDefault(); // Prevent form from submitting

  //! Validate all fields
  const isFullNameValid = validateFullName();
  const isEmailValid = validateEmail();
  const isPasswordValid = validatePassword();
  const isMobileValid = validateMobileNumber();
  const isTermsValid = validateTerms();

  //! If all validations pass
  if (
    isFullNameValid &&
    isEmailValid &&
    isPasswordValid &&
    isMobileValid &&
    isTermsValid
  ) {
    alert("Success");
  }
});
