$(document).ready(function () {
    $(".movie-trailer-button").click(function () {
        $(this).css("display", "none");
        $(".movie-banner").animate({ height: '550px' }, 1000, function () {
            $(".movie-trailer").css("display", "");
        });
    });

    $(".movie-banner").click(function () {
        if ($(".movie-trailer").css('display') != 'none') {
            $(".movie-trailer").css("display", "none");
            $(".movie-banner").animate({ height: '320px' }, 1000, function () {
                $(".movie-trailer-button").css("display", "");
            });
        }
    });
});