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
                    isValid = true;
                    for (int i = 0; i < game.frames.Count; i++)
                    {
                        Frame frame = game.frames[i];
                        bool isLastOfFull = i == MaxFrames - 1;
                        bool isStrike = frame.first == Pins;
                        bool isSpare = (frame.first + frame.second) == Pins;

                        if (isLastOfFull && isStrike)
                        {
                            isValid = isValid && (frame.second <= Pins) && (frame.third <= Pins) && (frame.second >= 0) && (frame.third == 0);
                        }
                        else if (isLastOfFull && isSpare)
                        {
                            isValid = isValid && (frame.first >= 0) && (frame.second >= 0) && (frame.third >= 0) && (frame.third <= Pins);
                        }
                        else
                        {
                            isValid = isValid && ((frame.first + frame.second) <= Pins) && (frame.first >= 0) && (frame.second >= 0) && (frame.third == 0);
                        }

                        if (!isValid) 
                        {
                            setErrorMessage(i, string.Empty);
                            break;
                        }
                    }
                }
            }
            return isValid;

        }

        private void setErrorMessage(int frameIndex, string detailMessage) 
        {
            ErrorMessage = string.Format("Frame #{0} doesn't contain valid data", frameIndex + 1);
        }
    }
}