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
        [Range(0, 10)]
        public int first { set; get; }

        [Range(0, 10)]
        public int second { set; get; }

        [Range(0, 10)]
        public int third { set; get; }

        public bool IsSpare()
        {
            return (!IsStrike()) && (first + second == 10);
        }

        public bool IsStrike()
        {
            return first == 10;
        }
    }
}
