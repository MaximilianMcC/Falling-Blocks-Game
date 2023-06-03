using SFML.Graphics;
using SFML.System;
using SFML.Window;

class Game
{
	public RenderWindow Window { get; private set; }
	public float DeltaTime { get; private set; }
	public List<Obstacle> Obstacles = new List<Obstacle>();


	public void Run()
	{
		// Create the SFML window
		Window = new RenderWindow(new VideoMode(800, 600), "Falling Obstacles");
		Window.SetFramerateLimit(60);
		Window.Closed += (sender, e) => Window.Close();


		// Create clocks
		Clock gameClock = new Clock();
		Clock deltaTimeClock = new Clock();


		// 60% down the screen
		float groundPosition = (Window.Size.Y * 60) / 100;



		// Make the player
		Player player = new Player(this, groundPosition);


		while (Window.IsOpen)
		{
			// Handle events
			Window.DispatchEvents();

			// Calculate delta time
			DeltaTime = deltaTimeClock.Restart().AsSeconds();

			// Update the player
			player.Update();

			// Spawn in a new obstacle every 2.5 seconds
			float spawnDelay = 2.5f;
			if (gameClock.ElapsedTime.AsSeconds() >= spawnDelay)
			{
				// Create a new obstacle
				Obstacle obstacle = new Obstacle(this);
				Obstacles.Add(obstacle);

				gameClock.Restart();
			}


			// Update all of the obstacles
			for (int i = 0; i < Obstacles.Count; i++)
			{
				Obstacles[i].Update();
			}


			// Clear the Window
			Window.Clear(Color.Magenta);

			// Draw the player
			player.Render();

			// Draw all of the obstacles
			for (int i = 0; i < Obstacles.Count; i++)
			{
				Obstacles[i].Render();
			}


			// Display the contents of the Window
			Window.Display();
		}
	}
}
