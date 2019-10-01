angular.module('nemesisApp').controller('categoryController', function ($scope, $rootScope, $http, $mdDialog) {

  if (!$rootScope.user)
    return
  $scope.profissions = [];

  $scope.isAdmin = $rootScope.user.tipo.toUpperCase() == 'ADMIN'

  $scope.refresh = () => {
    $http.get('/api/categoria')
      .then(res => {
        $scope.categories = res.data;
      }, err => {
        console.log(err)
      });
  }

  $scope.refresh();

  $scope.remove = (profission, ev) => {
    $mdDialog.show(
      $mdDialog.confirm()
        .textContent('Excluir categoria ?')
        .ariaLabel('Excluir')
        .targetEvent(ev)
        .ok('Sim')
        .cancel('Não')
    ).then(function () {
      $http.delete('api/categoria/' + profission.id)
        .then(response => {
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

  $scope.new = function (ev) {
    var confirm = $mdDialog.prompt()
      .placeholder('Nome da categoria')
      .ariaLabel('Nome')
      .targetEvent(ev)
      .ok('ok')
      .cancel('Cancelar');

    $mdDialog.show(confirm).then(function (name) {
      $http.post('api/categoria/', { nome: name })
        .then(response => {
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
})