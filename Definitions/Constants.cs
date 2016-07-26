using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Definitions
{
    public class Constants
    {
        public const string AGENT = "Agent";
        public const string USER = "User";

        public enum EntityType
        {
            DATE,
            TIME,
            CHOICE,
            LOCATION
        }
    }
}
