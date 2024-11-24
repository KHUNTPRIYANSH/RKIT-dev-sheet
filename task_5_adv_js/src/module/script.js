// Importing the exported classes, functions, and variables
import {
  Circle,
  Rectangle,
  calculateSquareArea,
  pi,
  shapeConstants,
} from "./mod.js";

// Using the imported classes
const myCircle = new Circle(5);
console.log(`Area of Circle: ${myCircle.area()}`); // Output: Area of Circle: 78.53981633974483

const myRectangle = new Rectangle(4, 5);
console.log(`Area of Rectangle: ${myRectangle.area()}`); // Output: Area of Rectangle: 20

// Using the imported function
console.log(`Area of Square: ${calculateSquareArea(4)}`); // Output: Area of Square: 16

// Using the imported variable
console.log(`Value of PI: ${pi}`); // Output: Value of PI: 3.141592653589793

// Using the imported object
console.log(`Value of shapeConstants.PI: ${shapeConstants.PI}`); // Output: Value of shapeConstants.PI: 3.141592653589793

// console.log(checkScope);
