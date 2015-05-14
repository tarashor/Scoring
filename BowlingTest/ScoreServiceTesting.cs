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

        IList<Frame> getEqualFrames(int N, int f, int s)
        {
            List<Frame> frames = new List<Frame>();
            for (int i = 0; i < N; i++)
            {
                frames.Add(new Frame(f, s));
            }
            return frames;
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

        IList<Frame> getSpare1(int N, int f, int s, int spareIndex)
        {
            IList<Frame> frames = getEqualFrames(N, f, s);
            frames[spareIndex].first = 10 - s;
            return frames;
        }

        [TestMethod]
        public void TestSpare1NotLastFull()
        {
            int first = 2;
            int second = 3;
            int spareIndex = 0;

            int expected = (FULL_FRAMES_COUNT - 1) * (first + second) + 10 + first;

            IList<Frame> frames = getSpare1(FULL_FRAMES_COUNT, first, second, spareIndex);
            int actualScore = scoreService.GetScore(frames);

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

            IList<Frame> frames = getSpare1(N, first, second, spareIndex);
            int actualScore = scoreService.GetScore(frames);

            Assert.AreEqual(expected, actualScore);
        }

        IList<Frame> getSpareLastFull(int f, int s, int t)
        {
            IList<Frame> frames = getSpare1(FULL_FRAMES_COUNT, f, s, FULL_FRAMES_COUNT - 1);
            frames[FULL_FRAMES_COUNT - 1].third = t;
            return frames;
        }

        [TestMethod]
        public void TestSpare1LastFull()
        {
            int first = 2;
            int second = 3;
            int third = 5;

            IList<Frame> frames = getSpareLastFull(first, second, third);

            int expected = (FULL_FRAMES_COUNT -1) * (first + second) + 10 + third;

            int actualScore = scoreService.GetScore(frames);

            Assert.AreEqual(expected, actualScore);
        }

        IList<Frame> getSpare2(int N, int f, int s, int spareIndex)
        {
            IList<Frame> frames = getEqualFrames(N, f, s);
            frames[spareIndex].first = 10 - s;
            frames[spareIndex+1].first = 10 - s;
            return frames;
        }

        

        [TestMethod]
        public void TestSpare2Full()
        {
            int first = 2;
            int second = 3;
            int spareIndex = 0;

            int expected = (FULL_FRAMES_COUNT - 2) * (first + second) + 10 + first + 10 + (10 - second);

            IList<Frame> frames = getSpare2(FULL_FRAMES_COUNT, first, second, spareIndex);
            int actualScore = scoreService.GetScore(frames);

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

            IList<Frame> frames = getSpare2(N, first, second, spareIndex);
            int actualScore = scoreService.GetScore(frames);

            Assert.AreEqual(expected, actualScore);
        }

        IList<Frame> getSpare2LastFull(int f, int s, int t)
        {
            IList<Frame> frames = getSpare2(FULL_FRAMES_COUNT, f, s, FULL_FRAMES_COUNT - 2);
            frames[FULL_FRAMES_COUNT - 1].third = t;
            return frames;
        }

        [TestMethod]
        public void TestSpare2LastFull()
        {
            int first = 2;
            int second = 3;
            int third = 5;

            int expected = (FULL_FRAMES_COUNT - 2) * (first + second) + 10 + (10 - second) + 10 + third;

            IList<Frame> frames = getSpare2LastFull(first, second, third);
            int actualScore = scoreService.GetScore(frames);

            Assert.AreEqual(expected, actualScore);
        }

        IList<Frame> getStrike1(int N, int f, int s, int strikeIndex)
        {
            IList<Frame> frames = getEqualFrames(N, f, s);
            frames[strikeIndex].first = 10;
            frames[strikeIndex].second = 0;
            return frames;
        }

        [TestMethod]
        public void TestStrike1NotLastFull()
        {
            int first = 2;
            int second = 3;
            int strikeIndex = 0;

            int expected = (FULL_FRAMES_COUNT - 1) * (first + second) + 10 + first + second;

            IList<Frame> frames = getStrike1(FULL_FRAMES_COUNT, first, second, strikeIndex);
            int actualScore = scoreService.GetScore(frames);

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

            IList<Frame> frames = getStrike1(N, first, second, strikeIndex);
            int actualScore = scoreService.GetScore(frames);

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

            IList<Frame> frames = getStrike1(N, first, second, strikeIndex);
            int actualScore = scoreService.GetScore(frames);

            Assert.AreEqual(expected, actualScore);
        }

        IList<Frame> getStrike1LastFull(int f, int s, int t)
        {
            IList<Frame> frames = getEqualFrames(FULL_FRAMES_COUNT, f, s);
            frames[FULL_FRAMES_COUNT-1].first = 10;
            frames[FULL_FRAMES_COUNT-1].third = t;
            return frames;
        }

        [TestMethod]
        public void TestStrike1LastFull()
        {
            int first = 2;
            int second = 3;
            int third = 5;

            int expected = (FULL_FRAMES_COUNT - 1) * (first + second) + 10 + second + third;

            IList<Frame> frames = getStrike1LastFull(first, second, third);
            int actualScore = scoreService.GetScore(frames);

            Assert.AreEqual(expected, actualScore);
        }

        IList<Frame> getStrike2(int N, int f, int s, int strikeIndex)
        {
            IList<Frame> frames = getEqualFrames(N, f, s);
            frames[strikeIndex].first = 10;
            frames[strikeIndex].second = 0;
            frames[strikeIndex+1].first = 10;
            frames[strikeIndex+1].second = 0;
            return frames;
        }

        [TestMethod]
        public void TestStrike2NotLastFull()
        {
            int first = 2;
            int second = 3;
            int strikeIndex = 0;

            int expected = (FULL_FRAMES_COUNT - 2) * (first + second) + 10 + 10 + first + 10 + first + second;

            IList<Frame> frames = getStrike2(FULL_FRAMES_COUNT, first, second, strikeIndex);
            int actualScore = scoreService.GetScore(frames);

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

            IList<Frame> frames = getStrike2(N, first, second, strikeIndex);
            int actualScore = scoreService.GetScore(frames);

            Assert.AreEqual(expected, actualScore);
        }

        IList<Frame> getStrike2LastFull(int f, int s, int t)
        {
            IList<Frame> frames = getEqualFrames(FULL_FRAMES_COUNT, f, s);
            frames[FULL_FRAMES_COUNT-2].first = 10;
            frames[FULL_FRAMES_COUNT - 2].second = 0;
            frames[FULL_FRAMES_COUNT - 1].first = 10;
            frames[FULL_FRAMES_COUNT - 1].second = s;
            frames[FULL_FRAMES_COUNT - 1].third = t;
            return frames;
        }

        [TestMethod]
        public void TestStrike2LastFull()
        {
            int first = 2;
            int second = 3;
            int third = 5;

            int expected = (FULL_FRAMES_COUNT - 2) * (first + second) + 10 + 10 + second + 10 + second + third;

            IList<Frame> frames = getStrike2LastFull(first, second, third);
            int actualScore = scoreService.GetScore(frames);

            Assert.AreEqual(expected, actualScore);
        }

        IList<Frame> getPerfectGame()
        {
            IList<Frame> frames = getEqualFrames(FULL_FRAMES_COUNT, 10, 0);
            frames[FULL_FRAMES_COUNT - 1].second = 10;
            frames[FULL_FRAMES_COUNT - 1].third = 10;
            return frames;
        }

        [TestMethod]
        public void TestPerfectGame()
        {
            int expected = 300;

            IList<Frame> frames = getPerfectGame();
            int actualScore = scoreService.GetScore(frames);

            Assert.AreEqual(expected, actualScore);
        }
    }
}
