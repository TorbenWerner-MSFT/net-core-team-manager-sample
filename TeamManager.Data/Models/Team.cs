using System.Collections.Generic;

namespace TeamManager.Data.Models
{
    public class Team
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string League { get; set; }

        public List<Player> Players { get; set; }
    }
}
