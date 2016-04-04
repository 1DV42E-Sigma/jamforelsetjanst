angular.module('textSizeSlider', [])
  .directive('textSizeSlider', ['$document', function ($document) {
      return {
          restrict: 'E',
          template: '<div class="text-size-slider"><span class="small-letter" ng-style="{ fontSize: min + unit }">A</span> <input type="range" min="{{ min }}" max="{{ max }}" step="{{ step || 0 }}" ng-model="textSize" class="slider" value="{{ value }}" /> <span class="big-letter" ng-style="{ fontSize: max + unit }">A</span></div>',
          scope: {
              min: '@',
              max: '@',
              unit: '@',
              value: '@',
              step: '@'
          },
          link: function (scope, element, attr) {
              scope.textSize = scope.value;
              console.log($document[0]);
              scope.$watch('textSize', function (size) {
                  $document[0].body.style.fontSize = size + scope.unit;
                  console.log($document);
              });
          }
      }
  }]);