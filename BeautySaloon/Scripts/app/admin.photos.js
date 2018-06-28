var ViewModel = {
    images: ko.observableArray(),
    id: ko.observable(),
    FileName: ko.observable(),
    photoDialog: new photoDialogExt(ko),
    deletePhotoDialog: new deletePhotoModalExt(ko)
};

function photoDialogExt(ko) {
    var self = this;
    self.file = ko.observable();
    self.savePhoto = function () {
        var files = $('#inputFile').get(0).files;
        if (files.length > 0) {
            if (window.FormData !== undefined) {
                var data = new FormData();
                data.append("file", files[0]);
                $.ajax({
                    type: "POST",
                    url: "/api/Photos?id=null",
                    contentType: false,
                    processData: false,
                    data: data,
                    success: function (data) {
                        console.log(data)
                        updatePhotos();
                    },
                    error: function (e) {
                        console.log(e)
                    }
                });
            }
        }
    }
    self.reset = function () {
        document.getElementById("inputFile").value = "";
    }
};

function deletePhoto(id) {
    ViewModel.deletePhotoDialog.id(id);
    $('#deletePhotoModal').modal('show');
};

function deletePhotoModalExt(ko) {
    var self = this;
    self.id = ko.observable();
    self.deletePhoto = function () {
        $.ajax({
            url: "/api/Photos?id=" + self.id(),
            type: 'DELETE',
            success: function (data) {
                console.log(data);
                updatePhotos();
            },
            error: function (e) {
                console.log(e)
            }
        });
    }
};

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
};

$(function () {
    ko.applyBindings(ViewModel);
    updatePhotos();
    $('#photoModal').on('hide.bs.modal', function () {
        ViewModel.photoDialog.reset();
    })
});