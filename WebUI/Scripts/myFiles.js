$(document).ready(function () {
    $('#searchBtn').click(function (event) {
        event.preventDefault();
        var stringForSearch = $("#stringForSearch").val();
        var url = "/File/FindMyFilesByName";

        $.post(url, { fileName: stringForSearch }, function (response) {
            $(".js-table").empty().append(response);
        });
    });
});