var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#DT_load').DataTable({
        "ajax": {
            "url": "/noticias/getall/",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "Titulo", "width": "20%" },
            { "data": "Subtitulo", "width": "20%" },
            { "data": "isbn", "width": "20%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                        <a href="/noticias/Editar?id=${data}" class='btn btn-success text-white' style='cursor:pointer; width:70px;'>
                            Edit
                        </a>
                        &nbsp;
                        <a class='btn btn-danger text-white' style='cursor:pointer; width:70px;'
                            onclick=Deletar('/api/noticias?id='+${data})>
                            Deletar
                        </a>
                        </div>`;
                }, "width": "40%"
            }
        ],
        "language": {
            "emptyTable": "no data found"
        },
        "width": "100%"
    });
}

function Deletar(url) {
    swal({
        title: "Você tem certeza?",
        text: "Uma vez deletado, você não terá como recuperar a notícia.",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willDeletar) => {
        if (willDeletar) {
            $.ajax({
                type: "Deletar",
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}