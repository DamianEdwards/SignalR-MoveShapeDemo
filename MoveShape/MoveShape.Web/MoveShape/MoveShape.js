/// <reference path="../Scripts/jquery-1.7.2.js" />
/// <reference path="../Scripts/jquery.signalR-1.0.1.js" />
$(function () {
    var hub = $.connection.moveShape,
        $shape = $("#shape"),
        $clientCount = $("#clientCount"),
        body = window.document.body;

    $.extend(hub.client, {
        shapeMoved: function (x, y) {
            $shape.css({
                left: (body.clientWidth - $shape.width()) * x,
                top: (body.clientHeight - $shape.height()) * y
            });
        },
        clientCountChanged: function (count) {
            $clientCount.text(count);
        }
    });

    $.connection.hub.start().done(function () {
        $shape.draggable({
            containment: "parent",
            drag: function () {
                var $this = $(this),
                    x = this.offsetLeft / (body.clientWidth - $this.width()),
                    y = this.offsetTop / (body.clientHeight - $this.height());
                hub.server.moveShape(x, y);
            }
        });
    });
});