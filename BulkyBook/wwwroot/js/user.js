﻿

let dataTable;
$(document).ready(function () {
    loadDataTable();
});
function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/User/GetAll"
        },
        "columns": [
            { "data": "name", "width": "15%" },
            { "data": "email", "width": "15%" },
            { "data": "phoneNumber", "width": "15%" },
            { "data": "company.name", "width": "15%" },
            { "data": "role", "width": "15%" },
            {
                "data": {
                    id: "id", lockoutEnd:"lockoutEnd"
                },
                "render": function (data) {
                    let today = new Date().getTime();
                    let lockout = new Date(data.lockoutEnd).getTime();
                    if (lockout > today) {
                        return `<div class="text-center">
                <a onclick=LockUnlock('${data.id}') class="btn btn-danger text-white" style="cursor:pointer;width:100px;">
                    <i class="fas fa-lock-open"></i> &nbsp;
                </a>           
            </div>`;
                    }
                    else {
                        return `<div class="text-center">
                <a onclick=LockUnlock('${data.id}') class="btn btn-success text-white" style="cursor:pointer;width:100px;">
                    <i class="fas fa-lock"></i> &nbsp;
                </a>           
            </div>`;
                    }
                }
            }
            
        ]
    })
}
function LockUnlock(id) {
    
            $.ajax({
                type: "POST",
                url: 'User/LockUnlock',
                data: JSON.stringify(id),
                contentType: 'application/json',
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            })
        
}