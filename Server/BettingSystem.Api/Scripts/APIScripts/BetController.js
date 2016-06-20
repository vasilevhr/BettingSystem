app.controller('BetController', function ($scope, betService) {

    getAllBets();

    function getAllBets() {
        var servCallBets = betService.getBets();
        servCallBets.then(function (b) {
            $scope.bets = b.data;
        }, function (error) {
            $log.error('Oops! Something went wrong while fetching the data.')
        })
    }
})