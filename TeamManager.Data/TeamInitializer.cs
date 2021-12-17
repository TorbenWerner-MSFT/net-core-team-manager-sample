using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using TeamManager.Data.Models;

namespace TeamManager.Data
{
    public class TeamInitializer
    {
		public static async Task InitializeDatabaseAsync(IServiceProvider serviceProvider)
		{
			using (var db = serviceProvider.GetService<TeamContext>())
			{
				if (db == null)
					return;

				var databaseCreated = await db.Database.EnsureCreatedAsync();

				if (!databaseCreated)
					return;

				var team = new Team
				{
					Name = "Torben's Team",
					League = "Betonliga"
				};

				var player = new Player
				{
					Name = "Torben Werner",
					Position = "Abwehr",
					ShirtNumber = 9,
					Team = team
				};

				await db.Players.AddAsync(player);
				await db.Teams.AddAsync(team);

				await db.SaveChangesAsync();
			}
		}
	}
}
