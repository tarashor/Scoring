var app = angular.module('bowlingApp', []);

app.controller('ScoreController', ['$http', function ($http) {
    var score = this;
    score.newFrame = {}
    score.match = { frames: [] };
    score.total = 0;

    score.addNewFrame = function () {
        var newFrames = score.match.frames.slice();
        newFrames.push(score.newFrame);

        $http.post('/Main/Count', { frames: newFrames }).
          success(function (data) {
              score.total = data.score;
              score.match.frames.push(score.newFrame);
              score.newFrame = {};
          }).
          error(function (data) {
          });

    };
}]);