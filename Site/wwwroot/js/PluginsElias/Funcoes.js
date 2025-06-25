$(document).ready(function () {
    
    RenderAction();

    $('.telefone').mask('(99) 99999-9999', { 'placeholder': '(  )' })
    $('.celular').mask('(99) 99999-9999', { 'placeholder': '(  )' })
    $('.date').mask('99/99/9999', { 'placeholder': '__/__/____' })
    $('.datetime').mask('99/99/9999 99:99', { 'placeholder': '__/__/____ 00:00' })
    $('.time').mask('99:99', { 'placeholder': '00:00' });
    $('.cpf').mask('999.999.999-99', { 'placeholder': '000.000.000-00' });
    $('.cep').mask('99999-999', { 'placeholder': '00000-000' });
});

function Excluir(e) {

    var token = $("input[type = hidden][name = __RequestVerificationToken]").val();

    var url = e.currentTarget.dataset.url;
    var id = e.currentTarget.dataset.id;

    var data = {
        __RequestVerificationToken: token,
        Id: id
    }

    Swal.fire({
        title: 'Deseja Excluir o Registro?',
        showDenyButton: false,
        showCancelButton: true,
        confirmButtonText: `Sim`,
        cancelButtonText: `Não`,
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                method: 'POST',
                url: url + id,
                data: data,
                success: function (data) {
                    if (data.result) {
                        singleMessage(data.message, data.type);
                        window.location.reload();
                    } else {
                        singleMessage(data.message, data.type);
                    }
                }
            });
        }
    })
}

function RenderAction() {

    var element = document.getElementsByClassName('RenderAction');

    for (var i = 0; i < element.length; i++) {

        var Url = element[i].dataset.url;
        var Id = "#" + element[i].id;

        $.ajax({
            method: 'GET',
            url: Url,
            dataType: 'html',
            success: function (content) {
                $(Id).html(content);
            }
        });
    }
}

function CriarCookie(cname, cvalue, exdays) {
    var d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    var expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
}

function ObterCookie(cname) {
    var name = cname + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}

function VerificaCookie() {
    var user = getCookie("username");
    if (user != "") {
        alert("Welcome again " + user);
    } else {
        user = prompt("Please enter your name:", "");
        if (user != "" && user != null) {
            setCookie("username", user, 365);
        }
    }
}

