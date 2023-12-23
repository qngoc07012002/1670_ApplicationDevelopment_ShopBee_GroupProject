var dataTable;
$(document).ready(function () {
    loadDataTable();
});
function loadDataTable() {
    dataTable = $('#tblDataCategory').DataTable({
        "ajax": { url: '/Admin/Category/getall' },
        "columns": [
            { data: 'id', "width": "20%" },
            { data: 'name', "width": "30%" },
            { data: 'status', "width": "30%",
                "render": function (data) {
                    if (data === 0) {
                        return 'Pending';
                    } else if (data === 1) {
                        return 'Active';
                    } else {
                        return null;
                    }
                }
            },
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
    dataTable = $('#tblDataStoreAdmin').DataTable({
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
    dataTable = $('#tblDataBookAdmin').DataTable({
        "ajax": { url: '/Admin/Book/getall' },
        "columns": [
            { data: 'id', "width": "20%" },
            { data: 'name', "width": "60%" },
            { data: 'store.name', "width": "60%" },
            { data: 'category.name', "width": "60%" },
            { data: 'actualPrice', "width": "60%" },
            { data: 'discountPrice', "width": "20%" },
            { data: 'stock', "width": "20%" },
            { data: 'author', "width": "30%" },
            { data: 'description', "width": "60%" },
            { data: 'createDate', "width": "60%" },
            { data: 'modifyDate', "width": "60%" },
            {
                data: 'id', "width": "20%",
                "render": function (data) {
                    return `<div class="w-25 btn-group"  role="group"> 
                    <a href="/admin/book/CreateUpdate?id=${data}" class="btn btn-primary mx-2" > <i class="bi bi-pencil-square"></i></a >
                    <a onClick=Delete('/admin/Book/delete/${data}') class="btn btn-danger mx-2"><i class="bi bi-trash-fill"></i></a>
                    </div >`
                }
            },
        ]
    });
    dataTable = $('#tblDataOrderAdmin').DataTable({
        "ajax": { url: '/Admin/Order/getall' },
        "columns": [
            { data: 'user.name', "width": "30%" },
            { data: 'totalPrice', "width": "20%" },
            { data: 'method', "width": "10%" },
            { data: 'createDate', "width": "10%" },
            { data: 'status', "width": "20%" },
            
            {
                data: 'id', "width": "20%",
                "render": function (data) {
                    return `<div class="w-25 btn-group"  role="group"> 
                    <a onClick=Delete('/admin/order/delete/${data}') class="btn btn-danger mx-2"><i class="bi bi-trash-fill"></i></a>
                    </div >`
                }
            },
        ]
    });
    dataTable = $('#tblDataUserAdmin').DataTable({
        "ajax": { url: '/Admin/User/getall' },
        "columns": [
            { data: 'id', "width": "5%" },
            { data: 'email', "width": "15%" },
            { data: 'phone', "width": "15%" },
            { data: 'password', "width": "20%" },
            { data: 'name', "width": "20%" },
            { data: 'gender', "width": "5%" },
            { data: 'adress', "width": "20%" },
            {
                data: 'id', "width": "20%",
                "render": function (data) {
                    return `<div class="w-25 btn-group"  role="group"> 
                    <a href="user/CreateUpdate?id=${data}" class="btn btn-primary mx-2" > <i class="bi bi-pencil-square"></i></a >
                    <a onClick=Delete('/admin/user/delete/${data}') class="btn btn-danger mx-2"><i class="bi bi-trash-fill"></i></a>
                    </div >`
                }
            },
        ]
    });
    dataTable = $('#tblDataUserRoleAdmin').DataTable({
        "ajax": { url: '/Admin/UserRole/getall' },
        "columns": [
            { data: 'user.email', "width": "40%" },
            { data: 'role.nomalizedName', "width": "40%" },
            {
                data: 'id', "width": "20%",
                "render": function (data) {
                    return `<div class="w-25 btn-group"  role="group"> 
                    <a onClick=Delete('/Admin/UserRole/Delete/${data}') class="btn btn-danger mx-2"><i class="bi bi-trash-fill"></i></a>
                    </div >`
                }
            },
        ]
    });
    dataTable = $('#tblDataBookStoreOwner').DataTable({
        "ajax": { url: '/Store/Book/getall' },
        "columns": [
            { data: 'id', "width": "20%" },
            { data: 'name', "width": "60%" },
            { data: 'category.name', "width": "60%" },
            { data: 'actualPrice', "width": "60%" },
            { data: 'discountPrice', "width": "60%" },
            { data: 'stock', "width": "60%" },
            { data: 'author', "width": "60%" },
            { data: 'description', "width": "60%" },
            {
                data: 'id', "width": "20%",
                "render": function (data) {
                    return `<div class="w-25 btn-group"  role="group"> 
                    <a href="/store/book/CreateUpdate?id=${data}" class="btn btn-primary mx-2" > <i class="bi bi-pencil-square"></i></a >
                    <a onClick=Delete('/store/Book/delete/${data}') class="btn btn-danger mx-2"><i class="bi bi-trash-fill"></i></a>
                    </div >`
                }
            },
        ]
    });
    dataTable = $('#tblDataFeedbackStoreOwner').DataTable({
        "ajax": { url: '/Store/Feedback/getall' },
        "columns": [
            { data: 'book.name', "width": "20%" },
            { data: 'content', "width": "20%" },
            { data: 'rating', "width": "10%" },
            { data: 'response', "width": "20%" },
            { data: 'createDate', "width": "20%" },
            {
                data: 'id', "width": "20%",
                "render": function (data) {
                    return `<div class="w-25 btn-group"  role="group"> 
                    <a href="/store/book/CreateUpdate?id=${data}" class="btn btn-primary mx-2" > <i class="bi bi-pencil-square"></i></a>
                    </div >`
                }
            },
        ]
    });
    dataTable = $('#tblDataOrderStoreOwner').DataTable({
        "ajax": { url: '/Store/Order/getall' },
        "columns": [
            { data: 'user.name', "width": "30%" },
            { data: 'totalPrice', "width": "20%" },
            { data: 'method', "width": "10%" },
            { data: 'createDate', "width": "10%" },
            { data: 'status', "width": "20%" },

            {
                data: 'id', "width": "20%",
                "render": function (data) {
                    return `<div class="w-25 btn-group"  role="group"> 
                    <a href="/Store/Order/details?orderId=${data}" class="view-detail" data-order-id="1">View Detail</a>
                    <a onClick=Delete('/store/order/delete/${data}') class="btn btn-danger mx-2"><i class="bi bi-trash-fill"></i></a>
                    </div >`
                }
            },
        ]
    });
    dataTable = $('#tblDataOrderStoreOwnerPending').DataTable({
        "ajax": { url: '/Store/Order/getall?status=Pending'},
        "columns": [
            { data: 'user.name', "width": "30%" },
            { data: 'totalPrice', "width": "20%" },
            { data: 'method', "width": "10%" },
            { data: 'createDate', "width": "10%" },
            { data: 'status', "width": "20%" },

            {
                data: 'id', "width": "20%",
                "render": function (data) {
                    return `<div class="w-25 btn-group"  role="group"> 
                    <a href="/Store/Order/details?orderId=${data}" class="view-detail" data-order-id="1">View Detail</a>
                    <a onClick=Delete('/store/order/delete/${data}') class="btn btn-danger mx-2"><i class="bi bi-trash-fill"></i></a>
                    </div >`
                }
            },
        ]
    });
    dataTable = $('#tblDataOrderStoreOwnerSuccessful').DataTable({
        "ajax": { url: '/Store/Order/getall?status=Successful'},
        "columns": [
            { data: 'user.name', "width": "30%" },
            { data: 'totalPrice', "width": "20%" },
            { data: 'method', "width": "10%" },
            { data: 'createDate', "width": "10%" },
            { data: 'status', "width": "20%" },
            {
                data: 'id', "width": "20%",
                "render": function (data) {
                    return `<div class="w-25 btn-group"  role="group"> 
                    <a href="/Store/Order/details?orderId=${data}" class="view-detail" data-order-id="1">View Detail</a>
                    <a onClick=Delete('/store/order/delete/${data}') class="btn btn-danger mx-2"><i class="bi bi-trash-fill"></i></a>
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