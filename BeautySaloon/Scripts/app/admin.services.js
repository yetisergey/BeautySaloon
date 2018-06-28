var ViewModel = {
    partFlag: ko.observable(),
    serviceParentId: ko.observable(),
    currentService: ko.observable(),
    createPart: function () {
        $('#partModal').modal('show');
    },
    createService: function () {
        ViewModel.serviceDialog.Id(null)

        $('#serviceModal').modal('show');
    },
    updateService: function () {
        if (ViewModel.partFlag()) {
            $.ajax({
                url: "/api/Services?id=" + ViewModel.currentService(),
                type: 'GET',
                success: function (data) {
                    ViewModel.serviceDialog.Id(data.Id)
                    ViewModel.serviceDialog.Name(data.Name);
                    ViewModel.serviceDialog.Coast(data.Coast);
                    $('#serviceModal').modal('show');
                },
                error: function (e) {
                    console.log(e)
                }
            });
        } else {
            $.ajax({
                url: "/api/Services?id=" + ViewModel.currentService(),
                type: 'GET',
                success: function (data) {
                    ViewModel.partDialog.Id(ViewModel.currentService());
                    ViewModel.partDialog.Name(data.Name);
                    ViewModel.partDialog.Description(data.Description);
                    $('#partModal').modal('show');
                },
                error: function (e) {
                    console.log(e)
                }
            });
        }
    },
    deleteService: function () {
        $.ajax({
            url: "/api/Services?id=" + ViewModel.currentService(),
            type: 'DELETE',
            success: function (data) {
                console.log(data);
                updateServices();
            },
            error: function (e) {
                console.log(e)
            }
        });
    },
    partDialog: new partDialogExt(ko),
    serviceDialog: new serviceDialogExt(ko),
    imageDialog: new imageDialogExt(ko),
    photoDialog: new photoDialogExt(ko),
    deletePhotoDialog: new deletePhotoModalExt(ko)
}

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
}

function deletePhoto(id) {
    ViewModel.deletePhotoDialog.id(id);
    $('#deletePhotoModal').modal('show');
}

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
                    url: "/api/Photos?id=" + ViewModel.currentService(),
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
}

function imageDialogExt(ko) {
    var self = this;
    self.createPhoto = function () {
        $('#photoModal').modal('show');
    };
    self.images = ko.observableArray();
}

function updatePhotos() {
    $.ajax({
        url: "/api/Photos?id=" + ViewModel.currentService(),
        type: 'GET',
        success: function (data) {
            ViewModel.imageDialog.images(data)
        },
        error: function (e) {
            console.log(e)
        }
    });
}

function serviceDialogExt() {
    var self = this;
    self.Id = ko.observable();
    self.Name = ko.observable();
    self.Coast = ko.observable();
    self.save = function () {
        var value = {
            Id: self.Id(),
            Name: self.Name(),
            Coast: self.Coast(),
            ParentId: ViewModel.serviceParentId()
        }
        $.ajax({
            url: "/api/Services",
            type: 'POST',
            data: JSON.stringify(value),
            contentType: 'application/json; charset=UTF-8',
            success: function (data) {
                console.log(data);
                updateServices();
            },
            error: function (e) {
                console.log(e)
            }
        });
    }
    self.reset = function () {
        self.Id(null);
        self.Name(null);
        self.Coast(null);
    }
}

function partDialogExt(ko) {
    var self = this;
    self.Id = ko.observable();
    self.Name = ko.observable();
    self.Description = ko.observable();
    self.savePart = function () {
        var value = {
            Id: self.Id(),
            Name: self.Name(),
            Description: self.Description()
        }
        $.ajax({
            url: "/api/Services",
            type: 'POST',
            data: JSON.stringify(value),
            contentType: 'application/json; charset=UTF-8',
            success: function (data) {
                console.log(data);
                updateServices();
            },
            error: function (e) {
                console.log(e)
            }
        });
    }
    self.reset = function () {
        self.Id(null);
        self.Name(null);
        self.Description(null);
    }
}

function updateServices() {
    ViewModel.currentService(null);
    $.ajax({
        url: "/api/Services",
        type: 'GET',
        success: function (data) {
            $('#listContainerTree').treeview({
                data: data,
                onNodeSelected: function (event, data) {
                    var parent = $('#listContainerTree').treeview('getParent', data.nodeId).Id;
                    ViewModel.serviceParentId(parent === undefined ? data.Id : parent)
                    ViewModel.partFlag(parent === undefined ? false : true);
                    ViewModel.currentService(data.Id);
                    updatePhotos()
                },
                onNodeUnselected: function (event, data) {
                    ViewModel.currentService(null);
                }
            });
        },
        error: function (e) {
            console.log(e)
        }
    });
}

$(function () {
    ko.applyBindings(ViewModel);
    updateServices();
    $('#photoModal').on('hide.bs.modal', function () {
        ViewModel.photoDialog.reset();
    })
    $('#serviceModal').on('hide.bs.modal', function () {
        ViewModel.serviceDialog.reset();
    })
    $('#partModal').on('hide.bs.modal', function () {
        ViewModel.partDialog.reset();
    })
})