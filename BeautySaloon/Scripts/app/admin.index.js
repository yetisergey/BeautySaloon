var ViewModel = {
    eventDialog: new eventDialogExt(ko)
}

function eventDialogExt(ko) {
    var self = this;
    self.Id = ko.observable();
    self.Name = ko.observable();
    self.reset = function () {
        self.Id("");
        self.Name("");
        $('#datetimepickerFrom').data('DateTimePicker').date(new Date());
        $('#datetimepickerTo').data('DateTimePicker').date(new Date());
    }
    self.saveEvent = function () {
        var value = {
            Id: self.Id(),
            Name: self.Name(),
            Start: $('#datetimepickerFrom').data('DateTimePicker').date()._d.addHours(3),
            End: $('#datetimepickerTo').data('DateTimePicker').date()._d.addHours(3)
        }
        $.ajax({
            url: "/api/Shedule",
            type: 'POST',
            data: JSON.stringify(value),
            contentType: 'application/json; charset=UTF-8',
            success: function (data) {
                console.log(data);
                updateEvents();
            },
            error: function (e) {
                console.log(e)
            }
        });
    }
    self.deleteEvent = function () {
        $.ajax({
            url: "/api/Shedule?id=" + self.Id(),
            type: 'DELETE',
            success: function (data) {
                console.log(data);
                updateEvents();

            },
            error: function (e) {
                console.log(e)
            }
        });
    }
}

function updateEvents() {
    $.ajax({
        url: "/api/Shedule",
        type: 'GET',
        success: function (data) {
            $("#calendar").fullCalendar('removeEvents')
            $("#calendar").fullCalendar('addEventSource', data);
        },
        error: function (e) {
            console.log(e)
        }
    });
}

Date.prototype.addHours = function (h) {
    this.setTime(this.getTime() + (h * 60 * 60 * 1000));
    return this;
}

$(function () {
    ko.applyBindings(ViewModel);
    $('#calendar').fullCalendar({
        header: {
            left: 'prev,next today',
            right: 'month,agendaWeek,agendaDay,listMonth'
        },
        locale: 'ru',
        buttonIcons: false,
        weekNumbers: true,
        navLinks: true,
        editable: false,
        eventLimit: true,
        events: [],
        eventClick: function (calEvent, jsEvent, view) {
            $.ajax({
                url: "/api/Shedule?id=" + calEvent.id,
                type: 'GET',
                success: function (data) {
                    ViewModel.eventDialog.Id(data.Id);
                    ViewModel.eventDialog.Name(data.Name);
                    $('#datetimepickerFrom').data("DateTimePicker").date(new Date(data.Start));
                    $('#datetimepickerTo').data("DateTimePicker").date(new Date(data.End));
                    $("#eventModal").modal("show");
                },
                error: function (e) {
                    console.log(e)
                }
            });
        }
    });
    updateEvents();
    $('#eventModal').on('hide.bs.modal', function () {
        ViewModel.eventDialog.reset();
    })
    $('#datetimepickerFrom').datetimepicker({
        locale: 'ru',
    });
    $('#datetimepickerTo').datetimepicker({
        locale: 'ru',
    });
});