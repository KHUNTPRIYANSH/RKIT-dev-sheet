$(function () {
  window.jsPDF = window.jspdf.jsPDF;

  const apiUrl = "https://localhost:44310/";
  const token = localStorage.getItem("utoken");
  const userRole = localStorage.getItem("urole");
  const userId = localStorage.getItem("uid");

  /**
   * Exports DataGrid data to PDF format.
   * @param {Object} e - Export event object.
   * @called_from - DataGrid toolbar button click
   */
  const pdfExporter = (e) => {
    const doc = new jsPDF();
    DevExpress.pdfExporter
      .exportDataGrid({
        jsPDFDocument: doc,
        component: e.component,
        indent: 5,
      })
      .then(() => doc.save("Attendance.pdf"));
  };

  /**
   * Exports DataGrid data to Excel format.
   * @param {Object} e - Export event object.
   * @called_from - DataGrid onExporting event
   */
  const excelExporter = (e) => {
    const wb = new ExcelJS.Workbook();
    const worksheet = wb.addWorksheet("Main sheet");

    DevExpress.excelExporter
      .exportDataGrid({
        worksheet: worksheet,
        component: e.component,
        customizeCell: customizeExcelCell,
      })
      .then(() => {
        wb.xlsx.writeBuffer().then((buffer) => {
          saveAs(
            new Blob([buffer], { type: "application/octet-stream" }),
            "Attendance.xlsx"
          );
        });
      });
    e.cancel = true;
  };

  /**
   * Customizes Excel cell formatting.
   * @param {Object} options - Cell customization options.
   * @called_from - excelExporter
   */
  const customizeExcelCell = (options) => {
    options.excelCell.alignment = { horizontal: "center" };
    options.excelCell.font = { name: "Arial", size: 17 };
  };

  /**
   * Custom store for attendance data operations.
   * @type {DevExpress.data.CustomStore}
   */
  const attendanceStore = new DevExpress.data.CustomStore({
    key: "ATT01F01",
    load: loadAttendanceRecords,
    insert: insertAttendanceRecord,
    update: updateAttendanceRecord,
    remove: deleteAttendanceRecord,
  });

  /**
   * Loads attendance records from the server.
   * @returns {Promise} - Promise containing attendance data.
   * @called_from - attendanceStore.load
   */
  function loadAttendanceRecords() {
    return apiHandler.getAll("/get_all_attendance").then((res) => res.Data);
  }

  /**
   * Inserts new attendance record.
   * @param {Object} values - Record values to insert.
   * @returns {Promise} - API response.
   * @called_from - attendanceStore.insert
   */
  function insertAttendanceRecord(values) {
    const data = {
      ATT01F01: 0,
      ATT01F02: values.ATT01F02,
      ATT01F03: values.ATT01F03,
      ATT01F04: values.ATT01F04,
      ATT01F05: values.ATT01F05 || null,
      ATT01F06: values.ATT01F06 || null,
      ATT01F08: values.ATT01F08,
      ATT01F09: values.ATT01F09,
    };
    return apiHandler.create("/add_attendance", data);
  }

  /**
   * Updates existing attendance record.
   * @param {number} key - Record ID to update.
   * @param {Object} values - New values for the record.
   * @returns {Promise} - API response.
   * @called_from - attendanceStore.update
   */
  function updateAttendanceRecord(key, values) {
    const data = {
      ATT01F01: key,
      ATT01F05: values.ATT01F05 || null,
      ATT01F06: values.ATT01F06 || null,
      ATT01F08: values.ATT01F08,
      ATT01F09: values.ATT01F09,
    };
    return apiHandler.update("/update_attendance", key, data);
  }

  /**
   * Deletes attendance record from the database.
   * @param {number} key - Record ID to delete.
   * @returns {Promise} - API response.
   * @called_from - attendanceStore.remove
   */
  function deleteAttendanceRecord(key) {
    return apiHandler.delete("/delete_attendance", key);
  }

  /**
   * Creates time mask rules for time input fields.
   * @returns {Object} Mask rules for time input.
   * @called_from - createTimeEditorConfig
   */
  function createTimeMaskRules() {
    return {
      H: (char) => char >= 0 && char <= 2,
      h: (char, index, fullStr) => {
        if (fullStr[0] === "2") {
          return [0, 1, 2, 3].includes(parseInt(char));
        }
        return [0, 1, 2, 3, 4, 5, 6, 7, 8, 9].includes(parseInt(char));
      },
      M: (char) => char >= 0 && char <= 5,
      m: (char) => char >= 0 && char <= 9,
      S: (char) => char >= 0 && char <= 5,
      s: (char) => char >= 0 && char <= 9,
    };
  }

  /**
   * Initializes and configures the DataGrid component.
   * @type {DevExpress.ui.dxDataGrid}
   */
  const dataGridRef = $("#gridContainer")
    .dxDataGrid({
      dataSource: attendanceStore,
      filterValue: userRole === "User" ? ["ATT01F02", "=", userId] : null,
      editing: configureEditing(),
      columns: configureColumns(),
      onRowInserted: refreshGrid,
      onRowUpdated: refreshGrid,
      onRowRemoved: refreshGrid,
      onCellPrepared: styleCells,
      onToolbarPreparing: configureToolbar,
      onExporting: excelExporter,
      export: { allowExportSelectedData: true, enabled: true },
      summary: configureSummary(),
    })
    .dxDataGrid("instance");

  /**
   * Configures DataGrid editing properties.
   * @returns {Object} Editing configuration object.
   * @called_from - DataGrid configuration
   */
  function configureEditing() {
    return {
      mode: "popup",
      popup: {
        title: "Attendance Record",
        fullScreen: true,
        showTitle: true,
        position: { my: "center", at: "center", of: window },
      },
      allowAdding: ["Admin", "Editor"].includes(userRole),
      allowUpdating: ["Admin", "Editor"].includes(userRole),
      allowDeleting: userRole === "Admin",
      form: { items: configureFormItems() },
    };
  }

  /**
   * Configures form items for editing popup.
   * @returns {Array} Form items configuration.
   * @called_from - configureEditing
   */
  function configureFormItems() {
    return [
      "ATT01F02",
      "ATT01F03",
      createDateEditorConfig(),
      createTimeEditorConfig("ATT01F05", "Enter time (HH:MM:SS)..."),
      createTimeEditorConfig("ATT01F06", "Enter time (HH:MM:SS)..."),
      "ATT01F08",
      "ATT01F09",
    ];
  }

  /**
   * Creates date editor configuration.
   * @returns {Object} DateBox configuration object.
   * @called_from - configureFormItems
   */
  function createDateEditorConfig() {
    return {
      dataField: "ATT01F04",
      editorType: "dxDateBox",
      editorOptions: {
        type: "date",
        width: "100%",
        displayFormat: "yyyy-MM-dd",
        value: new Date(),
      },
    };
  }

  /**
   * Creates time editor configuration.
   * @param {string} field - Data field name.
   * @param {string} placeholder - Input placeholder text.
   * @returns {Object} TextBox configuration object.
   * @called_from - configureFormItems
   */
  function createTimeEditorConfig(field, placeholder) {
    return {
      dataField: field,
      editorType: "dxTextBox",
      editorOptions: {
        mask: "Hh:Mm:Ss",
        showClearButton: true,
        useMaskedValue: true,
        maskRules: createTimeMaskRules(),
        placeholder: placeholder,
      },
    };
  }

  /**
   * Configures DataGrid columns.
   * @returns {Array} Column configuration array.
   * @called_from - DataGrid configuration
   */
  function configureColumns() {
    return [
      { dataField: "ATT01F01", caption: "ID", allowEditing: false },
      {
        dataField: "ATT01F02",
        caption: "Employee ID",
        validationRules: [{ type: "required" }],
      },
      {
        dataField: "ATT01F03",
        caption: "Department ID",
        validationRules: [{ type: "required" }],
      },
      {
        dataField: "ATT01F04",
        caption: "Date",
        dataType: "date",
        format: "yyyy-MM-dd",
      },
      { dataField: "ATT01F05", caption: "Check-In", dataType: "string" },
      { dataField: "ATT01F06", caption: "Check-Out", dataType: "string" },
      {
        dataField: "ATT01F07",
        caption: "Work Hours",
        allowEditing: false,
        calculateCellValue: (data) => {
          if (!data.ATT01F05 || !data.ATT01F06) return 0;
          const [h1, m1, s1] = data.ATT01F05.split(":").map(Number);
          const [h2, m2, s2] = data.ATT01F06.split(":").map(Number);
          const diff = h2 * 3600 + m2 * 60 + s2 - (h1 * 3600 + m1 * 60 + s1);
          return (diff / 3600).toFixed(2);
        },
      },
      {
        dataField: "ATT01F08",
        caption: "Status",
        lookup: { dataSource: ["Present", "Absent", "Half-Day", "Leave"] },
      },
      {
        dataField: "ATT01F10",
        caption: "Is Late",
        allowEditing: false,
        calculateCellValue: (data) => {
          if (!data.ATT01F05) return false;
          const [hours, minutes] = data.ATT01F05.split(":").map(Number);
          return hours > 9 || (hours === 9 && minutes > 0);
        },
      },
      { dataField: "ATT01F09", caption: "Remarks" },
    ];
  }

  /**
   * Refreshes the DataGrid instance.
   * @called_from - DataGrid onRowInserted, onRowUpdated, onRowRemoved
   */
  function refreshGrid() {
    $("#gridContainer").dxDataGrid("instance").refresh();
  }

  /**
   * Styles cells based on their values.
   * @param {Object} e - Cell preparation event object.
   * @called_from - DataGrid onCellPrepared
   */
  function styleCells(e) {
    if (e.rowType === "data") {
      if (e.column.dataField === "ATT01F10" && e.value) {
        e.cellElement.addClass("late-entry");
      }
      if (e.column.dataField === "ATT01F07" && e.value > 0) {
        e.cellElement.addClass("on-time");
      }
    }
  }

  /**
   * Configures the DataGrid toolbar.
   * @param {Object} e - Toolbar preparation event object.
   * @called_from - DataGrid onToolbarPreparing
   */
  function configureToolbar(e) {
    e.toolbarOptions.items.push({
      widget: "dxButton",
      location: "before",
      options: {
        text: "Export PDF",
        stylingMode: "filled",
        onClick: () => {
          dataGridRef.option("onExporting", pdfExporter);
          e.component.exportToExcel();
          dataGridRef.option("onExporting", excelExporter);
        },
      },
    });
  }

  /**
   * Configures DataGrid summary.
   * @returns {Object} Summary configuration object.
   * @called_from - DataGrid configuration
   */
  function configureSummary() {
    return {
      totalItems: [
        {
          column: "ATT01F07",
          summaryType: "sum",
          displayFormat: "Total Hours: {0}",
          valueFormat: { type: "fixedPoint", precision: 2 },
        },
      ],
    };
  }
});
