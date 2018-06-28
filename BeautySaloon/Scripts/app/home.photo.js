var ViewModel = {
    images: ko.observableArray(),
    id: ko.observable(),
    FileName: ko.observable(),
    Size: ko.observable()
}

function updatePhotos() {
    $.ajax({
        url: "/api/Photos",
        type: 'GET',
        success: function (data) {
            ViewModel.images(data);
        },
        error: function (e) {
            console.log(e)
        }
    });
}

$(function () {
    ko.applyBindings(ViewModel);
    $("#mdb-lightbox-ui").load("/Content/mdb-addons/lightbox/mdb-lightbox-ui.html");
    updatePhotos();
});