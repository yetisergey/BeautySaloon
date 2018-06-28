var ViewModel = {
    mainServicesList: new mainServicesExt(ko),
    subServicesList: new subServicesListExt(ko)
}

function mainServicesExt(ko) {
    var self = this;
    self.List = ko.observableArray();
    self.Click = function (dataMain) {
        $.ajax({
            url: "/api/ClientServices?id=" + dataMain.Id,
            type: 'GET',
            success: function (dataSub) {
                ViewModel.subServicesList.List(dataSub);
                ViewModel.subServicesList.title(dataMain.Name);
                ViewModel.subServicesList.description(dataMain.Description);
                $("#mainList").slideToggle(function () {
                    $("#subList").slideToggle();
                });
            },
            error: function (e) {
                console.log(e)
            }
        });
    }
}

function subServicesListExt(ko) {
    var self = this;
    self.List = ko.observableArray();
    self.title = ko.observable();
    self.description = ko.observable();
    self.back = function () {
        $("#subList").slideToggle(function () {
            $("#mainList").slideToggle();
        });
    }
}

$(function () {
    ko.applyBindings(ViewModel);
    initMainServices();
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
                        $("#orderModal").modal("hide");
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
});

function initMainServices() {
    $.ajax({
        url: "/api/ClientServices",
        type: 'GET',
        success: function (data) {
            ViewModel.mainServicesList.List(data);
        },
        error: function (e) {
            console.log(e)
        }
    });
}