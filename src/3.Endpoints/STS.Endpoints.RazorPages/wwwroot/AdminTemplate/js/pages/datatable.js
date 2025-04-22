var tableMain = $('#data-table-User').DataTable({
    "columnDefs": [{
        "targets": [0,5],
        "orderable": false
    }],
    "aaSorting": [],
    "pageLength": 25,
    "drawCallback": function() {
        var topestStatus = $("#btn-check-all-toggle").prop("checked");
        $("table td input[type='checkbox']").each(function(){
            currentStatus = $(this).prop("checked");
            if(topestStatus != currentStatus){
                console.log("Reversed");
                $("#btn-check-all-toggle").prop("checked", currentStatus);
            }
        });

        Modiran.initiCkeck();
    },
});
var tableMain = $('#data-table-ApplicationUser').DataTable({
    "columnDefs": [{
        "targets": [0, 5],
        "orderable": false
    }],
    "aaSorting": [],
    "pageLength": 25,
    "drawCallback": function () {
        var topestStatus = $("#btn-check-all-toggle").prop("checked");
        $("table td input[type='checkbox']").each(function () {
            currentStatus = $(this).prop("checked");
            if (topestStatus != currentStatus) {
                console.log("Reversed");
                $("#btn-check-all-toggle").prop("checked", currentStatus);
            }
        });

        Modiran.initiCkeck();
    },
});
var tableMain = $('#data-table-MainCategory').DataTable({
    "columnDefs": [{
        "targets": [0, 3],
        "orderable": false
    }],
    "aaSorting": [],
    "pageLength": 25,
    "drawCallback": function () {
        var topestStatus = $("#btn-check-all-toggle").prop("checked");
        $("table td input[type='checkbox']").each(function () {
            currentStatus = $(this).prop("checked");
            if (topestStatus != currentStatus) {
                console.log("Reversed");
                $("#btn-check-all-toggle").prop("checked", currentStatus);
            }
        });

        Modiran.initiCkeck();
    },
});
var tableMain = $('#data-table-SubCategory').DataTable({
    "columnDefs": [{
        "targets": [0, 2],
        "orderable": false
    }],
    "aaSorting": [],
    "pageLength": 25,
    "drawCallback": function () {
        var topestStatus = $("#btn-check-all-toggle").prop("checked");
        $("table td input[type='checkbox']").each(function () {
            currentStatus = $(this).prop("checked");
            if (topestStatus != currentStatus) {
                console.log("Reversed");
                $("#btn-check-all-toggle").prop("checked", currentStatus);
            }
        });

        Modiran.initiCkeck();
    },
});
var tableMain = $('#data-table-TaskItems').DataTable({
    "columnDefs": [{
        "targets": [0, 2],
        "orderable": false
    }],
    "aaSorting": [],
    "pageLength": 25,
    "drawCallback": function () {
        var topestStatus = $("#btn-check-all-toggle").prop("checked");
        $("table td input[type='checkbox']").each(function () {
            currentStatus = $(this).prop("checked");
            if (topestStatus != currentStatus) {
                console.log("Reversed");
                $("#btn-check-all-toggle").prop("checked", currentStatus);
            }
        });

        Modiran.initiCkeck();
    },
});
var tableMain = $('#data-table-FeedBacks').DataTable({
    "columnDefs": [{
        "targets": [0, 5],
        "orderable": false
    }],
    "aaSorting": [],
    "pageLength": 25,
    "drawCallback": function () {
        var topestStatus = $("#btn-check-all-toggle").prop("checked");
        $("table td input[type='checkbox']").each(function () {
            currentStatus = $(this).prop("checked");
            if (topestStatus != currentStatus) {
                console.log("Reversed");
                $("#btn-check-all-toggle").prop("checked", currentStatus);
            }
        });

        Modiran.initiCkeck();
    },
});
var tableMain = $('#data-table-Suggestions').DataTable({
    "columnDefs": [{
        "targets": [0, 5],
        "orderable": false
    }],
    "aaSorting": [],
    "pageLength": 25,
    "drawCallback": function () {
        var topestStatus = $("#btn-check-all-toggle").prop("checked");
        $("table td input[type='checkbox']").each(function () {
            currentStatus = $(this).prop("checked");
            if (topestStatus != currentStatus) {
                console.log("Reversed");
                $("#btn-check-all-toggle").prop("checked", currentStatus);
            }
        });

        Modiran.initiCkeck();
    },
});


$(window).on( 'resize', function () {
    $('#data-table').css("width", "100%");
} );

// Checkboxes
$(document).on('ifChanged', 'input#btn-check-all-toggle', function (event) {
    var isChecked = $("#btn-check-all-toggle").prop("checked");
    if(isChecked){
        $("table td input[type='checkbox']").iCheck("check").iCheck("update");
    }else{
        $("table td input[type='checkbox']").iCheck("uncheck").iCheck("update");
    }
});