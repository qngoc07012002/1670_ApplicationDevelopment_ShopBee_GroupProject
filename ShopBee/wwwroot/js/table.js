﻿var dataTable;
$(document).ready(function () {
    loadDataTable();
});
function loadDataTable() {
    dataTable = $('#tblPendingCategory').DataTable({
        "ajax": { url: '/Admin/Category/getall' },
        "columns": [
            { data: 'id', "width": "20%" },
            { data: 'name', "width": "30%" },
            {
                data: 'id', "width": "20%",
                "render": function (data, type, row) {
                    if (row.status === 0) { // chỉ hiển thị khi status là Pending
                        return `<div class="w-25 btn-group"  role="group">
                        <a onClick=ConfirmCategory('/admin/category/confirm/${data}') class="btn btn-success mx-2 accept-button"> <i class="bi bi-check-square"></i>Accept</a>
                        <a onClick=Delete('/admin/category/delete/${data}') class="btn btn-danger mx-2"><i class="bi bi-trash-fill"></i></a>

                        
                    </div >`;
                    } else {
                        return null;
                    }
                }
            },
        ],
        "paging": false, // Tắt chức năng phân trang
        "info": false, // Tắt thông tin số dòng và trang
        "drawCallback": function (settings) {
            // Ẩn toàn bộ các dòng có status là Active
            var api = this.api();
            var rows = api.rows({ search: 'applied' }).nodes();
            rows.each(function (row) {
                var data = api.row(row).data();
                if (data.status === 1) {
                    $(row).hide();
                }
            });
        }
    });


    dataTable = $('#tblActiveCategory').DataTable({
        "ajax": { url: '/Admin/Category/getall' },
        "columns": [
            { data: 'id', "width": "20%" },
            { data: 'name', "width": "30%" },

            {
                data: 'id', "width": "20%",
                "render": function (data, type, row) {
                    if (row.status === 1) { // chỉ hiển thị khi status là Active
                        return `<div class="w-25 btn-group"  role="group"> 
                            <a href="category/edit?id=${data}" class="btn btn-primary mx-2" > <i class="bi bi-pencil-square"></i></a >
                            <a onClick=Delete('/admin/category/delete/${data}') class="btn btn-danger mx-2"><i class="bi bi-trash-fill"></i></a>
                        </div >`;
                    } else {
                        return null;
                    }
                }
            },
        ],
        "drawCallback": function (settings) {
            // Ẩn các dòng có status là Pending
            var api = this.api();
            var rows = api.rows({ search: 'applied' }).nodes();
            rows.each(function (row) {
                var data = api.row(row).data();
                if (data.status === 0) {
                    $(row).hide();
                }
            });
        }
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
    dataTable = $('#tblDataFeedbackStoreOwner').DataTable({
        "ajax": { url: '/Store/Feedback/getall' },
        "columns": [
            { data: 'book.name', "width": "20%" },
            { data: 'content', "width": "20%" },
            {
                data: 'rating',
                "width": "20%",
                "render": function (data) {
                    let stars = '';
                    for (let i = 0; i < data; i++) {
                        stars += '<i class="star-checked fa-solid fa-star"></i>';
                    }
                    for (let i = data; i < 5; i++) {
                        stars += '<i class="star-unchecked far fa-star"></i>'
                    }
                    return stars;
                }
            },
            {
                data: 'response',
                width: '20%',
                render: function (data, type, rowData) {
                    if (!data) {
                        return '<input type="text" class="responseInput" data-id="' + rowData.id + '" name="respone" placeholder="Enter your comment">';
                    } else {
                        return data;
                    }
                }
            },

            {
                data: 'createDate', "width": "10%",
                "render": function (data) {
                    const date = new Date(data);
                    const formattedDate = date.toLocaleDateString('en-GB');
                    return formattedDate;
                }
            }
        ]
    });
    dataTable = $('#tblDataOrderStoreOwner').DataTable({
        "ajax": { url: '/Store/Order/getall' },
        "columns": [
            { data: 'user.name', "width": "30%" },
            { data: 'totalPrice', "width": "20%" },
            { data: 'method', "width": "10%" },
            {
                data: 'createDate', "width": "10%",
                "render": function (data) {
                    const date = new Date(data);
                    const formattedDate = date.toLocaleDateString('en-GB');
                    return formattedDate;
                }
            },
            { data: 'status', "width": "20%" },
            {
                data: 'id', "width": "20%",
                "render": function (data, type, row) {
                    if (row.status === "Pending") {
                        return `<div class="w-25 btn-group" role="group"> 
                        <a href="/Store/Order/details?orderId=${data}" class="view-detail btn btn-success mx-2" data-order-id="${data}"><i class="bi bi-check-square"></i></a>
                        <a onClick=Delete('/store/order/delete/${data}') class="btn btn-danger mx-2"><i class="bi bi-trash-fill"></i></a>
                    </div>`;
                    } else if (row.status === "Successful") {
                        return `<div class="w-25 btn-group" role="group"> 
                        <a href="/Store/Order/details?orderId=${data}" class="view-detail" data-order-id="${data}">View Detail</a>
                    </div>`;
                    } else {
                        return '';
                    }
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
            {
                data: 'createDate', "width": "10%",
                "render": function (data) {
                    const date = new Date(data);
                    const formattedDate = date.toLocaleDateString('en-GB');
                    return formattedDate;
                }
            },
            { data: 'status', "width": "20%" },
            {
                data: 'id', "width": "20%",
                "render": function (data) {
                    return `<div class="w-25 btn-group"  role="group"> 
                    <a href="/Store/Order/details?orderId=${data}" class="view-detail btn btn-success mx-2" data-order-id="1"><i class="bi bi-check-square"></i></a>
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
            {
                data: 'createDate', "width": "10%",
                "render": function (data) {
                        const date = new Date(data);
                        const formattedDate = date.toLocaleDateString('en-GB');
                        return formattedDate;
                }
            },
            { data: 'status', "width": "20%" },
            {
                data: 'id', "width": "20%",
                "render": function (data) {
                    return `<div class="w-25 btn-group" role="group"> 
                    <a href="/Store/Order/details?orderId=${data}" class="view-detail" data-order-id="${data}">View Detail</a>
                    </div>`;

                }
            },
        ]
    });
}

$('#tblDataFeedbackStoreOwner').on('keyup', '.responseInput', function (e) {
    if (e.key === 'Enter') {
        const id = $(this).data('id');
        const response = $(this).val();
        Swal.fire({
            title: "Are you sure?",
            text: "The response can't change after !",
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: "#3085d6",
            cancelButtonColor: "#d33",
            confirmButtonText: "Yes, Do it!"
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    url: '/store/feedback/update/' + id,
                    type: 'POST',
                    data: { response: response },
                    success: function (data) {
                        Swal.fire({
                            title: "Done!",
                            text: data.message,
                            icon: "success",
                            timer: 2000,
                            timerProgressBar: true,
                            showConfirmButton: false
                        }).then(() => {
                            location.reload();
                        });
                    }
                });
            }
        });

    }
});



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
                    Swal.fire({
                        title: "Deleted!",
                        text: data.message,
                        icon: "success",
                        timer: 2000,
                        timerProgressBar: true,
                        showConfirmButton: false
                    }).then(() => {

                        location.reload();
                    });
                }
            });
        }
    });
}
function ConfirmCategory(url) {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to accept this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, confirm it!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'POST',
                success: function (data) {
                    Swal.fire({
                        title: "Confirmed!",
                        text: data.message,
                        icon: "success",
                        timer: 2000,
                        timerProgressBar: true,
                        showConfirmButton: false
                    }).then(() => {

                        location.reload();
                    });
                }
            });
        }
    });
}