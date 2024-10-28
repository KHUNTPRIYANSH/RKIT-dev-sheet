let arr = [1, 2, 3, 4, 5, 6, 7, 8];

// ! i want to make new array it includes all the elements of arr with some extras

console.log("Arr : ", arr);

// + following is not copy its ref
// let arr2 = arr;
// + following is copy
let arr2 = [...arr, 9, 10];

console.log("Arr2 : ", arr2);

// - spread means to put all the values of the perticular element where it used
// - in above ex it will put all the values belong to arr in the arr2 and we have also added 9 and 10 along with those values from arr1 into arr2

console.log();
console.log();
// ! i want to handle all the remaining values in the function param
let f1 = (a, b, c) => {
  console.log("first  param's value : ", a);
  console.log("second param's value : ", b);
  console.log("third  param's value : ", c);
};
f1(1, 2, 3, 4, 5, 6, 7);
console.log();
let f2 = (a, b, c, ...d) => {
  console.log("first  param's value : ", a);
  console.log("second param's value : ", b);
  console.log("third  param's value : ", c);
  console.log("rest values : ", d);
};
f2(1, 2, 3, 4, 5, 6, 7);
