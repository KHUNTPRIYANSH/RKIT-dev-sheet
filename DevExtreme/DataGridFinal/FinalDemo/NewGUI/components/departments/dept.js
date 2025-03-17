$(function () {
  /** @type {string} API base URL */
  const apiUrl = "https://localhost:44310/get_all_departments";
  /** @type {string} User role from local storage */
  const userRole = localStorage.getItem("urole");
  /** @type {string} JWT token from local storage */
  const token = localStorage.getItem("utoken");

  /**
   * CustomStore for DevExtreme DataGrid.
   * Handles CRUD operations for department data.
   * @type {DevExpress.data.CustomStore}
   */
  const customStore = new DevExpress.data.CustomStore({
    key: "T01F01", // Primary key field
    loadMode: "raw",
    load: loadDepartments,
    insert: insertDepartment,
    update: updateDepartment,
    remove: deleteDepartment,
  });

  /**
   * Loads all departments from the server.
   * @returns {Promise} - Promise containing department data.
   * @called_from - customStore.load
   */
  function loadDepartments() {
    return apiHandler.getAll("/get_all_departments").then((res) => res.Data);
  }

  /**
   * Inserts a new department record.
   * @param {Object} values - Department data to insert.
   * @returns {Promise} - API response.
   * @called_from - customStore.insert
   */
  function insertDepartment(values) {
    return apiHandler.create("/add_department", values).done(refreshDataGrid);
  }

  /**
   * Updates an existing department record.
   * @param {number} key - Department ID to update.
   * @param {Object} values - New values for the department.
   * @returns {Promise} - API response.
   * @called_from - customStore.update
   */
  function updateDepartment(key, values) {
    return apiHandler
      .update("/update_department", key, values)
      .done(refreshDataGrid);
  }

  /**
   * Deletes a department record.
   * @param {number} key - Department ID to delete.
   * @returns {Promise} - API response.
   * @called_from - customStore.remove
   */
  function deleteDepartment(key) {
    return apiHandler.delete("/delete_department", key).done(refreshDataGrid);
  }

  /**
   * Refreshes the DataGrid instance.
   * @called_from - insertDepartment, updateDepartment, deleteDepartment
   */
  function refreshDataGrid() {
    $("#dataGridContainer").dxDataGrid("instance").refresh();
  }

  /**
   * Initializes and configures the DataGrid component.
   * @type {DevExpress.ui.dxDataGrid}
   */
  $("#dataGridContainer").dxDataGrid({
    dataSource: customStore,
    keyExpr: "T01F01",
    columns: configureColumns(),
    editing: configureEditing(),
    paging: configurePaging(),
    pager: configurePager(),
    scrolling: configureScrolling(),
    filterPanel: configureFilterPanel(),
    filterRow: { visible: true },
    searchPanel: { visible: true },
    showBorders: true,
    rowAlternationEnabled: true,
    summary: configureSummary(),
  });

  /**
   * Configures DataGrid columns.
   * @returns {Array} Column configuration array.
   * @called_from - DataGrid configuration
   */
  function configureColumns() {
    return [
      {
        dataField: "T01F01",
        dataType: "number",
        caption: "ID",
        allowEditing: false,
      },
      {
        dataField: "T01F02",
        caption: "Department Name",
        validationRules: [{ type: "required" }],
      },
      // { dataField: "T01F03", caption: "Is Active", dataType: "boolean" }, // Uncomment if needed
    ];
  }

  /**
   * Configures DataGrid editing properties.
   * @returns {Object} Editing configuration object.
   * @called_from - DataGrid configuration
   */
  function configureEditing() {
    return {
      mode: "popup",
      allowAdding: ["Admin", "Editor"].includes(userRole),
      allowUpdating: ["Admin", "Editor"].includes(userRole),
      allowDeleting: userRole === "Admin",
    };
  }

  /**
   * Configures DataGrid paging properties.
   * @returns {Object} Paging configuration object.
   * @called_from - DataGrid configuration
   */
  function configurePaging() {
    return {
      enabled: true,
      pageSize: 5,
      pageIndex: 0,
    };
  }

  /**
   * Configures DataGrid pager properties.
   * @returns {Object} Pager configuration object.
   * @called_from - DataGrid configuration
   */
  function configurePager() {
    return {
      label: "Departments Page Navigation",
      showPageSizeSelector: true,
      displayMode: "adaptive",
      allowedPageSizes: [5, 10, "all"],
      showNavigationButtons: true,
      showInfo: true,
      infoText: "Page {0} of {1} ({2} Departments)",
      visible: true,
    };
  }

  /**
   * Configures DataGrid scrolling properties.
   * @returns {Object} Scrolling configuration object.
   * @called_from - DataGrid configuration
   */
  function configureScrolling() {
    return {
      mode: "standard",
      scrollByContent: false,
      scrollByThumb: true,
      showScrollbar: "onHover",
      scrollDirection: "vertical",
    };
  }

  /**
   * Configures DataGrid filter panel properties.
   * @returns {Object} Filter panel configuration object.
   * @called_from - DataGrid configuration
   */
  function configureFilterPanel() {
    return {
      visible: true,
      customizeText: function (e) {
        return "Filter by " + e.text;
      },
    };
  }

  /**
   * Configures DataGrid summary properties.
   * @returns {Object} Summary configuration object.
   * @called_from - DataGrid configuration
   */
  function configureSummary() {
    return {
      totalItems: [
        {
          column: "T01F01",
          summaryType: "count",
          alignment: "right",
          cssClass: "totalCount",
          recalculateWhileEditing: true,
        },
      ],
    };
  }
});
