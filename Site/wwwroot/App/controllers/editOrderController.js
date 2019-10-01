angular.module('nemesisApp')
  .controller('editOrderController', function ($scope, $rootScope, $http, $mdDialog, $location) {

    if (!$rootScope.user)
      return

    $scope.products = []
    $scope.total = '0,00'

    $scope.computeTotal = () => {
      $scope.total = 0
      for (let i = 0; i < $scope.products.length; i++)
        $scope.total += $scope.products[i].preco * $scope.products[i].count
      $scope.total = toMoney($scope.total)
    }

    $scope.save = () => {
      $http.post('api/pedido/', { produtos: $scope.products })
        .then((response) => {
          $scope.errors = response.data.errors
          if (!$scope.errors) {
            $location.path('/order');
          } else {
            console.log($scope.errors)
          }
        });
    }

    const getItemFromList = (id) => {
      let itemList = null
      if (id)
        for (let i = 0; i < $scope.products.length; i++)
          if (id == $scope.products[i].id)
            itemList = $scope.products[i]
      return itemList
    }

    const removeItemList = (id) => {
      let temp = []
      for (let i = 0; i < $scope.products.length; i++)
        if ($scope.products[i].id != id)
          temp.push($scope.products[i])
      $scope.products = temp
    }

    $scope.remove = (product) => {
      let itemList = getItemFromList(product.id)
      if (itemList.count > 1)
        itemList.count--;
      else {
        let temp = []
        removeItemList(product.id)
      }
      $scope.computeTotal()
    }

    $scope.openProductModal = (product) => {
      $mdDialog.show({
        clickOutsideToClose: true,
        templateUrl: 'App/templates/product.html',
        controller: 'productController',
        locals: { orderModal: true }
      }).finally(function () {
        if ($rootScope.selectProduct) {
          const p = $rootScope.selectProduct
          p.count = 1
          let itemList = null
          for (let i = 0; i < $scope.products.length; i++)
            if (p.id == $scope.products[i].id)
              itemList = $scope.products[i]
          if (itemList)
            itemList.count++
          else
            $scope.products.push(p)
          $scope.computeTotal()
        }
        $rootScope.selectProduct = null
      });
    }
  })