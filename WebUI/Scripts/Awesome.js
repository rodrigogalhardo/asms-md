function applyAwesomeStyles() {
    $(".lookupButton").empty().prepend('<span class="ui-icon ui-icon-newwin"></span>');
    $(".clearLookupButton").empty().prepend('<span class="ui-icon ui-icon-gear"></span>');
    mybutton(".lookupButton");
    mybutton(".clearLookupButton");
}

$(function () {
    $(".pagination .current").addClass('ui-state-highlight');
    mybutton(".pagination a");
    applyAwesomeStyles();
    $("body").ajaxComplete(applyAwesomeStyles);
});

function mybutton(sel) {
$(sel).unbind('mousedown mouseup mouseleave')
        .hover(function () { $(this).addClass("ui-state-hover"); },
	            function () { $(this).removeClass("ui-state-hover"); })
        .bind({ 'mousedown mouseup': function () { $(this).toggleClass('ui-state-active'); } })
        .addClass("ui-state-default").addClass("ui-corner-all")
        .bind('mouseleave', function () { $(this).removeClass('ui-state-active') });
}