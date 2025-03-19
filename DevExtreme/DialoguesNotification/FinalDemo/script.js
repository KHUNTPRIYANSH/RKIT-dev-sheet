const LOCAL_STORAGE_KEY = "productManagerData";

const fixedCategories = [
  "Keyboards",
  "Mice",
  "Headsets",
  "Webcams",
  "Monitors",
  "Speakers",
  "Cables",
  "Adapters",
  "Storage",
  "Accessories",
];

/**
 * Retrieves data from local storage.
 * @returns {Object|null} Parsed data from local storage or null if no data exists.
 */
function getLocalData() {
  const data = localStorage.getItem(LOCAL_STORAGE_KEY);
  return data ? JSON.parse(data) : null;
}

/**
 * Saves data to local storage.
 * @param {Object} data - The data to be saved in local storage.
 */
function saveLocalData(data) {
  localStorage.setItem(LOCAL_STORAGE_KEY, JSON.stringify(data));
}

/**
 * Initializes data by checking if local storage is empty and filling it if necessary.
 */
function initializeData() {
  if (!getLocalData()) {
    saveLocalData(productData);
  }
  refreshUI();
}

let selectedItems = [];
let currentDeleteItem = null;

$(function () {
  initializeData();
  initializeMenu();
  initializeTreeView();
  initializePopups();
  initializeLoaderAndToast();
});

/**
 * Initializes the menu with actions.
 */
function initializeMenu() {
  $("#topMenu").dxMenu({
    items: [
      { text: "Fill Local Store", action: "fillLocalStore" },
      { text: "Reset Local Store", action: "resetLocalStore" },
      { text: "Show Tree View", action: "showTreeView" },
      { text: "Add Product", action: "addProduct" },
    ],
    onItemClick: (e) => handleMenuAction(e.itemData.action),
  });
}

/**
 * Handles menu actions based on the action type.
 * @param {string} action - The action to be performed.
 */
function handleMenuAction(action) {
  switch (action) {
    case "fillLocalStore":
      fillLocalStore();
      break;
    case "resetLocalStore":
      showResetConfirmPopup();
      break;
    case "showTreeView":
      toggleTreeView();
      break;
    case "addProduct":
      showAddPopup();
      break;
  }
}

var treeView;

/**
 * Initializes the tree view with data from local storage.
 */
function initializeTreeView() {
  treeView = $("#treeView")
    .dxTreeView({
      dataSource: JSON.parse(localStorage.getItem(LOCAL_STORAGE_KEY)),
      keyExpr: "id",
      displayExpr: "name",
      itemsExpr: "items",
      selectionMode: "multiple",
      showCheckBoxesMode: "normal",
      onSelectionChanged: handleTreeViewSelectionChanged,
    })
    .dxTreeView("instance");
}

/**
 * Handles tree view selection change event.
 * @param {Object} e - The event object.
 */
function handleTreeViewSelectionChanged(e) {
  DevExpress.ui.notify("Data Selected", "success");
  selectedItems = e.component.getSelectedNodes().map((n) => n.itemData);
  renderSelectedItems();
}

/**
 * Initializes popups for adding products, deleting items, and resetting local storage.
 */
function initializePopups() {
  initializeAddProductPopup();
  initializeDeleteConfirmationPopup();
  initializeResetConfirmationPopup();
}

/**
 * Initializes the add product popup.
 */
function initializeAddProductPopup() {
  $("#addPopup").dxPopup({
    title: "Add Product",
    width: 400,
    contentTemplate: () =>
      $("<div>")
        .dxForm({
          items: [
            {
              dataField: "category",
              editorType: "dxSelectBox",
              editorOptions: { dataSource: fixedCategories },
            },
            {
              dataField: "name",
              validationRules: [{ type: "required" }],
            },
            {
              dataField: "price",
              editorType: "dxNumberBox",
              editorOptions: { format: "$#0.00", min: 0.01 },
            },
            { dataField: "icon", label: "Icon (Emoji)" },
          ],
          formData: {},
        })
        .append(
          $("<div>").dxButton({
            text: "Save",
            onClick: saveNewProduct,
          })
        ),
  });
}

/**
 * Initializes the delete confirmation popup.
 */
function initializeDeleteConfirmationPopup() {
  $("#deletePopup").dxPopup({
    title: "Confirm Delete",
    contentTemplate: () =>
      $("<div>").append(
        $("<p>").text("Delete this item permanently?"),
        $("<div>").dxButton({
          text: "Delete",
          type: "danger",
          onClick: confirmDelete,
        }),
        $("<div>").dxButton({
          text: "Cancel",
          onClick: hideDeletePopup,
        })
      ),
  });
}

/**
 * Initializes the reset confirmation popup.
 */
function initializeResetConfirmationPopup() {
  $("#resetConfirmPopup").dxPopup({
    title: "Confirm Reset",
    contentTemplate: () =>
      $("<div>").append(
        $("<p>").text(
          "Are you sure you want to reset the local storage? This action cannot be undone."
        ),
        $("<div>").dxButton({
          text: "Reset",
          type: "danger",
          onClick: handleResetConfirmation,
        }),
        $("<div>").dxButton({
          text: "Cancel",
          onClick: hideResetConfirmPopup,
        })
      ),
  });
}

/**
 * Saves a new product to local storage.
 */
function saveNewProduct() {
  const form = $("#addPopup .dx-form").dxForm("instance");
  if (!form.validate().isValid) return;

  const data = getLocalData() || [];
  const formData = form.option("formData");
  const category = data.find((c) => c.name === formData.category);

  if (!category) {
    showToast("Invalid category", "error");
    return;
  }

  const newProduct = {
    id: `prod_${Date.now()}`,
    ...formData,
    items: [],
  };

  category.items.push(newProduct);
  saveLocalData(data);
  refreshUI();
  hideAddPopup();
  showToast("Product added", "success");
}

/**
 * Confirms deletion of an item.
 */
function confirmDelete() {
  const data = getLocalData();
  for (const category of data) {
    const index = category.items.findIndex(
      (i) => i.id === currentDeleteItem.id
    );
    if (index > -1) {
      category.items.splice(index, 1);
      saveLocalData(data);
      refreshUI();
      showToast("Product deleted", "warning");
      break;
    }
  }
  hideDeletePopup();
}

/**
 * Fills local storage with initial data.
 */
function fillLocalStore() {
  const loader = $("#loader").dxLoadPanel("instance");
  loader.show();

  setTimeout(() => {
    saveLocalData(productData);
    refreshUI();
    showToast("Data restored from data.js", "success");
    loader.hide();
  }, 1000);
}

/**
 * Resets local storage.
 */
function resetLocalStore() {
  localStorage.removeItem(LOCAL_STORAGE_KEY);
  selectedItems = []; // Clear selected items
  refreshUI();
  showToast("Local storage cleared", "warning");
}

/**
 * Toggles the visibility of the tree view.
 */
function toggleTreeView() {
  $("#treeView").toggle();
  refreshUI();
}

/**
 * Refreshes the UI by updating the tree view and clearing selected items.
 */
function refreshUI() {
  const data = getLocalData();

  if (treeView) {
    treeView.option("dataSource", data || []); // Ensure empty array when data is null
  }

  selectedItems = []; // Clear selected items when data is cleared
  $("#cards-container").empty(); // Remove all product cards

  if (!data) {
    showToast(
      "No data available. Click 'Fill Local Store' to load data.",
      "info"
    );
  }
}

/**
 * Renders selected items in the UI.
 */
function renderSelectedItems() {
  $("#cards-container").empty();
  selectedItems.forEach((item) => {
    if (!item.items) {
      // Only show products, not categories
      const card = $("<div>").addClass("item-card").html(`
      <div id="icon">${item.icon}</div>
              <h3>${item.name}</h3>
              <p>Price: $${item.price.toFixed(2)}</p>
            `);
      $("#cards-container").append(card);
    }
  });
}

/**
 * Initializes the loader and toast components.
 */
function initializeLoaderAndToast() {
  $("#loader").dxLoadPanel({
    shading: true,
    message: "Loading...",
    visible: false,
  });
  $("#toast").dxToast({ displayTime: 2000 });
}

/**
 * Shows a toast message.
 * @param {string} message - The message to display.
 * @param {string} type - The type of toast (e.g., "success", "error").
 */
function showToast(message, type) {
  $("#toast").dxToast("instance").option({
    message,
    type,
    visible: true,
  });
}

/**
 * Shows the add product popup.
 */
function showAddPopup() {
  $("#addPopup").dxPopup("show");
}

/**
 * Hides the add product popup.
 */
function hideAddPopup() {
  $("#addPopup").dxPopup("hide");
}

/**
 * Shows the reset confirmation popup.
 */
function showResetConfirmPopup() {
  $("#resetConfirmPopup").dxPopup("show");
}

/**
 * Hides the reset confirmation popup.
 */
function hideResetConfirmPopup() {
  $("#resetConfirmPopup").dxPopup("hide");
}

/**
 * Handles the reset confirmation action.
 */
function handleResetConfirmation() {
  resetLocalStore();
  hideResetConfirmPopup();
}

/**
 * Shows the delete confirmation popup.
 */
function showDeletePopup() {
  $("#deletePopup").dxPopup("show");
}

/**
 * Hides the delete confirmation popup.
 */
function hideDeletePopup() {
  $("#deletePopup").dxPopup("hide");
}
