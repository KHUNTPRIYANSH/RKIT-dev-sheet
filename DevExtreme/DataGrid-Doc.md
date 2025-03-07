# DevExtreme DataGrid Demo

# 1. Data Binding

## Options Used

1. **columns**: Defines the columns to be displayed in the DataGrid. Each column has a `dataField` and a `caption`.
2. **paging**: Configures the paging options.
   - `pageSize`: Specifies the number of rows per page.
3. **filterRow**: Configures the filter row options.
   - `visible`: Determines whether the filter row is visible.
4. **searchPanel**: Configures the search panel options.
   - `visible`: Determines whether the search panel is visible.
5. **dataSource**: Specifies the data source for the DataGrid.

## Methods Used

1. **dxDataGrid**: Initializes the DataGrid component.
2. **option**: Sets the specified option of the DataGrid instance.

## Events Used

1. **click**: Attaches click event handlers to buttons to perform specific actions.

# 2. Paging

## Options Used

1. **dataSource**: Specifies the data source for the DataGrid.
2. **columns**: Defines the columns to be displayed in the DataGrid. Each column has a `dataField` and a `caption`.
   - **cellTemplate**: Custom template for the cell content.
3. **paging**: Configures the paging options.
   - **enabled**: Enables or disables paging.
   - **pageSize**: Specifies the number of rows per page.
   - **pageIndex**: Specifies the current page index.
4. **pager**: Configures the pager options.
   - **label**: Specifies an aria-label attribute for the pager.
   - **showPageSizeSelector**: Shows or hides the page size selector.
   - **displayMode**: Specifies the display mode of the pager.
     - Options: 'full', 'compact', 'adaptive'
   - **allowedPageSizes**: Specifies the available page sizes.
   - **showNavigationButtons**: Shows or hides the navigation buttons.
   - **showInfo**: Shows or hides the page information.
   - **infoText**: Custom text for page info.
   - **visible**: Shows or hides the pager.
5. **filterRow**: Configures the filter row options.
   - **visible**: Shows or hides the filter row.
6. **searchPanel**: Configures the search panel options.
   - **visible**: Shows or hides the search panel.
7. **showRowLines**: Shows or hides row borders.
8. **showColumnLines**: Shows or hides column borders.

## Methods Used

1. **dxDataGrid**: Initializes the DataGrid component.
2. **option**: Sets the specified option of the DataGrid instance.

# 3. Scrolling

## Options Used

1. **height**: Specifies the height of the DataGrid.
2. **dataSource**: Configures the data source for the DataGrid.
   - **load**: Function to load data using an AJAX request.
3. **scrolling**: Configures the scrolling options.
   - **mode**: Specifies the scrolling mode.
     - Options: 'infinite', 'virtual', 'standard'
   - **useNative**: Enables or disables native scrolling.
     - Options: 'auto', true, false
   - **preloadEnabled**: Enables or disables preloading of adjacent pages for smoother scrolling.
   - **scrollByContent**: Allows scrolling with swipe gestures.
   - **scrollByThumb**: Disables thumb-based scrolling.
   - **showScrollbar**: Controls when the scrollbar is visible.
     - Options: 'always', 'never', 'onHover', 'onScroll'
   - **scrollDirection**: Specifies the scrolling direction (vertical or horizontal).
4. **paging**: Configures the paging options.
   - **enabled**: Enables or disables paging.
5. **columns**: Defines the columns to be displayed in the DataGrid. Each column has a `dataField`, `dataType`, and a `caption`.
6. **filterRow**: Configures the filter row options.
   - **visible**: Shows or hides the filter row.
7. **searchPanel**: Configures the search panel options.
   - **visible**: Shows or hides the search panel.
8. **showRowLines**: Shows or hides row borders.
9. **showColumnLines**: Shows or hides column borders.

## Methods Used

1. **dxDataGrid**: Initializes the DataGrid component.
2. **option**: Sets the specified option of the DataGrid instance.

## Scrolling Properties

- **mode**: Specifies the scrolling mode for the grid.
  - `"infinite"`: Loads data gradually as the user scrolls down.
  - `"standard"`: Loads all data at once.
  - `"virtual"`: Loads only visible rows to optimize performance.
- **scrollByContent**: Allows scrolling by swipe gestures (enabled on touch devices).
  - If set to true, users can swipe to scroll the content.
- **scrollByThumb**: Disables the scrollbar thumb for scrolling.
  - If false, users will not be able to scroll using the scrollbar thumb.
- **showScrollbar**: Controls when the scrollbar is visible.
  - `"onScroll"`: Shows the scrollbar when scrolling.
  - `"onHover"`: Displays the scrollbar when hovering over the grid.
  - `"always"`: Always shows the scrollbar.
  - `"never"`: Hides the scrollbar.
- **useNative**: Enables or disables native scrolling.
  - `"auto"` (default): Uses native scrolling on most platforms.
  - `true`: Forces native scrolling on all platforms.
  - `false`: Forces simulated scrolling on all platforms.

# 4. Editing

## Options Used

1. **dataSource**: Configures the data source for the DataGrid.
2. **keyExpr**: Specifies the key field.
3. **showBorders**: Shows or hides the borders of the DataGrid.
4. **refreshMode**: Specifies the mode for refreshing the grid.
   - Options: 'full', 'reshape', 'repaint'
5. **editing**: Configures the editing options.
   - **allowAdding**: Allows adding new rows.
   - **allowDeleting**: Allows deleting rows.
   - **allowUpdating**: Allows updating rows.
   - **confirmDelete**: Asks for confirmation before deleting a row.
   - **selectTextOnEditStart**: Selects text when editing starts.
   - **startEditAction**: Specifies the action to start editing.
     - Options: 'click', 'dblClick'
   - **texts**: Customizes text messages for various actions.
6. **scrolling**: Configures the scrolling options.
   - **mode**: Specifies the scrolling mode.
     - Options: 'infinite', 'virtual', 'standard'
   - **useNative**: Enables or disables native scrolling.
     - Options: 'auto', true, false
   - **preloadEnabled**: Enables or disables preloading of adjacent pages.
   - **scrollByContent**: Allows scrolling by swipe gestures.
   - **scrollByThumb**: Disables thumb-based scrolling.
   - **showScrollbar**: Controls when the scrollbar is visible.
     - Options: 'always', 'never', 'onHover', 'onScroll'
   - **scrollDirection**: Specifies the scrolling direction (vertical or horizontal).
7. **paging**: Configures the paging options.
   - **enabled**: Enables or disables paging.
8. **columns**: Defines the columns to be displayed in the DataGrid. Each column has a `dataField`, `caption`, and optional `validationRules`.
   - **validationRules**: Sets validation rules for columns.
   - **lookup**: Configures lookup options for a column.
   - **cellTemplate**: Custom template for the cell content.
9. **filterRow**: Configures the filter row options.
   - **visible**: Shows or hides the filter row.
10. **searchPanel**: Configures the search panel options.
    - **visible**: Shows or hides the search panel.
11. **showRowLines**: Shows or hides row borders.
12. **showColumnLines**: Shows or hides column borders.

## Events Used

- **onEditingStart**: Triggered when editing starts.
- **onInitNewRow**: Triggered when a new row is initialized.
- **onRowInserting**: Triggered when a row is being inserted.
- **onRowUpdated**: Triggered when a row is updated.
- **onRowRemoved**: Triggered when a row is removed.
- **onSaved**: Triggered when data is saved.
- **onEditCanceling**: Triggered when edit is being canceled.
- **onEditCanceled**: Triggered when edit is canceled.
- **onOptionChanged**: Triggered when an option is changed.
- **onRowUpdating**: Triggered when a row is being updated.
- **onRowRemoving**: Triggered when a row is being removed.
- **onSaving**: Triggered when changes are being saved.

# 5. Grouping

## Options Used

1. **dataSource**: Configures the data source for the DataGrid.
2. **keyExpr**: Specifies the key field.
3. **showBorders**: Shows or hides the borders of the DataGrid.
4. **editing**: Configures the editing options.
   - **mode**: Specifies the editing mode.
     - Options: 'row', 'batch', 'cell', 'form', 'popup'
   - **allowAdding**: Allows adding new rows.
   - **allowDeleting**: Allows deleting rows.
   - **allowUpdating**: Allows updating rows.
   - **confirmDelete**: Asks for confirmation before deleting a row.
5. **grouping**: Configures the grouping options.
   - **autoExpandAll**: Automatically expands all groups.
   - **expandMode**: Specifies how groups are expanded.
     - Options: 'rowClick', 'buttonClick'
   - **contextMenuEnabled**: Enables context menu for grouping.
   - **texts**: Customizes text messages for grouping actions.
6. **groupPanel**: Configures the group panel options.
   - **emptyPanelText**: Custom text for the empty group panel.
   - **visible**: Shows or hides the group panel.
7. **scrolling**: Configures the scrolling options.
   - **mode**: Specifies the scrolling mode.
     - Options: 'infinite', 'virtual', 'standard'
8. **columns**: Defines the columns to be displayed in the DataGrid. Each column has a `dataField`, `caption`, and optional `validationRules`.
   - **validationRules**: Sets validation rules for columns.
   - **lookup**: Configures lookup options for a column.
   - **setCellValue**: Custom function to set cell value.
9. **onEditorPreparing**: Event to dynamically configure editor options.

## Methods Used

1. **dxDataGrid**: Initializes the DataGrid component.
2. **option**: Sets the specified option of the DataGrid instance.

## Events Used

- **onEditorPreparing**: Triggered before an editor is prepared for a cell.

## Data Structure

### Product Data

```json
[
  {
    "id": 1,
    "emoji": "💻",
    "name": "Laptop",
    "category": "Electronics",
    "subcategory": "Computers",
    "link": "#",
    "price": 1200
  },
  {
    "id": 2,
    "emoji": "📱",
    "name": "Smartphone",
    "category": "Electronics",
    "subcategory": "Mobile Phones",
    "link": "#",
    "price": 800
  },
  {
    "id": 3,
    "emoji": "🎧",
    "name": "Bluetooth Headphones",
    "category": "Electronics",
    "subcategory": "Audio",
    "link": "#",
    "price": 150
  },
  {
    "id": 4,
    "emoji": "👟",
    "name": "Sneakers",
    "category": "Fashion",
    "subcategory": "Footwear",
    "link": "#",
    "price": 100
  },
  {
    "id": 5,
    "emoji": "👖",
    "name": "Jeans",
    "category": "Fashion",
    "subcategory": "Clothing",
    "link": "#",
    "price": 25
  },
  {
    "id": 6,
    "emoji": "👕",
    "name": "T-Shirt",
    "category": "Fashion",
    "subcategory": "Clothing",
    "link": "#",
    "price": 25
  }
]
```

### Category Mapping

```json
[
  {
    "category": "Electronics",
    "subcategories": [
      "Computers",
      "Mobile Phones",
      "Audio",
      "Entertainment",
      "Photography"
    ]
  },
  {
    "category": "Fashion",
    "subcategories": ["Clothing", "Footwear", "Accessories"]
  },
  {
    "category": "Home Appliances",
    "subcategories": [
      "Kitchen Appliances",
      "Cleaning Appliances",
      "Cooling Appliances",
      "Smart Home"
    ]
  },
  { "category": "Furniture", "subcategories": ["Furniture"] },
  { "category": "Sports", "subcategories": ["Sports Equipment"] }
]
```

# 6. Filtering

## Options Used

1. **dataSource**: Configures the data source for the DataGrid.
2. **columns**: Defines the columns to be displayed in the DataGrid. Each column has a `dataField` and a `caption`.
   - **cellTemplate**: Custom template for the cell content.
3. **paging**: Configures the paging options.
   - **enabled**: Enables or disables paging.
   - **pageSize**: Specifies the number of rows per page.
4. **filterPanel**: Configures the filter panel options.
   - **visible**: Shows or hides the filter panel.
   - **customizeText**: Customizes the text in the filter panel.
5. **filterRow**: Configures the filter row options.
   - **visible**: Shows or hides the filter row.
   - **applyFilter**: Specifies when the filter is applied.
     - Options: 'auto', 'onClick'
   - **applyFilterText**: Custom text for the apply filter button.
   - **betweenEndText**: Custom text for the end value in the 'between' filter operation.
   - **betweenStartText**: Custom text for the start value in the 'between' filter operation.
   - **operationDescriptions**: Customizes descriptions for various filter operations.
     - Options: 'between', 'contains', 'equal', 'greaterThan', 'greaterThanOrEqual', 'lessThan', 'lessThanOrEqual', 'notContains', 'notEqual', 'startsWith', 'endsWith'
   - **resetOperationText**: Custom text for the reset filter button.
   - **showAllText**: Custom text for the 'all' option in lookups.
   - **showOperationChooser**: Shows or hides the operation chooser in the filter row.
6. **filterValue**: Specifies the initial filter value.
   - Options: '=', '<>', '<', '>', '<=', '>=', 'between', 'contains', 'notcontains', 'startswith', 'endswith', 'anyof', 'noneof'
7. **searchPanel**: Configures the search panel options.
   - **visible**: Shows or hides the search panel.
8. **showRowLines**: Shows or hides row borders.
9. **showColumnLines**: Shows or hides column borders.

## Methods Used

1. **dxDataGrid**: Initializes the DataGrid component.
2. **option**: Sets the specified option of the DataGrid instance.

# 7. Sorting

## Options Used

1. **dataSource**: Configures the data source for the DataGrid.
2. **sorting**: Configures the sorting options.
   - **mode**: Specifies the sorting mode.
     - Options: 'single', 'multiple', 'none'
   - **ascendingText**: Custom text for ascending sort.
   - **descendingText**: Custom text for descending sort.
   - **clearText**: Custom text for resetting sort.
   - **showSortIndexes**: Shows or hides the sorting order number (only for 'multiple' mode).
3. **columns**: Defines the columns to be displayed in the DataGrid. Each column has a `dataField` and a `caption`.
   - **allowSorting**: Enables or disables sorting for a column.
   - **sortOrder**: Specifies the initial sorting order for a column.
     - Options: 'asc', 'desc'
   - **sortIndex**: Specifies the sorting index for a column.
   - **cellTemplate**: Custom template for the cell content.
4. **showRowLines**: Shows or hides row borders.
5. **showColumnLines**: Shows or hides column borders.

## Methods Used

1. **dxDataGrid**: Initializes the DataGrid component.
2. **clearSorting**: Removes all sorts from the DataGrid.

# 8. Selecting

## Options Used

1. **dataSource**: Configures the data source for the DataGrid.
2. **selection**: Configures the selection options.
   - **mode**: Specifies the selection mode.
     - Options: 'single', 'multiple', 'none'
   - **allowSelectAll**: Allows or disallows selecting all rows.
   - **selectAllMode**: Specifies the mode for selecting all rows.
     - Options: 'page', 'allPages'
   - **showCheckBoxesMode**: Specifies when checkboxes are shown.
     - Options: 'always', 'none', 'onClick', 'onLongTap'
3. **columns**: Defines the columns to be displayed in the DataGrid. Each column has a `dataField` and a `caption`.
   - **cellTemplate**: Custom template for the cell content.
4. **editing**: Configures the editing options.
   - **mode**: Specifies the editing mode.
     - Options: 'row', 'batch', 'cell', 'form', 'popup'
   - **allowUpdating**: Allows updating rows.
   - **startEditAction**: Specifies the action to start editing.
     - Options: 'click', 'dblClick'
5. **showRowLines**: Shows or hides row borders.
6. **showColumnLines**: Shows or hides column borders.

## Methods Used

1. **dxDataGrid**: Initializes the DataGrid component.
2. **clearSelection**: Clears the selection in the DataGrid.

# 9. Column

## Options Used

1. **dataSource**: Configures the data source for the DataGrid.
2. **allowColumnResizing**: Enables or disables column resizing.
3. **columnResizingMode**: Specifies how columns are resized.
   - Options: 'widget', 'nextColumn'
4. **columnMinWidth**: Specifies the minimum width for columns.
5. **showBorders**: Shows or hides the borders of the DataGrid.
6. **columnAutoWidth**: Enables or disables automatic column width adjustment.
7. **allowColumnReordering**: Enables or disables column reordering.
8. **columnFixing**: Configures column fixing options.
   - **enabled**: Enables or disables column fixing.
9. **columns**: Defines the columns to be displayed in the DataGrid. Each column has a `dataField` and a `caption`.
   - **alignment**: Specifies the alignment of the column content.
     - Options: 'center', 'left', 'right'
   - **allowEditing**: Enables or disables editing for the column.
   - **allowFiltering**: Enables or disables filtering for the column.
   - **allowFixing**: Enables or disables fixing for the column.
   - **allowGrouping**: Enables or disables grouping for the column.
   - **allowHeaderFiltering**: Enables or disables header filtering for the column.
   - **allowHiding**: Enables or disables hiding for the column.
   - **allowReordering**: Enables or disables reordering for the column.
   - **allowResizing**: Enables or disables resizing for the column.
   - **allowSearch**: Enables or disables search for the column.
   - **allowSorting**: Enables or disables sorting for the column.
   - **fixed**: Fixes the column in place.
   - **fixedPosition**: Specifies the position of the fixed column.
     - Options: 'left', 'right'
   - **cssClass**: Specifies the CSS class for the column.
   - **filterOperations**: Specifies the filter operations available for the column.
     - Options: '=', '<>', '<', '<=', '>', '>=', 'contains', 'endswith', 'isblank', 'isnotblank', 'notcontains', 'startswith', 'between', 'anyof', 'noneof'
   - **filterValue**: Specifies the default filter value for the column.
   - **showInColumnChooser**: Shows or hides the column in the column chooser.
   - **headerCellTemplate**: Custom template for the header cell.
   - **cellTemplate**: Custom template for the cell content.
   - **type**: Specifies the type of the column.
     - Options: 'adaptive', 'buttons', 'detailExpand', 'groupExpand', 'selection', 'drag'
   - **buttons**: Defines buttons for the column.
     - Options: 'cancel', 'delete', 'edit', 'save', 'undelete'
     - **template**: Custom template for the buttons.

## Methods Used

1. **dxDataGrid**: Initializes the DataGrid component.

# 10. State Persistence

## Options Used

1. **dataSource**: Configures the data source for the DataGrid.
2. **stateStoring**: Configures the state storing options.
   - **type**: Specifies the storage type for state storing.
     - Options: 'custom', 'localStorage', 'sessionStorage'
   - **enabled**: Enables or disables state storing.
   - **storageKey**: Specifies the key used to store the state.
3. **showRowLines**: Shows or hides row borders.
4. **showColumnLines**: Shows or hides column borders.

## Methods Used

1. **dxDataGrid**: Initializes the DataGrid component.
2. **state**: Gets or sets the state of the DataGrid.

## Events Used

- **onClick**: Triggered when a button is clicked.

## Button Actions

1. **Save State**: Saves the current state of the DataGrid to sessionStorage.
2. **Load State**: Loads the saved state from sessionStorage and applies it to the DataGrid.
3. **Reset State**: Resets the DataGrid to its default state.

# 11. Appearance

## Options Used

1. **showRowLines**: Shows or hides row borders.
2. **showColumnLines**: Shows or hides column borders.
3. **rowAlternationEnabled**: Enables or disables row alternation (shading).
4. **showBorders**: Shows or hides borders around the grid.

# 12. Template

## Options Used

1. **dataSource**: Configures the data source for the DataGrid.
2. **columns**: Defines the columns to be displayed in the DataGrid. Each column has a `dataField` and a `caption`.
   - **cellTemplate**: Custom template for the cell content.
   - **headerCellTemplate**: Custom template for the header cell content.
3. **showRowLines**: Shows or hides row borders.
4. **showColumnLines**: Shows or hides column borders.
5. **columnAutoWidth**: Enables or disables automatic column width adjustment.
6. **paging**: Configures the paging options.
   - **enabled**: Enables or disables paging.
   - **pageSize**: Specifies the number of rows per page.
7. **pager**: Configures the pager options.
   - **showPageSizeSelector**: Shows or hides the page size selector.
   - **allowedPageSizes**: Specifies the allowed page sizes.
   - **showInfo**: Shows or hides the page information.
8. **rowAlternationEnabled**: Enables or disables row alternation (shading).
9. **onCellPrepared**: Event to customize cell appearance based on conditions.
10. **dataRowTemplate**: Custom template for the data row.
11. **onToolbarPreparing**: Event to customize the toolbar with additional filters and actions.

## Custom Filters

1. **Category Filter**: A `dxSelectBox` added to the toolbar to filter products by category.
2. **Budget Filter**: A `dxTextBox` added to the toolbar to filter products by price.
3. **Clear Filters Button**: A `dxButton` added to the toolbar to clear all filters.

## Methods Used

1. **dxDataGrid**: Initializes the DataGrid component.
2. **filter**: Applies a filter to the DataGrid.
3. **clearFilter**: Clears all filters from the DataGrid.
4. **clearSearch**: Clears the search filter from the DataGrid.
5. **option**: Sets the specified option of a component instance.

# 13. DataSummaries

## Options Used

1. **dataSource**: Configures the data source for the DataGrid.
2. **selection**: Configures the selection options.
   - **mode**: Specifies the selection mode.
     - Options: 'single', 'multiple', 'none'
3. **columns**: Defines the columns to be displayed in the DataGrid. Each column has a `dataField` and a `caption`.
4. **summary**: Configures the summary options for the DataGrid.
   - **texts**: Customizes text messages for various summary types.
     - Options: 'sum', 'min', 'max', 'avg', 'count', 'custom'
   - **totalItems**: Defines the total summary items for the DataGrid.
     - **column**: Specifies the column for which the summary is calculated.
     - **summaryType**: Specifies the type of summary calculation.
       - Options: 'sum', 'min', 'max', 'avg', 'count', 'custom'
     - **valueFormat**: Specifies the format of the summary value.
     - **alignment**: Specifies the alignment of the summary value.
     - **cssClass**: Specifies the CSS class for the summary value.
     - **displayFormat**: Customizes the display format of the summary value.
     - **skipEmptyValues**: Specifies whether to skip empty values in the summary calculation.
     - **recalculateWhileEditing**: Specifies whether to recalculate the summary while editing.
   - **calculateCustomSummary**: Custom function to calculate custom summaries.
5. **onSelectionChanged**: Event triggered when the selection is changed.

## Custom Summary Calculation

- **SelectedRowsSummary**: Custom summary calculation for selected rows.
  - **summaryType**: 'custom'
  - **calculateCustomSummary**: Calculates the sum of prices for selected rows.

## Methods Used

1. **dxDataGrid**: Initializes the DataGrid component.
2. **refresh**: Refreshes the DataGrid.
3. **isRowSelected**: Checks if a row is selected.

# 14. Master

## Options Used

1. **dataSource**: Configures the data source for the DataGrid.
2. **columns**: Defines the columns to be displayed in the DataGrid. Each column has a `dataField` and a `caption`.
3. **showRowLines**: Shows or hides row borders.
4. **showColumnLines**: Shows or hides column borders.
5. **columnAutoWidth**: Enables or disables automatic column width adjustment.
6. **paging**: Configures the paging options.
   - **enabled**: Enables or disables paging.
   - **pageSize**: Specifies the number of rows per page.
7. **masterDetail**: Configures the master-detail view options.
   - **enabled**: Enables or disables the master-detail view.
   - **template**: Custom template for the master-detail view.

## Master-Detail View

- **template**: Custom template for displaying product reviews in the master-detail view.

## Methods Used

1. **dxDataGrid**: Initializes the DataGrid component.

# 15. Adaptability

## Options Used

1. **dataSource**: Configures the data source for the DataGrid.
2. **columns**: Defines the columns to be displayed in the DataGrid. Each column has a `dataField`, `caption`, `hidingPriority`, and `columnMinWidth`.
   - **dataField**: Specifies the field name from the data source.
   - **caption**: Specifies the column header text.
   - **hidingPriority**: Specifies the priority for hiding the column when screen space is limited.
   - **columnMinWidth**: Specifies the minimum width for the column.
   - **cellTemplate**: Custom template for the cell content.
3. **columnHidingEnabled**: Enables or disables column hiding based on priorities.
4. **showRowLines**: Shows or hides row borders.
5. **showColumnLines**: Shows or hides column borders.

## Methods Used

1. **dxDataGrid**: Initializes the DataGrid component.

# 16. Export

## External Scripts Used

### Excel Export

- [Babel Polyfill](https://cdnjs.cloudflare.com/ajax/libs/babel-polyfill/7.4.0/polyfill.min.js)
- [ExcelJS](https://cdnjs.cloudflare.com/ajax/libs/exceljs/4.1.1/exceljs.min.js)
- [FileSaver.js](https://cdnjs.cloudflare.com/ajax/libs/FileSaver.js/2.0.2/FileSaver.min.js)

### PDF Export

- [jsPDF](https://cdnjs.cloudflare.com/ajax/libs/jspdf/2.5.1/jspdf.umd.min.js)
- [jsPDF-AutoTable](https://cdnjs.cloudflare.com/ajax/libs/jspdf-autotable/3.7.1/jspdf.plugin.autotable.min.js)
- [JSZip](https://cdnjs.cloudflare.com/ajax/libs/jszip/3.10.1/jszip.min.js)

## Options Used

1. **dataSource**: Configures the data source for the DataGrid.
2. **selection**: Configures the selection options.
   - **selectAllMode**: Specifies the selection mode for the "Select All" checkbox.
     - Options: 'allPages', 'page'
   - **showCheckBoxesMode**: Specifies when the selection checkboxes appear.
     - Options: 'always', 'onClick', 'onLongTap', 'none'
   - **mode**: Specifies the selection mode.
     - Options: 'single', 'multiple', 'none'
3. **columns**: Defines the columns to be displayed in the DataGrid. Each column has a `dataField` and a `caption`.
   - **dataField**: Specifies the field name from the data source.
   - **caption**: Specifies the column header text.
   - **cellTemplate**: Custom template for the cell content.
4. **showRowLines**: Shows or hides row borders.
5. **showColumnLines**: Shows or hides column borders.
6. **export**: Configures the export options for the DataGrid.
   - **allowExportSelectedData**: Allows exporting selected data.
   - **enabled**: Enables or disables the export feature.
7. **onToolbarPreparing**: Event to customize the toolbar with additional buttons.
8. **onExporting**: Event to handle the exporting process.

## Methods Used

1. **dxDataGrid**: Initializes the DataGrid component.
2. **option**: Sets the specified option of a component instance.
3. **exportDataGrid**: Exports the DataGrid data to the specified format.
