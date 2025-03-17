$(function () {
    const apiUrl = "https://localhost:44310/get_all_departments";
    const userRole = localStorage.getItem("urole"); // Get user role from localStorage
    const token = localStorage.getItem("utoken"); // JWT token

    // CustomStore for DevExtreme DataGrid
    const customStore = new DevExpress.data.CustomStore({
        key: "T01F01",  // Primary key field (Ensure this matches API)
        loadMode: "raw",
        load: function () {
            return $.ajax({
                url: apiUrl,
                method: "GET",
                headers: { "Authorization": "Bearer " + token },
                dataType: "json"
            }).then(response => {
                console.log("Data Loaded: ", response.Data);
                return response.Data;
            });
        },
        insert: function (values) {
            return $.ajax({
                url: "https://localhost:44310/add_department",
                method: "POST",
                headers: { "Authorization": "Bearer " + token },
                contentType: "application/json",
                data: JSON.stringify({
                    T01F02: values.T01F02,  // Department Name

                })
            }).done(() => {
                $("#dataGridContainer").dxDataGrid("instance").refresh();
            });
        },
        update: function (key, values) {
            return $.ajax({
                url: `https://localhost:44310/update_department`,
                method: "PUT",
                headers: { "Authorization": "Bearer " + token },
                contentType: "application/json",
                data: JSON.stringify({
                    T01F01: key,  // ID must be included in update
                    T01F02: values.T01F02,  // Department Name

                })
            }).done(() => {
                $("#dataGridContainer").dxDataGrid("instance").refresh();
            });
        },
        remove: function (key) {
            return $.ajax({
                url: `https://localhost:44310/delete_department?id=${key}`,
                method: "DELETE",
                headers: { "Authorization": "Bearer " + token }
            }).done(() => {
                $("#dataGridContainer").dxDataGrid("instance").refresh();
            });
        }
    });

    // Configure the DataGrid
    $("#dataGridContainer").dxDataGrid({
        dataSource: customStore,
        keyExpr: "T01F01",
        columns: [
            { dataField: "T01F01", dataType: "number", caption: "ID", allowEditing: false },
            { dataField: "T01F02", caption: "Department Name", validationRules: [{ type: "required" }] },
            //{ dataField: "T01F03", caption: "Is Active", dataType: "boolean" },
        ],

        rowAlternationEnabled: true, // Show one dark and one light row

        editing: {
            mode: "popup",
            allowAdding: userRole === "Admin" || userRole === "Editor",
            allowUpdating: userRole === "Admin" || userRole === "Editor",
            allowDeleting: userRole === "Admin"
        },
        paging: {
            enabled: true, //Default Value: true
            pageSize: 5,  // Set the default page size
            pageIndex: 0, // Set the default page index [zero based index]
        },

        pager: {
            label: "Departments Page Navigation",
            showPageSizeSelector: true, // Enable page size selector 
            // displayMode: 'full', // [default 'full'] 'compact' | 'full' |  'adaptive' 
            // displayMode: 'compact',
            displayMode: 'adaptive', //The "adaptive" display mode switches between these two modes based on the component width.
            allowedPageSizes: [5, 10, 'all'], // [default 'auto'] Available page sizes
            showNavigationButtons: true, // Enable navigation buttons
            showInfo: true, // shows summary of page like [Page 1 of 10 (100 items)]
            infoText: "Page {0} of {1} ({2} Departments )", // Custom text for page info
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
                console.log(e)
                return "Filter by " + e.text;
            },
            // filterEnabled: false,
        },
        filterRow: { visible: true },
        searchPanel: { visible: true },
        showBorders: true,

        summary: {
            //"sum","min","max","avg","count","custom"

            totalItems: [{
                column: "T01F01",
                summaryType: "count",
                alignment: "right",
                cssClass: "totalCount",
                recalculateWhileEditing: true,
            },


            ]
        }
    });
});