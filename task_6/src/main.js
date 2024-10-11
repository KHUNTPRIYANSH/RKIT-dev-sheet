//-============================================================================================================================
//+ Loading jQuery after dom
//-============================================================================================================================

//!    this is old method
//*    $(document).ready(function(){
//*        // this function ensures that code written inside this function will get executed after dom is fully loaded or ready
//*        // your jQuery code
//*         ...
//*         ...
//*         ...
//*    });

//!    this is new method
//*    $(function(){
//*        // this function ensures that code written inside this function will get executed after dom is fully loaded or ready
//*        // your jQuery code
//*         ...
//*         ...
//*         ...
//*    });

//-============================================================================================================================
//+ there are three main type of selectors in jQ
//-============================================================================================================================

//! 1. element selector
//* $("selector").action();

$("p").click(function () {
  console.log("clicked on : ", this);
});

//! 2. class selector
//* $(".class-selector").action();

$(".title").click();

//! 3. id selector
//* $("#id-selector").action();

$("#headLine").click();

//! 4. universal selector
//* $("*").action();

$("*").click();

//! 5. attribute with specific name selector
//* $("attribute.class-name").action();

$("p.title").click();

//* $("attribute#id-name").action();

$("p#headLine").click();

//! 6. first child selector
//* $("attribute:first").action();

$("p:first").click();

// %============================================================================================================================
