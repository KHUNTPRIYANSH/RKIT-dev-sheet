// ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
// ! index.js: Importing and using functions, constants, and classes from mathUtils.js
// ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

// Importing named exports
import { add, subtract, PI, Calculator } from "./mathUtils.js";

// Importing the default export
import greet from "./mathUtils.js";

// Using the imported functions and constants
console.log(greet()); // Output: Hello from mathUtils!

console.log("Add:", add(5, 3)); // Output: Add: 8
console.log("Subtract:", subtract(10, 4)); // Output: Subtract: 6
console.log("Value of PI:", PI); // Output: Value of PI: 3.14159

// Using the imported class
const calculator = new Calculator();
console.log("Multiply:", calculator.multiply(6, 7)); // Output: Multiply: 42
console.log("Divide:", calculator.divide(10, 2)); // Output: Divide: 5
