'use strict';
app.controller('boardDetailController', ['$scope', 'boardDetailService', '$routeParams', '$modal', function ($scope, boardDetailService, $routeParams, $modal) {
    $scope.boardId = $routeParams.boardId;
    $scope.board = {};

    boardDetailService.getBoard($scope.boardId).then(function (results) {
        $scope.board = results.data;
    }, function (error) {
        console.log(error.data.message);
    });

    $scope.addCardShow = function (list) {
        _.each($scope.board.lists, function(lt) {
            lt.isAddCardShow = false;
        });
        list.isAddCardShow = true;
    };

    $scope.addCard = function (list) {
        if (list.newCardTitle) {
            var newCardData = {
                boardId: $scope.boardId,
                listId: list.id,
                name: list.newCardTitle
            };
            boardDetailService.addList(newCardData).then(function (results) {
                list.cards = results.data;
                hideAddCard(list);
            }, function(error) {
                console.error(error.data.message);
            });
        }
    };

    $scope.cancelAddCard = function (list) {
        hideAddCard(list);
    };

    var hideAddCard = function(list) {
        list.isAddCardShow = false;
        list.newCardTitle = "";
    };

    $scope.cardsSortableOptions = {
        placeholder: "list-card placeholder",
        connectWith: ".list-cards",
        start: function (e, ui) {
            ui.placeholder.height(ui.item.height());
        },
        stop: function (e, ui) {
            var listId = ui.item.scope().card.listId;
            var cards = ui.item.sortable.sourceModel;
            var ids = _.map(cards, function (item) {
                return item.id;
            });
            boardDetailService.updateCardsPosition(listId, ids).then(function (results) {
            }, function (error) {
                console.error(error.data.message);
            });
        }
    };

    $scope.listsSortableOptions = {
        placeholder: "list placeholder",
        start: function (e, ui) {
            ui.placeholder.height(ui.item.height());
        },
        stop: function (e, ui) {
            var lists = ui.item.sortable.sourceModel;
            var ids = _.map(lists, function (item) {
                return item.id;
            });
            boardDetailService.updateListsPosition(ids).then(function (results) {
            }, function (error) {
                console.error(error.data.message);
            });
        }
    };

    $scope.showDetailCard = function (detailCard) {
        var modalInstance = $modal.open({
            templateUrl: 'detailCardModal.html',
            scope: $scope,
            controller: function ($scope, $modalInstance, detailCard) {
                //$scope.newBoard = newBoard;
                //$scope.submit = function () {
                //    boardsService.addBoard($scope.newBoard).then(function (results) {
                //        $scope.$parent.boards = results.data;
                //        $scope.$parent.updateStarredBoards();
                //    }, function (error) {
                //        console.error(error.data.message);
                //    });
                //    $scope.$parent.resetNewBoard();
                //    $modalInstance.dismiss('cancel');
                //};
                //$scope.cancel = function (e) {
                //    e.preventDefault();
                //    $scope.$parent.resetNewBoard();
                //    $modalInstance.dismiss('cancel');
                //};
            },
            resolve: {
                detailCard: function () {
                    return detailCard;
                }
            }
        });
    };

}]);