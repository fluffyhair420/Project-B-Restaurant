using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant
{
    public class Screen
    {
        private Screen _prev;

        public Screen()
        {

        }

        public void SetPrevious(Screen s)
        {
            _prev = s;
        }

        public void Log(string message)
        {
            // Write to log.txt
        }

        public void Display(string message)
        {
            Console.WriteLine(message);
        }

        public virtual void Show()
        {
            throw new NotImplementedException("Let op: deze functie overschrijven");
        }

        public void Back()
        {
            Log("[Back]");

            if (_prev != null)
            {
                _prev.Show();
            }
        }
    }
}