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
            alert("Verify the score of the frame that you are trying to add.");
        }
    };

    score.isStrike = function (frame) {
        return frame.first === score.PINS_IN_FRAME;
    };

    score.isSpare = function (frame) {
        return (frame.first + frame.second) === score.PINS_IN_FRAME && !score.isStrike(frame);
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

    //it is for this requirement "When a resulting score is returned it should be displayed somewhere on the current page along with the result for each round played. "
    score.scoreOfFrame = function (frame) {
        var res = '';
        if (score.hasThirdRoll(frame)) {
            res = frame.first + frame.second + frame.third;
        }
        else
        {
            if (!score.isSpare(frame) && !score.isStrike(frame)) {
                res = frame.first + frame.second;
            }
            else {
                var index = score.frames.indexOf(frame);
                if (index < score.frames.length - 1) {
                    var nextFrame = score.frames[index + 1]
                    if (score.isSpare(frame)) {
                        res = score.PINS_IN_FRAME + nextFrame.first;
                    } else if (score.isStrike(frame)) {
                        if (score.isStrike(nextFrame)) {
                            if (index < score.frames.length - 2) {
                                var nextNextFrame = score.frames[index + 2]
                                res = score.PINS_IN_FRAME + nextFrame.first + nextNextFrame.first;
                            }
                        }
                        else {
                            res = score.PINS_IN_FRAME + nextFrame.first + nextFrame.second;
                        }
                    }
                }
            }
            
        }
        return res;
    };

    score.validateNewFrame = function () {
        var isValid = (score.newFrame.first >= 0) && (score.newFrame.first <= score.PINS_IN_FRAME) && (score.newFrame.first >= 0) && (score.newFrame.second <= score.PINS_IN_FRAME)&&(score.newFrame.second>= 0) && (score.newFrame.third<= score.PINS_IN_FRAME);
        if (!score.canRollThird()) {
            isValid = isValid && (score.newFrame.first + score.newFrame.second <= score.PINS_IN_FRAME) && (score.newFrame.third === 0);
        }
        return isValid;
    };

}]);