angular.module('PartyApp', []).
controller('PartyController', function ($scope) {
    $scope.partyPrice = 500;
    $scope.incomePrice = 100;
    $scope.privatePartyPrice = 30;
    $scope.restrictedPartyPrice = 20;
    $scope.exclusivePartyPrice = 30;
    $scope.checklistPrice = 20;
    $scope.orderedPrice = 30;
    $scope.selfServicePrice = 40;

    $scope.incomeValue;
    $scope.publicPartyValue;
    $scope.partyTypeValue;
    $scope.foodTypeValue;
    $scope.drinkTypeValue;

    $scope.init = function (incomeValue, partyTypeValue, foodTypeValue, drinkTypeValue, publicPartyValue) {
        $scope.incomeValue = incomeValue;
        $scope.partyTypeValue = partyTypeValue;
        $scope.foodTypeValue = foodTypeValue;
        $scope.drinkTypeValue = drinkTypeValue;
        $scope.publicPartyValue = publicPartyValue.toLowerCase() == "true";
    }

    $scope.SummedPrice = function () {
        var summedPrice = 0;
        
        summedPrice += $scope.partyPrice;

        if (!$scope.publicPartyValue)
            summedPrice += $scope.privatePartyPrice;

        if ($scope.partyTypeValue == "Restricted")
            summedPrice += $scope.restrictedPartyPrice;
        else if ($scope.partyTypeValue == "Exclusive")
            summedPrice += $scope.exclusivePartyPrice;

        if ($scope.foodTypeValue == "Checklist")
            summedPrice += $scope.checklistPrice;
        else if ($scope.foodTypeValue == "Ordered")
            summedPrice += $scope.orderedPrice;
        else if ($scope.foodTypeValue == "Self-service")
            summedPrice += $scope.selfServicePrice;

        if ($scope.drinkTypeValue == "Checklist")
            summedPrice += $scope.checklistPrice;
        else if ($scope.drinkTypeValue == "Ordered")
            summedPrice += $scope.orderedPrice;
        else if ($scope.drinkTypeValue == "Self-service")
            summedPrice += $scope.selfServicePrice;

        if ($scope.incomeValue > 0)
            summedPrice += $scope.incomePrice;

        return summedPrice + " PP.";
    }
});