using Bowling.Models.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bowling.Models
{
    [BowlingGameValidation(Pins = 10, MaxFrames = 10)]
    public class Game
    {
        public Game() {
            frames = new List<Frame>();
        }
        public IList<Frame> frames { get; private set; }
    }
}