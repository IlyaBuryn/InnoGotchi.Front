function InviteFriend() {

    var username = $("#setUsername").val()
    console.log(username)
    if (username != '') {
        let formData = {
            username: username,
        }

        $.ajax({
            url: "/Farm/InviteFriend",
            type: "POST",
            data: formData,
            success: function (response) {
                console.log('success!');
                location.reload();
            },
            error: function (request, status, error) {
                alert(status + ': ' + error + ' - ' + request);
            }
        });

    }
    else {
        console.log('empty field');
    }
};

function setImageView(image, bpType, bpId) {
    var imageView = document.getElementById("imageView" + bpType);
    var ROOT = '../';
    imageView.src = ROOT + image.slice(2);

    var input = document.getElementById("bpInput" + bpType)
    input.value = bpId
};