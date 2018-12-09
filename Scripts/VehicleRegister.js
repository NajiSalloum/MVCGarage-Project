$(document).ready(function () {
    $("input[type='submit']").on("click", OnClick);
    $("#Regnr").on("focusout", OnFocusOut);
});

function OnClick(e) {
    if ($("#Regnr").parent().parent().hasClass("has-error")) {
        e.preventDefault();
        $("#warningText").removeClass("hidden");
    }
}

function OnFocusOut(e) {
    $.ajax(
        {
            url: "/Vehicles/UniqueRegnr",
            type: "post",
            dataType: "json",
            data:
                {
                    DataName: this.id,
                    text: this.value
                },
            context: this,
            success: function (data) {
                if (data !== null && data.length > 0) {
                    $(this).parent().parent().addClass("has-error");
                    $("#warningText").removeClass("hidden");
                }
                else {
                    $(this).parent().parent().removeClass("has-error");
                    $("#warningText").addClass("hidden");
                }
            },
            error: function (data) {
                $(this).parent().removeClass("has-error");
                $("#warningText").addClass("hidden");
            }
        });
}

