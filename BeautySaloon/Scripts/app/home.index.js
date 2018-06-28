$("#sendMessage").click(function () {
    var dto = {
        Phone: $("#form-phone").val(),
        Name: $("#form-name").val(),
    }
    if (dto.Name) {
        if (dto.Phone) {
            $.ajax({
                url: "/api/Telega",
                type: 'POST',
                data: JSON.stringify(dto),
                contentType: 'application/json; charset=UTF-8',
                success: function (data) {
                    $("#sendMessage").prop('disabled', true);
                    toastr.success(data);
                },
                error: function (e) {
                    toastr.error(e)
                }
            });
        } else {
            toastr.error("Заполните Ваш Телефон!");
        }
    } else {
        toastr.error("Заполните Ваше Имя!");
    }
});