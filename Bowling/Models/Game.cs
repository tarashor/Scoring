using Bowling.Models.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bowling.Models
{
    [BowlingGameValidation]
    public class Game
    {
        public Game() {
            frames = new List<Frame>();
        }
        public IList<Frame> frames { get; private set; }
    }
}