
 function exportToExcel  (tableID, excelName, sheetName) {
    // Get the table element
    var table = document.getElementById(tableID);

    // Create a new workbook and add a worksheet
    var workbook = XLSX.utils.book_new();
    var ws = XLSX.utils.table_to_sheet(table);

    // Add the worksheet to the workbook
    XLSX.utils.book_append_sheet(workbook, ws, sheetName);

    // Prompt the user to enter a file name
    var fileName = prompt("Enter a name for the Excel file", excelName);

    // If the user provides a file name, save the workbook
    if (fileName) {
        XLSX.writeFile(workbook, fileName);
    }
}
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
    $('#notificationAlert').removeClass('hide');
    $('#notificationMessage').text(message);
    setTimeout(function () {
        $('#notificationAlert').addClass('hide');
    }, 3000);
}





/*DOWNLOAD*/

function callDownload(btnId,action) {
    $(`#${btnId}`).prop('disabled', true);
    $(`#${btnId}`).text('Exporting...');
    // Create a hidden iframe dynamically
    var iframe = $("<iframe/>").css("display", "none").appendTo("body");

    // Set the source of the iframe to the download action
    iframe.attr("src", `/Dashboard/${action}`);

    // Listen for the load event on the iframe
    iframe.on('load', function () {
        // Re-enable the button
        $(`#${btnId}`).prop('disabled', false);

        // Remove the iframe from the DOM
        iframe.remove();
    });

    // Check if the download has completed every second
    var checkDownloadInterval = setInterval(function () {
        try {
            var iframeDocument = iframe[0].contentWindow.document;

            // If the iframe's document is accessible, and its readyState indicates complete, the download has completed
            if (iframeDocument && iframeDocument.readyState === 'complete') {
                // Re-enable the button
                $(`#${btnId}`).prop('disabled', false);
                $(`#${btnId}`).html('<i class="fas fa-file-excel" > </i> Export to Excel');

                // Notify the user that the download is completed
                alert('Download completed!')

                // Remove the iframe from the DOM
                iframe.remove();

                // Clear the interval timer
                clearInterval(checkDownloadInterval);
            }
        } catch (e) {
            console.log(e)
        }
    }, 1000); // Check every second
}