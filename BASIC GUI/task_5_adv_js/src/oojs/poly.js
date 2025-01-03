// overriding

// Base class
class Animal {
  speak() {
    console.log("The animal makes a sound");
  }
}

// Derived class: Dog
class Dog extends Animal {
  speak() {
    console.log("The dog barks");
  }
}

// Derived class: Cat
class Cat extends Animal {
  speak() {
    console.log("The cat meows");
  }
}

// Create instances of Dog and Cat
const myDog = new Dog();
const myCat = new Cat();

// Call the function with different animal instances
myDog.speak();
myCat.speak();

class TempClass {
  sum(a) {
    return a;
  }

  sum(a, b) {
    return a + b;
  }

  sum(a, b = 0) {
    return a + b;
  }

  sum(a, b = 0, ...c) {
    return { sum: a + b, c };
  }
}
const obj = new TempClass();
// console.log(obj.sum(5));
// console.log(obj.sum(5, 100));
console.log(obj.sum(5, 100, 54, 85));
// console.log(obj.sum(5));
