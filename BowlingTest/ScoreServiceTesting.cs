using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bowling.Services;
using Bowling.Models;
using System.Collections.Generic;

namespace BowlingTest
{
    [TestClass]
    public class ScoreServiceTesting
    {
        private ScoreService scoreService;
        public const int FULL_FRAMES_COUNT = 10;

        [TestInitialize]
        public void Setup() {
            scoreService = new ScoreService();
        }

        Game getEqualFrames(int N, int f, int s)
        {
            Game game = new Game();
            for (int i = 0; i < N; i++)
            {
                game.frames.Add(new Frame(f, s));
            }
            return game;
        }

        [TestMethod]
        public void TestNoSpareNoStrikeFull()
        {
            int first = 2;
            int second = 3;

            int expected = FULL_FRAMES_COUNT*(first + second);

            int actualScore = scoreService.GetScore(getEqualFrames(FULL_FRAMES_COUNT, first, second));
            Assert.AreEqual(expected, actualScore);
        }

        [TestMethod]
        public void TestNoSpareNoStrike()
        {
            int N = 5;
            int first = 2;
            int second = 3;

            int expected = N * (first + second);

            int actualScore = scoreService.GetScore(getEqualFrames(N, first, second));
            Assert.AreEqual(expected, actualScore);
        }

        Game getSpare1(int N, int f, int s, int spareIndex)
        {
            Game game = getEqualFrames(N, f, s);
            game.frames[spareIndex].first = 10 - s;
            return game;
        }

        [TestMethod]
        public void TestSpare1NotLastFull()
        {
            int first = 2;
            int second = 3;
            int spareIndex = 0;

            int expected = (FULL_FRAMES_COUNT - 1) * (first + second) + 10 + first;

            Game game = getSpare1(FULL_FRAMES_COUNT, first, second, spareIndex);
            int actualScore = scoreService.GetScore(game);

            Assert.AreEqual(expected, actualScore);
        }

        [TestMethod]
        public void TestSpare1Last()
        {
            int N = 5;
            int first = 2;
            int second = 3;
            int spareIndex = N - 1;

            int expected = (N - 1) * (first + second);

            Game game = getSpare1(N, first, second, spareIndex);
            int actualScore = scoreService.GetScore(game);

            Assert.AreEqual(expected, actualScore);
        }

        Game getSpareLastFull(int f, int s, int t)
        {
            Game game = getSpare1(FULL_FRAMES_COUNT, f, s, FULL_FRAMES_COUNT - 1);
            game.frames[FULL_FRAMES_COUNT - 1].third = t;
            return game;
        }

        [TestMethod]
        public void TestSpare1LastFull()
        {
            int first = 2;
            int second = 3;
            int third = 5;

            Game game = getSpareLastFull(first, second, third);

            int expected = (FULL_FRAMES_COUNT -1) * (first + second) + 10 + third;

            int actualScore = scoreService.GetScore(game);

            Assert.AreEqual(expected, actualScore);
        }

        Game getSpare2(int N, int f, int s, int spareIndex)
        {
            Game game = getEqualFrames(N, f, s);
            game.frames[spareIndex].first = 10 - s;
            game.frames[spareIndex+1].first = 10 - s;
            return game;
        }

        

        [TestMethod]
        public void TestSpare2Full()
        {
            int first = 2;
            int second = 3;
            int spareIndex = 0;

            int expected = (FULL_FRAMES_COUNT - 2) * (first + second) + 10 + first + 10 + (10 - second);

            Game game = getSpare2(FULL_FRAMES_COUNT, first, second, spareIndex);
            int actualScore = scoreService.GetScore(game);

            Assert.AreEqual(expected, actualScore);
        }

        [TestMethod]
        public void TestSpare2Last()
        {
            int N = 5;
            int first = 2;
            int second = 3;
            int spareIndex = N-2;

            int expected = (N - 2) * (first + second) + 10 + (10-second);

            Game game = getSpare2(N, first, second, spareIndex);
            int actualScore = scoreService.GetScore(game);

            Assert.AreEqual(expected, actualScore);
        }

        Game getSpare2LastFull(int f, int s, int t)
        {
            Game game = getSpare2(FULL_FRAMES_COUNT, f, s, FULL_FRAMES_COUNT - 2);
            game.frames[FULL_FRAMES_COUNT - 1].third = t;
            return game;
        }

        [TestMethod]
        public void TestSpare2LastFull()
        {
            int first = 2;
            int second = 3;
            int third = 5;

            int expected = (FULL_FRAMES_COUNT - 2) * (first + second) + 10 + (10 - second) + 10 + third;

            Game game = getSpare2LastFull(first, second, third);
            int actualScore = scoreService.GetScore(game);

            Assert.AreEqual(expected, actualScore);
        }

        Game getStrike1(int N, int f, int s, int strikeIndex)
        {
            Game game = getEqualFrames(N, f, s);
            game.frames[strikeIndex].first = 10;
            game.frames[strikeIndex].second = 0;
            return game;
        }

        [TestMethod]
        public void TestStrike1NotLastFull()
        {
            int first = 2;
            int second = 3;
            int strikeIndex = 0;

            int expected = (FULL_FRAMES_COUNT - 1) * (first + second) + 10 + first + second;

            Game game = getStrike1(FULL_FRAMES_COUNT, first, second, strikeIndex);
            int actualScore = scoreService.GetScore(game);

            Assert.AreEqual(expected, actualScore);
        }

        [TestMethod]
        public void TestStrike1NotLast()
        {
            int N = 5;
            int first = 2;
            int second = 3;
            int strikeIndex = 0;

            int expected = (N - 1) * (first + second) + 10 + first + second;

            Game game = getStrike1(N, first, second, strikeIndex);
            int actualScore = scoreService.GetScore(game);

            Assert.AreEqual(expected, actualScore);
        }

        

        [TestMethod]
        public void TestStrike1Last()
        {
            int N = 5;
            int first = 2;
            int second = 3;
            int strikeIndex = N - 1;

            int expected = (N - 1) * (first + second);

            Game game = getStrike1(N, first, second, strikeIndex);
            int actualScore = scoreService.GetScore(game);

            Assert.AreEqual(expected, actualScore);
        }

        Game getStrike1LastFull(int f, int s, int t)
        {
            Game game = getEqualFrames(FULL_FRAMES_COUNT, f, s);
            game.frames[FULL_FRAMES_COUNT - 1].first = 10;
            game.frames[FULL_FRAMES_COUNT - 1].third = t;
            return game;
        }

        [TestMethod]
        public void TestStrike1LastFull()
        {
            int first = 2;
            int second = 3;
            int third = 5;

            int expected = (FULL_FRAMES_COUNT - 1) * (first + second) + 10 + second + third;

            Game game = getStrike1LastFull(first, second, third);
            int actualScore = scoreService.GetScore(game);

            Assert.AreEqual(expected, actualScore);
        }

        Game getStrike2(int N, int f, int s, int strikeIndex)
        {
            Game game = getEqualFrames(N, f, s);
            game.frames[strikeIndex].first = 10;
            game.frames[strikeIndex].second = 0;
            game.frames[strikeIndex+1].first = 10;
            game.frames[strikeIndex+1].second = 0;
            return game;
        }

        [TestMethod]
        public void TestStrike2NotLastFull()
        {
            int first = 2;
            int second = 3;
            int strikeIndex = 0;

            int expected = (FULL_FRAMES_COUNT - 2) * (first + second) + 10 + 10 + first + 10 + first + second;

            Game game = getStrike2(FULL_FRAMES_COUNT, first, second, strikeIndex);
            int actualScore = scoreService.GetScore(game);

            Assert.AreEqual(expected, actualScore);
        }

        [TestMethod]
        public void TestStrike2Last()
        {
            int N = 5;
            int first = 2;
            int second = 3;
            int strikeIndex = N - 2;

            int expected = (N - 2) * (first + second);

            Game game = getStrike2(N, first, second, strikeIndex);
            int actualScore = scoreService.GetScore(game);

            Assert.AreEqual(expected, actualScore);
        }

        Game getStrike2LastFull(int f, int s, int t)
        {
            Game game = getEqualFrames(FULL_FRAMES_COUNT, f, s);
            game.frames[FULL_FRAMES_COUNT-2].first = 10;
            game.frames[FULL_FRAMES_COUNT - 2].second = 0;
            game.frames[FULL_FRAMES_COUNT - 1].first = 10;
            game.frames[FULL_FRAMES_COUNT - 1].second = s;
            game.frames[FULL_FRAMES_COUNT - 1].third = t;
            return game;
        }

        [TestMethod]
        public void TestStrike2LastFull()
        {
            int first = 2;
            int second = 3;
            int third = 5;

            int expected = (FULL_FRAMES_COUNT - 2) * (first + second) + 10 + 10 + second + 10 + second + third;

            Game game = getStrike2LastFull(first, second, third);
            int actualScore = scoreService.GetScore(game);

            Assert.AreEqual(expected, actualScore);
        }

        Game getPerfectGame()
        {
            Game game = getEqualFrames(FULL_FRAMES_COUNT, 10, 0);
            game.frames[FULL_FRAMES_COUNT - 1].second = 10;
            game.frames[FULL_FRAMES_COUNT - 1].third = 10;
            return game;
        }

        [TestMethod]
        public void TestPerfectGame()
        {
            int expected = 300;

            Game game = getPerfectGame();
            int actualScore = scoreService.GetScore(game);

            Assert.AreEqual(expected, actualScore);
        }
    }
}
