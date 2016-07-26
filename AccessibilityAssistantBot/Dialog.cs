using Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessibilityAssistantBot
{
    public class Dialog
    {
        public Guid Identifier;
        public List<Slot> Slots;

        public Dialog(Guid id, List<Slot> slots)
        {
            this.Identifier = id;
            this.Slots = slots;
        }

        public string PromptForSlot(Session session)
        {
            var updatedSession = UpdateFilledSlots(session);
            if(updatedSession != null)
            {
                session = updatedSession;
            }
            else
            {
                return "Sorry I don't understand that. Could you please clarify your choice?";
            }

            var missingSlots = Slots.Where(s => !session.SlotFilledData.Keys.Contains(s.Key));
            if (missingSlots.Any())
            {
                return missingSlots.First().Question;
            }
            else
            {
                return null;
            }
        }

        public Session UpdateFilledSlots(Session session)
        {
            var lastUserUtterance = session.ConversationHistory.Where(x => x.Speaker == Constants.USER).LastOrDefault();
            var lastAgentUtterance = session.ConversationHistory.Where(x => x.Speaker == Constants.AGENT).LastOrDefault();

            var unfilledSlots = Slots.Where(slot => !session.SlotFilledData.Keys.Contains(slot.Key));
            var slotEntities = ExtractEntitiesFromUtterance(lastUserUtterance.Message, unfilledSlots.ToList());

            if (slotEntities.Any())
            {
                foreach(var slotEntity in slotEntities)
                {
                    session.SlotFilledData.Add(slotEntity.Key.Key, slotEntity.Value.Value);
                }

                return session;
            }
            else
            {
                return null;
            }
        }

        public Dictionary<Slot, Entity> ExtractEntitiesFromUtterance(string userUtteranceMessage, List<Slot> potentialSlotFillings)
        {
            var filledSlots = new Dictionary<Slot, Entity>();
            foreach(var slot in potentialSlotFillings)
            {
                var candidateResponses = slot.ExpectedAnswers.Where(entity => entity.Exists(userUtteranceMessage));
                if(candidateResponses.Count() == 1)
                {
                    filledSlots.Add(slot, candidateResponses.First());
                }
            }

            return filledSlots;
        }
    }
}
