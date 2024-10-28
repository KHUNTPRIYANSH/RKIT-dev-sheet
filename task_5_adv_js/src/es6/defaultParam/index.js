let f1 = function (x, y) {
  console.log("f1 [x] : ", x);
  console.log("f1 [x] : ", y);
  console.log();
};
let f2 = function (x = 0, y) {
  console.log("f2 [x] : ", x);
  console.log("f2 [x] : ", y);
  console.log();
};

console.log("case 1 : function [f 1] without param");
f1();
console.log("case 2 : function [f 1] with 1 param");
f1(5);
console.log("case 3 : function [f 1] with 2 param");
f1(5, 10);
console.log("case 4 : function [f 2] without param");
f2();
console.log("case 5 : function [f 2] with 1 param");
f2(5);
console.log("case 6 : function [f 2] with 2 param");
f2(5, 10);
