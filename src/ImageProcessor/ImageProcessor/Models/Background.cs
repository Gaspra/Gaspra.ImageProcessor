using System;

namespace ImageProcessor.Models
{
    public class Background
    {
        public int Red { get; set; }

        public int Green { get; set; }

        public int Blue { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;

            Background background = (Background)obj;

            return
                background.Red.Equals(Red)
                && background.Green.Equals(Green)
                && background.Blue.Equals(Blue);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Red, Green, Blue);
        }
    }
}
