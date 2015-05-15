using Bowling.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bowling.Services
{
    public class ScoreService
    {
        public int GetScore(Game game) 
        {
            int totalScore = 0;
            if (game != null && game.frames != null)
            {
                bool isGameFinished = game.frames.Count == GameSettings.MAX_FRAMES_COUNT;
                for (int i = 0; i < game.frames.Count; i++)
                {
                    bool isLast = i == game.frames.Count - 1;

                    if (game.frames[i].IsStrike())
                    {
                        bool isPreLast = i == game.frames.Count - 2;

                        int nextRoll = isLast ? (isGameFinished ? game.frames[i].second : -1) : game.frames[i + 1].first;

                        int nextNextRoll = isLast ? (isGameFinished ? game.frames[i].third : -1)
                            : (!game.frames[i + 1].IsStrike() ? game.frames[i + 1].second
                                : !isPreLast
                                    ? game.frames[i + 2].first
                                    : (isGameFinished ? game.frames[i + 1].second : -1));

                        if ((nextRoll > -1) && (nextNextRoll > -1))
                        {
                            totalScore += 10 + nextRoll + nextNextRoll;
                        }
                    }
                    else if (game.frames[i].IsSpare())
                    {
                        int nextRoll = isLast ? (isGameFinished ? game.frames[i].third : 0) : game.frames[i + 1].first;
                        if (isGameFinished || !isLast)
                        {
                            totalScore += 10 + nextRoll;
                        }
                    }
                    else
                    {
                        totalScore += game.frames[i].first + game.frames[i].second;
                    }
                }
            }
            return totalScore;
        }

    }
}