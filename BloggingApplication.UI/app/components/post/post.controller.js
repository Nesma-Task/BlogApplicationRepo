
angular.module('components').controller('PostController', ['$scope', '$http', '$window', '$q', 'Upload', function PostController($scope, $http, $window, $q, Upload) {

    $scope.posts = [];
    $scope.addPost = {};
    $scope.position = {};


    var input = document.querySelector('input[type=file]'); // see Example 4

    input.onchange = function () {
        var file = input.files[0];
        var fileName = file.name;
        var idxDot = fileName.lastIndexOf(".") + 1;
        var extFile = fileName.substr(idxDot, fileName.length).toLowerCase();
        var fileSize = file.size; // in bytes
        if (fileSize < 2097152) {

            if (extFile === "jpg" || extFile === "png") {
                upload(file);
                drawOnCanvas(file);   // see Example 6
                displayAsImage(file); // see Example 7
            } else {

                alert("Only jpg/jpeg and png files are allowed!");
                return false;
            }
        }
        else
        {
            alert('file size is more than 2MB  bytes');
            return false;
        }
        
    };
  
    function upload(file) {
        var form = new FormData(),
            xhr = new XMLHttpRequest();

        form.append('image', file);
        
    }
    function drawOnCanvas(file) {
        var reader = new FileReader();

        reader.onload = function (e) {
            var dataURL = e.target.result,
                c = document.querySelector('canvas'), // see Example 4
                ctx = c.getContext('2d'),
                img = new Image();

            img.onload = function () {
                c.width = img.width;
                c.height = img.height;
                ctx.drawImage(img, 0, 0);
            };

            img.src = dataURL;
        };

        reader.readAsDataURL(file);
    }
    function displayAsImage(file) {
        var imgURL = URL.createObjectURL(file),
            img = document.createElement('img');

        img.onload = function () {
            URL.revokeObjectURL(imgURL);
        };

        img.src = imgURL;
        //document.body.appendChild(img);
    }

    $scope.getCurrentPosition = function () {
        var deferred = $q.defer();

        if (!$window.navigator.geolocation) {
            deferred.reject('Geolocation not supported.');
        } else {
            $window.navigator.geolocation.getCurrentPosition(
                function (position) {
                    deferred.resolve(position);
                    $scope.position = position;
                    $scope.addPost.Latitude = $scope.position.coords.latitude;
                    $scope.addPost.Longitude = $scope.position.coords.longitude;
                },
                function (err) {
                    deferred.reject(err);
                });
        }

        return deferred.promise;



    };




    $scope.getAllPosts = function () {

        let token = $window.sessionStorage.getItem('token');
        delete $http.defaults.headers.common['X-Requested-With'];
        $http.defaults.headers.common['Authorization'] = 'Bearer ' + token;
        //$http.defaults.headers.common['Content-Type'] = 'application/x-www-form-urlencoded;charset-utf-8';
       
    
        $http({
            method: "GET",
            url: 'http://localhost:9080/api/BlogUserPost/GetAll'
            ,
            headers: {
                //'Content-Type': 'application/json'
                 'Authorization': 'Bearer ' + token 
            } 
        })
            .then(function (response) {
                // success result code handling 
                $scope.posts = response.data;
            },
                function (error) {
                    // error handling code
                    alert('Error on getting data please try again');
                }
            )
        //});
        //$http.get('https://localhost:44392/api/BlogUserPost/GetAll').then(function (response) {
        //    $scope.posts = response.data;
        //});



    };
    $scope.createPost = function () {
        let token = $window.sessionStorage.getItem('token');
        delete $http.defaults.headers.common['X-Requested-With'];
        $http.defaults.headers.common['Authorization'] = 'Bearer ' + token;
        $scope.getCurrentPosition().then(function () {

            Upload.upload({

                url: 'http://localhost:9080/api/BlogUserPost/Create',
                method: 'POST',
                data: $scope.addPost,

                dataType: 'json',
                headers: {
                    //'Content-Type': 'application/json'
                    'Authorization': 'Bearer ' + token
                } 
                //headers: {
                //    "Content-Type": "application/x-www-form-urlencoded",
                //}
            }).then(
                onSuccess, onError
            );

            function onSuccess(response) {
                location.href = "/Posts.html";

            }

            function onError(e) {
                alert(e.data.message);
            }
        });


    }

    this.$onInit = function () {
        $scope.getAllPosts();

    };

}]);