﻿var dataTable;
$(document).ready(function () {
    loadDataTable();
});
function loadDataTable() {
    dataTable = $('#tblDataStore').DataTable({
        "ajax": { url: '/Admin/Store/getall' },
        "columns": [
            { data: 'id', "width": "20%" },
            { data: 'name', "width": "60%" },
            {
                data: 'id', "width": "20%",
                "render": function (data) {
                    return `<div class="w-25 btn-group"  role="group"> 
                    <a href="store/edit?id=${data}" class="btn btn-primary mx-2" > <i class="bi bi-pencil-square"></i></a >
                    <a onClick=Delete('/admin/store/delete/${data}') class="btn btn-danger mx-2"><i class="bi bi-trash-fill"></i></a>
                    </div >`
                }
            },
        ]
    });

}


function Delete(url) {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    dataTable.ajax.reload();
                    toastr.success(data.message);
                }
            })
        }
    });
}