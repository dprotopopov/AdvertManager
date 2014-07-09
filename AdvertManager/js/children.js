(function ($) {
    $.addChildren = function (event) {
        var childrenCount = parseInt($("input[name='childrenCount']", "#children").attr("value"));
        var childTemplate = $(".childTemplate", "#children");
        var child = $(childTemplate).clone();
        $(childTemplate).before(child);
        $(child).css("visibility", "visible");
        $(child).removeClass("childTemplate").addClass("child");
        $("input,textarea", child).each(function () {
            $(this).attr("name", $(this).attr("name").concat("[", childrenCount.toString(), "]"));
        });
        $("input[name='childrenCount']", "#children").attr("value", childrenCount + 1);
    };
    $.removeChildren = function (event) {
        $("tr.child:has(input[name^='checkbox']:checked)", "#children").each(function () {
            $(this).remove();
        });
    };
    $.checkChildren = function (event) {
        //http://stackoverflow.com/questions/426258/how-do-i-check-a-checkbox-with-jquery
        if ($("input[name='checkbox']", "#children").prop('checked'))
            $("input[name^='checkbox']", "#children").prop('checked', true);
    };
    $.childrenSubmit = function (event) {
        event.preventDefault();
        $("input[name='action']:checked", "#children").each(function () {
            eval("$." + $(this).attr("value") + "Children(event);");
        });
    };
})(jQuery);