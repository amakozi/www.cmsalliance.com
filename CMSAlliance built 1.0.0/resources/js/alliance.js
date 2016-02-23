$(document).ready(function() {
    $("#signin a:first-child").click(function () {
        var logindiv = $("#loginform");
        if($(logindiv).is(':visible'))
        {
            $(logindiv).hide(500);
            $("#signin").removeClass("current");
        }else{
            $(logindiv).show(500);
            $("#signin").addClass("current");
        }
    });
});