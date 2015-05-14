using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Bowling.Models
{
    public class Frame
    {
        public Frame() 
        {
            first = 0;
            second = 0;
            third = 0;
        }

        public Frame(int first, int second)
        {
            this.first = first;
            this.second = second;
            this.third = 0;
        }

        [Required]
        //[Range(0, GameSettings.PINS_COUNT)]
        public int first { set; get; }

        //[Range(0, GameSettings.PINS_COUNT)]
        public int second { set; get; }

        //[Range(0, GameSettings.PINS_COUNT)]
        public int third { set; get; }

        public bool IsSpare()
        {
            return (!IsStrike()) && (first + second == GameSettings.PINS_COUNT);
        }

        public bool IsStrike()
        {
            return first == GameSettings.PINS_COUNT;
        }
    }
}
