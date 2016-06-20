app.service("appService", function ($http) {
    this.getMatches = function () {
        return $http.get("api/Matches")
    }
})


app.service("betService", function ($http) {
    this.getBets = function () {
        return $http.get("api/Bets")
    }
})
