namespace CybersecurityChatbot
{
    /// <summary>
    /// Stores user information (name and favourite topic) for personalization
    /// </summary>
    public class MemoryStore
    {
        public string UserName { get; set; } = "Friend";
        public string FavouriteTopic { get; set; } = "";

        public void StoreName(string name) => UserName = name;

        public void StoreFavouriteTopic(string topic) => FavouriteTopic = topic;

        /// <summary>
        /// Returns personalized text to make responses feel more natural
        /// </summary>
        public string GetPersonalisedMessage()
        {
            return string.IsNullOrEmpty(FavouriteTopic)
                ? ""
                : $"As someone interested in {FavouriteTopic}, ";
        }
    }
}