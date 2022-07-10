$(function () {
    $("#searchInput").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: '/Home/AutoComplete/',
                data: { "prefix": request.term },
                type: "POST",
                success: function (data) {
                    response($.map(data, function (item) {
                        return item;
                    }))
                },
                error: function (response) {
                    alert(response.responseText);
                },
                failure: function (response) {
                    alert(response.responseText);
                }
            });
        },
        select: function (e, i) {
            $("#hfItem").val(i.item.val);
        },
        minLength: 1
    });
});