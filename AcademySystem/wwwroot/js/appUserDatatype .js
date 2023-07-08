$(document).ready(function () {
    $('#AppUsersTable').DataTable({
        "serverSide": true,
        "filter": true,
        "dataSrc": "",
        "ajax": {
            "url": "/AppUsers/GetAllUsers",
            "type": "POST",
            "datatype": "json",
            "columnDefs": [{
                "defaultContent": "-",
                "targets": "_all",
                //"targets": [0],
                "visible": false,
                "searchable": false
            },],
            
        },
        "columns": [
            { "data": "userName", "name": "UserName", "autowidth": true },
            { "data": "email", "name": "Email", "autowidth": true },
            { "data": "phoneNumber", "name": "PhoneNumber", "autowidth": true },
            { "data": "isActive", "name": "isActive", "autowidth": true },
            {
                "data": null,
                "name": "Actions",
                "sorting":false,
                "render": function (data, type, row) {
                    return '<div> <a href="#" class = "btn btn-danger" onclick =  DeleteStudent();> Delete </a> '+
                        '<a href = "#" class = "btn btn-info" onclick = DeleteStudent(); > Edit </a > </div>';},
                "orderable": false,
            },
        ],
    },);
},);