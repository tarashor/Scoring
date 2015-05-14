var app = angular.module('bowlingApp', []);

function createNewFrame()
{
    return { first: 0, second: 0, third: 0 };
}

app.controller('ScoreController', ['$http', function ($http) {
    var score = this;
    score.MAX_FRAMES = 10;
    score.newFrame = createNewFrame();
    score.match = { frames: [] };
    score.total = 0;

    score.addNewFrame = function () {
        var newFrames = score.match.frames.concat(score.newFrame);

        $http.post('/Main/Count', { frames: newFrames }).
          success(function (data) {
              score.total = data.score;
              score.match.frames.push(score.newFrame);
              score.newFrame = createNewFrame();
          }).
          error(function (data) {
              alert(data.message);
          });
    };

    score.isLastFrame = function () {
        return score.match.frames.length === (score.MAX_FRAMES - 1);
    };

    score.isStrike = function (frame) {
        return frame.first === 10;
    };

    score.isSpare = function (frame) {
        return (frame.first + frame.second) === 10;
    };

    score.canRollThird = function () {
        return score.isLastFrame() && (score.isSpare(score.newFrame) || score.isStrike(score.newFrame));
    };

    score.gameFinished = function () {
        return score.match.frames.length === score.MAX_FRAMES;
    };

    

}]);