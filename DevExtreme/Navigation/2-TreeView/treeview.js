$(() => {
  /**
   * purpose: generate product cards based on selected items
   * @param {array} selectedItems : selected items from treeview
   */
  const updateProductCards = (selectedItems) => {
    const container = $("#product-container");
    container.empty();
    selectedItems.forEach((item) => {
      // only product sub chaild have property price
      // i want to display sub products not the cateogry
      // so we checking for price
      if (item.price) {
        const card = `<div class="product-card" id="product-details">
                    <div class="icon">${item.icon}</div>
                    <div class="price">$${item.price}</div>
                    <div class="name">${item.name}</div>
                </div>`;
        container.append(card);
      }
    });
  };
  $("#treeview").dxTreeView({
    dataSource: productData,
    animationEnabled: false,
    displayExpr: (item) => `${item.icon} ${item.name}`,
    dataStructure: "tree", // 'plain' | 'tree'

    //  If data has a plain structure, set the dataStructure property to "plain". In this case, each data item should have a text, a unique id, and a parentId. For root items, parentId should be equal to 0 or undefined:

    // ex

    // var plainData = [
    //     { id: '1', text: 'Fruits' },     // A root item
    //     { id: '1_1', text: 'Apples', parentId: '1' },
    //     { id: '1_2', text: 'Oranges', parentId: '1' },
    //     { id: '2', text: 'Vegetables' }, // Also a root item
    // ],

    disabledExpr: "disabled",
    expandAllEnabled: true,
    expandedExpr: "expanded",
    expandEvent: "dblClick", // 'dblClick' | 'click'

    expandNodesRecursive: true,
    // whether or not all parent nodes of an initially expanded node are displayed expanded

    hasItemsExpr: "hasItems",
    itemHoldTimeout: 2500, // period in milliseconds before the onItemHold event is raised.
    keyExpr: "id",
    noDataText: "No data available",

    onItemClick: (e) => {
      console.log("Item Clicked");
      console.log(e.component);
      if (e.itemData.name) {
        DevExpress.ui.notify(`Clicked on ${e.itemData.name}`, "success", 1500);
      }
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

    onItemSelectionChanged: (e) => {
      /**
       * purpose: getting array of selected items
       * how: getSelectedNodes() return array of selected nodes
       */
      console.log("Item Selection Changed", e);
      const selectedItems = e.component
        .getSelectedNodes()
        .map((node) => node.itemData);
      updateProductCards(selectedItems);
    },
    onItemCollapsed: (e) => {
      console.log("Item Collapsed");
      console.log(e);
    },
    onItemExpanded: (e) => {
      console.log("Item Expanded");
      console.log(e);
    },
    onItemHold: (e) => {
      alert("Item Hold Called");
      console.log(e);
    },

    onItemSelectionChanged: (e) => {
      console.log("Item Selection Changed", e);
      const selectedItems = e.component
        .getSelectedNodes()
        .map((node) => node.itemData);
      //   console.log(selectedItems);
      updateProductCards(selectedItems);
    },
    onSelectAllValueChanged: (e) => {
      console.log("Select All Value Changed");
      console.log(e);
    },
    parentIdExpr: "parentId", // for Plain mode
    rootValue: 0, // for Plain mode

    scrollDirection: "both", //  'both' | 'horizontal' | 'vertical'

    searchEditorOptions: {
      visible: true,
      width: 200,

      placeholder: "Search...",
    },
    searchExpr: ["name", "icon"],
    searchMode: "contains",
    searchEnabled: true,
    searchTimeout: 2500,

    selectionMode: "multiple",

    showCheckBoxesMode: "selectAll", // 'normal' | 'selectAll' | 'none'

    selectAllText: "Select All Products",
    selectByClick: true,
    selectedExpr: "selected",

    selectNodesRecursive: true,
    //  If the selectNodesRecursive property is set to true, the widget selects all child nodes when a user selects a parent node. If the property is set to false, the widget selects only the clicked node.
    virtualModeEnabled: true,
    //  If the virtualModeEnabled property is set to true, the widget loads child nodes on demand. In this mode, the widget loads only root nodes initially. When a user expands a node, the widget sends a request to the server to load the node's children. The server should return the children in the same format as the root nodes

    useNativeScrolling: true,
  });
});
