// Class definition
class Circle {
  constructor(radius) {
    this.radius = radius;
  }

  area() {
    return Math.PI * this.radius ** 2;
  }
}

class Rectangle {
  constructor(length, width) {
    this.length = length;
    this.width = width;
  }

  area() {
    return this.length * this.width;
  }
}

// Function definition
function calculateSquareArea(side) {
  return side ** 2;
}

// Variable definition
const pi = Math.PI;

// Object definition
const shapeConstants = {
  PI: Math.PI,
  E: Math.E,
};
var checkScope = "module/mod";
// Exporting classes, functions, variables, and objects
export { Circle, Rectangle, calculateSquareArea, pi, shapeConstants };
