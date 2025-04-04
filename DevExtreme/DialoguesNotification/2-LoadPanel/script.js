$(() => {
  const imageData = {
    ImageUrl: "../Data/load.png",
  };
  /**
   * after load panel is hidden it will get called and show the image
   */
  const showImage = () => {
    $("#loadedImage").attr("src", imageData.ImageUrl).fadeIn();
  };

  const loadPanel = $(".loadpanel")
    .dxLoadPanel({
      closeOnOutsideClick: true, // bool /  func ! its generally used with popup
      shadingColor: "rgba(0,0,0,0.4)",
      position: { of: "#imageContainer" },
      visible: false,
      delay: 1000,
      message: "Loaderrr ...", // wont work with material ui css
      showIndicator: true,
      showPane: true,
      shading: true,
      // hideOnOutsideClick: false,

      onShown() {
        /**
         * as its static img we are hiding the load panel after 2 seconds
         * in actual scenario we will hide it after the image is loaded
         * Solution: instand of setTimeout we can use image onload event
         */
        setTimeout(() => {
          loadPanel.hide();
        }, 2000);
      },

      onHidden() {
        showImage();
      },
    })
    .dxLoadPanel("instance");

  $("#loadImage").on("click", function () {
    loadPanel.show();
    $("#loadedImage").hide();
  });
});
