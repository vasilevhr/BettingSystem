app.controller('APIController', function ($scope, appService) {

    getAll();

    function getAll() {
        var servCall = appService.getMatches();
        servCall.then(function (d) {
            $scope.matches = d.data;
        }, function (error) {
            $log.error('Oops! Something went wrong while fetching the data.')
        })
    }
})



