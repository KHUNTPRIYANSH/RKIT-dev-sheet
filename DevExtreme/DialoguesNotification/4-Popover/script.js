$(function () {
  let smallPopover = $("#smallPopover")
    .dxPopover({
      visible: false,
      position: "top",
      showEvent: "mouseenter",
      hideEvent: "mouseleave",
      target: "#area-min",
    })
    .dxPopover("instance");
  // Initialize Normal Popover
  let Popover = $("#PopoverContainer")
    .dxPopover({
      title: "Normal Popover 1",
      width: 300,
      height: 300,
      visible: false, // Initially hidden
      closeOnOutsideClick: true,
      showCloseButton: true,
      showTitle: true,
      position: { my: "center", at: "center", of: "#area" }, // Positioning near #area
      animation: {
        show: { type: "fadeIn", duration: 500 },
        hide: { type: "fadeOut", duration: 500 },
      },
      toolbarItems: [
        {
          toolbar: "bottom",
          location: "after",
          widget: "dxButton",
          options: {
            text: "Close",
            onClick: function () {
              Popover.hide();
            },
          },
        },
      ],

      hoverStateEnabled: true,
      showEvent: "mouseenter",
      hideEvent: "mouseleave",
      target: "#area-min",
    })
    .dxPopover("instance"); // Ensure we get the instance

  // Initialize ScrollView Popover
  let PopoverWithScrollView = $("#PopoverWithScrollView")
    .dxPopover({
      title: "Popover with ScrollView 2",
      width: 300,
      height: 300,
      showTitle: true,

      visible: false,
      closeOnOutsideClick: true,
      showCloseButton: true,
      position: { my: "center", at: "center", of: "#area" },
      contentTemplate: function () {
        let $scrollView = $("<section>");
        $scrollView.append(
          $("<section>").html(`
              <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit.</p>
              <p>Vestibulum et lacus nec justo dignissim hendrerit.</p>
              <p>Curabitur aliquam metus nec magna commodo, eu pharetra risus rhoncus.</p>
              <p>Fusce tincidunt nisl id augue congue, eget tincidunt sapien consequat.</p>
              <p>Proin ut ex nec sapien cursus facilisis.</p>
              <p>Donec vulputate risus ut felis dictum, sed fermentum nisi aliquet.</p>
              <p>Suspendisse potenti. Integer vehicula massa a est ultricies accumsan.</p>
              <p>Aliquam auctor metus ut sem tempus, id ultrices turpis placerat.</p>
            `)
        );

        $scrollView.dxScrollView({
          width: "100%",
          height: "100%",
        });

        return $scrollView;
      },
    })
    .dxPopover("instance");

  // Button to Show Normal Popover
  $("#showPopoverButton").dxButton({
    text: "Show Popover",

    type: "success",
    onClick: function () {
      Popover.show(); // Correctly referencing the instance
    },
  });

  // Button to Show Popover with ScrollView
  $("#showPopoverWithScrollViewButton").dxButton({
    text: "Show Popover with ScrollView",

    type: "danger",
    onClick: function () {
      PopoverWithScrollView.show();
    },
  });
});
