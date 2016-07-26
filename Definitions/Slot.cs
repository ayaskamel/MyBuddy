using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Definitions
{
    public class Slot
    {
        public string Key;
        public string Question;
        public List<Entity> ExpectedAnswers;

        public Slot(string key, string question, List<Entity> answers)
        {
            this.Key = key;
            this.Question = question;
            this.ExpectedAnswers = answers;
        }
    }
}
