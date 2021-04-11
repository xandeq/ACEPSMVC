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
            { "data": "ordemVisualizacao", "width": "20%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                                <a href="/Admin/categoria/Upsert/${data}" class='btn btn-success text-white' style='cursor: pointer; width: 100px'>
                                    <i class="far fa-edit"></i> Editar
                                </a>
                                &nbsp;
                                <a onclick=Delete("/Admin/categoria/Deletar/${data}") class='btn btn-danger text-white' style='cursor: pointer; width: 100px'>
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

function Delete(url) {
    swal({
        title: "Você tem certeza que deseja deletar?",
        text: "Você não poderá recuperar após deletado!",
        type: "warning",
        showCancelButton: true,
        showCloseButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Sim, deletar!",
        closeOnconfirm: true,
        buttons: ["Cancelar","Sim, deletar!"]
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: 'DELETE',
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        $('#tblData').DataTable().ajax.reload(); 
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            })
        } else {
            swal("Seu registro não foi deletado!");
        }
    });
}

function ShowMessage(msg) {
    toastr.success(msg);
}