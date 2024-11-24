unction applyMixins(derivedClass, baseClasses) {
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