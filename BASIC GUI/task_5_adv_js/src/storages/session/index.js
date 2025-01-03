// + ++++++++++++++++++++++++++++++++++++++++++
// ! Session Storage
// - ------------------------------------
// * Session Storage allows storing key-value pairs in the web browser.
// * Unlike Local Storage, Session Storage data is only available for
// * the duration of the page session and is cleared when the tab or
// * browser is closed.
// *
// * Data is stored as strings, so all values (objects, arrays, etc.)
// * must be serialized (converted to JSON format) before storage.
// *
// * Use Cases: Temporary data for a single session, like form inputs
// * or temporary state management within a session.
// + +++++++++++++++++++++++++++++++++++++++++

// ! Basic Methods of Session Storage

// ! 1. Storing an item (key-value pair)
sessionStorage.setItem("sessionUsername", "JaneDoe"); // Stores a simple string value with the key 'sessionUsername'

// ! 2. Retrieving an item
const sessionUser = sessionStorage.getItem("sessionUsername"); // Retrieves the value for the key 'sessionUsername'
console.log("Session Username:", sessionUser); // Logs "Session Username: JaneDoe"

// ! 3. Removing an item
// sessionStorage.removeItem("sessionUsername"); // Removes the item with the key 'sessionUsername'

// ! 4. Clearing all items in Session Storage
// sessionStorage.clear(); // Clears all stored data in Session Storage

// ! 5. length function in Session Storage
console.log("Length of session storage:", sessionStorage.length); // Logs the number of items in Session Storage

// ! 6. key function in Session Storage
console.log(
  "At index 0, item's key in session storage is:",
  sessionStorage.key(0)
); // Logs the key at index 0 in Session Storage

// + +++++++++++++++++++++++++++++++++++++++++
// ! Advanced Methods & Techniques
// * -------------------------------------
// * Using JSON to store and retrieve complex data structures
// * like objects and arrays, which must be serialized and parsed.
// + +++++++++++++++++++++++++++++++++++++++++

// - Storing complex data (objects, arrays) in Session Storage

const sessionUserProfile = {
  name: "Jane Doe",
  age: 30,
  preferences: {
    theme: "dark",
    language: "English",
  },
};

// - Store as JSON string (serialization)
sessionStorage.setItem(
  "sessionUserProfile",
  JSON.stringify(sessionUserProfile)
); // Convert object to string and store

// - Retrieve and parse back to object
const storedSessionProfile = JSON.parse(
  sessionStorage.getItem("sessionUserProfile")
); // Convert string back to object
console.log("Stored Session Profile:", storedSessionProfile); // Logs the parsed object

// + +++++++++++++++++++++++++++++++++++++++++
// * Checking for Session Storage Support
// - ------------------------------------
// * Ensures the browser supports Session Storage before using it.
// + +++++++++++++++++++++++++++++++++++++++++

if (typeof sessionStorage !== "undefined") {
  console.log("Session Storage is supported");
} else {
  console.warn("Session Storage is not supported in this browser");
}
