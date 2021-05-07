var reg = {
    init: function () {

    },
    registerEvent: function () {
        $('#register').off('click').on('click', function (e) {
            e.preventDefault();
            $.ajax({
                url:"/User/"
            })
        });
    }
}