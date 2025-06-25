$(document).ready(function () {
    $.ajax({
        method: 'GET',
        url: '/Admin/Home/Notifications',
        success: function (retorno) {
            messageList(retorno);
        }
    });
});

function messageList(retorno) {
    for (var i = 0; i < retorno.list.length; i++) {
        switch (retorno.list[i].type) {
            //Success
            case 0:
                PNotify.success({
                    title: retorno.list[i].message,
                    text: '',
                    delay: 5000
                });
                break;
            //Error
            case 1:
                PNotify.error({
                    title: retorno.list[i].message,
                    text: '',
                    delay: 5000
                });
                break;
            //Info
            case 2:
                PNotify.info({
                    title: retorno.list[i].message,
                    text: '',
                    delay: 5000
                });
                break;
            //Warning
            case 3:
                PNotify.notice({
                    title: retorno.list[i].message,
                    text: '',
                    delay: 5000
                });
                break;
        }
    }
}

function singleMessage(message, type) {

    switch (type) {
        //Success
        case 0:
            PNotify.success({
                title: message,
                text: '',
                delay: 5000
            });
            break;
        //Error
        case 1:
            PNotify.error({
                title: message,
                text: '',
                delay: 5000
            });
            break;
        //Info
        case 2:
            PNotify.info({
                title: message,
                text: '',
                delay: 5000
            });
            break;
        //Warning
        case 3:
            PNotify.notice({
                title: message,
                text: '',
                delay: 5000
            });
            break;
    }
}