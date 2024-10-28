// ++++++++++++++++++++++++++++++++++++++++++++++++++++++
// ! Cookies in JavaScript: A Detailed Guide
// ------------------------------------------------------
// * Cookies are small pieces of data stored by the browser, typically
// * used for tracking sessions, storing user preferences, and managing
// * login states across sessions.
// *
// * Cookies have expiration dates, path restrictions, and can be
// * secured with the `Secure` and `HttpOnly` flags to enhance security.
// *
// * Use Cases: Authentication tokens, user preferences, temporary data.
// ++++++++++++++++++++++++++++++++++++++++++++++++++++++

// ! 1. Setting a Cookie
// * This function sets a cookie with a given name, value, and optional settings like expiration.
function setCookie(name, value, days = 7, path = "/") {
  const date = new Date();
  date.setTime(date.getTime() + days * 24 * 60 * 60 * 1000); // Calculate expiration date
  const expires = `expires=${date.toUTCString()}`; // Set cookie expiration
  document.cookie = `${name}=${encodeURIComponent(
    value
  )}; ${expires}; path=${path}`;
  // * Path determines where the cookie is accessible. Default is root (/), so it’s accessible site-wide.
  // * encodeURIComponent helps prevent issues with special characters in the cookie value.
}

// Example usage
const cookieName = prompt("Enter cookie name to create cookie : ");
const cookieValue = prompt("Enter value of cookie : ");
const cookieLifeInDays = prompt("Enter expiry in number of days : ");
setCookie(cookieName, cookieValue, cookieLifeInDays);

// ! 2. Getting a Cookie
// * This function retrieves the value of a specified cookie by its name.
function getCookie(name) {
  const nameEQ = `${name}=`;
  const cookiesArray = document.cookie.split(";"); // Splits all cookies into an array
  for (let i = 0; i < cookiesArray.length; i++) {
    let cookie = cookiesArray[i].trim(); // Removes extra whitespace
    if (cookie.startsWith(nameEQ)) {
      return decodeURIComponent(cookie.substring(nameEQ.length)); // Decodes and returns the cookie value
    }
  }
  return null; // Returns null if cookie is not found
}

// Example usage
console.log("Username Cookie:", getCookie("username")); // Logs the value of 'username' cookie if it exists

// ! 3. Deleting a Cookie
// * This function deletes a cookie by setting its expiration date to a past date.
function deleteCookie(name, path = "/") {
  document.cookie = `${name}=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=${path}`;
  // * By setting the expiration date to the past, the browser automatically removes the cookie.
}

// Example usage
// deleteCookie("username"); // Deletes the 'username' cookie

// ++++++++++++++++++++++++++++++++++++++++++++++++++++++
// ! Advanced Cookie Techniques
// ------------------------------------------------------

// ! 4. Updating a Cookie
// * To update a cookie, simply set a new value with the same name.
setCookie("username", "JaneDoeUpdated", 7); // Updates the 'username' cookie to a new value

// ++++++++++++++++++++++++++++++++++++++++++++++++++++++
// ! Setting Cookies with Secure and HttpOnly Attributes
// ------------------------------------------------------
// * Secure cookies are only sent over HTTPS connections.
// * HttpOnly cookies are inaccessible to JavaScript (set by the server).

// ! 6. Setting Secure Cookies (Only over HTTPS)
// * This function sets a secure cookie, usable only in HTTPS.
// * To test `Secure` cookies, ensure your site is served over HTTPS.
function setSecureCookie(name, value, days = 7, path = "/") {
  const date = new Date();
  date.setTime(date.getTime() + days * 24 * 60 * 60 * 1000);
  const expires = `expires=${date.toUTCString()}`;
  document.cookie = `${name}=${encodeURIComponent(
    value
  )}; ${expires}; path=${path}; Secure`;
}

// Example usage (only works over HTTPS)
setSecureCookie("secureUser", "JaneSecure", 7);

// ++++++++++++++++++++++++++++++++++++++++++++++++++++++
// ! Cookie Settings Summary
// ------------------------------------------------------
// * `name=value`: Key-value pair for the cookie.
// * `expires=Date`: Expiration date; required to persist cookie beyond session.
// * `path=/`: Defines where the cookie is accessible (default is root).
// * `Secure`: Limits the cookie to HTTPS-only contexts (optional).
// * `HttpOnly`: Prevents JavaScript access, enhancing security (server-side only).
// ++++++++++++++++++++++++++++++++++++++++++++++++++++++

// Example Recap
// Setting, retrieving, updating, and deleting cookies in JavaScript:

// 1. Set a basic cookie
setCookie("userId", "12345", 5); // Cookie expires in 5 days

// 2. Get and log the cookie value
console.log("User ID Cookie:", getCookie("userId"));

// 3. Update the cookie
setCookie("userId", "67890", 5); // New value with same name

// 4. Delete the cookie
deleteCookie("userId");

// ! limitations of cookies

// ! size
// + 4kb per cookie

// ! security
// + Cookies are not inherently secure; they are stored as plain text

// ! Limited Lifetime (Expires/Max-Age)
// + Cookies have a default session-based lifespan, meaning they are deleted once the browser is closed unless explicitly set to persist
