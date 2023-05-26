namespace Restaurant
{
    public static class RestaurantContact
    {

        public static void Info()
        {
            Console.WriteLine(@"
            About us

                                            === Welcome to {our super awesome and original restaurant name}! ===
                                                    
                                    {our super awesome and original restaurant name} is a newly opened restaurant in Rotterdam.
                                Sustainability is our most valued quality. All products used to make our delicious dishes come mainly
                                    from the Rotterdam area, may that be meat or fish, or even vegetarian and vegan options!
                                To keep things spicy, our menu changes every month, according to the seasons. That way there will
                                            always be something new for you to taste! There is a dish for everyone.
                                        
                                
                                Whether you are a student, young family or pensioner, we will have something to your appeal.
                                        The prices of our dishes range widely, so do not worry about the bill ;)
                                    A night at {our super awesome and original restaurant name} is a REAL night out,
                                        so if you want to go for a quick bite, you have come to the wrong place.
                                        
                                        
                                                                     --- Contact ---

                                                                         Address:
                                                                      Wijnhaven 107
                                                                         3011 WN
                                                                        Rotterdam
                                                                    
                                                                         Phone:
                                                                     +31 10 1234567

                                                                         Email:
                                                 {our super awesome and original restaurant name}@gmail.com
                                                               
                            <<< 1. Homepage <<<");
            bool wrongInput = true;
            while (wrongInput)
            {
                string userinput = Console.ReadLine();
                switch (userinput)
                {
                    case "1":
                        wrongInput = false;
                        MainMenu.Main();
                        break;
                    default:
                        Console.WriteLine("Invalid input. Please enter 1 to go back to the Homepage.");
                        break;
                }
            }
        }
    }
}

