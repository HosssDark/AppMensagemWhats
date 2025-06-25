$(document).ready(function () {
    $('#File').change(readImage);
});

function readImage() {
    if (this.files && this.files[0]) {
        var file = new FileReader();
        file.onload = function (e) {
            document.getElementById("preview").src = e.target.result;
        };

        file.readAsDataURL(this.files[0]);
    }
}

function readImageCover() {
    if (this.files && this.files[0]) {
        var file = new FileReader();
        file.onload = function (e) {
            document.getElementById("previewCover").src = e.target.result;
        };

        file.readAsDataURL(this.files[0]);
    }
}