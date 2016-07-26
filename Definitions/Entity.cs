using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Definitions
{
    public class Entity
    {
        public string Identifier;
        public string Value;
        public List<string> MatchingText;
        public Constants.EntityType Type;

        public Entity(string identifier, Constants.EntityType type, List<string> matchingText = null)
        {
            this.Identifier = identifier;
            this.Type = type;
            if(matchingText != null)
            {
                this.MatchingText = matchingText;
            }
            else
            {
                this.MatchingText = new List<string>();
            }
        }

        public bool Exists(string message)
        {
            switch (this.Type)
            {
                case Constants.EntityType.CHOICE: return ChoiceExists(message);
                case Constants.EntityType.DATE: return DateExists(message);
                case Constants.EntityType.TIME: return TimeExists(message);
                case Constants.EntityType.LOCATION: return LocationExists(message);
                default: return false;
            }
        }

        public bool ChoiceExists(string message)
        {
            var messageTokens = message.Split(' ');
            var matches = MatchingText.Where(text => messageTokens.Contains(text));
            if (matches.Any())
            {
                this.Value = this.Identifier;
                return true;
            }

            return false;
        }

        public bool DateExists(string message)
        {
            if (message.ToLower().Contains("today"))
            {
                var today = DateTime.Now;
                this.Value = today.Day + "-" + today.Month + "-" + today.Year;
                return true;
            }
            else if (message.ToLower().Contains("tomorrow"))
            {
                var tomorrow = DateTime.Today.AddDays(1);
                this.Value = tomorrow.Day + "-" + tomorrow.Month + "-" + tomorrow.Year;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool TimeExists(string message)
        {
            return true;
        }

        public bool LocationExists(string message)
        {
            return false;
        }
    }
}
