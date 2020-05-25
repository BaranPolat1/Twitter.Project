$(document).ready(function () {
    $("#userSearch").autocomplete({
        source: '/Member/Search/SearchUser',

        minLength: 1,
        select: function (event, ui) {
            window.location = ui.item.url;
        }

    });
});