$(() => {
  /**
   * Initialize the global loader
   * Purpose : It will be displayed when the page is loading on top of entire page
   */
  let globalLoader = $("#globalLoader").dxLoadIndicator({
    visible: true,
    height: 150,
    width: 150,
    // indicatorSrc: "../Data/loader.gif",
  });

  /**
   * Hide the Global loader after 3 seconds
   * Also hide background overlay
   * Other option : hide it when data gread is loaded on their onContentReady event
   */

  setTimeout(() => {
    $("#loadOverlay").hide();
    globalLoader.dxLoadIndicator("instance").option("visible", false);
  }, 3000);

  $.ajax({
    url: "https://dummyjson.com/products",
    type: "GET",
    success: function (data) {
      console.log(data.products);
      const productData = data.products;

      let dataGridRef = $("#dataGridContainer")
        .dxDataGrid({
          dataSource: productData,
          columns: [
            { dataField: "id", caption: "ID" },
            {
              dataField: "images",
              caption: "Images",
              alignment: "center",
              cellTemplate: function (container, options) {
                // handling the image loading
                const indicator = $("<div id='light'>").appendTo(container);
                indicator.dxLoadIndicator({
                  height: 150,
                  width: 150,
                  indicatorSrc: "../Data/loader.gif",
                });

                // handling the image
                const img = $("<img>").attr("src", options.value).css({
                  maxWidth: "100%",
                  height: "250px",
                  margin: "auto",
                  display: "none", // Hidden initially
                });

                img.appendTo(container);

                /**
                 * Show the image after 5 seconds [for static]
                 * For dynamic image, we can use the load event of image
                 * Solution : instand of setTimeout, use img.on("load", () => {})
                 */
                img.on("load", () => {
                  setTimeout(() => {
                    indicator
                      .dxLoadIndicator("instance")
                      .option("visible", false);
                    img.css("display", "block");
                  }, 5000); // assuming 5sec network delay
                });
              },
            },
            { dataField: "title", caption: "Title" },
            { dataField: "category", caption: "Category" },
            {
              dataField: "price",
              caption: "Price",
              cellTemplate: function (container, options) {
                // Customize the price column to display with a dollar sign
                const price = options.value;
                container.text(`$${price.toFixed(2)}`);
              },
            },
          ],
          showRowLines: true,
          showColumnLines: true,
          columnAutoWidth: true,
          paging: {
            enabled: true,
            pageSize: 10,
          },
          pager: {
            showPageSizeSelector: true,
            allowedPageSizes: [5, 10, 20],
            showInfo: true,
          },
        })
        .dxDataGrid("instance");
    },
  });
});
