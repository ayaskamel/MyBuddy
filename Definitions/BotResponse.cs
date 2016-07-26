using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Definitions
{
    public class BotResponse
    {
        public Session Session;
        public string AgentUtterance;
        public bool FinalUtterance;

        public BotResponse(Session session, string agentUtterance, bool finalUtterance)
        {
            this.Session = session;
            this.AgentUtterance = agentUtterance;
            this.FinalUtterance = finalUtterance;
        }
    }
}
