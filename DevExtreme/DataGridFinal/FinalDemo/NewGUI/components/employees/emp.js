$(function () {
  const apiUrl = "https://localhost:44310/get_all_employees";
  const token = localStorage.getItem("utoken");
  const userRole = localStorage.getItem("urole");

  /**
   * CustomStore for DevExtreme DataGrid.
   * Handles CRUD operations for employee data.
   * @type {DevExpress.data.CustomStore}
   */
  const customStore = new DevExpress.data.CustomStore({
    key: "P01F01",
    loadMode: "raw",
    load: loadEmployees,
    insert: insertEmployee,
    update: updateEmployee,
    remove: deleteEmployee,
  });

  /**
   * Loads all employees from the server.
   * @returns {Promise} - Promise containing employee data.
   * @called_from - customStore.load
   */
  function loadEmployees() {
    return apiHandler.getAll("/get_all_employees").then((response) => {
      console.log("Data Loaded:", response.Data);
      return response.Data;
    });
  }

  /**
   * Inserts a new employee record.
   * @param {Object} values - Employee data to insert.
   * @returns {Promise} - API response.
   * @called_from - customStore.insert
   */
  function insertEmployee(values) {
    console.log("Insert Values:", values);
    const employeeData = {
      P01F01: 0, // Employee ID (Auto-generated)
      P01F02: values.P01F02, // First Name
      P01F03: values.P01F03, // Last Name
      P01F04: values.P01F04, // Date of Birth
      P01F05: values.P01F05, // Department ID
      P01F06: values.P01F06, // Position
      P01F08: values.P01F08, // Salary
      P01F09: values.P01F09, // User ID
    };
    return apiHandler
      .create("/add_employee", employeeData)
      .done(refreshDataGrid);
  }

  /**
   * Updates an existing employee record.
   * @param {number} key - Employee ID to update.
   * @param {Object} values - New values for the employee.
   * @returns {Promise} - API response.
   * @called_from - customStore.update
   */
  function updateEmployee(key, values) {
    console.log("Update Values:", values);
    return apiHandler.getById("/get_employee_by_id", key).then((res) => {
      const existingRecord = res.Data;
      const updatedRecord = {
        P01F01: existingRecord.P01F01,
        P01F02: values.P01F02 ?? existingRecord.P01F02,
        P01F03: values.P01F03 ?? existingRecord.P01F03,
        P01F04: values.P01F04 ?? existingRecord.P01F04,
        P01F05: values.P01F05 ?? existingRecord.P01F05,
        P01F06: values.P01F06 ?? existingRecord.P01F06,
        P01F08: values.P01F08 ?? existingRecord.P01F08,
        P01F09: values.P01F09 ?? existingRecord.P01F09,
      };
      console.log("Merged Update Data:", updatedRecord);
      return apiHandler
        .update("/update_employee", key, updatedRecord)
        .done(refreshDataGrid);
    });
  }

  /**
   * Deletes an employee record.
   * @param {number} key - Employee ID to delete.
   * @returns {Promise} - API response.
   * @called_from - customStore.remove
   */
  function deleteEmployee(key) {
    console.log("Delete Employee ID:", key);
    return apiHandler.delete("/delete_employee", key).done(refreshDataGrid);
  }

  /**
   * Refreshes the DataGrid instance.
   * @called_from - insertEmployee, updateEmployee, deleteEmployee
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
    keyExpr: "P01F01",
    columns: configureColumns(),
    editing: configureEditing(),
    paging: configurePaging(),
    pager: configurePager(),
    grouping: configureGrouping(),
    groupPanel: configureGroupPanel(),
    scrolling: configureScrolling(),
    filterPanel: configureFilterPanel(),
    filterRow: configureFilterRow(),
    searchPanel: { visible: true },
    selection: configureSelection(),
    showBorders: true,
    rowAlternationEnabled: true,
    onCellPrepared: styleCells,
    onSelectionChanged: handleSelectionChange,
    onToolbarPreparing: configureToolbar,
    onRowInserted: logRowInserted,
    onRowUpdated: logRowUpdated,
    onRowRemoved: logRowRemoved,
    summary: configureSummary(),
    export: configureExport(),
    onExporting: handleExporting,
  });

  /**
   * Configures DataGrid columns.
   * @returns {Array} Column configuration array.
   * @called_from - DataGrid configuration
   */
  function configureColumns() {
    return [
      {
        dataField: "P01F01",
        dataType: "number",
        caption: "ID",
        allowEditing: false,
      },
      {
        dataField: "P01F02",
        caption: "First Name",
        validationRules: [{ type: "required" }],
      },
      {
        dataField: "P01F03",
        caption: "Last Name",
        validationRules: [{ type: "required" }],
      },
      {
        dataField: "P01F04",
        caption: "Date of Birth",
        dataType: "date",
        validationRules: [{ type: "required" }],
      },
      {
        dataField: "P01F05",
        caption: "Department ID",
        dataType: "number",
        validationRules: [{ type: "required" }],
      },
      {
        dataField: "P01F06",
        caption: "Position",
        validationRules: [{ type: "required" }],
      },
      {
        dataField: "P01F08",
        caption: "Salary",
        dataType: "number",
        alignment: "center",
        validationRules: [{ type: "required" }],
      },
      {
        dataField: "P01F09",
        dataType: "number",
        caption: "User ID",
        validationRules: [{ type: "required" }],
      },
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
      label: "Employees Page Navigation",
      showPageSizeSelector: true,
      displayMode: "adaptive",
      allowedPageSizes: [5, 10, "all"],
      showNavigationButtons: true,
      showInfo: true,
      infoText: "Page {0} of {1} ({2} Employees)",
      visible: true,
    };
  }

  /**
   * Configures DataGrid grouping properties.
   * @returns {Object} Grouping configuration object.
   * @called_from - DataGrid configuration
   */
  function configureGrouping() {
    return {
      autoExpandAll: false,
      expandMode: "rowClick",
      contextMenuEnabled: true,
    };
  }

  /**
   * Configures DataGrid group panel properties.
   * @returns {Object} Group panel configuration object.
   * @called_from - DataGrid configuration
   */
  function configureGroupPanel() {
    return {
      emptyPanelText: "Drag a column header here to group by that column",
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
      customizeText: (e) => `Filter by ${e.text}`,
    };
  }

  /**
   * Configures DataGrid filter row properties.
   * @returns {Object} Filter row configuration object.
   * @called_from - DataGrid configuration
   */
  function configureFilterRow() {
    return {
      visible: true,
      applyFilter: "onClick",
      applyFilterText: "Apply Filter",
      betweenEndText: "To",
      betweenStartText: "From",
      resetOperationText: "Reset",
      showAllText: "Show All",
      showOperationChooser: true,
    };
  }

  /**
   * Configures DataGrid selection properties.
   * @returns {Object} Selection configuration object.
   * @called_from - DataGrid configuration
   */
  function configureSelection() {
    return {
      mode: "multiple",
      allowSelectAll: true,
      selectAllMode: "page",
      showCheckBoxesMode: "always",
    };
  }

  /**
   * Styles cells based on their values.
   * @param {Object} e - Cell preparation event object.
   * @called_from - DataGrid onCellPrepared
   */
  function styleCells(e) {
    if (e.rowType === "data" && e.column.dataField === "P01F08") {
      if (e.data.P01F08 < 15000) {
        e.cellElement.addClass("bpl");
      } else if (e.data.P01F08 >= 30000 && e.data.P01F08 < 60000) {
        e.cellElement.addClass("middleClass");
      } else {
        e.cellElement.addClass("rich");
      }
    }
  }

  /**
   * Handles selection change events.
   * @param {Object} e - Selection change event object.
   * @called_from - DataGrid onSelectionChanged
   */
  function handleSelectionChange(e) {
    const data = e.selectedRowsData;
    const removedItems = e.removedItems;
    const previousSelectionCount = e.component.getSelectedRowKeys().length;

    console.log(data);
    $("#selectionInfo").html("");

    if (data && data.length > 0) {
      const curr = data[data.length - 1];
      data.forEach((element) => {
        $("#selectionInfo").append(
          `<div>${element.P01F01} from department[${element.P01F05}] added into the selection list</div>`
        );
      });
      DevExpress.ui.notify(
        `${curr.P01F02} added successfully!`,
        "success",
        1500
      );
    }

    if (removedItems && removedItems.length > 0) {
      removedItems.forEach((item) => {
        DevExpress.ui.notify(
          `${item.P01F02} removed from selection list successfully!`,
          "error",
          1500
        );
      });
    }

    if (previousSelectionCount === 0 && (!data || data.length === 0)) {
      DevExpress.ui.notify("Selection list is empty!", "error", 1500);
    }
  }

  /**
   * Configures DataGrid toolbar properties.
   * @param {Object} e - Toolbar preparation event object.
   * @called_from - DataGrid onToolbarPreparing
   */
  function configureToolbar(e) {
    e.toolbarOptions.items.push({
      widget: "dxButton",
      options: {
        text: "Clear Selection",
        onClick: () => e.component.clearSelection(),
      },
      location: "after",
    });
  }

  /**
   * Logs row insertion events.
   * @param {Object} e - Row insertion event object.
   * @called_from - DataGrid onRowInserted
   */
  function logRowInserted(e) {
    console.log("Row Inserted:", e.data);
    e.component.refresh();
  }

  /**
   * Logs row update events.
   * @param {Object} e - Row update event object.
   * @called_from - DataGrid onRowUpdated
   */
  function logRowUpdated(e) {
    console.log("Row Updated:", e.data);
    e.component.refresh();
  }

  /**
   * Logs row removal events.
   * @param {Object} e - Row removal event object.
   * @called_from - DataGrid onRowRemoved
   */
  function logRowRemoved(e) {
    console.log("Row Removed:", e.data);
    e.component.refresh();
  }

  /**
   * Configures DataGrid summary properties.
   * @returns {Object} Summary configuration object.
   * @called_from - DataGrid configuration
   */
  function configureSummary() {
    return {
      groupItems: [
        {
          column: "P01F08",
          summaryType: "sum",
          valueFormat: "currency",
          alignment: "right",
          showInGroupFooter: true,
          recalculateWhileEditing: true,
        },
        {
          column: "P01F08",
          summaryType: "min",
          alignment: "center",
          displayFormat: "Min Salary : {0}",
          skipEmptyValues: false,
        },
        {
          column: "P01F08",
          summaryType: "avg",
          alignment: "left",
        },
        {
          column: "P01F01",
          summaryType: "count",
        },
      ],
      totalItems: [
        {
          column: "P01F08",
          summaryType: "sum",
          valueFormat: "currency",
          alignment: "right",
          cssClass: "totalSalary",
          recalculateWhileEditing: true,
        },
        {
          column: "P01F08",
          summaryType: "min",
          alignment: "center",
          displayFormat: "Min Price : {0}",
          skipEmptyValues: false,
        },
        {
          column: "P01F08",
          summaryType: "avg",
          alignment: "left",
          cssClass: "avgPrice",
        },
        {
          column: "P01F01",
          summaryType: "count",
        },
      ],
    };
  }

  /**
   * Configures DataGrid export properties.
   * @returns {Object} Export configuration object.
   * @called_from - DataGrid configuration
   */
  function configureExport() {
    return {
      enabled: true,
      allowExportSelectedData: true,
    };
  }

  /**
   * Handles exporting data to Excel.
   * @param {Object} e - Exporting event object.
   * @called_from - DataGrid onExporting
   */
  function handleExporting(e) {
    const workbook = new ExcelJS.Workbook();
    const worksheet = workbook.addWorksheet("Employees");

    DevExpress.excelExporter
      .exportDataGrid({
        component: e.component,
        worksheet,
        autoFilterEnabled: true,
      })
      .then(() => {
        workbook.xlsx.writeBuffer().then((buffer) => {
          saveAs(
            new Blob([buffer], { type: "application/octet-stream" }),
            "Users.xlsx"
          );
        });
      });
  }
});
