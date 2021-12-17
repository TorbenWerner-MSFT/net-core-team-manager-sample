using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TeamManager.Data.Models;

namespace TeamManager.Models
{
    public class EditPlayerViewModel
    {
        public EditPlayerViewModel()
        {

        }

        public EditPlayerViewModel(Player player)
        {
            Id = player.Id;
            TeamId = player.TeamId;
            Name = player.Name;
            ShirtNumber = player.ShirtNumber;
            Position = player.Position;
        }

        public List<Team> Teams { get; set; }

        [Required]
        public int Id { get; set; }

        [Required]
        public int TeamId { get; set; }

        [Required]
        public string Name { get; set; }

        [Range(1, 99)]
        public int ShirtNumber { get; set; }

        [Required]
        [RegularExpression("\\b(Sturm|Abwehr|Torhüter|Mittelfeld)\\b")]
        public string Position { get; set; }

        public Player GetPlayer()
        {
            return new Player
            {
                Id = Id,
                TeamId = TeamId,
                Name = Name,
                ShirtNumber = ShirtNumber,
                Position = Position
            };
        }
    }
}
