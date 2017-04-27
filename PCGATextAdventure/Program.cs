using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCGATextAdventure
{

	public class Player
	{

		public string Name { get; set; }
		public string Gender { get; set; }
		public byte HitPoints { get; set; }

		public string Title
		{
			get
			{
				if (Gender == "M")
				{
					return "Prince";
				}
				return "Princess";
			}
		}

		public string FullName
		{
			get { return $"{Title} {Name}"; }
		}

	}

	public abstract class Enemy {

		// Members
		public string Name { get; set; }
		public byte HitPoints { get; set; }

		// Method
		public abstract byte Attack();

	}

	public class GoblinFighter : Enemy {

		public override byte Attack() {

			return 20;
		}

	}

	public class GoblinArcher : Enemy {

		public override byte Attack() {

			return 40;
		}

	}

	class Program
	{
		static void Main(string[] args) {

			Player player = new Player();
			player.HitPoints = 100;
			Console.Write("Please enter your name: ");
			player.Name = Console.ReadLine();
			Console.Write("Please enter M for male or F for female: ");
			player.Gender = Console.ReadLine();

			Console.WriteLine($"This is the story of {player.FullName}, heir to the throne in the kingdom of PCGamingdia.");
			Console.WriteLine($"One night, the {player.Title} decided to sneak out of the castle to the near by village for a night of friends and fun. On the way back at 2AM, {player.FullName} came upon a band of goblins huddled in a clearing.");
			byte playerChoice = getPlayerChoice(player, new[] { "1. Try to sneak around to return to the castle", "2. Try to sneak closer to hear what the goblins are saying", "3. Turn back around and head back to town" });
			switch (playerChoice)
			{
				case 1:
					Console.WriteLine($"{player.FullName} made choice 1");
					break;
				case 2:
					sneakCloser(player);
					break;
				case 3:
					Console.WriteLine($"{player.FullName} made choice 3");
					break;
				default:
					Console.WriteLine($"{player.FullName} made an invalid choice");
					break;
			}

			Console.WriteLine("Please hit enter to exit...");
			Console.ReadLine();
		}

		private static byte getPlayerChoice(Player player, string[] choices)
		{

			Console.WriteLine($"{player.FullName} could:");
			foreach (string choice in choices)
			{
				Console.WriteLine(choice);
			}
			byte playerChoice;
			do
			{
				Console.Write("Please select an option: ");
				playerChoice = Convert.ToByte(Console.ReadLine());
				if (playerChoice <= 0 || playerChoice > choices.Length)
				{
					Console.WriteLine($"Oh no {player.Title} made an incorrect choice. This is unacceptable.");
				}
			} while (playerChoice <= 0 || playerChoice > choices.Length);

			return playerChoice;
		}

		private static void sneakCloser(Player player) {

			Console.WriteLine($"{player.FullName} attempts to sneak closer to the goblins. However, the goblin's supreme hearing alerts them to the presence of an intruder. They jump up and surround our hapless hero.");
			Enemy[] goblins = new Enemy[3];
			goblins[0] = new GoblinFighter();
			goblins[1] = new GoblinArcher();
			goblins[2] = new GoblinArcher();

			foreach (var goblin in goblins) {
				player.HitPoints = Convert.ToByte(player.HitPoints - goblin.Attack());
			}

			Console.WriteLine($"After this attack, {player.FullName} is left with {player.HitPoints} life");

			if (player.HitPoints <= 0) {
				Console.WriteLine($"Oh no! {player.FullName} is dead!");
				return;
			}

			byte playerChoice = getPlayerChoice(player, new string[] {"1. Continue to fight", "2. Attempt to run away"});

			if (playerChoice == 2) {
				Console.WriteLine($"{player.FullName} successfully ran away.");
				return;
			}
		}
	}
}
