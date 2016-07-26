using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Definitions
{
    public class Utterance
    {
        public string Message;
        public string Speaker;

        public Utterance(string message, string speaker)
        {
            this.Message = message;
            this.Speaker = speaker;
        }
    }
}
