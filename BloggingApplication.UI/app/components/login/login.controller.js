angular.module('components').controller('LoginController', ['$scope',  '$http', '$window', function LoginController($scope, $http, $window) {

    $scope.user = {};
    
    

    $scope.loginIn = function () {



        $http({
            url: 'http://localhost:9080/api/User/Login',
            method: 'POST',
            params: { username: $scope.user.userName, password: $scope.user.password }
            ,
            dataType: 'json',
            headers: {
                "Content-Type": "application/json"
            }
        }).then(
            onSuccess,onError
        );

        function onSuccess(response) { 
           
            var token = response.data.data.token;
            $window.sessionStorage.setItem('token', token);
            location.href =  "/Posts.html";
            
        }

        function onError(e) {
            alert(e.data.message);
        }


    }

}]);