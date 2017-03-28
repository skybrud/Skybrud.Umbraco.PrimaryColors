angular.module('umbraco').controller('Skybrud.PrimaryColors.Controller', function ($scope) {

    $scope.color = null;
    $scope.mostUsedColors = [];
    $scope.primaryColors = [];

    $scope.hideMostUsedColors = $scope.model.config.hideMostUsedColors == '1';

    function updateModel() {
        var temp = [];
        temp.push($scope.color + '');
        temp.push($scope.mostUsedColors.join(' '));
        temp.push($scope.primaryColors.join(' '));
        $scope.model.value = temp.join('\n');
    }

    function getArray(str) {

        str = $.trim(str);

        var temp = [];
        angular.forEach(str.split(' '), function (item) {
            temp.push(item);
        });

        return temp;

    }

    if ($scope.model.value) {

        var array = $scope.model.value.split('\n');

        if (array[0]) $scope.color = $.trim(array[0]);

        if (array.length > 1) $scope.mostUsedColors = getArray(array[1]);
        if (array.length > 2) $scope.primaryColors = getArray(array[2]);

    }

    $scope.select = function (item) {
        $scope.color = item;
        updateModel();
    };

});