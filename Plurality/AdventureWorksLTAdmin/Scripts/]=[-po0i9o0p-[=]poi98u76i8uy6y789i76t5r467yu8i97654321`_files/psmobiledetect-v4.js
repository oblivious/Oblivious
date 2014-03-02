if (typeof PS == 'undefined') {
    PS = {};
}

PS.cookieName = "skipmobile";

PS.detectMobilePlatform = function (url) {
    var ua = navigator.userAgent;

    if (PS.readCookie(PS.cookieName))
        return;

    var mobileAppLanding = url;

    var plats = [
        'iPhone',
        'Android',
        'iPad',
        'Windows Phone OS'
    ];

    $.each(plats, function (ndx, patt) {
        if (ua.match(new RegExp(patt, "i"))) {
            window.location = mobileAppLanding;
        }
    });
};

PS.readCookie = function (name) {
    var nameEQ = name + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1, c.length);
        if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
    }
    return null;
};

PS.createCookie = function (name, value, days) {
    var expires = "";

    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        expires = "; expires=" + date.toGMTString();
    }
    document.cookie = name + "=" + value + expires + "; path=/";
};

PS.skipMobileDetection = function () {
    PS.createCookie(PS.cookieName, "skip", 10 * 365);
};
   