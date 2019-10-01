angular.module('nemesisApp').controller('loginController', function ($location, $scope, $http) {

    $scope.login = () => {
        $http.post('/token', { login: $scope.name, password: $scope.password })
            .then(res => {
                const user = res.data.user;
                user.token = res.data.token;
                localStorage.setItem('nemesisUser', JSON.stringify(user));
                location.reload();
            }).catch(() => {
                $scope.error = 'Credencias inválidas.';
            });
    }


    $scope.onKeyDown = (e) => {
        if (e.key == 'Enter')
            $scope.login();
        else
            $scope.error = '';
    }
});