var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#DT_load').DataTable({
        "ajax": {
            "url": "/Noticias/GetAll/",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "dataCriacao", "width": "20%" },
            { "data": "titulo", "width": "20%" },
            {
                "data": "imagemDestaque", "width": "20%", "render": function (data) {
                    return '<img style="width: 30%" src="../noticias/' + data + '" />';
                }
            }, {
                "data": "imagemInterna", "width": "20%", "render": function (data) {
                    return '<img style="width: 30%" src="../noticias/' + data + '" />';
                }
            },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                        <a href="/Noticias/Upsert?id=${data}" class='btn btn-success text-white' style='cursor:pointer; width:70px;'>
                            Editar
                        </a>
                        &nbsp;
                        <a class='btn btn-danger text-white' style='cursor:pointer; width:70px;'
                            onclick=Deletar('/Noticias/Delete?id='+${data})>
                            Deletar
                        </a>
                        </div>`;
                }, "width": "20%"
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
                type: "Delete",
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