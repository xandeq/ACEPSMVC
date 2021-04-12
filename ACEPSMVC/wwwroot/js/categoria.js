var dataTable;

$(document).ready(function () {
    loadDataTable();
})

function loadDataTable() {
    $('#tblData').dataTable({
        "ajax": {
            "url": "/admin/categoria/GetAll",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "nome", "width": "50%" },
            { "data": "ordemvisualizacao", "width": "20%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                                <a href="/Admin/categoria/Upsert/${data}" class='btn btn-success text-white' style='cursor: pointer; width: 100%'>
                                    <i class="far fa-edit"></i> Editar
                                </a>
                                &nbsp;
                                <a onclick=Delete("/Admin/categoria/Deletar/${data}") class='btn btn-success text-white' style='cursor: pointer; width: 100%'>
                                    <i class="far fa-trash-alt"></i> Deletar
                                </a>
                            </div>`;
                },
                "width": "30%"
            }
        ],
        "language": {
            "emptyTable": "Nenhum registro encontrado"
        },
        "width": "100%"
    });
}