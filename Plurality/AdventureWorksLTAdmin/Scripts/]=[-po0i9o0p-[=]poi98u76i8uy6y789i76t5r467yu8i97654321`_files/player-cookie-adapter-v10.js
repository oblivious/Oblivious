/*!3ec7371f-671b-4791-8b70-37aa4ad5c752*/

function PlayerCookieAdapter(viewData, activexSupportService) {
    PlayerCookieAdapter.prototype.viewData = viewData;
    PlayerCookieAdapter.prototype.activexSupportService = activexSupportService;
}

PlayerCookieAdapter.prototype.Html5PlayerString = "html5";
PlayerCookieAdapter.prototype.SilverlightPlayerString = "slv";
PlayerCookieAdapter.prototype.WmpPlayerString = "wmp";
PlayerCookieAdapter.prototype.TranscriptWidthDefault = 500;
PlayerCookieAdapter.prototype.TranscriptHeightDefault = 100;
PlayerCookieAdapter.prototype.TranscriptTopDefault = 60;
PlayerCookieAdapter.prototype.TranscriptLeftDefault = 200;
PlayerCookieAdapter.prototype.TranscriptsDockedDefault = true;
PlayerCookieAdapter.prototype.TranscriptsEnabledDefault = false;

PlayerCookieAdapter.prototype.cookie = null;

PlayerCookieAdapter.prototype.getCookie = function () {
    if (this.cookie) {
        return this.cookie;
    }

    var cookieString = $.cookie('psPlayer');
    var cookie;
    if (cookieString) {
        cookie = JSON.parse(cookieString);
    } else {
        cookieString = $.cookie('psPlayerCookie');
        if (cookieString) {
            this.saveCookie(cookieString);
            $.removeCookie('psPlayerCookie', { path: '/' });
            cookie = JSON.parse(cookieString);
        } else {
            cookie = { };
        }
    }
    this.cookie = cookie;
    return cookie;
};

PlayerCookieAdapter.prototype.saveCookie = function (cookieString) {
    $.cookie('psPlayer', cookieString, { expires: 10000, path: '/', domain: this.viewData.cookieDomain });
};

PlayerCookieAdapter.prototype.setCookie = function (cookie) {
    this.cookie = cookie;
    this.saveCookie(JSON.stringify(cookie));
};
PlayerCookieAdapter.prototype.getCookieValue = function (attributeName) {
    var cookie = this.getCookie();
    return cookie[attributeName];
};
PlayerCookieAdapter.prototype.setCookieValue = function (attributeName, value) {
    var cookie = this.getCookie();
    cookie[attributeName] = value;
    this.setCookie(cookie);
};

PlayerCookieAdapter.prototype.getVideoScaling = function () {
    var scaling = this.getCookieValue("videoScaling");
    if (!scaling) {
        scaling = "Scaled";
    }
    return scaling;
};
PlayerCookieAdapter.prototype.setVideoScaling = function (value) {
    this.setCookieValue("videoScaling", value);
};

PlayerCookieAdapter.prototype.getVideoQuality = function () {
    var quality = this.getCookieValue("videoQuality");
    if (!quality) {
        quality = "High";
    }
    return quality;
};
PlayerCookieAdapter.prototype.setVideoQuality = function (value) {
    this.setCookieValue("videoQuality", value);
};

PlayerCookieAdapter.prototype.getPlaybackSpeed = function () {
    var speed = this.getCookieValue("playbackSpeed");
    if (!speed) {
        speed = 1.0;
    }
    return speed;
};
PlayerCookieAdapter.prototype.setPlaybackSpeed = function (value) {
    this.setCookieValue("playbackSpeed", value);
};

PlayerCookieAdapter.prototype.getVolume = function () {
    var volume = this.getCookieValue("volume");
    if (!volume && volume != 0) {
        volume = 50;
    }
    return volume;
};
PlayerCookieAdapter.prototype.setVolume = function (value) {
    this.setCookieValue("volume", value);
};

PlayerCookieAdapter.prototype.getAutoPlayEnabled = function () {
    var autoPlayEnabled = this.getCookieValue("autoPlayEnabled");
    if (typeof autoPlayEnabled === "undefined") {
        autoPlayEnabled = true;
    }
    return autoPlayEnabled ? "true" : "false";
};
PlayerCookieAdapter.prototype.setAutoPlayEnabled = function (value) {
    var boolVal = (value === true || value === "true");
    this.setCookieValue("autoPlayEnabled", boolVal);
};

PlayerCookieAdapter.prototype.getLeftNavEnabled = function () {
    var leftNavEnabled = this.getCookieValue("leftNavEnabled");
    if (typeof leftNavEnabled === "undefined") {
        leftNavEnabled = false;
    }
    return leftNavEnabled ? "true" : "false";
};
PlayerCookieAdapter.prototype.setLeftNavEnabled = function (value) {
    var boolVal = (value === true || value === "true");
    this.setCookieValue("leftNavEnabled", boolVal);
};

PlayerCookieAdapter.prototype.getPlayerHintsDismissalCount = function () {
    var count = this.getCookieValue("playerHintsDismissalCount");
    if (!count) {
        count = 0;
    }
    return count;
};
PlayerCookieAdapter.prototype.setPlayerHintsDismissalCount = function (value) {
    this.setCookieValue("playerHintsDismissalCount", value);
};

PlayerCookieAdapter.prototype.getPreferredPlayer = function () {
    var preferredPlayer = this.getCookieValue("preferredPlayer");
    if (!preferredPlayer) {
        preferredPlayer = this.Html5PlayerString;
    }
    return preferredPlayer;
};


PlayerCookieAdapter.prototype.setPreferredPlayer = function (value) {
    this.setCookieValue("preferredPlayer", value);
};

PlayerCookieAdapter.prototype.getPreferredVideoPlayback = function () {
    var versionContains6 = viewData.browserVersion.indexOf('6.') > -1;
    var versionContains7 = viewData.browserVersion.indexOf('7.') > -1;
    var versionContains8 = viewData.browserVersion.indexOf('8.') > -1;

    var versionIs6or7or8 = versionContains6 || versionContains7 || versionContains8;

    var preferredVideoPlayback = this.getCookieValue("preferredVideoPlayback");
    if (!this.activexSupportService.isActivexSupported) {
        preferredVideoPlayback = playerEnum.Html5;
    }
    if (!preferredVideoPlayback) {
        var preferredPlayer = this.getPreferredPlayer();
        if (!preferredPlayer) {
            preferredVideoPlayback =  playerEnum.Html5;
        }
        else if (preferredPlayer === this.Html5PlayerString) {
            preferredVideoPlayback = playerEnum.Html5;
        }
        else if (preferredPlayer === this.SilverlightPlayerString) {
            preferredVideoPlayback = playerEnum.Silverlight;
        }
        else if (preferredPlayer === this.WmpPlayerString && viewData.isMsie) {
            preferredVideoPlayback = playerEnum.Wmp;
        } else {
            preferredVideoPlayback = playerEnum.Html5;
        }
    }
    if (preferredVideoPlayback == playerEnum.Html5 && viewData.isMsie && versionIs6or7or8) {
        preferredVideoPlayback = playerEnum.Silverlight;
    }
    return preferredVideoPlayback;
};
PlayerCookieAdapter.prototype.setPreferredVideoPlayback = function (value) {
    this.setCookieValue("preferredVideoPlayback", value);
};

PlayerCookieAdapter.prototype.getTranscriptsEnabled = function () {
    var transcriptsEnabled = this.getCookieValue("transcriptsEnabled");
    if (!transcriptsEnabled) {
        transcriptsEnabled = this.TranscriptsEnabledDefault;
    }
    return transcriptsEnabled;
};
PlayerCookieAdapter.prototype.setTranscriptsEnabled = function (value) {
    this.setCookieValue("transcriptsEnabled", value);
};