using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bowling.Models.Validation
{
    public class BowlingGameValidation : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            Game game = value as Game;

            if (game == null)
            {
                ErrorMessage = "Game is empty";
                return false;
            }
            if (game.frames == null)
            {
                ErrorMessage = "Frames has invalid format";
                return false;
            }

            if (game.frames.Count > GameSettings.MAX_FRAMES_COUNT)
            {
                ErrorMessage = string.Format("Number of frames should be less then or equal {0}", GameSettings.MAX_FRAMES_COUNT);
                return false;
            }


            for (int i = 0; i < game.frames.Count; i++)
            {
                Frame frame = game.frames[i];

                if (!isFrameRollsInRange(frame)) 
                {
                    setErrorMessage(i, string.Format("The pins knocked in each roll should be between 0 and {0}", GameSettings.PINS_COUNT));
                    return false;
                }

                bool isLastOfFull = i == GameSettings.MAX_FRAMES_COUNT - 1;

                if (!isFrameRollsValid(frame, isLastOfFull))
                {
                    setErrorMessage(i, string.Format("The pins knocked in frame should not be more than {0}", GameSettings.PINS_COUNT));
                    return false;
                }
            }

            return true;

        }

        private bool isFrameRollsValid(Frame frame, bool isLastOfFull)
        {
            bool isValid = true;

            if (!isLastOfFull||(!frame.IsSpare() && !frame.IsStrike()))
            {
                isValid = ((frame.first + frame.second) <= GameSettings.PINS_COUNT) && (frame.third == 0);
            }
            return isValid;
        }

        private bool isFrameRollsInRange(Frame frame)
        {
            return (frame.first >= 0) && (frame.first <= GameSettings.PINS_COUNT)
                && (frame.second >= 0) && (frame.second <= GameSettings.PINS_COUNT)
                && (frame.third >= 0) && (frame.third <= GameSettings.PINS_COUNT);
        }

        private void setErrorMessage(int frameIndex, string detailMessage) 
        {
            ErrorMessage = string.Format("Frame #{0} doesn't contain valid data.", frameIndex + 1);
        }
    }
}