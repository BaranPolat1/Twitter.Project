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
    $.post(this.href).then(function (result) {
        $(de).find("span.contentTweet").text(result.message);
    });
});
