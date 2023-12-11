var dataTable;
$(document).ready(function () {
    loadDataTable();
});
function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url:'/Admin/Product/getall'},
        "columns": [
            { data: 'name', "width": "20%" },
            { data: 'category.name', "width": "20%" },
            { data: 'author', "width": "10%" },
            { data: 'price', "width": "10%" },
            {
                data: 'imgUrl', "width": "25%",
                "render": function (data) {
                    return `<img class="col-2" src="${data}" width="200%" style="border-radius:5px; border:1px solid #bbb9b9" />`
                }
            },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-25 btn-group"  role="group"> 
                        <a href="product/upsert?id=${data}" class="btn btn-primary mx-2" > <i class="bi bi-pencil-square"></i></a >
                        <a onClick=Delete('/admin/product/delete/${data}') class="btn btn-danger mx-2"><i class="bi bi-trash-fill"></i></a>
                    </div > `
                }
            },
        ]
    });
    dataTable = $('#tblDataCategory').DataTable({
        "ajax": { url: '/Admin/Category/getall' },
        "columns": [
            { data: 'id', "width": "20%" },
            { data: 'name', "width": "60%" },
            {data: 'id', "width": "20%",
                "render": function (data) {
                    return `<div class="w-25 btn-group"  role="group"> 
                    <a href="category/edit?id=${data}" class="btn btn-primary mx-2" > <i class="bi bi-pencil-square"></i></a >
                    <a onClick=Delete('/admin/category/delete/${data}') class="btn btn-danger mx-2"><i class="bi bi-trash-fill"></i></a>
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