$(() => {
  $("#menu").dxMenu({
    // dataSource: productData,
    // adaptivityEnabled: true, //This property is in effect only if the orientation is "horizontal".
    orientation: "vertical", //  'horizontal' | 'vertical'

    // following are animation properties
    animation: {
      show: { type: "fade", from: 0, to: 1, duration: 1000 },
      hide: { type: "fade", from: 1, to: 0, duration: 1000 },
    },

    disabledExpr: "abcd",
    cssClass: "menuBack",
    disabled: false,
    // displayExpr: (e) => {
    //   let data = `${e.icon} ${e.name}`;
    //   return $("<h1>").append(e.name);
    // },
    //// display expression only work in call back when it return string not html

    items: [
      {
        items: productData,
        icon: "save",
        closeMenuOnClick: true,
        template: "<button>Click here to open menu </button>",
      },
    ],
    itemsExpr: "items",
    hideSubmenuOnMouseLeave: false,
    itemTemplate: function (itemData, itemIndex, itemElement) {
      console.log(itemData);
      itemElement.append(
        "<h5>" + `${itemData.icon} ${itemData.name}` + "</h5>"
      );
    },

    onItemContextMenu: (e) => {
      console.log("Item Context Menu");
      console.log(e);

      DevExpress.ui.notify(`Right Clicked`, "info", 1500);
    },
    onItemRendered: (e) => {
      console.log("Item Rendered");
      console.log(e);
    },
    onSubmenuHidden: (e) => {
      console.log("Submenu Hidden");
      console.log(e);
    },
    onSubmenuHiding: (e) => {
      console.log("Submenu Hiding");
      console.log(e);
    },
    onSubmenuShowing: (e) => {
      console.log("Submenu Showing");
      console.log(e);
    },
    onSubmenuShown: (e) => {
      console.log("Submenu Shown");
      console.log(e);
    },
    selectByClick: false, // omItemClick work if this is false
    onItemClick(data) {
      console.log(data.itemData);
      const item = data.itemData;
      if (data.itemData.name) {
        DevExpress.ui.notify(
          `Clicked on ${data.itemData.name}`,
          "success",
          1500
        );
      }
      if (item.price) {
        $("#product-details").removeClass("hidden");
        $("#product-details > .icon").text(`${item.icon}`);
        $("#product-details > .price").text(`\$${item.price}`);
        $("#product-details > .name").text(item.name);
      }
    },
    // selectedItem: null,
    selectedExpr: "selected",
    selectionMode: "single", // 'none' | 'single'
    showFirstSubmenuMode: "onHover", // 'onClick' | 'onHover'
    showSubmenuMode: "onClick", // 'onClick' | 'onHover'
    submenuDirection: "auto", // 'auto' | 'leftOrTop' | 'rightOrBottom'
  });
});
