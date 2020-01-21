

/**
 * Осуществляет инициализацию модулей.
 */
var initialize = () => {
    initializeViewIndex();
}

/**
 * Инициализирует скрипт выполняемый на главной странице с таблицей.
 */
function initializeViewIndex(): void {
    let viewIndex = new ViewIndex(
        "#tool-strip-edit-button",
        "#tool-strip-delete-button",
        "#tool-strip-details-button",
        "#tool-strip-workers-button",
        "#main-table");
}

class ViewIndex {
    /**
     * Конструктор.
     * @param editButtonQuery - Имя элемента содержащего кнопку "Редактировать".
     * @param deleteButtonQuery - Имя элемента содержащего кнопку "Удалить".
     * @param detailsButtonQuery - Имя элемента содержащего кнопку "Подробнее".
     * @param workersButtonQuery - Имя элемента содержащего кнопку "Работники проекта".
     * @param tableQuery - Имя элемента содержащего таблицу с данными.
     */
    constructor(editButtonQuery: string,
        deleteButtonQuery: string,
        detailsButtonQuery: string,
        workersButtonQuery: string,
        tableQuery: string)
    {

        let editButton = document.querySelector(editButtonQuery) as HTMLLinkElement;
        let deleteButton = document.querySelector(deleteButtonQuery) as HTMLLinkElement;
        let detailsButton = document.querySelector(detailsButtonQuery) as HTMLLinkElement;
        let workersButton = document.querySelector(workersButtonQuery) as HTMLLinkElement;;
        let table = document.querySelector(tableQuery) as HTMLTableElement;

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
     * Элемент содержащий кнопку "Редактировать".
     */
    private editButton: HTMLLinkElement;

    /**
     * Элемент содержащий кнопку "Удалить".
     */
    private deleteButton: HTMLLinkElement;

    /**
     * Элемент содержащий кнопку "Подробнее".
     */
    private detailsButton: HTMLLinkElement;

    /**
     * Элемент содержащий кнопку "Работники проекта".
     */
    private workersButton: HTMLLinkElement;

    /**
     * Элемент содержащий таблицу с данными.
     */
    private table: HTMLTableElement;

    /**
     * Инициализация событий.
     */
    private initializeEvents(): void {
        let tableRows = this.table.querySelectorAll("tbody > tr");

        for (let i = 0; i < tableRows.length; i++) {
            let row = tableRows[i] as HTMLTableRowElement;
            row.onclick = event => this.tableRowClick(row);
        }
    }

    /**
     * Происходит при нажатии на строку.
     * @param row
     */
    private tableRowClick(row: HTMLTableRowElement): void {
        let selectedRows = this.table.querySelectorAll("tbody > tr.table-active");

        for (let i = 0; i < selectedRows.length; i++) {
            let selectedRow = selectedRows[i] as HTMLTableRowElement;
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

        this.deleteButton.onclick = event => this.deleteButtonClick(event);
    }

    /**
     * Происходит при нажатии на кнопку "Удалить".
     * @param event
     */
    private deleteButtonClick(event: MouseEvent): void {
        if (!confirm("Вы уверены, что хотите удалить данную строку?"))
            event.preventDefault();
    }
}

window.addEventListener("load", initialize);