using Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessibilityAssistantBot
{
    public static class Bot
    {
        public static Dictionary<Guid, Dialog> AvailableDialogs;
        public static Dictionary<string, Entity> EntitySet;

        public static void Init()
        {
            EntitySet = LoadEntities();
            AvailableDialogs = LoadDialogs();
        }

        public static BotResponse GetNextUtterance(Session session, string userMessage)
        {
            if (!session.ConversationHistory.Any())
            {
                // First query - Classify intent
                session.DialogGuid = Guid.Empty;
            }

            if (session.AwaitingConfirmation)
            {
                // Send Confirmed Form
                return new BotResponse(session, "Your request has been successfully placed.", true);
            }

            session.ConversationHistory.Add(new Utterance(userMessage, Constants.USER));
            string agentMessage = AvailableDialogs[session.DialogGuid].PromptForSlot(session);

            if(agentMessage != null)
            {
                session.ConversationHistory.Add(new Utterance(agentMessage, Constants.AGENT));
                return new BotResponse(session, agentMessage, false);
            }
            else
            {
                // Verify data before submitting response
                session.AwaitingConfirmation = true;
                agentMessage = "Request Summary:\n" + string.Join("\n", session.SlotFilledData.Select(d => d.Key + ":" + d.Value)) + "\nConfirm?";
                return new BotResponse(session, agentMessage, false);
            }
        }

        private static Dictionary<Guid, Dialog> LoadDialogs()
        {
            var dict = new Dictionary<Guid, Dialog>();

            List<Slot> slots = new List<Slot>
            {
                new Slot("DATE", "For today or tomorrow?", new List<Entity> { EntitySet["DATE"] }),
                new Slot("TIME", "At what time exactly?", new List<Entity> { EntitySet["TIME"] }),
                new Slot("GENDER", "Would you like a male or a female assistant?", new List<Entity> { EntitySet["MALE"], EntitySet["FEMALE"], EntitySet["DOESNT MATTER"] }),
                new Slot("ACTIVITY", "What is the activity you need assistance for?", new List<Entity> { EntitySet["ENTERTAINMENT"], EntitySet["EVENT"], EntitySet["DRIVE"], EntitySet["WORK"] })
            };

            Dialog tempDialog = new Dialog(Guid.Empty, slots);
            dict.Add(Guid.Empty, tempDialog);
            return dict;
        }

        private static Dictionary<string, Entity> LoadEntities()
        {
            Dictionary<string, Entity> entities = new Dictionary<string, Entity>();
            entities.Add("DATE", new Entity("DATE", Constants.EntityType.DATE));
            entities.Add("MALE", new Entity("MALE", Constants.EntityType.CHOICE, new List<string> { "male", "man", "guy" }));
            entities.Add("FEMALE", new Entity("FEMALE", Constants.EntityType.CHOICE, new List<string> { "female", "girl", "lady" }));
            entities.Add("DOESNT MATTER", new Entity("DOESNT MATTER", Constants.EntityType.CHOICE, new List<string> { "doesn't matter", "doesnt matter", "anything", "whatever" }));
            entities.Add("ENTERTAINMENT", new Entity("ENTERTAINMENT", Constants.EntityType.CHOICE, new List<string> { "shop", "shopping", "buy", "mall", "friends", "cinema" }));
            entities.Add("EVENT", new Entity("EVENT", Constants.EntityType.CHOICE, new List<string> { "event", "course", "wedding", "engagement", "workshop" }));
            entities.Add("DRIVE", new Entity("DRIVE", Constants.EntityType.CHOICE, new List<string> { "drive", "car" }));
            entities.Add("WORK", new Entity("WORK", Constants.EntityType.CHOICE, new List<string> { "work", "office", "meeting" }));
            entities.Add("TIME", new Entity("TIME", Constants.EntityType.TIME));
            return entities;
        }
    }
}
