$(document).ready(function () {
    var counter = 10;
    var interval = setInterval(function () {
        counter--;
        $(".time").text(counter);
        if (counter == 0) {
            var url = '/';
            $(location).attr('href', url);
        }
    }, 1000);
});