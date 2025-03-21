$(function () {
    window.jsPDF = window.jspdf.jsPDF;

    const pdfExporter = (e) => {
      const doc = new jsPDF();

      DevExpress.pdfExporter
        .exportDataGrid({
          jsPDFDocument: doc,
          component: e.component,
          indent: 5,
        })
        .then(() => {
          doc.save("Attendance.pdf");
        });
    };
    const excelExporter = (e) => {
      var wb = new ExcelJS.Workbook();
      console.log(e);
      var worksheet = wb.addWorksheet("Main sheet");
      DevExpress.excelExporter
        .exportDataGrid({
          worksheet: worksheet,
          component: e.component,
          customizeCell: function (options) {
            var excelCell = options;
            excelCell.alignment = { horizontal: "center" };
            excelCell.font = { name: "Arial", size: 17 };
          },
        })
        .then(function () {
          wb.xlsx.writeBuffer().then(function (buffer) {
            saveAs(
              new Blob([buffer], { type: "application/octet-stream" }),
              "Attendance.xlsx"
            );
          });
        });
      e.cancel = true;
    };
    const apiUrl = "https://localhost:44310/";
    const token = localStorage.getItem("utoken");
    const userRole = localStorage.getItem("urole");
    const userId = localStorage.getItem("uid");

    const attendanceStore = new DevExpress.data.CustomStore({
      key: "ATT01F01",
      load: () =>
        $.ajax({
          url: apiUrl + "get_all_attendance",
          headers: { Authorization: "Bearer " + token },
        }).then((res) => res.Data),

      insert: (values) => {
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

        return $.ajax({
          url: apiUrl + "add_attendance",
          method: "POST",
          headers: {
            Authorization: "Bearer " + token,
            "Content-Type": "application/json",
          },
          data: JSON.stringify(data),
        });
      },

      update: (key, values) => {
        const data = {
          ATT01F01: key,
          ATT01F05: values.ATT01F05 || null,
          ATT01F06: values.ATT01F06 || null,
          ATT01F08: values.ATT01F08,
          ATT01F09: values.ATT01F09,
        };

        return $.ajax({
          url: apiUrl + "update_attendance",
          method: "PUT",
          headers: {
            Authorization: "Bearer " + token,
            "Content-Type": "application/json",
          },
          data: JSON.stringify(data),
        });
      },

      remove: (key) =>
        $.ajax({
          url: apiUrl + "delete_attendance?id=" + key,
          method: "DELETE",
          headers: { Authorization: "Bearer " + token },
        }),
    });

    function formatDate(date) {
      return date.toISOString().split("T")[0];
    }
    const filterValue =
      userRole === "User" ? ["ATT01F02", "=", userId] : null;

    let dataGridRef = $("#gridContainer")
      .dxDataGrid({
        dataSource: attendanceStore,
        filterValue: filterValue,
        editing: {
          mode: "popup",
          popup: {
            title: "Attendance Record",
            fullScreen: true,
            showTitle: true,
            position: {
              my: "center",
              at: "center",
              of: window,
            },
          },
          allowAdding: userRole === "Admin" || userRole === "Editor",
          allowUpdating: userRole === "Admin" || userRole === "Editor",
          allowDeleting: userRole === "Admin",
          form: {
            items: [
              "ATT01F02",
              "ATT01F03",
              {
                dataField: "ATT01F04",
                editorType: "dxDateBox",
                editorOptions: {
                  type: "date",
                  width: "100%",
                  displayFormat: "yyyy-MM-dd",
                  value: new Date(),
                },
              },
              {
                dataField: "ATT01F05",
                editorType: "dxTextBox",
                editorOptions: {
                  mask: "Hh:Mm:Ss",
                  showClearButton: true,
                  useMaskedValue: true,
                  maskRules: {
                    H: (char) => char >= 0 && char <= 2,
                    h: (char, index, fullStr) => {
                      if (fullStr[0] === "2") {
                        return [0, 1, 2, 3].includes(parseInt(char));
                      }
                      return [0, 1, 2, 3, 4, 5, 6, 7, 8, 9].includes(
                        parseInt(char)
                      );
                    },
                    M: (char) => char >= 0 && char <= 5,
                    m: (char) => char >= 0 && char <= 9,
                    S: (char) => char >= 0 && char <= 5,
                    s: (char) => char >= 0 && char <= 9,
                  },
                  placeholder: "Enter time (HH:MM:SS)...",
                },
              },
              {
                dataField: "ATT01F06",
                editorType: "dxTextBox",
                editorOptions: {
                  mask: "Hh:Mm:Ss",
                  showClearButton: true,
                  useMaskedValue: true,
                  maskRules: {
                    H: (char) => char >= 0 && char <= 2,
                    h: (char, index, fullStr) => {
                      if (fullStr[0] === "2") {
                        return [0, 1, 2, 3].includes(parseInt(char));
                      }
                      return [0, 1, 2, 3, 4, 5, 6, 7, 8, 9].includes(
                        parseInt(char)
                      );
                    },
                    M: (char) => char >= 0 && char <= 5,
                    m: (char) => char >= 0 && char <= 9,
                    S: (char) => char >= 0 && char <= 5,
                    s: (char) => char >= 0 && char <= 9,
                  },
                  placeholder: "Enter time (HH:MM:SS)...",
                },
              },
              "ATT01F08",
              "ATT01F09",
            ],
          },
        },
        columns: [
          {
            dataField: "ATT01F01",
            caption: "ID",
            allowEditing: false,
          },
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
          {
            dataField: "ATT01F05",
            caption: "Check-In",
            dataType: "string",
          },
          {
            dataField: "ATT01F06",
            caption: "Check-Out",
            dataType: "string",
          },
          {
            dataField: "ATT01F07",
            caption: "Work Hours",
            allowEditing: false,
            calculateCellValue: (data) => {
              if (!data.ATT01F05 || !data.ATT01F06) return 0;
              const [h1, m1, s1] = data.ATT01F05.split(":").map(Number);
              const [h2, m2, s2] = data.ATT01F06.split(":").map(Number);
              const diff =
                h2 * 3600 + m2 * 60 + s2 - (h1 * 3600 + m1 * 60 + s1);
              return (diff / 3600).toFixed(2);
            },
          },
          {
            dataField: "ATT01F08",
            caption: "Status",
            lookup: {
              dataSource: ["Present", "Absent", "Half-Day", "Leave"],
            },
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
          {
            dataField: "ATT01F09",
            caption: "Remarks",
          },
        ],
        onRowInserted: () =>
          $("#gridContainer").dxDataGrid("instance").refresh(),
        onRowUpdated: () =>
          $("#gridContainer").dxDataGrid("instance").refresh(),
        onRowRemoved: () =>
          $("#gridContainer").dxDataGrid("instance").refresh(),
        onCellPrepared: (e) => {
          if (e.rowType === "data") {
            if (e.column.dataField === "ATT01F10" && e.value) {
              e.cellElement.addClass("late-entry");
            }
            if (e.column.dataField === "ATT01F07" && e.value > 0) {
              e.cellElement.addClass("on-time");
            }
          }
        },
        export: {
          allowExportSelectedData: true,
          enabled: true,
        },
        onToolbarPreparing: function (e) {
          console.log(e);

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
              // component.exportToExcel
            },
          });
        },
        onExporting: excelExporter,
        summary: {
          totalItems: [
            {
              column: "ATT01F07",
              summaryType: "sum",
              displayFormat: "Total Hours: {0}",
              valueFormat: { type: "fixedPoint", precision: 2 },
            },
          ],
        },
      })
      .dxDataGrid("instance");
  });