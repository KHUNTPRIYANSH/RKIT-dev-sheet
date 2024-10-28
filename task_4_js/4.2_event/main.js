//! code for box 1
const box1 = document.getElementById("box1");
const inputField = document.getElementById("field");

box1.addEventListener("mouseover", () => {
  console.log("Box 1 :  mouse over");
});
box1.addEventListener("mouseout", () => {
  console.log("Box 1 :  mouse out");
});
box1.addEventListener("mousedown", () => {
  console.log("Box 1 :  mouse down");
});
box1.addEventListener("mouseup", () => {
  console.log("Box 1 :  mouse up");
});
box1.addEventListener("mousemove", (e) => {
  let x = e.clientX;
  let y = e.clientY;
  // console.log("x: " + x);
  // console.log("y: " + y);
});

//!  code for box having form field
const keyInformation = document.getElementById("field");
keyInformation.addEventListener("keypress", (e) => {
  // e.ctrlKey
  // e.shiftKey
  // e.key == "F2"
  console.log("Key pressed !!!", e.key);
});
const onKeyUpEvent = () => {
  console.log("Key up !!!");
};
// document.getElementById('field').addEventListener("keydown", () => {
//     console.log("Key down !!!")
// })

//! code for box 4

let child = document.getElementById("box4");
let container = document.getElementById("container");

//+ concept of bubbling ::false
//? event will be triggered on top most child in z index
//? here box has some event and its working and their may be case in which container also may have event and body can also have so event will applied on top most element in z index

// child.addEventListener("click", () => {
//   console.log("Child box clicked");
// });
// container.addEventListener("click", () => {
//   console.log("Parent container clicked");
// });

//+ capture concept ::true
//? at first parent tag get event than its only direct child get event and than so on

child.addEventListener("click", () => {
  console.log("Child box clicked");
});
container.addEventListener(
  "click",
  () => {
    console.log("Parent container clicked");
  },
  true
);
