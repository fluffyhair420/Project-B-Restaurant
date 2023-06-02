using System;

namespace Restaurant
{
    public class Screen
    {
        private Screen _prev;

        public void SetPrevious(Screen s)
        {
            _prev = s;
        }
        public virtual void Show()
        {
            throw new NotImplementedException("Let op: deze functie overschrijven");
        }

    }
}