// ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
// ! mathUtils.js: Module for basic math operations
// ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

// Exporting a named function
export function add(a, b) {
  return a + b;
}

// Exporting a named function
export function subtract(a, b) {
  return a - b;
}

// Exporting a constant variable
export const PI = 3.14159;

// Exporting a class
export class Calculator {
  multiply(a, b) {
    return a * b;
  }

  divide(a, b) {
    if (b === 0) {
      return "Cannot divide by zero!";
    }
    return a / b;
  }
}

// Exporting a default function
// Only one `default export` is allowed per module
export default function greet() {
  return "Hello from mathUtils!";
}
