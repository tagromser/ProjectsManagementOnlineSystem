/**
 * Осуществляет инициализацию модулей.
 */
var initialize = function () {
    initializeViewIndex();
};
/**
 * Инициализирует скрипт выполняемый на главной странице с таблицей.
 */
function initializeViewIndex() {
    var viewIndex = new ViewIndex("#tool-strip-edit-button", "#tool-strip-delete-button", "#tool-strip-details-button", "#tool-strip-workers-button", "#main-table");
}
var ViewIndex = /** @class */ (function () {
    /**
     * Конструктор.
     * @param editButtonQuery - Имя элемента содержащего кнопку "Редактировать".
     * @param deleteButtonQuery - Имя элемента содержащего кнопку "Удалить".
     * @param detailsButtonQuery - Имя элемента содержащего кнопку "Подробнее".
     * @param workersButtonQuery - Имя элемента содержащего кнопку "Работники проекта".
     * @param tableQuery - Имя элемента содержащего таблицу с данными.
     */
    function ViewIndex(editButtonQuery, deleteButtonQuery, detailsButtonQuery, workersButtonQuery, tableQuery) {
        var editButton = document.querySelector(editButtonQuery);
        var deleteButton = document.querySelector(deleteButtonQuery);
        var detailsButton = document.querySelector(detailsButtonQuery);
        var workersButton = document.querySelector(workersButtonQuery);
        ;
        var table = document.querySelector(tableQuery);
        //Если элементы не найдены.
        //if (!editButton || !deleteButton || !detailsButton || !workersButton || !table)
        //    return;
        this.editButton = editButton;
        this.deleteButton = deleteButton;
        this.detailsButton = detailsButton;
        this.workersButton = workersButton;
        this.table = table;
        this.initializeEvents();
    }
    /**
     * Инициализация событий.
     */
    ViewIndex.prototype.initializeEvents = function () {
        var _this = this;
        var tableRows = this.table.querySelectorAll("tbody > tr");
        var _loop_1 = function (i) {
            var row = tableRows[i];
            row.onclick = function (event) { return _this.tableRowClick(row); };
        };
        for (var i = 0; i < tableRows.length; i++) {
            _loop_1(i);
        }
    };
    /**
     * Происходит при нажатии на строку.
     * @param row
     */
    ViewIndex.prototype.tableRowClick = function (row) {
        var _this = this;
        var selectedRows = this.table.querySelectorAll("tbody > tr.table-active");
        for (var i = 0; i < selectedRows.length; i++) {
            var selectedRow = selectedRows[i];
            selectedRow.classList.remove("table-active");
        }
        row.classList.add("table-active");
        if (this.deleteButton != null)
            this.deleteButton.href = row.dataset["delete"];
        if (this.editButton != null)
            this.editButton.href = row.dataset["edit"];
        if (this.detailsButton != null)
            this.detailsButton.href = row.dataset["details"];
        if (this.workersButton != null)
            this.workersButton.href = row.dataset["workers"];
        if (this.editButton.classList.contains("disabled"))
            this.editButton.classList.remove("disabled");
        if (this.deleteButton.classList.contains("disabled"))
            this.deleteButton.classList.remove("disabled");
        if (this.detailsButton.classList.contains("disabled"))
            this.detailsButton.classList.remove("disabled");
        if (this.workersButton.classList.contains("disabled"))
            this.workersButton.classList.remove("disabled");
        this.deleteButton.onclick = function (event) { return _this.deleteButtonClick(event); };
    };
    /**
     * Происходит при нажатии на кнопку "Удалить".
     * @param event
     */
    ViewIndex.prototype.deleteButtonClick = function (event) {
        if (!confirm("Вы уверены, что хотите удалить данную строку?"))
            event.preventDefault();
    };
    return ViewIndex;
}());
window.addEventListener("load", initialize);
//# sourceMappingURL=generic.js.map