/*! 4168ca71-feaf-4541-912e-2bf96d1107af */
(function (w) {
    "use strict";

    w.LaunchPlayerController = function ($scope, windowAdapter) {
        $scope.launchPlayerWindow = function (rootPath, playerParameters) {
            windowAdapter.openPlayerWindow(rootPath + "/Player?" + playerParameters);
        };
    };

    w.LaunchPlayerController.$inject = ['$scope', 'windowAdapter'];
} (window));