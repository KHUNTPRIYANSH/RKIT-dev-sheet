// ++++++++++++++++++++++++++++++++++++++++++++++++++++++++
// ! Object-Oriented Programming (OOP) in JavaScript
// --------------------------------------------------------
// * JavaScript supports OOP concepts including Classes, Objects,
// * Inheritance, Encapsulation, Abstraction, and Polymorphism.
// *
// * Classes define blueprints, Objects are instances, and Inheritance
// * allows sharing behaviors. Encapsulation hides details, Abstraction
// * simplifies interactions, and Polymorphism allows flexible method usage.
// ++++++++++++++++++++++++++++++++++++++++++++++++++++++++

// ++++++++++++++++++++++++++++++++++++++++++++++++++++++++
// ! Defining a Class (Blueprint) - Animal
// --------------------------------------------------------
// * A class is a blueprint for creating objects. It contains properties
// * (variables) and methods (functions) shared by all instances.
// ++++++++++++++++++++++++++++++++++++++++++++++++++++++++

// ! Constructor method to initialize properties
// * The constructor is called automatically when a new object is created.

class Animal {
  constructor(name, age) {
    this.name = name; // * Name of the animal (property)
    this.age = age; // * Age of the animal (property)
  }

  displayInfo() {
    console.log(`Name: ${this.name}, Age: ${this.age}`);
  }
}

// - Method to display animal information (Abstraction)
// * Abstraction allows the user to interact without needing
// * to know the internal details.

// ++++++++++++++++++++++++++++++++++++++++++++++++++++++++
// ! Creating an Object - Example of Animal
// --------------------------------------------------------
// * An object is an instance of a class. We create an object
// * using the `new` keyword with the class.
// ++++++++++++++++++++++++++++++++++++++++++++++++++++++++

const animal1 = new Animal("Lion", 5);
animal1.displayInfo(); // Output: Name: Lion, Age: 5

// ++++++++++++++++++++++++++++++++++++++++++++++++++++++++
// ! Inheritance - Extending a Class
// --------------------------------------------------------
// * Inheritance allows creating a subclass that inherits properties
// * and methods from a parent class. This is useful for code reuse.
// ++++++++++++++++++++++++++++++++++++++++++++++++++++++++

// * Use `super` to call the constructor of the parent class
// ! Overriding Method (Polymorphism)
// * Polymorphism allows a method in a subclass to have a different
// * implementation than the parent class.

class Dog extends Animal {
  constructor(name, age, breed) {
    super(name, age);
    this.breed = breed; // * Additional property for the subclass
  }

  displayInfo() {
    console.log(`Name: ${this.name}, Age: ${this.age}, Breed: ${this.breed}`);
  }

  // ! New Method specific to Dog class
  bark() {
    console.log(`${this.name} says Woof!`);
  }
}

// Creating a Dog Object
const dog1 = new Dog("Buddy", 3, "Golden Retriever");
dog1.displayInfo(); // Output: Name: Buddy, Age: 3, Breed: Golden Retriever
dog1.bark(); // Output: Buddy says Woof!

// ++++++++++++++++++++++++++++++++++++++++++++++++++++++++
// ! Encapsulation - Private Fields and Methods
// --------------------------------------------------------
// * Encapsulation hides the internal details of a class from
// * external access, creating a more secure and robust structure.
// * Private fields (marked with #) cannot be accessed outside the class.
// ++++++++++++++++++++++++++++++++++++++++++++++++++++++++

class Bird extends Animal {
  #wingSpan; // * Private field - only accessible within this class

  constructor(name, age, wingSpan) {
    super(name, age);
    this.#wingSpan = wingSpan;
  }

  // Public method to safely access private data
  getWingSpan() {
    return `${this.name}'s wingspan is ${this.#wingSpan} cm`;
  }
}

// Creating a Bird Object
const bird1 = new Bird("Eagle", 4, 200);
console.log(bird1.getWingSpan()); // Output: Eagle's wingspan is 200 cm

// - Trying to access private field directly will result in an error
// console.log(bird1.#wingSpan); // ! Error: Private field '#wingSpan' must be declared in an enclosing class

// ++++++++++++++++++++++++++++++++++++++++++++++++++++++++
// ! Static Properties and Methods
// --------------------------------------------------------
// * Static properties and methods belong to the class itself, not
// * to any specific instance. They are accessed directly from the class.
// ++++++++++++++++++++++++++++++++++++++++++++++++++++++++

// - Static method for addition
// - Static property

class Calculator {
  static add(a, b) {
    return a + b;
  }

  static description = "Basic Calculator Class";
}

// Using static method and property without creating an instance
console.log(Calculator.description); // Output: Basic Calculator Class
console.log(Calculator.add(10, 5)); // Output: 15

// ++++++++++++++++++++++++++++++++++++++++++++++++++++++++
// ! Abstraction - Simplifying Complex Systems
// --------------------------------------------------------
// * Abstraction is the concept of hiding complex implementation details
// * from the user, exposing only essential features.
// * Example: A class to represent a Car's functions.
// ++++++++++++++++++++++++++++++++++++++++++++++++++++++++

class Car {
  constructor(make, model) {
    this.make = make;
    this.model = model;
  }

  startEngine() {
    console.log("Starting engine..."); // Complex logic hidden
    this.#fuelCheck(); // Private method used internally
  }

  // Private method for internal use only
  #fuelCheck() {
    console.log("Fuel level checked. Engine started successfully.");
  }
}

// Creating a Car Object
const myCar = new Car("Toyota", "Supra");
const myCar2 = new Car("Koenigsegg", "Jesko");
// myCar.fuelCheck(); // gives error
myCar2.startEngine(); // Only public method is accessible

// ++++++++++++++++++++++++++++++++++++++++++++++++++++++++
// ! Polymorphism in Action
// --------------------------------------------------------
// * Polymorphism allows the same method to behave differently
// * based on which class is calling it.
// * Here, displayInfo() behaves differently in each class.
// ++++++++++++++++++++++++++++++++++++++++++++++++++++++++

// Base class instance
const animal = new Animal("Elephant", 10);
animal.displayInfo(); // Uses Animal's displayInfo method

// Dog (inherited) class instance
const dog = new Dog("Max", 5, "Bulldog");
dog.displayInfo(); // Uses Dog's overridden displayInfo method
