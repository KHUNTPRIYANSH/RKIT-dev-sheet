$(function () {
    const apiUrl = "https://localhost:44310/get_all_employees";
    const token = localStorage.getItem("utoken");
    const userRole = localStorage.getItem("urole");

    // Define the CustomStore for the DataGrid
    const customStore = new DevExpress.data.CustomStore({
      key: "P01F01",
      loadMode: "raw",
      load: function () {
        return $.ajax({
          url: apiUrl,
          method: "GET",
          headers: { Authorization: "Bearer " + token },
          dataType: "json",
        }).then((response) => {
          console.log("Data Loaded: ", response.Data);
          return response.Data;
        });
      },
      insert: function (values) {
        console.log("Insert Values:", values);
        let serData = JSON.stringify({
          P01F01: 0, // Employee ID (Auto-generated)
          P01F02: values.P01F02, // First Name
          P01F03: values.P01F03, // Last Name
          P01F04: values.P01F04, // Date of Birth
          P01F05: values.P01F05, // Department ID
          P01F06: values.P01F06, // Position
          P01F08: values.P01F08, // Salary
          P01F09: values.P01F09, // User ID
        });
        console.log("Serialized Data:", serData);
        return $.ajax({
          url: "https://localhost:44310/add_employee",
          method: "POST",
          headers: { Authorization: "Bearer " + token },
          contentType: "application/json",
          data: serData,
        })
          .done(() => {
            $("#dataGridContainer").dxDataGrid("instance").refresh();
          })
          .fail((jqXHR, textStatus, errorThrown) => {
            console.error(
              "Insert Error:",
              textStatus,
              errorThrown,
              jqXHR.responseText
            );
          });
      },
      update: function (key, values) {
        console.log("Update Values:", values);
        // alert("Update Values:", values);
        // Fetch the existing record first
        return $.ajax({
          url: `https://localhost:44310/get_employee_by_id?id=${key}`,
          method: "GET",
          headers: { Authorization: "Bearer " + token },
          dataType: "json",
        })
          .then((res) => {
            // Merge existing fields with the new values
            console.log("res", res);
            let existingRecord = res.Data;
            console.log("Existing Record:", existingRecord);
            let updatedRecord = {
              P01F01: existingRecord.P01F01, // Employee ID
              P01F02:
                values.P01F02 !== undefined
                  ? values.P01F02
                  : existingRecord.P01F02, // First Name
              P01F03:
                values.P01F03 !== undefined
                  ? values.P01F03
                  : existingRecord.P01F03, // Last Name
              P01F04:
                values.P01F04 !== undefined
                  ? values.P01F04
                  : existingRecord.P01F04, // Date of Birth
              P01F05:
                values.P01F05 !== undefined
                  ? values.P01F05
                  : existingRecord.P01F05, // Department ID
              P01F06:
                values.P01F06 !== undefined
                  ? values.P01F06
                  : existingRecord.P01F06, // Position
              P01F08:
                values.P01F08 !== undefined
                  ? values.P01F08
                  : existingRecord.P01F08, // Salary
              P01F09:
                values.P01F09 !== undefined
                  ? values.P01F09
                  : existingRecord.P01F09, // Salary
            };

            console.log("Merged Update Data:", updatedRecord);

            return $.ajax({
              url: "https://localhost:44310/update_employee",
              method: "PUT",
              headers: { Authorization: "Bearer " + token },
              contentType: "application/json",
              data: JSON.stringify(updatedRecord),
            });
          })
          .done(() => {
            $("#dataGridContainer").dxDataGrid("instance").refresh();
          })
          .fail((jqXHR, textStatus, errorThrown) => {
            console.error(
              "Update Error:",
              textStatus,
              errorThrown,
              jqXHR.responseText
            );
          });
      },
      remove: function (key) {
        console.log("Delete Employee ID:", key);

        return $.ajax({
          url: `https://localhost:44310/delete_employee?id=${key}`,
          method: "DELETE",
          headers: { Authorization: "Bearer " + token },
        })
          .done(() => {
            $("#dataGridContainer").dxDataGrid("instance").refresh();
          })
          .fail((jqXHR, textStatus, errorThrown) => {
            console.error(
              "Delete Error:",
              textStatus,
              errorThrown,
              jqXHR.responseText
            );
          });
      },
    });

    // Configure the DataGrid
    $("#dataGridContainer").dxDataGrid({
      dataSource: customStore,
      keyExpr: "P01F01",
      columns: [
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
          groupIndex: 0,
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
      ],

      rowAlternationEnabled: true, // Show one dark and one light row

      editing: {
        mode: "popup",
        allowAdding: userRole === "Admin" || userRole === "Editor",
        allowUpdating: userRole === "Admin" || userRole === "Editor",
        allowDeleting: userRole === "Admin",
      },
      paging: {
        enabled: true, //Default Value: true
        pageSize: 5, // Set the default page size
        pageIndex: 0, // Set the default page index [zero based index]
      },

      pager: {
        label: "Employeess Page Navigation",
        showPageSizeSelector: true, // Enable page size selector
        // displayMode: 'full', // [default 'full'] 'compact' | 'full' |  'adaptive'
        // displayMode: 'compact',
        displayMode: "adaptive", //The "adaptive" display mode switches between these two modes based on the component width.
        allowedPageSizes: [5, 10, "all"], // [default 'auto'] Available page sizes
        showNavigationButtons: true, // Enable navigation buttons
        showInfo: true, // shows summary of page like [Page 1 of 10 (100 items)]
        infoText: "Page {0} of {1} ({2} Employeess )", // Custom text for page info
        visible: true, // [default] Show pager at the bottom of the DataGrid
      },
      grouping: {
        // allowCollapsing: true,
        autoExpandAll: false,
        expandMode: "rowClick", // "rowClick" | "buttonClick"
        contextMenuEnabled: true, // when true , we can right click on header of column to group
      },
      groupPanel: {
        // allowColumnDragging: false,
        emptyPanelText: "Drag a column header here to group by that column",
        visible: true,
      },
      scrolling: {
        mode: "standard", // Renders only the rows within the viewport
        scrollByContent: false, // [works only if height is given] Allows scrolling with swipe gesture
        scrollByThumb: true, // [works only if height is given] Disables thumb-based scrolling
        showScrollbar: "onHover", // [works only if height is given]  'always' | 'never' | 'onHover' | 'onScroll'
        scrollDirection: "vertical", // Defines vertical scrolling direction
      },
      filterRow: { visible: true },
      searchPanel: { visible: true },

      filterPanel: {
        visible: true,
        customizeText: function (e) {
          console.log(e);
          return "Filter by " + e.text;
        },
        // filterEnabled: false,
      },

      filterRow: {
        visible: true,
        applyFilter: "onClick", // "auto"
        applyFilterText: "Apply Filter", // Custom text for the apply filter button
        betweenEndText: "To", // Custom text for the end value in the 'between' filter operation
        betweenStartText: "From", // Custom text for the start value in the 'between' filter operation
        resetOperationText: "Reset", // Custom text for the reset filter button
        showAllText: "Show All", // for lookup we get (ALL) as placeholder we can change it by this value
        showOperationChooser: true, // Show the operation chooser in the filter row
      },
      filterValue: ["P01F01", ">", 7],

      selection: {
        mode: "multiple", // single | multiple | none (default)
        allowSelectAll: true, // can/can't select all
        selectAllMode: "page", // page | allPages (default)
        showCheckBoxesMode: "always", // always | none | onClick (default) | onLongTap
      },
      onCellPrepared: (e) => {
        if (e.rowType === "data" && e.column.dataField === "P01F08") {
          if (e.data.P01F08 < 15000) {
            e.cellElement.addClass("bpl");
          } else if (e.data.P01F08 >= 30000 && e.data.P01F08 < 60000) {
            e.cellElement.addClass("middleClass");
          } else {
            e.cellElement.addClass("rich");
          }
        }
      },
      onSelectionChanged: (e) => {
        var data = e.selectedRowsData;
        var removedItems = e.removedItems; // Tracks removed items
        var previousSelectionCount =
          e.component.getSelectedRowKeys().length;

        console.log(data);

        // Clear selection info
        $("#selectionInfo").html("");

        if (data && data.length > 0) {
          let curr = data[data.length - 1];
          data.forEach((element) => {
            $("#selectionInfo").append(
              `<div>${element.P01F01} from department[${element.P01F05}] added into the selection list</div>`
            );
          });

          // Show success notification for the last selected item
          DevExpress.ui.notify(
            `${curr.P01F02} added successfully!`,
            "success",
            1500
          );
        }

        // Notify when items are deselected (removed from selection)
        if (removedItems && removedItems.length > 0) {
          removedItems.forEach((item) => {
            DevExpress.ui.notify(
              `${item.P01F02} removed from selection list successfully!`,
              "error",
              1500
            );
          });
        }

        // If no items are left in the cart
        if (previousSelectionCount === 0 && (!data || data.length === 0)) {
          DevExpress.ui.notify("Selection list is empty!", "error", 1500);
        }
      },

      onToolbarPreparing: (e) => {
        var toolbarItems = e.toolbarOptions.items;

        toolbarItems.push({
          widget: "dxButton",
          options: {
            text: "Clear Selection",
            onClick: function () {
              grid2Instance.clearSelection();
            },
          },
          location: "after",
        });
      },
      showBorders: true,
      onRowInserted: function (e) {
        console.log("Row Inserted:", e.data);
        e.component.refresh();
      },
      onRowUpdated: function (e) {
        console.log("Row Updated:", e.data);
        e.component.refresh();
      },
      onRowRemoved: function (e) {
        console.log("Row Removed:", e.data);
        e.component.refresh();
      },
      summary: {
        //"sum","min","max","avg","count","custom"

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
          {},
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
          {},
        ],
      },

      export: {
        enabled: true,
        allowExportSelectedData: true,
      },
      onExporting(e) {
        const workbook = new ExcelJS.Workbook();
        const worksheet = workbook.addWorksheet("Employees");

        DevExpress.excelExporter
          .exportDataGrid({
            component: e.component,
            worksheet,
            autoFilterEnabled: true, // to get header filter in excel
          })
          .then(() => {
            workbook.xlsx.writeBuffer().then((buffer) => {
              saveAs(
                new Blob([buffer], { type: "application/octet-stream" }),
                "Users.xlsx"
              );
            });
          });
      },
    });
  });