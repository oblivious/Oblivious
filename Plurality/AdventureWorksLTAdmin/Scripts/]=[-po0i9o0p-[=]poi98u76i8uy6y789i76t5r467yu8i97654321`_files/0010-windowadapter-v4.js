/*! 599105db-c53f-41d4-8716-6b47c29e7353 */
(function (w) {
    "use strict";

    w.DomModule.factory('windowAdapter', [function () {

        var windowAdapter = {

            openPlayerWindow: function (url) {
                var playerWindowOptions = "width=1354,height=836,status=0,titlebar=0,scrollbars=0,menubar=0,toolbar=0,location=0,resizable=1";

                window.open(url, "psplayer", playerWindowOptions);
            }

        };

        return windowAdapter;
    } ]);

} (window));