using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    class UserInterface
    {
        private Store store = new Store();

        /// <summary>
        /// Provides all communication with human user.
        /// 
        /// All Console.Readline() and Console.WriteLine() statements belong 
        /// in this class.
        /// 
        /// NO Console.Readline() and Console.WriteLine() statements should be 
        /// in any other class
        /// 
        /// </summary>
        public void Run()
        {
            bool done = false;

            while (!done)
            {
                Console.WriteLine("Greetings from the User Interface object.");
                Console.ReadLine();

            }
        }
    }
}
