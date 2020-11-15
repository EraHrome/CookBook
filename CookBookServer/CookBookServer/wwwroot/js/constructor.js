$(function () {
    $(document).on('click', '.btn-add', function () {
        var pointId = $(this).data("checkpointid");
        $.ajax({
            url: "/Recipe/Ingredient?pointId=" + pointId, success: function (html) {
                $("#" + pointId).append(html);
            }
        });
    });

    $(document).on('click', '.btn-del', function () {
        let id = $(this).data("indid");
        console.log("remove: " + id);
        $('#' + id).remove();
    });

    $(document).on('click', '.btn-add-checkpoint', function () {
        $.ajax({
            url: "/Recipe/Checkpoint", success: function (html) {
                $("#checkpoints").append(html);
            }
        });
    });
});

