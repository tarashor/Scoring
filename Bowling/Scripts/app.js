var app = angular.module('bowlingApp', []);

function createNewFrame()
{
    return { first: 0, second: 0, third: 0 };
}

app.controller('ScoreController', ['$http', function ($http) {
    var score = this;
    score.MAX_FRAMES = 10;
    score.PINS_IN_FRAME = 10;
    score.newFrame = createNewFrame();
    score.frames = [];
    score.total = 0;

    score.addNewFrame = function () {
        if (score.validateNewFrame()) {
            var newFrames = score.frames.concat(score.getNewFrame());

            $http.post('/Main/Count', { frames: newFrames }).
              success(function (data) {
                  score.total = data.score;
                  score.frames.push(score.getNewFrame());
                  score.newFrame = createNewFrame();
              }).
              error(function (data) {
                  alert(data.message);
              });
        } else {
            alert("Input values is not correct!!");
        }
    };

    score.isStrike = function (frame) {
        return frame.first === 10;
    };

    score.isSpare = function (frame) {
        return (frame.first + frame.second) === 10;
    };

    score.canRollSecond = function () {
        return !score.isStrike(score.newFrame) || score.isLastFrame();
    };

    score.hasSecondRoll = function (frame) {
        return !score.isStrike(frame) || (score.frames.indexOf(frame) === (score.MAX_FRAMES - 1));
    };

    score.canRollThird = function () {
        return score.isLastFrame() && (score.isSpare(score.newFrame) || score.isStrike(score.newFrame));
    };

    score.hasThirdRoll = function (frame) {
        return (score.frames.indexOf(frame) === score.MAX_FRAMES - 1) && (score.isSpare(frame) || score.isStrike(frame));
    };

    score.isLastFrame = function () {
        return score.frames.length === (score.MAX_FRAMES - 1);
    };

    score.gameFinished = function () {
        return score.frames.length === score.MAX_FRAMES;
    };

    score.getNewFrame = function () {
        if (score.canRollThird())
            return score.newFrame;
        else
            return { first: score.newFrame.first, second: score.newFrame.second };
    };

    score.validateNewFrame = function () {
        if (!score.canRollThird()) {
            return (score.newFrame.first + score.newFrame.second <= score.PINS_IN_FRAME) && (score.newFrame.third === 0);
        }
        else {
            if (score.isSpare(score.newFrame)) {
                return (score.newFrame.first + score.newFrame.second <= score.PINS_IN_FRAME) && (score.newFrame.third <= score.PINS_IN_FRAME);
            } else if (score.isStrike(score.newFrame)) {
                return (score.newFrame.first <= score.PINS_IN_FRAME) && (score.newFrame.second <= score.PINS_IN_FRAME) && (score.newFrame.third <= score.PINS_IN_FRAME);
            }
        }
    };

}]);