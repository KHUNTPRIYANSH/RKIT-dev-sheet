//-============================================================================================================================
//+ there are three main type of selectors in jQ
//-============================================================================================================================
$("button").click(function(){
    console.clear();
})
//! 1. element selector
//* $("selector").action();
$("h1").click(function () {
  console.log("element clicked by element selector: ", this);
}).css("color","crimson");

//! 2. class selector
//* $(".class-selector").action();
$(".className").click(function () {
  console.log("element clicked by class name selector: ", this);
});

//! 3. id selector
//* $("#id-selector").action();
$("#idName").click(function () {
  console.log("element clicked by id selector: ", this);
});

//! 4. universal selector
//* $("*").action();
$("*").css(
    "cursor" , "pointer"
)
//! 5. attribute with specific name selector
//* $("attribute.class-name").action();
$("div.child-1").click(function () {
  console.log("element clicked by element having specific class : ", this);
}).css("color","gold");

//* $("attribute#id-name").action();
$("div#child2").click(function () {
  console.log("element clicked by element having specific id: ", this);
}).css("color","lime");

//! 6. last child selector
//* $("attribute:last").action();
$(".child:last").click(function () {
  console.log("element clicked by first child of parent element: ", this);
}).css("color","aqua");

//! 7. multiple elements at once
//* $("element_1" , "element_2" , ..... "element_N").action();
$(".child-3", "#child4").click(function () {
  console.log("element clicked by multiple selector: ", this);
});

//! 8. parent's child selector
//* $("parent child sub-child").action();
$(".parent .child-3").click(function () {
  console.log("element clicked by parent's child: ", this);
});

//! 9. other child selector
//* element:first
//* element:last
//* element:even
//* element:odd
//* element:eq(number)
//* element:gt(number)
//* element:lt(number)

//! to clear console

// %============================================================================================================================
//! storing a element in variable

//* let variableA = document.getElementById('myID');
let variableA = $("#child1").html()
console.log("Logging variableA : ", variableA);

// %============================================================================================================================
// %============================================================================================================================
