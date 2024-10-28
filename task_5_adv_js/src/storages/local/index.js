// + ++++++++++++++++++++++++++++++++++++++++++
// ! Local Storage
// - ------------------------------------
// * Local Storage allows storing key-value pairs in the web browser.
// * It persists even after the browser or tab is closed, making it ideal
// * for saving user preferences and state.
// *
// * Data is stored as strings, so all values (objects, arrays, etc.)
// * must be serialized (converted to JSON format) before storage.
// *
// * Use Cases: Persisting user settings, caching data, storing temporary states.
// + +++++++++++++++++++++++++++++++++++++++++

const userKey = prompt("Give me key name");
const userValue = prompt("value: ");
localStorage.setItem(userKey, userValue);

// ! Basic Methods of Local Storage

// ! 1. Storing an item (key-value pair)
localStorage.setItem("username", "JaneDoe"); // Stores a simple string value with the key 'username'

// ! 2. Retrieving an item
const user = localStorage.getItem("username"); // Retrieves the value for the key 'username'
console.log("Username: ", user); // Logs "Username: JaneDoe"

// ! 3. Removing an item
// localStorage.removeItem("username"); // Removes the item with the key 'username'

// ! 4. Clearing all items in Local Storage
// localStorage.clear(); // Clears all stored data in Local Storage
// ! 5. length function in Local Storage
console.log("Length of local storage : " + localStorage.length);
// ! 6. key function in Local Storage
console.log(
  "At index 1 item's key in local storage is : " + localStorage.key(0)
);
// + +++++++++++++++++++++++++++++++++++++++++
// !  Advanced Methods & Techniques
// *  -------------------------------------
// *  Using JSON to store and retrieve complex data structures
// *  like objects and arrays, which must be serialized and parsed.
// + +++++++++++++++++++++++++++++++++++++++++

// - Storing complex data (objects, arrays) in Local Storage

const userProfile = {
  name: "Jane Doe",
  age: 30,
  preferences: {
    theme: "dark",
    language: "English",
  },
};

// - Store as JSON string (serialization)

localStorage.setItem("userProfile", JSON.stringify(userProfile)); // Convert object to string and store

// - Retrieve and parse back to object
const storedProfile = JSON.parse(localStorage.getItem("userProfile")); // Convert string back to object
console.log("Stored Profile:", storedProfile);

// + +++++++++++++++++++++++++++++++++++++++++
// * Checking for Local Storage Support
// - ------------------------------------
// * Ensures the browser supports Local Storage before using it.
// + +++++++++++++++++++++++++++++++++++++++++

if (typeof localStorage !== "undefined") {
  console.log("Local Storage is supported");
} else {
  console.warn("Local Storage is not supported in this browser");
}
