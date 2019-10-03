angular.module('nemesisApp').controller('orderController', function ($rootScope, $scope, $http, $mdDialog, $location) {
  if (!$rootScope.user)
    return
  $scope.isAdmin = $rootScope.user.tipo.toUpperCase() == 'ADMIN'

  $scope.orders = []

  $scope.refresh = () => {
    $http.get('api/pedido/')
      .then((response) => {
        $scope.errors = response.data.errors
        if ($scope.errors) {
          console.log($scope.errors)
        } else {
          $scope.orders = response.data
        }
      });
  }

  $scope.changeStatus = (id) => {
    $http.post('api/pedido/status/' + id)
      .then((response) => {
        $scope.errors = response.data.errors
        if ($scope.errors) {
          console.log($scope.errors)
        } else {
          $scope.refresh()
        }
      });
  }

  $scope.refresh()

  $scope.openDetail = (p) => {
    $rootScope.currentOrder = p
    $location.path('detail-order')
  }
})