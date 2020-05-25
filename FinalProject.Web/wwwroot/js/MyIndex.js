$(document).on("click", "a.like", function (e) {
    e.preventDefault();

    var me = this;
    $.post(this.href).then(function (result) {
        $(me).find("span.like-count").text(result.likes);
    });
});
$(document).on("click", "a.retweet", function (a) {
    a.preventDefault();

    var ne = this;
    $.post(this.href).then(function (results) {

        $(ne).find("span.retweet-count").text(results.retweets);
    });
});
$(document).on("click", "a.delete", function (e) {
    e.preventDefault();
    var de = this;
    $(de).css({ "visibility": "hidden" });
    $.post(this.href).then(function (result) {

    });
});
window.addEventListener('load', function () {
    document.querySelector('input[type="file"]').addEventListener('change', function () {
        if (this.files && this.files[0]) {
            $("#myImg").css({ "visibility": "visible" });
            var img = document.querySelector('#myImg');
            img.src = URL.createObjectURL(this.files[0]); // set src to blob url
            img.height = "100";
            img.width = "100";
            img.onload = imageIsLoaded;
        }
    });
});

