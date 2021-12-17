namespace TeamManager.Data.Models
{
    public class Player
    {
        public int Id { get; set; }

        public int TeamId { get; set; }

        public string Name { get; set; }

        public int ShirtNumber { get; set; }

        public string Position { get; set; }

        public Team Team { get; set; }
    }

    public enum PlayerPosition
    {
        Keeper,
        Defender,
        Midfielder,
        Attacker,
        Substitute
    }
}
