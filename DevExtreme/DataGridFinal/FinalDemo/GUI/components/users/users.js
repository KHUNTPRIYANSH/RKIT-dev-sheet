$(function () {
    const apiUrl = "https://localhost:44310/get_all_users";
    const token = localStorage.getItem("utoken");

    const userRole = localStorage.getItem("urole"); // Get user role from localStorage

    // Define the CustomStore for the DataGrid
    const customStore = new DevExpress.data.CustomStore({
      key: "R01F01",
      loadMode: "raw",

      load: function () {
        return $.ajax({
          url: apiUrl,
          headers: {
            Authorization: "Bearer " + localStorage.getItem("utoken"),
          },
          method: "GET",
          dataType: "json",
        }).then((res) => {
          console.log("Data : ");
          console.log(res.Data);
          return res.Data;
        });
      },
      insert: function (values) {
        return $.ajax({
          url: "https://localhost:44310/add_user",
          method: "POST",
          headers: {
            Authorization: "Bearer " + localStorage.getItem("utoken"),
          },
          contentType: "application/json",
          data: JSON.stringify(values),
        });
      },
      update: function (key, values) {
        console.log("Update Values:", values);
        // alert("Update Values:", values);
        // Fetch the existing record first
        return $.ajax({
          url: `https://localhost:44310/get_user_by_id?id=${key}`,
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
              R01F01: existingRecord.R01F01,
              R01F02:
                values.R01F02 !== undefined
                  ? values.R01F02
                  : existingRecord.R01F02,
              R01F03:
                values.R01F03 !== undefined
                  ? values.R01F03
                  : existingRecord.R01F03,
              R01F04:
                values.R01F04 !== undefined
                  ? values.R01F04
                  : existingRecord.R01F04,
            };

            console.log("Merged Update Data:", updatedRecord);

            return $.ajax({
              url: "https://localhost:44310/update_user",
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
        return $.ajax({
          url: `https://localhost:44310/delete_user?id=${key}`,
          headers: {
            Authorization: "Bearer " + localStorage.getItem("utoken"),
          },
          method: "DELETE",
        });
      },
    });
    // key to role mapping
    let roleToVal = [
      { key: 3, role: "Admin" },
      { key: 2, role: "Editor" },
      { key: 1, role: "User" },
    ];
    // Configure the DataGrid
    $("#dataGridContainer").dxDataGrid({
      dataSource: customStore,
      keyExpr: "R01F01",
      columns: [
        {
          dataField: "R01F01",
          dataType: "number",
          caption: "ID",
          allowEditing: false,
          fixed: true,
          cssClass: "grd-2",
        },
        { dataField: "R01F02", caption: "User Name" },
        {
          dataField: "R01F03",
          caption: "Password",
          dataType: "string",
          visible: false,
        },
        {
          dataField: "R01F04",
          caption: "Role",
          lookup: {
            dataSource: roleToVal,
            valueExpr: "key",
            displayExpr: "role",
          },
          groupIndex: 0, // group by roles 
        },
      ],

      rowAlternationEnabled: true, // Show one dark and one light row

      editing: {
        mode: "popup",
        allowAdding: userRole === "Admin" || userRole === "Editor",
        allowUpdating: userRole === "Admin" ,
        allowDeleting: userRole === "Admin",
      },
      paging: {
        enabled: true, //Default Value: true
        pageSize: 5, // Set the default page size
        pageIndex: 4, // Set the default page index [zero based index]
      },

      pager: {
        label: "Users Page Navigation",
        showPageSizeSelector: true, // Enable page size selector
        // displayMode: 'full', // [default 'full'] 'compact' | 'full' |  'adaptive'
        // displayMode: 'compact',
        displayMode: "adaptive", //The "adaptive" display mode switches between these two modes based on the component width.
        allowedPageSizes: [5, 10, "all"], // [default 'auto'] Available page sizes
        showNavigationButtons: true, // Enable navigation buttons
        showInfo: true, // shows summary of page like [Page 1 of 10 (100 items)]
        infoText: "Page {0} of {1} ({2} Users )", // Custom text for page info
        visible: true, // [default] Show pager at the bottom of the DataGrid
      },

      scrolling: {
        mode: "standard", // Renders only the rows within the viewport
        scrollByContent: false, // [works only if height is given] Allows scrolling with swipe gesture
        scrollByThumb: true, // [works only if height is given] Disables thumb-based scrolling
        showScrollbar: "onHover", // [works only if height is given]  'always' | 'never' | 'onHover' | 'onScroll'
        scrollDirection: "vertical", // Defines vertical scrolling direction
      },

      filterPanel: {
        visible: true,
        customizeText: function (e) {
          console.log(e);
          return "Filter by " + e.text;
        },
        // filterEnabled: false,
      },

      stateStoring: {
        type: "sessionStorage", //  'custom' | 'localStorage' (default)| 'sessionStorage'
        enabled: true,
        storageKey: "lastGridState",
      },
      filterRow: { visible: true },
      searchPanel: { visible: true },
      showBorders: true,
      onRowInserted: function (e) {
        e.component.refresh();
      },
      onRowUpdated: function (e) {
        e.component.refresh();
      },
      onRowRemoved: function (e) {
        e.component.refresh();
      },
      export: {
        enabled: true,
        allowExportSelectedData: true,
      },
      // handling sheet export
      onExporting(e) {
        const workbook = new ExcelJS.Workbook();
        const worksheet = workbook.addWorksheet("Users");

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

    // Save State Button Click Event
    $("#saveStateButton").on("click", function () {
      var state = dataGridRef.state(); // gets the current state
      console.log(state);
      DevExpress.ui.notify("State Saved", "success", 500);
    });

    // Load State Button Click Event
    $("#loadStateButton").on("click", function () {
      var state = JSON.parse(sessionStorage.getItem("lastGridState"));
      dataGridRef.state(state);
      DevExpress.ui.notify("State Loaded", "success", 500);
    });

    // Reset State Button Click Event
    $("#resetStateButton").on("click", function () {
      dataGridRef.state(null);
      DevExpress.ui.notify("State Reset", "success", 500);
    });
  });