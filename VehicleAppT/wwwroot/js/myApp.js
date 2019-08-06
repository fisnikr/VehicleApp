var myApp;
myApp = (function (app) {
    $('#printTable').click(function () {
        myApp.print();
    });

    app.print = function () {
        $.ajax({
            url: 'Home/Print',
            success: function (data) {
                if (myApp.arePopupsBlocked()) {
                    alert('Please allow popups.');
                }
                var printWindow = window.open();
                if (printWindow) {
                    $(printWindow.document.body).html(data);

                } else {
                    alert('Please allow popups.');
                }
            },
            error: function () {
                alert('Error');
            }
        });
    };

    app.arePopupsBlocked = function () {
        var aWindow = window.open(null, "", "width=1,height=1");
        try {
            aWindow.close();
            return false;
        } catch (e) {
            return true;
        }
    };
    return app;
})(window.myApp || {})