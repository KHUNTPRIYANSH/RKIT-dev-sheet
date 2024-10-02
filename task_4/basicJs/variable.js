// ! this is how we write single line comment in js
/*  this is how we write multiline comment in js */

// ? variable in js
// ? variables are identifiers which is used to give name to any data class function or anything
// ? we have three ways to make variable
// *****************************************************************************************************************************
// ? var it is module scoped
var x = 3;
var x = 5;
var y = 6;
var z = x + y;
console.log("z: ", z);

// *****************************************************************************************************************************
// ? let
// ? The let keyword was introduced in ES6 (2015)
// ? Variables declared with let have Block Scope
// ? Variables declared with let must be Declared before use
// ? Variables declared with let cannot be re declared in the same scope
let a = 5;
// let a = 6;
let b = 6;
let c = a + b;
console.log("c: ", c);

// *****************************************************************************************************************************
// ? const
// ? The let keyword was introduced in ES6 (2015)
// ? Variables declared with let have Block Scope
// ? Variables declared with let must be Declared before use
// ? Variables declared with let cannot be re declared in the same scope
// const p;
const p = 5;
const q = 6;
// const r = p + q;
let r = p + q;
console.log("r: ", r);

// *****************************************************************************************************************************

// ? variable name rules
// ? it cant start with number
// ? it cant have wide space
// ? it can start with _ or $ as special char other are not allowed
