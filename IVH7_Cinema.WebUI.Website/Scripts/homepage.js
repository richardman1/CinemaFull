$(document).ready(function () {

    $("#theTarget").skippr({

        transition: 'slide',
        speed: 1000,
        easing: 'easeOutQuart',
        navType: 'block',
        childrenElementType: 'div',
        arrows: false,
        autoPlay: true,
        autoPlayDuration: 10000,
        keyboardOnAlways: false,
        hidePrevious: false

    });

});