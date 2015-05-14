using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bowling.Models.Validation
{
    public class BowlingGameValidation : ValidationAttribute
    {
        public int Pins { get; set; }
        public int MaxFrames { get; set; }

        public override bool IsValid(object value)
        {
            bool isValid = false;
            Game game = value as Game;
            if (game != null)
            {
                if (game.frames.Count <= MaxFrames)
                {
                    for (int i = 0; i < game.frames.Count; i++)
                    {
                        Frame frame = game.frames[i];
                        if (i == MaxFrames - 1)
                        {
                            if (frame.first == Pins) 
                            {
                                //Strike
                            }
                            else if ((frame.first + frame.second) == Pins) 
                            { 
                                //Spare
                            }
                        }

                        bool isFrameValid = ((frame.first + frame.second) <= Pins) && (frame.first >= 0) && (frame.second >= 0) && (frame.third == 0);

                    }
                }
            }
            return isValid;

        }
    }
}