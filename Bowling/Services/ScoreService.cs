using Bowling.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bowling.Services
{
    public class ScoreService
    {
        public const int MAXCOUNT = 10;

        public int GetScore(IList<Frame> frames) 
        {
            bool isGameFinished = frames.Count == MAXCOUNT;
            int totalScore = 0;
            for (int i = 0; i < frames.Count; i++) 
            {
                bool isLast = i == frames.Count - 1;

                if (frames[i].IsStrike()) 
                {
                    bool isPreLast = i == frames.Count - 2;
                    
                    int nextRoll = isLast ? (isGameFinished ? frames[i].second : -1) : frames[i + 1].first;
                    int nextNextRoll = isLast ? (isGameFinished ? frames[i].third : -1) 
                        : (!frames[i + 1].IsStrike() ? frames[i + 1].second
                            : !isPreLast 
                                ? frames[i + 2].first
                                : (isGameFinished ? frames[i + 1].second : -1));

                    
                    if ((nextRoll > -1) && (nextNextRoll > -1)){
                        totalScore += 10 + nextRoll + nextNextRoll;
                    }
                }
                else if (frames[i].IsSpare()) {
                    int nextRoll = isLast ? (isGameFinished ? frames[i].third : 0) : frames[i + 1].first;
                    if (isGameFinished || !isLast)
                    {
                        totalScore += 10 + nextRoll;
                    }
                }
                else
                {
                    totalScore += frames[i].first + frames[i].second;
                }
            }
            return totalScore;
        }
    }
}