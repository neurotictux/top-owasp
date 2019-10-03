angular.module('nemesisApp').controller('productController', function ($scope, $rootScope, $http, $mdDialog, $location) {

  $scope.product = {}
  $scope.isAdmin = $rootScope.user.tipo.toUpperCase() == 'ADMIN'
  $scope.orderPrice = ''

  $scope.isOrderModal = $location.path().indexOf('edit-order') != -1

  $http.get('/api/categoria')
    .then(res => {
      $scope.categories = res.data
    }, err => {
      console.log(err)
    })

  $scope.itemClick = (product) => {
    if ($scope.isOrderModal) {
      $rootScope.selectProduct = angular.copy(product)
      $mdDialog.hide()
    }
  }

  $scope.refresh = () => {
    let filter = ($scope.category ? $scope.category : 0) + '/' + ($scope.orderPrice ? $scope.orderPrice : 'a')
    $http.get('/api/produto/' + filter)
      .then(res => {
        $scope.products = res.data
      }, err => {
        console.log(err)
      })
  }

  $scope.refresh()

  $scope.filterPrice = () => {
    if (!$scope.orderPrice || $scope.orderPrice == 'asc')
      $scope.orderPrice = 'desc'
    else
      $scope.orderPrice = 'asc'
    $scope.refresh()
  }

  $scope.clearFilter = () => {
    $scope.orderPrice = ''
    $scope.category = undefined
    $scope.refresh()
  }

  $scope.remove = (product) => {
    var confirm = $mdDialog.confirm()
      .title('Remover este produto ?')
      .textContent(product.nome)
      .ok('Sim')
      .cancel('Não')

    $mdDialog.show(confirm).then(() => {

      $http.delete('api/produto/' + product.id)
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

  $scope.openModal = (product) => {
    let callback = $scope.refresh
    $mdDialog.show({
      clickOutsideToClose: false,
      templateUrl: 'App/templates/editProduct.html',
      controller: function ($scope, $mdDialog, $http) {
        $scope.close = () => { $mdDialog.hide() }
        $scope.product = product ? angular.copy(product) : {}

        $http.get('/api/categoria')
          .then(res => {
            $scope.categories = res.data
          }, err => {
            console.log(err)
          })

        $scope.save = () => {
          if ($scope.product.id > 0) {
            $http.put('api/produto/', $scope.product)
              .then((response) => {
                $scope.errors = response.data.errors
                if (!$scope.errors) {
                  callback()
                  $mdDialog.hide()
                }
              });
          } else {
            $http.post('api/produto/', $scope.product)
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

  $scope.showHistory = (product) => {
    let callback = $scope.refresh
    $mdDialog.show({
      clickOutsideToClose: true,
      templateUrl: 'App/templates/historyPrice.html',
      controller: function ($scope, $http) {
        $scope.history = product.historico ? angular.copy(product.historico) : []
        $scope.title = product.nome
      }
    })
  }

})