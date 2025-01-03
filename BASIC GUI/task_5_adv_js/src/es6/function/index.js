// ! function declaration

function sum(a, b) {
  return a + b;
}

// ! function expression

const mul = function (a, b) {
  return a + b;
};

// ! arrow function expression

const sub = (a, b) => {
  return a - b;
};
// ! arrow function expression with implicit return

const div = a => a ;

// + ===================================================================
// ! Difference between Arrow Function and Normal Function in JavaScript
// + ===================================================================

const xyz = {
  x: 5,
  y: "hello",

  // Regular function (function expression)
  f1: function (x, y) {
    console.log("f1 [x] :", x);
    console.log("f1 [this.x] :", this.x);
    console.log("f1 [y] :", y);
    console.log("f1 [this.y] :", this.y);
    console.log("f1 this:", this); // `this` refers to the function's calling context (the `xyz` function)
  },

  // Arrow function
  f2: (x, y) => {
    console.log();
    console.log();

    console.log("f2 [x] :", x);
    console.log("f2 [this.x] :", this.x);
    console.log("f2 [y] :", y);
    console.log("f2 [this.y] :", this.y);
    console.log("f2 this:", this); // `this` refers to the parent `this` (lexical scoping), which is `xyz`'s scope

    setTimeout(function () {
      console.log("refers to the parent's this : ", this);
    }, 2000);
  },
};

// Call the functions
xyz.f1(9, "morning");
xyz.f2(15, "noon");
console.log();
console.log("Global this : ", this);
