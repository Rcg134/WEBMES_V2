
function callErrorPage(message) {
    $.ajax({
        type: "GET",
        url: "/Error/ErrorView",
        data: {
            ErrorMessage: message
        },
        success: function (response) {

            $("#renderBody").html(response);
        },
    });
}




function showSkeletonLoading(formid, columnCount, rowCount) {
    var tableHeader = '<thead><tr>';
    for (var i = 0; i < columnCount; i++) {
        tableHeader += '<th></th>';
    }
    tableHeader += '</tr></thead>';

    var tableBody = '<tbody>';
    for (var j = 0; j < rowCount; j++) {
        tableBody += '<tr>';
        for (var k = 0; k < columnCount; k++) {
            tableBody += '<td class="skeleton-row"></td>';
        }
        tableBody += '</tr>';
    }
    tableBody += '</tbody>';

    var skeletonTable = `
        <div class='table-responsive'>
            <table class="table table-bordered skeleton-row">
                ${tableHeader}
                ${tableBody}
            </table>
        </div>`;

    $(`#${formid}`).html(skeletonTable)
}


function hideSkeletonLoading(formid) {
    $(`#${formid}`).html('');
}



function showNotif(message) {
    return new Promise((resolve, reject) => {
        $('#notificationAlertid').removeClass('d-none');
        $('#notificationMessage').text(message);
        setTimeout(() => {
            $('#notificationAlertid').addClass('d-none');
            resolve(); // Resolve the promise after the notification is hidden
        }, 3000);
    });
}
