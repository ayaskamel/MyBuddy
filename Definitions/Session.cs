using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Definitions
{
    public class Session
    {
        public List<Utterance> ConversationHistory;
        public Guid DialogGuid;
        public Dictionary<string, string> SlotFilledData;
        public bool AwaitingConfirmation;

        public Session()
        {
            ConversationHistory = new List<Utterance>();
            SlotFilledData = new Dictionary<string, string>();
            AwaitingConfirmation = false;
        }
    }
}
