using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
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
				if (db != null)
				{
					var databaseCreated = await db.Database.EnsureCreatedAsync();

					if (databaseCreated)
					{
						await CreateSampleData(db);
					}
				}
				else
				{
					await CreateSampleData(db);
				}
			}
		}

		public static async Task CreateSampleData(TeamContext context)
		{
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

			context.Players.Add(player);
			context.Teams.Add(team);

			await context.SaveChangesAsync();
        }
	}
}
