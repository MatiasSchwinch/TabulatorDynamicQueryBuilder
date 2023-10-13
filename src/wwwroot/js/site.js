document.addEventListener('DOMContentLoaded', () => {
    const $navbarBurgers = Array.prototype.slice.call(document.querySelectorAll('.navbar-burger'), 0);
    $navbarBurgers.forEach(el => {
        el.addEventListener('click', () => {
            const target = el.dataset.target;
            const $target = document.getElementById(target);

            el.classList.toggle('is-active');
            $target.classList.toggle('is-active');
        });
    });

    const tagDictionary = {
        "Mr": "is-primary",
        "Miss": "is-danger",
        "Ms": "is-success",
        "Mrs": "is-info",
        "Monsieur": "is-link",
        "Mademoiselle": "is-warning",
        "Madame": "is-dark",
    };

    /**
        * Tabulator Setup options docs: https://tabulator.info/docs/5.5/options 
    */
    const table = new Tabulator("#persons", {
        layout: "fitDataFill",
        pagination: true,
        paginationMode: "remote",
        paginationSize: 10,
        paginationSizeSelector: [10, 15, 20, 25],
        filterMode: "remote",
        sortMode: "remote",
        ajaxURL: "/Home/GetPersons",
        ajaxConfig: "POST",
        index: "personId",
        columnDefaults: {
            vertAlign: "middle"
        },
        columns: [
            {
                title: "Id",
                field: "personId",
                visible: false
            },
            {
                title: "Photo",
                field: "photo",
                formatter: "image",
                formatterParams: {
                    height: "48px",
                    width: "48px"
                },
                headerSort: false
            },
            {
                title: "Title",
                field: "title",
                formatter: (cell) => {
                    const value = cell.getValue();
                    return `<b class="tag ${tagDictionary[value]} is-light">${value}</b>`;
                },
                headerFilter: "list",
                headerFilterParams: {
                    values: {
                        "": "All",
                        "Mr": "Mr",
                        "Mrs": "Mrs",
                        "Miss": "Miss",
                        "Madame": "Madame",
                        "Monsieur": "Monsieur",
                        "Mademoiselle": "Mademoiselle"
                    }
                },
                headerFilterFunc: "=",
                minWidth: "120"
            },
            {
                title: "First Name",
                field: "firstName",
                headerFilter: "input",
                headerFilterFunc: "starts"
            },
            {
                title: "Last Name",
                field: "lastName",
                headerFilter: "input",
            },
            {
                title: "Date of Bird",
                field: "date",
                formatter: "datetime",
                formatterParams: {
                    inputFormat: "iso",
                    outputFormat: "dd/MM/yyyy",
                    invalidPlaceholder: "(invalid date)"
                },
                headerFilter: "date",
                headerFilterFunc: ">"
            },
            {
                title: "Age",
                field: "age",
                headerFilter: "number",
            },
            {
                title: "Email",
                field: "email",
                formatter: "link",
                formatterParams: {
                    urlPrefix: "mailto://",
                    target: "_blank",
                },
                headerFilter: "input"
            },
            {
                title: "Nationality",
                field: "nationality",
                headerFilter: "input"
            },
        ],
        initialSort: [
            {
                column: "firstName",
                dir: "asc"
            }
        ],
        placeholder: "No information matching the filters"
    });
});