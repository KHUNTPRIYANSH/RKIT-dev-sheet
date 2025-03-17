$(function () {
  const apiUrl = "https://localhost:44310/get_all_users";
  const token = localStorage.getItem("utoken");
  const userRole = localStorage.getItem("urole"); // Get user role from localStorage

  // Role mapping for lookup column in DataGrid
  let roleToVal = [
      { key: 3, role: "Admin" },
      { key: 2, role: "Editor" },
      { key: 1, role: "User" },
  ];

  /**
   * Fetch all users from the API.
   * @returns {Promise<Array>} - Resolves to an array of user objects.
   * @called_from - users.js : load
   */
  function loadUsers() {
      return apiHandler.getAll("/get_all_users").then(res => res.Data);
  }

  /**
   * Inserts a new user into the database.
   * @param {Object} values - User object containing details to insert.
   * @returns {Promise} - API response.
   * @called_from - users.js : insert
   */
  function insertUser(values) {
      return apiHandler.create("/add_user", values);
  }

  /**
   * Updates an existing user record.
   * @param {number} key - User ID to update.
   * @param {Object} values - Updated user data.
   * @returns {Promise} - API response.
   * @called_from - users.js : update
   */
  function updateUser(key, values) {
      return apiHandler.getById("/get_user_by_id", key)
          .then(res => {
              let existingRecord = res.Data;
              let updatedRecord = {
                  R01F01: existingRecord.R01F01,
                  R01F02: values.R01F02 ?? existingRecord.R01F02,
                  R01F03: values.R01F03 ?? existingRecord.R01F03,
                  R01F04: values.R01F04 ?? existingRecord.R01F04
              };
              return apiHandler.update("/update_user", key, updatedRecord);
          })
          .done(() => refreshDataGrid());
  }

  /**
   * Deletes a user from the database.
   * @param {number} key - User ID to delete.
   * @returns {Promise} - API response.
   * @called_from - users.js : remove
   */
  function deleteUser(key) {
      return apiHandler.delete("/delete_user", key);
  }

  /**
   * Refreshes the DataGrid UI after a CRUD operation.
   * @called_from - onRowInserted, onRowUpdated, onRowRemoved
   */
  function refreshDataGrid() {
      $("#dataGridContainer").dxDataGrid("instance").refresh();
  }

  // CustomStore for DataGrid CRUD operations
  const customStore = new DevExpress.data.CustomStore({
      key: "R01F01",
      loadMode: "raw",
      load: loadUsers,
      insert: insertUser,
      update: updateUser,
      remove: deleteUser
  });

  $("#dataGridContainer").dxDataGrid({
      dataSource: customStore,
      keyExpr: "R01F01",
      columns: [
          { dataField: "R01F01", dataType: "number", caption: "ID", allowEditing: false, fixed: true, cssClass: "grd-2" },
          { dataField: "R01F02", caption: "User Name" },
          { dataField: "R01F03", caption: "Password", dataType: "string", visible: false },
          {
              dataField: "R01F04",
              caption: "Role",
              lookup: { dataSource: roleToVal, valueExpr: "key", displayExpr: "role" },
              groupIndex: 0
          }
      ],
      rowAlternationEnabled: true,
      editing: {
          mode: "popup",
          allowAdding: userRole === "Admin" || userRole === "Editor",
          allowUpdating: userRole === "Admin",
          allowDeleting: userRole === "Admin",
      },
      paging: { enabled: true, pageSize: 5, pageIndex: 4 },
      pager: {
          label: "Users Page Navigation",
          showPageSizeSelector: true,
          displayMode: "adaptive",
          allowedPageSizes: [5, 10, "all"],
          showNavigationButtons: true,
          showInfo: true,
          infoText: "Page {0} of {1} ({2} Users )",
          visible: true,
      },
      searchPanel: { visible: true },
      filterRow: { visible: true },
      showBorders: true,
      onRowInserted: refreshDataGrid,
      onRowUpdated: refreshDataGrid,
      onRowRemoved: refreshDataGrid,
      export: { enabled: true, allowExportSelectedData: true },
      onExporting: exportToExcel,
  });

  /**
   * Exports the DataGrid data to an Excel file.
   * @param {Object} e - DevExtreme event parameter.
   * @called_from - onExporting
   */
  function exportToExcel(e) {
      const workbook = new ExcelJS.Workbook();
      const worksheet = workbook.addWorksheet("Users");

      DevExpress.excelExporter.exportDataGrid({ component: e.component, worksheet, autoFilterEnabled: true })
          .then(() => {
              workbook.xlsx.writeBuffer().then((buffer) => {
                  saveAs(new Blob([buffer], { type: "application/octet-stream" }), "Users.xlsx");
              });
          });
  }

  // State Management Handlers
  $("#saveStateButton").on("click", () => saveState());
  $("#loadStateButton").on("click", () => loadState());
  $("#resetStateButton").on("click", () => resetState());

  /**
   * Saves the current state of the DataGrid.
   * @called_from - #saveStateButton.click
   */
  function saveState() {
      var state = $("#dataGridContainer").dxDataGrid("instance").state();
      DevExpress.ui.notify("State Saved", "success", 500);
  }

  /**
   * Loads the previously saved DataGrid state.
   * @called_from - #loadStateButton.click
   */
  function loadState() {
      var state = JSON.parse(sessionStorage.getItem("lastGridState"));
      $("#dataGridContainer").dxDataGrid("instance").state(state);
      DevExpress.ui.notify("State Loaded", "success", 500);
  }

  /**
   * Resets the DataGrid state.
   * @called_from - #resetStateButton.click
   */
  function resetState() {
      $("#dataGridContainer").dxDataGrid("instance").state(null);
      DevExpress.ui.notify("State Reset", "success", 500);
  }
});