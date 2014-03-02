$(document).ready(function ()
{
    $("#expandAll").click(expandAll);
    $("#collapseAll").click(collapseAll);
    $(".tocModule").click(toggleModule);
});

function expandAll()
{
    $(".tocClips").show();
}

function collapseAll()
{
    $(".tocClips").hide();
}

function toggleModule()
{
    $(this).parent().nextUntil(".module").toggle();
}