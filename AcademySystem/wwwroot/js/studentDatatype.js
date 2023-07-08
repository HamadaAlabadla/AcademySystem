$(document).ready(function () {
    $('#StudentsTable').DataTable({
        "serverSide": true,
        "filter": true,
        "dataSrc": "",
        "ajax": {
            "url": "/Students/GetAllStudents",
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
            { "data": "id", "name": "ID", "autowidth": true },
            { "data": "fullName", "name": "FullName", "autowidth": true },
            { "data": "identity", "name": "Identity", "autowidth": true },
            { "data": "phoneNumber", "name": "PhoneNumber", "autowidth": true },
            { "data": "level", "name": "Level", "autowidth": true },
            {
                "data": null,
                "name": null,
                "sorting":false,
                "render": function (data, type, row) {
                    debugger
                    return '<div> <a href="#" class = "btn btn-danger" onclick =  DeleteStudent("' + row.id + '");> Delete </a> '+
                        '<a href = "#" class = "btn btn-info" onclick = DeleteStudent("' + row.id + '"); > Edit </a > </div>';},
                "orderable": false,
            },
        ],
    },);
},);