angular.module('consume', [])
.controller('Hello', function($scope, $http) {
    $http.get('localhost:4200/snacks').
        then(function(response) {
            $scope.greeting = response.data;
        });
});