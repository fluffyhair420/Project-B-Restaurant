using System;

namespace ScreenManagement
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

    class Program
    {
        static void Main(string[] args)
        {
            Application app = new Application();

            app.Run();
        }
    }

    public class Application
    {
        public Application()
        {

        }

        public void Run()
        {
            var menu = new Menu();

            menu.Show();
        }
        
    }



}