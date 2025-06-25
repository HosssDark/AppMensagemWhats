$(function () {
    DashCards();
    DashMensagem();
    //Notification();
    UserImage();
});

function DashCards() {
    $.ajax({
        method: 'GET',
        url: '/Admin/Home/DashCards',
        dataType: 'html',
        success: function (content) {
            $('#DashCards').html(content);
        }
    });
}

function DashMensagem() {
    $.ajax({
        method: 'GET',
        url: '/Admin/Home/DashMensagem',
        dataType: 'html',
        success: function (content) {
            $('#DashMensagem').html(content);
        }
    });
}

function Notification() {
    $.ajax({
        method: 'GET',
        url: '/Admin/Notification/GetNotification',
        dataType: 'html',
        success: function (content) {
            $('#Notification').html(content);
        }
    });
}

function UserImage() {
    $.ajax({
        method: 'GET',
        url: '/Admin/Home/UserImage',
        dataType: 'html',
        success: function (content) {
            $('#UserImage').html(content);
        }
    });
}