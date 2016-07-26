using Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessibilityAssistantBot
{
    class Program
    {
        static void Main(string[] args)
        {
            Bot.Init();
            Console.WriteLine("Please enter your name");
            var name = Console.ReadLine();
            
            Session session = new Session();
            string message = "Hi " + name + ", how can I help you today?";
            Console.WriteLine(message);

            while (true)
            {
                message = Console.ReadLine();
                BotResponse response = Bot.GetNextUtterance(session, message);
                session = response.Session;
                Console.WriteLine(response.AgentUtterance);
                if (response.FinalUtterance)
                {
                    Console.WriteLine("Have a good day. Bye!");
                    break;
                }
            }
        }
    }
}
