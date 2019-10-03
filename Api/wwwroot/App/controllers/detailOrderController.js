angular.module('nemesisApp')
  .controller('detailOrderController', function ($scope, $rootScope, $http) {

    if (!$rootScope.user)
      return

    $scope.products = []
    $scope.total = '0,00'
    $scope.order = {}

    const order = $rootScope.currentOrder

    $scope.computeTotal = () => {
      $scope.total = 0
      for (let i = 0; i < $scope.products.length; i++)
        $scope.total += $scope.products[i].preco * $scope.products[i].count
      $scope.total = toMoney($scope.total)
    }

    if (order) {
      $rootScope.currentOrder = null
      $scope.isDetail = true
      $scope.products = order.produtos
      $scope.order = order
      $scope.computeTotal()
    }
  })