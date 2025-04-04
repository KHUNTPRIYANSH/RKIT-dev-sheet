$(function () {
  // Button to Show Normal Popup
  $("#showPopupButton").dxButton({
    text: "Show Popup",
    type: "success",
    onClick: function () {
      popup.show();
    },
  });

  // Button to Show Popup with ScrollView
  $("#showPopupWithScrollViewButton").dxButton({
    text: "Show Popup with ScrollView",
    type: "danger",
    onClick: function () {
      popupWithScrollView.show();
    },
  });

  let popup = $("#popupContainer")
    .dxPopup({
      title: "Normal Popup", // onlu visible when showtitle is true
      titleTemplate: (e) => {
        console.log("E of title template : ", e);
        let logo = $("<img>")
          .attr(
            "src",
            "https://mticketdemo1.rkitsoftware.com/Theme/img/miracle.png"
          )
          .css({
            height: "30px",
            margin: " 5px",
          });
        return $("<a>")
          .attr({
            href: "https://www.rkitsoftware.com/",
            target: "_blank",
          })
          .append(logo);
      },
      width: 500,
      height: 250,
      visible: false,
      closeOnOutsideClick: true,
      container: "#container", // default "dx-viewport" [drag work only if we use container]
      showCloseButton: true,
      //// doubt in drag
      dragEnabled: true,
      //// resize doubt
      resizeEnabled: true,
      shading: true,
      shadingColor: "rgba(15, 255, 85, 0.6)",
      fullScreen: false,
      showTitle: true,
      animation: {
        show: { type: "fadeIn", duration: 3000 },
        hide: { type: "fadeOut", duration: 3000 },
      },
      toolbarItems: [
        {
          toolbar: "bottom", // top , bottom
          location: "before", // before, after
          widget: "dxButton",
          options: {
            accessKey: "c",
            text: "Close ❌",
            onClick: handlePopupClose,
          },
        },
      ],

      onShowing: function () {
        console.log("Popup is about to show");
      },

      onShown: function () {
        console.log("Popup is shown");
      },

      onHiding: function () {
        console.log("Popup is about to hide");
      },

      onHidden: function (e) {
        console.log("Popup is hidden");
        console.log(e);
        popup.option("position", {
          my: "center",
          at: "center",
          of: "#area",
        });
      },
    })
    .dxPopup("instance");

  // Content for ScrollView inside the popup
  let scrollViewContent = `
    <div id="inputFeild"> </div>
      <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit.</p>
      <p>Vestibulum et lacus nec justo dignissim hendrerit.</p>
      <p>Curabitur aliquam metus nec magna commodo, eu pharetra risus rhoncus.</p>
      <p>Fusce tincidunt nisl id augue congue, eget tincidunt sapien consequat.</p>
      <p>Proin ut ex nec sapien cursus facilisis.</p>
      <p>Donec vulputate risus ut felis dictum, sed fermentum nisi aliquet.</p>
      <p>Suspendisse potenti. Integer vehicula massa a est ultricies accumsan.</p>
      <p>Aliquam auctor metus ut sem tempus, id ultrices turpis placerat.</p>
  `;

  // Popup with ScrollView Instance
  let popupWithScrollView = $("#popupWithScrollView")
    .dxPopup({
      title: "Popup with ScrollView",
      //// doubt in title template
      //// if we use titletemplate then toolbar will not shown
      titleTemplate: (e) => {
        console.log("E of title template : ", e);
        let logo = $("<img>")
          .attr(
            "src",
            "https://mticketdemo1.rkitsoftware.com/Theme/img/miracle.png"
          )
          .css({
            height: "30px",
            margin: " 5px",
          });
        return $("<a>")
          .attr({
            href: "https://www.rkitsoftware.com/",
            target: "_blank",
          })
          .append(logo);
      },
      width: 300,
      height: 300,
      visible: true,
      closeOnOutsideClick: true,
      showCloseButton: true,
      onHidden: function (e) {
        console.log("Popup is hidden");
        console.log(e);
        popupWithScrollView.option("position", {
          my: "center",
          at: "center",
          of: "#area",
        });
      },
      toolbarItems: [
        {
          toolbar: "bottom",
          location: "after",
          widget: "dxButton",
          options: {
            accessKey: "c",
            stylingMode: "outlined",
            text: "Close ❌",
            onClick: function () {
              popupWithScrollView.hide(); // Hide the popup when clicking the close button
            },
          },
        },
      ],
      position: { my: "center", at: "center", of: "#area" },
      contentTemplate: function () {
        let scrollView = $("<section>");
        scrollView.append($("<section>").html(scrollViewContent));

        scrollView.dxScrollView({
          width: "100%",
          height: "100%",
        });

        return scrollView;
      },
    })
    .dxPopup("instance");

  // as discussed in reconsile added input field in popup
  $("#inputFeild").dxTextBox({
    value: "",
    placeholder: "Enter your name ...",
    stylingMode: "outlined",
  });

  function handlePopupClose() {
    popup.hide();
  }
});
