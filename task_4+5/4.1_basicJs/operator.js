//! 1. Arithmetic Operators

console.log("1. Arithmetic Operators");

let num1 = 10;
let num2 = 5;

//+  code written between `` called template in js can we can use value of variable with ${variable name} so no need of concat

console.log(`num1 = ${num1}, num2 = ${num2}`);
console.log(`Addition (num1 + num2): ${num1 + num2}`); // 15
console.log(`Subtraction (num1 - num2): ${num1 - num2}`); // 5
console.log(`Multiplication (num1 * num2): ${num1 * num2}`); // 50
console.log(`Division (num1 / num2): ${num1 / num2}`); // 2
console.log(`Modulus (num1 % num2): ${num1 % num2}`); // 0

// ?Increment and Decrement
let count = 0;
console.log(`\nInitial count: ${count}`);

count++; // Increment by 1
console.log(`After count++: ${count}`); // 1

count--; // Decrement by 1
console.log(`After count--: ${count}`); // 0

console.log("\n----------------------------------------\n");

//! 2. Assignment Operators

console.log("2. Assignment Operators");

let a = 20;
console.log(`Initial a = ${a}`);

a += 10; // a = a + 10
console.log(`After a += 10: ${a}`); // 30

a -= 5; // a = a - 5
console.log(`After a -= 5: ${a}`); // 25

a *= 2; // a = a * 2
console.log(`After a *= 2: ${a}`); // 50

a /= 5; // a = a / 5
console.log(`After a /= 5: ${a}`); // 10

a %= 3; // a = a % 3
console.log(`After a %= 3: ${a}`); // 1

console.log("\n----------------------------------------\n");

//! 3. Comparison Operators

console.log("3. Comparison Operators");

let x = 10;
let y = "10";
let z = 15;

console.log(`x = ${x}, y = '${y}', z = ${z}`);

console.log(`x == y: ${x == y}`); // true (loose equality, compares value)
console.log(`x === y: ${x === y}`); // false (strict equality, compares value and type)
console.log(`x != z: ${x != z}`); // true
console.log(`x !== y: ${x !== y}`); // true

console.log(`x > z: ${x > z}`); // false
console.log(`x < z: ${x < z}`); // true
console.log(`x >= y: ${x >= y}`); // true
console.log(`x <= y: ${x <= y}`); // true

console.log("\n----------------------------------------\n");

//! 4. Logical Operators

console.log("4. Logical Operators");

let isSunny = true;
let hasUmbrella = false;

console.log(`isSunny = ${isSunny}, hasUmbrella = ${hasUmbrella}`);

//+ Logical AND (&&)
console.log(`isSunny && hasUmbrella: ${isSunny && hasUmbrella}`); // false

//+ Logical OR (||)
console.log(`isSunny || hasUmbrella: ${isSunny || hasUmbrella}`); // true

//+ Logical NOT (!)
console.log(`!isSunny: ${!isSunny}`); // false

console.log("\n----------------------------------------\n");

// ! 5. String Operators

console.log("5. String Operators");

let firstName = "Jane";
let lastName = "Doe";

console.log(`firstName = '${firstName}', lastName = '${lastName}'`);

// ?Concatenation using +
let fullName = firstName + " " + lastName;
console.log(`Full Name (firstName + ' ' + lastName): '${fullName}'`);

// ?Concatenation using +=
let greeting = "Hello";
greeting += ", " + fullName + "!";
console.log(`Greeting after +=: '${greeting}'`); // 'Hello, Jane Doe!'

console.log("\n----------------------------------------\n");

//! 6. Ternary Operator

console.log("6. Ternary Operator");

let age = 18;

// ?Using ternary to decide voting eligibility
let canVote = age >= 18 ? "Yes" : "No";
console.log(`Age = ${age}, Can Vote? ${canVote}`); // 'Yes'

// ?Another example by chaining of ternary conditions
let score = 75;
let grade =
  score >= 90
    ? "A"
    : score >= 80
    ? "B"
    : score >= 70
    ? "C"
    : score >= 60
    ? "D"
    : "F";
console.log(`Score = ${score}, Grade = ${grade}`); // 'C'

console.log("\n--- End of Demonstration ---");
