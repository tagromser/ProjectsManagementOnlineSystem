(function () { function r(e, n, t) { function o(i, f) { if (!n[i]) { if (!e[i]) { var c = "function" == typeof require && require; if (!f && c) return c(i, !0); if (u) return u(i, !0); var a = new Error("Cannot find module '" + i + "'"); throw a.code = "MODULE_NOT_FOUND", a } var p = n[i] = { exports: {} }; e[i][0].call(p.exports, function (r) { var n = e[i][1][r]; return o(n || r) }, p, p.exports, r, e, n, t) } return n[i].exports } for (var u = "function" == typeof require && require, i = 0; i < t.length; i++)o(t[i]); return o } return r })()({
     1: [function (require, module, exports) {
        "use strict";
        exports.__esModule = true;
        /**
        * Скрипт выполняемый на главной странице с таблицей.
        */
        var ViewIndex = (function () {
            /**
             * Конструктор.
             * @param editButtonQuery - Имя элемента содержащего кнопку "Редактировать".
             * @param deleteButtonQuery - Имя элемента содержащего кнопку "Удалить".
             * @param detailsButtonQuery - Имя элемента содержащего кнопку "Подробнее".
             * @param tableQuery - Имя элемента содержащего таблицу с данными.
             */
            function ViewIndex(editButtonQuery, deleteButtonQuery, detailsButtonQuery, tableQuery) {
                var editButton = document.querySelector(editButtonQuery);
                var deleteButton = document.querySelector(deleteButtonQuery);
                var detailsButton = document.querySelector(detailsButtonQuery);
                var table = document.querySelector(tableQuery);
                //Если элементы не найдены.
                if (!editButton || !deleteButton || !detailsButton || !table)
                    return;
                this.editButton = editButton;
                this.deleteButton = deleteButton;
                this.detailsButton = detailsButton;
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
                this.editButton.href = row.dataset["edit"];
                this.deleteButton.href = row.dataset["delete"];
                this.detailsButton.href = row.dataset["details"];
                if (this.editButton.classList.contains("disabled"))
                    this.editButton.classList.remove("disabled");
                if (this.deleteButton.classList.contains("disabled"))
                    this.deleteButton.classList.remove("disabled");
                if (this.detailsButton.classList.contains("disabled"))
                    this.detailsButton.classList.remove("disabled");
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
        exports.ViewIndex = ViewIndex;
    }, {}],
    2: [function (require, module, exports) {
        "use strict";
        exports.__esModule = true;
        var ViewIndex_1 = require("./ViewIndex");
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
            var viewIndex = new ViewIndex_1.ViewIndex("#tool-strip-edit-button", "#tool-strip-delete-button", "#tool-strip-details-button", "#main-table");
        }
        window.addEventListener("load", initialize);
    }, { "./ViewIndex": 1 }]
}, {}, [2])