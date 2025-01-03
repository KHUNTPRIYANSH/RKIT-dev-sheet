// ! mouse events
$("#c-1").click(() => {
  $("#c-1").css({
    background: "gold",
    "border-radius": "50%",
    border: "15px dashed purple",
  });
});
$("#c-2").dblclick(function () {
  $(this).css("background", "purple");
});
$("#c-3").contextmenu(function () {
  $(this).css("background", "tomato");
});
$("#c-4").mouseenter(function () {
  $(this).css("background", "aquamarine");
});
$("#c-5").mouseenter(function () {
  $(this).css("background", "#aaa");
});

// ! keyboard events

$("#k-1").keypress(function () {
  $(this).css("background", "tomato");
});
$("#k-2").keydown(function () {
  $(this).css("background", "gold");
});
$("#k-3").keyup(function () {
  $(this).css("background", "purple");
});

// ! form events

$("#f-1 input").focus(function () {
  $(this).css("background", "tomato");
});
$("#f-2 input").blur(function () {
  $(this).css("background", "gold");
});
$("#f-3 select").change(function () {
  $(this).css("background", "purple");
});
$("#f-4").select(function () {
  $(this).css("background", "#aaa");
});
$("form").submit(function (e) {
  e.preventDefault();
  alert("Form submitted !!!");
  $("#f-5").css("background", "aquamarine");
});

// ! window events
//* .load()
//* .unload()
//* above both event is removed from jQ after version 3

$("#w-1").scroll(function () {
  $(this).css("background", "tomato");
});
$(window).resize(function () {
  $("#w-2").css("background", "gold");
});

// ! jQ get methods
$("#g-1").click(function () {
  let temp = $(this).text();
  console.log("Get method [text] : ", temp);
});
$("#g-2").click(function () {
  let temp = $(this).html();
  console.log("Get method [html] : ", temp);
});
$("#g-3").click(function () {
  let temp = $(this).attr("class");
  console.log("Get method [attr] : ", temp);
});
$("#g-4 input").click(function () {
  let temp = $(this).val();
  console.log("Get method [val] : ", temp);
});

// ! jQ set methods
$("#s-1").click(function () {
  $(this).css("background", "gold");
  $("#s-1 span").text("text coming from jQ");
});
$("#s-2").click(function () {
  $(this).css("background", "purple");
  $("#s-2 span").html("<marquee> text coming from jQ</marquee>");
});
$("#s-3").click(function () {
  $("#s-3 span").attr("class", "red");
});
$("#s-4").click(function () {
  $("#s-4 input").val("you clicked !!!").css("background", "crimson");
});

// ! jQ class methods
$("button.add-btn").click(function () {
  $("#cl-1").addClass("newClass");
});
$("button.remove-btn").click(function () {
  $("#cl-1").removeClass("newClass");
});
$("button.toggle-btn").click(function () {
  $("#cl-1").toggleClass("newClass");
});

// ! jQ on off methods
$("#onOffMethods").on({
  click: function () {
    $("#onf-1").addClass("onClick");
  },
  dblclick: function () {
    $("#onf-1").addClass("ondblClick");
  },
  contextmenu: function () {
    $("#onf-1").addClass("onRightClick");
  },
});
// .off({
//   click: function () {
//     $("#onf-1").addClass("onClick");
//   },
//   dblclick: function () {
//     $("#onf-1").addClass("ondblClick");
//   },
//   contextmenu: function () {
//     $("#onf-1").addClass("onRightClick");
//   },
// })

// ! jQ event trigger
$("#triggerButton").click(function () {
  //! Triggering custom event when the button is clicked
  $("#message").trigger("displayMessage");
  $("#clickEvent").trigger("click");
});

$("#message").on("displayMessage", function () {
  $(this).text("The event has been triggered programmatically!");
});
$("#clickEvent").css("background", "purple");
