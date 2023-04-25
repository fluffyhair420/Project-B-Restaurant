using System;

namespace Restaurant
{
    public class Screen_1_Welcome : Screen
    {
        private readonly Screen _next;

        public Screen_1_Welcome()
        {
            _next = new Menu();
            _next.SetPrevious(this);
        }

    }

    // class Program
    // {
    //     static void Main(string[] args)
    //     {
    //          var menu = new Menu();

    //         menu.Show();
    //     }
    // }





}