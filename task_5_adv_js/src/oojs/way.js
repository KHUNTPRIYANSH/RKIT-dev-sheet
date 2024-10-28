// +++++++++++++++++++++++++++++++++++++++++++++++++++++++
// ! 1. ES6 `class` Syntax - The Modern, Readable Approach
// +++++++++++++++++++++++++++++++++++++++++++++++++++++++

class AnimalClass {
  constructor(name) {
    this.name = name;
  }

  speak() {
    console.log(`${this.name} makes a noise.`);
  }
}

const animal1 = new AnimalClass("Dog");
animal1.speak(); // Dog makes a noise.

// ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
// ! 2. Function Constructor - The Traditional Approach
// ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

function AnimalConstructor(name) {
  this.name = name;
}

// Adding methods to the prototype
AnimalConstructor.prototype.speak = function () {
  console.log(`${this.name} makes a noise.`);
};

const animal2 = new AnimalConstructor("Cat");
animal2.speak(); // Cat makes a noise.

// +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
// ! 3. Factory Function (Object Literal) - Lightweight Objects
// +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

// * Cons: No inheritance; duplicates methods for each instance, increasing memory use

function createAnimal(name) {
  return {
    name,
    speak() {
      console.log(`${this.name} makes a noise.`);
    },
  };
}

const animal3 = createAnimal("Bird");
animal3.speak(); // Bird makes a noise.

// +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
// ! 4. Object.create() - Direct Prototype-Based Inheritance
// +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

// * Pros: no constructor needed
// * Cons: Can be less readable; lacks `class` structure

const animalPrototype = {
  speak() {
    console.log(`${this.name} makes a noise.`);
  },
};

const animal4 = Object.create(animalPrototype);
animal4.name = "Lion";
animal4.speak(); // Lion makes a noise.

// +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
// ! 5. ES6 Class Inheritance (extends) - OOP-style Inheritance
// +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

class Dog extends AnimalClass {
  speak() {
    console.log(`${this.name} barks.`);
  }
}

const dog = new Dog("Buddy");
dog.speak(); // Buddy barks.

// +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
// ! 6. ES2022 Private Fields (#) - Encapsulation within Classes
// +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

// * Cons: Less compatible with older browsers; `#` syntax may look unusual

class PrivateAnimal {
  #name; // private field

  constructor(name) {
    this.#name = name;
  }

  speak() {
    console.log(`${this.#name} makes a noise.`);
  }
}

const animal5 = new PrivateAnimal("Tiger");
animal5.speak(); // Tiger makes a noise.
// animal5.#name; // Error: Cannot access private field

// +=============================================================================+
// ! Summary of Approaches:                                                      |
// +-----------------------------------------------------------------------------+
// - 1. ES6 `class` Syntax          - Clean, readable, supports inheritance.     |
// - 2. Function Constructor        - Flexible, with prototype support.          |
// - 3. Factory Function            - Lightweight, great for simple objects.     |
// - 4. Object.create()             - Direct prototype inheritance.              |
// - 5. ES6 Class Inheritance       - OOP-style with `extends`.                  |
// - 6. Private Fields (#)          - Encapsulation in ES2022+                   |
// +=============================================================================+
