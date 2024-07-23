function callErrorPage(message) {
  $.ajax({
    type: "GET",
    url: "/Error/ErrorView",
    data: {
      ErrorMessage: message,
    },
    success: function (response) {
      $("#renderBody").html(response);
    },
  });
}

function showSkeletonLoading(formid, columnCount, rowCount) {
  var tableHeader = "<thead><tr>";
  for (var i = 0; i < columnCount; i++) {
    tableHeader += "<th></th>";
  }
  tableHeader += "</tr></thead>";

  var tableBody = "<tbody>";
  for (var j = 0; j < rowCount; j++) {
    tableBody += "<tr>";
    for (var k = 0; k < columnCount; k++) {
      tableBody += '<td class="skeleton-row"></td>';
    }
    tableBody += "</tr>";
  }
  tableBody += "</tbody>";

  var skeletonTable = `
        <div class='table-responsive'>
            <table class="table table-bordered skeleton-row">
                ${tableHeader}
                ${tableBody}
            </table>
        </div>`;

  $(`#${formid}`).html(skeletonTable);
}

function hideSkeletonLoading(formid) {
  $(`#${formid}`).html("");
}

function showNotif(message) {
  return new Promise((resolve, reject) => {
    $("#notificationAlertid").removeClass("d-none");
    $("#notificationMessage").text(message);
    setTimeout(() => {
      $("#notificationAlertid").addClass("d-none");
      resolve(); // Resolve the promise after the notification is hidden
    }, 3000);
  });
}

function initializeDatatable(tableId) {
  //Datatable initialization
  new DataTable(`#${tableId}`, {
    initComplete: function () {
      this.api()
        .columns()
        .every(function () {
          let column = this;
          let title = column.footer().textContent;

          let input = document.createElement("input");
          input.placeholder = title;
          column.footer().replaceChildren(input);

          input.addEventListener("keyup", () => {
            if (column.search() !== this.value) {
              column.search(input.value).draw();
            }
          });
        });
    },
    dom: "<'row'<'col-sm-6'f><'col-sm-6'<'float-right'B>>>tip",
    buttons: ["csv", "pdf"],
  });

  $(`#${tableId} tfoot tr`).appendTo(`#${tableId} thead`);
}

function getLettersBeforeTO(str) {
  var parts = str.split("TO");
  if (parts.length > 1) {
    return parts[0].trim() + " Out";
  }
  return "Tracked In Date and Time";
}

function getLettersAfterTO(str) {
  var parts = str.split("TO");
  if (parts.length > 1) {
    return parts[1].trim() + " In";
  }
  return "Tracked Out Date and Time";
}
