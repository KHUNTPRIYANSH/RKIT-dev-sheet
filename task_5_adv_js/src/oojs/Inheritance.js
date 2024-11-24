// ! 1. Single Inheritance
// ---------------------
// + In Single Inheritance, one class (Dog) inherits from another class (Animal).

class Animal {
  constructor(name) {
    this.name = name;
  }
  makeSound() {
    console.log("Some generic animal sound");
  }
}

class Dog extends Animal {
  constructor(name, breed) {
    super(name); // Calls the parent class (Animal) constructor
    this.breed = breed;
  }
  makeSound() {
    console.log("Woof! Woof!");
  }
}

const myDog = new Dog("Buddy", "Golden Retriever");
myDog.makeSound(); // Output: "Woof! Woof!"
console.log(myDog.name); // Output: "Buddy"

// ! 2. Multilevel Inheritance
// -------------------------
// + In Multilevel Inheritance, a class (GermanShepherd) inherits from another derived class (Dog).

class GermanShepherd extends Dog {
  constructor(name, breed, age) {
    super(name, breed); // Calls the Dog class constructor
    this.age = age;
  }
  guard() {
    console.log(`${this.name} is guarding!`);
  }
}

const myGSD = new GermanShepherd("Max", "German Shepherd", 5);
myGSD.makeSound(); // Output: "Woof! Woof!" (from Dog class)
myGSD.guard(); // Output: "Max is guarding!"

console.log();
console.log();
console.log();
// ! 3. Multiple Inheritance using Mixins
// ------------------------------------
// + JavaScript doesn’t support multiple inheritance directly, but we can use mixins to add additional functionality.

class A {
  constructor() {
    this.propertyA = "Property from A";
  }
  methodA() {
    console.log("Method from A");
  }
}

class B {
  constructor() {
    this.propertyB = "Property from B";
  }
  methodB() {
    console.log("Method from B");
  }
}

// Class C that combines properties and methods from A and B
class C {
  constructor() {
    Object.assign(this, new A(), new B());
  }
  // Explicitly assign methods to ensure they are available
  methodA() {
    return A.prototype.methodA.call(this);
  }

  methodB() {
    return B.prototype.methodB.call(this);
  }
}

// Usage
const cObj = new C();

console.log(cObj.propertyA); // Property from A
console.log(cObj.propertyB); // Property from B
cObj.methodA(); // Method from A
cObj.methodB(); // Method from B

console.log();
console.log();
console.log();

// ! other example with mixin
// Define base classes
class Animalll {
  speak() {
    console.log("Animal speaks");
  }
}

class Swimmer {
  swim() {
    console.log("Swimming");
  }
}

// ! Mixin function to apply multiple inheritance
// +Base Classes
// -Swimmer
// -Flyer
// +Derived Class
// -Dolphin
function applyMixins(derivedClass, baseClasses) {
  // Loop through each base class provided in the baseClasses array
  baseClasses.forEach((baseClass) => {
    // Get all method names from the base class prototype
    const methodNames = Object.getOwnPropertyNames(baseClass.prototype);

    // Loop through each method name
    methodNames.forEach((name) => {
      // Copy the method from the base class to the derived class
      derivedClass.prototype[name] = baseClass.prototype[name];
    });
  });
}

// Define Dolphin class that extends Animal
class Dolphin extends Animalll {}

// Apply mixins to add Swimmer's methods to Dolphin
applyMixins(Dolphin, [Swimmer]);

// Create an instance of Dolphin
const machli = new Dolphin();
machli.speak(); // Output: Animal speaks
machli.swim(); // Output: Swimming
