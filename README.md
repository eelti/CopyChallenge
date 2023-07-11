# CopyChallenge
## dotNet core DevExpress DataGrid create a feature for copy past between cells

Features:
- From a button
  - Copy 1 cell to aother cell
  - Copy 1 cell to many cell in same row
  - Copy 1 cell to many cell/row(s)
  - From one row to many cells 
  - Copy many cells from many rows
- From selection 
  - Copy 1 cell to aother cell
  - Copy 1 cell to many cell in same row
  - Copy 1 cell to many cell/row(s)
  - Copy many cells from one row
  - Copy many cells from many rows
 
For the function to clone a row see this exemple I just implement:
```
        columns.Add()
            .Type(GridCommandColumnType.Buttons)
            .Width(110)
            .Buttons(b => {
                              b.Add().Name(GridColumnButtonName.Edit);
                              b.Add().Name(GridColumnButtonName.Delete);
                              b.Add()
                                  .Hint("Clone")
                                  .Icon("copy")
                                  /*.Visible(new JS("isCloneIconVisible"))
                                  .Disabled(new JS("isCloneIconDisabled"))*/
                                  .OnClick("onCloneIconClick");
            });
    })
    .Editing(e => e
        .AllowAdding(true)
        .AllowUpdating(true)
        .AllowDeleting(true)
    .UseIcons(true)
    )
)

<script>
    function onCloneIconClick(e) {
        var clonedItem = $.extend({}, e.row.data);
        var clonedItem = $.extend({}, e.row.data, { Project_FTE_Mapping_BD_ActivityDesc_ID: null });
        var dataGrid = $("#Mapping_BD_ActivityDesc").dxDataGrid({
        }).dxDataGrid("instance");
        var dataSource = dataGrid.getDataSource();
        dataSource.store().insert(clonedItem).then(function () {
            dataSource.reload();
        });

    }
```

https://supportcenter.devexpress.com/ticket/details/t1085435/datagrid-for-devextreme-how-to-allow-users-select-multiple-cells

This what I found like to use selection from the devExpress forum
```
@using ASPMultipleSelection.Models

@section styles{

    <style>

        .cell-selected {

            background-color: lightgreen;

        }


        .dx-datagrid {

            user-select: none;

        }


        .selected-data {

            margin-top: 10px;

            overflow-y: scroll;

            max-height: 200px;

        }

    </style>

}

@(Html.DevExtreme().DataGrid<Customer>()

    .ID("gridContainer")

    .ShowBorders(true)

    .DataSource(d => d.Mvc().Controller("SampleData").LoadAction("Get").Key("ID"))

    .Columns(col =>

    {

        col.AddFor(m => m.CompanyName);

        col.AddFor(m => m.City);

        col.AddFor(m => m.State);

        col.AddFor(m => m.Phone);

        col.AddFor(m => m.Fax);

    })

    .OnCellPrepared("onCellPrepared")

    .OnCellClick("onCellClick")

    .OnCellHoverChanged("onCellHoverChanged")

    .OnContentReady("onContentReady")

)

<div class="selected-data">

    <h3 class="caption">Selected Cells:</h3>

    <ul></ul>

</div>


<script>

    let selectedRange = {};

    let shiftSelectRange = {};

    let cellsInfo = [];

    let dataGrid;

    let data = [];

    function foreachRange(selectedRange, func) {

        data = [];

        if (selectedRange.startRowIndex >= 0) {

            let minRowIndex = Math.min(selectedRange.startRowIndex, selectedRange.endRowIndex);

            let maxRowIndex = Math.max(selectedRange.startRowIndex, selectedRange.endRowIndex);

            let minColumnIndex = Math.min(selectedRange.startColumnIndex, selectedRange.endColumnIndex);

            let maxColumnIndex = Math.max(selectedRange.startColumnIndex, selectedRange.endColumnIndex);


            for (let rowIndex = minRowIndex; rowIndex <= maxRowIndex; rowIndex++) {

                for (let columnIndex = minColumnIndex; columnIndex <= maxColumnIndex; columnIndex++) {

                    func(rowIndex, columnIndex);

                    data.push({ rowIndex: rowIndex, columnIndex: columnIndex })

                }

            }

        }

    }

    function showSelection(component, selectedRange) {

        component.element().find(".cell-selected").removeClass("cell-selected");

        //You can then get the cell's value here

        foreachRange(selectedRange, function (rowIndex, columnIndex) {

            component.getCellElement(rowIndex, columnIndex).addClass("cell-selected");

        });

        renderList();

    }

    function renderList() {

        $(".selected-data ul").html(null);

        data.map((item => {

            $(".selected-data ul").append($("<li>").text(dataGrid.cellValue(item.rowIndex, item.columnIndex)))

        }))

    }

    function onContentReady(e) {

        data = [];

        renderList();

    }

    function onCellPrepared(e) {

        if (e.rowType === "data") cellsInfo.push(

            {

                cellElement: e.cellElement[0],

                rowIndex: e.rowIndex,

                columnIndex: e.columnIndex,

            }

        );

        e.cellElement.on("touchstart", function (args) { //Save the first touched cell

            selectedRange.startRowIndex = selectedRange.endRowIndex = e.rowIndex;

            selectedRange.startColumnIndex = selectedRange.endColumnIndex = e.columnIndex;

            showSelection(e.component, selectedRange);

        })

    }

    function onCellClick(e) {

        if (e.rowType !== "data") return;

        if (e.event.ctrlKey === true && e.event.shiftKey === false) { //selects or deselects a single cell when Ctrl + Left Click

            if (e.cellElement.hasClass("cell-selected")) {

                data = data.filter(item => !(item.rowIndex === e.rowIndex && item.columnIndex === e.columnIndex));

                e.cellElement.removeClass("cell-selected");

            } else {

                data.push({ rowIndex: e.rowIndex, columnIndex: e.columnIndex })

                e.cellElement.addClass("cell-selected");

            }

            renderList();

        }

        else if (e.event.ctrlKey === false && e.event.shiftKey === true) { //Selection via Shift + Left Click


            shiftSelectRange.endRowIndex = e.rowIndex;

            shiftSelectRange.endColumnIndex = e.columnIndex;

            showSelection(e.component, shiftSelectRange);


        }

        else {

            shiftSelectRange.startRowIndex = e.rowIndex;

            shiftSelectRange.startColumnIndex = e.columnIndex;

            selectedRange.startRowIndex = selectedRange.endRowIndex = e.rowIndex;

            selectedRange.startColumnIndex = selectedRange.endColumnIndex = e.columnIndex;

            isSelectionStopped = false;

            showSelection(e.component, selectedRange);

        }

    }


    function onCellHoverChanged(e) {

        let event = e.event;

        if (event.buttons === 1) {

            if (isSelectionStopped) {

                isSelectionStopped = false;

                selectedRange = {};

            }

            if (selectedRange.startRowIndex === undefined) {

                selectedRange.startRowIndex = e.rowIndex;

            }

            if (selectedRange.startColumnIndex === undefined) {

                selectedRange.startColumnIndex = e.columnIndex;

            }


            selectedRange.endRowIndex = e.rowIndex;

            selectedRange.endColumnIndex = e.columnIndex;


            showSelection(e.component, selectedRange);


        }

        else {

            isSelectionStopped = true;

        }

    }


    $(() => {

        dataGrid = $('#gridContainer').dxDataGrid("instance");

        $('#gridContainer').on("touchmove", function (args) { //Attach a touchmove event to save the last touched cell

            let event = args.touches[0]

            let element = document.elementFromPoint(event.clientX, event.clientY);

            let cellInfo = cellsInfo.filter(x => x.cellElement === element)[0];

            if (cellInfo) {

                selectedRange.endRowIndex = cellInfo.rowIndex;

                selectedRange.endColumnIndex = cellInfo.columnIndex;

                showSelection(dataGrid, selectedRange);

            }

        });

    })

</script>
```
