var profiles = [];
var current = 0;

function preloadImage(url) {
    var img = new Image();
    img.src = url;
}

function fetchSomeProfiles() {
    for (var i = 0; i < 5 - (profiles.length - current) ; i++) {
        fetchNextProfile();
    }
}

function setPhoto(ind) {
    $("#photoblock-photo").css('opacity', '1.0');
    $("#photoblock-photo").attr('src', profiles[ind].ImgUrl);
    console.log(ind);
    console.log(profiles);
}

function fetchNextProfile() {
    var formdata = $("#filter-form").serialize();
    $.post($("#filter-form").attr('action'), formdata, function (response) {
        //alert(response);
        var prof = response;
        //var prof = JSON.parse(response);
        profiles.push(prof);
        preloadImage(prof.ImgUrl);
        if (current == profiles.length - 1) {
            setPhoto(current);
        }
    });
}
function setWait() {
    $("#photoblock-photo").css('opacity', '0.3');
}

$(document).ready(function () {
    //fetchSomeProfiles();
    fetchNextProfile();
    //alert("here");
    $("#btn-prev-photo").click(function () {
        current = Math.max(current - 1, 0);
        setPhoto(current);
        return false;
    });
    $("#btn-next-photo").click(function () {
        if (current < profiles.length - 1) {
            setPhoto(++current);
        }
        else if (current == profiles.length - 1) {
            ++current;
            setWait();
        }
        fetchSomeProfiles();
        return false;
    });
});