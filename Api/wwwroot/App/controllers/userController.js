angular.module('nemesisApp').controller('userController',
  function ($scope, $mdSidenav, $rootScope, $location, $http, $mdDialog) {

    $scope.user = {}

    $scope.refresh = () => {
      $http.get('/api/usuario')
        .then(res => {
          $scope.users = res.data
        }, err => {
          console.log(err)
        })
    }

    $scope.refresh()

    $scope.remove = (user) => {

      // Appending dialog to document.body to cover sidenav in docs app
      var confirm = $mdDialog.confirm()
        .title('Remover usuário ?')
        .textContent(user.email)
        .ariaLabel('Lucky day')
        .ok('Sim')
        .cancel('Não');

      $mdDialog.show(confirm).then(() => {

        $http.delete('api/usuario/' + user.id)
          .then((response) => {
            const errors = response.data.errors
            if (errors) {
              $mdDialog.show(
                $mdDialog.alert()
                  .clickOutsideToClose(true)
                  .title(errors[0])
                  .ariaLabel('Alert Dialog Demo')
                  .ok('OK')
              )
            } else
              $scope.refresh()
          })

      })
    }

    $scope.openModal = (user) => {
      let callback = $scope.refresh
      $mdDialog.show({
        clickOutsideToClose: false,
        templateUrl: 'App/templates/editUser.html',
        controller: function ($scope, $mdDialog, $http) {
          $scope.close = () => { $mdDialog.hide() }
          $scope.user = user ? angular.copy(user) : {}

          if ($scope.user.tipo)
            $scope.user.tipo = $scope.user.tipo.toLowerCase()

          $scope.validate = () => {
            $scope.errors = []
            if ($scope.user.password && $scope.user.password != $scope.user.confirm)
              $scope.errors.push('A senhas não coincidem')
            return $scope.errors.length == 0
          }

          $scope.save = () => {
            if (!$scope.validate())
              return
            if ($scope.user.id > 0) {
              $http.put('api/usuario/', $scope.user)
                .then((response) => {
                  $scope.errors = response.data.errors
                  if (!$scope.errors) {
                    callback()
                    $mdDialog.hide()
                  }
                });
            } else {
              $http.post('api/usuario/', $scope.user)
                .then((response) => {
                  $scope.errors = response.data.errors
                  if (!$scope.errors) {
                    callback()
                    $mdDialog.hide()
                  }
                });
            }
          }
        }
      })
    }
  })