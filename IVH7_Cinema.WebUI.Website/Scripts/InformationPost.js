window.onbeforeunload = function () {
    
    $.ajax({

        url: '<%= Url.Action("ActionName", "ControllerName") %>', 

    });

};

