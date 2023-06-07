using SFML.Graphics;
using SFML.System;
using SFML.Window;

class Game
{
	public RenderWindow Window { get; private set; }
	public float DeltaTime { get; private set; }
	public List<Obstacle> Obstacles = new List<Obstacle>();
	public int Score { get; set; }

	public void Run()
	{
		// Create the SFML window
		Window = new RenderWindow(new VideoMode(800, 600), "Falling Obstacles");
		Window.SetFramerateLimit(60);
		Window.Closed += (sender, e) => Window.Close();

		// Fonts and text
		Font font = new Font("./assets/fonts/EndlessBossBattleRegular.ttf");

		// Create clocks
		Clock gameClock = new Clock();
		Clock deltaTimeClock = new Clock();


		// Make the player
		Player player = new Player(this);


		while (Window.IsOpen)
		{
			// Handle events
			Window.DispatchEvents();

			// Calculate delta time
			DeltaTime = deltaTimeClock.Restart().AsSeconds();

			// Check for if the player has died or not
			if (player.Dead == false)
			{
				// Update the player
				player.Update();

				// Spawn in a new obstacle every 2.5 seconds
				float spawnDelay = 1f;
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
			}
			else
			{
				// Check for if they press space to retry
				if (Keyboard.IsKeyPressed(Keyboard.Key.Space))
				{
					// Reset everything
					player.Respawn();
					Score = 0;
					Obstacles = new List<Obstacle>();
				}
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

			// Update the text
			if (player.Dead)
			{
				// Create the game over text
				//TODO: Don't do this every frame
				Text gameOverText = new Text("GAME OVER", font, 55);
				gameOverText.Origin = new Vector2f((gameOverText.GetGlobalBounds().Width / 2), (gameOverText.GetGlobalBounds().Height / 2));
				gameOverText.Position = new Vector2f((Window.Size.X / 2), (Window.Size.Y / 2) - 60);
				gameOverText.FillColor = Color.Black;

				// Create the score text
				Text scoreText = new Text($"SCORE: {Score}", font);
				scoreText.Origin = new Vector2f((scoreText.GetGlobalBounds().Width / 2), (scoreText.GetGlobalBounds().Height / 2));
				scoreText.Position = new Vector2f((Window.Size.X / 2), (Window.Size.Y / 2));
				scoreText.FillColor = Color.Black;

				// Create the retry text
				// TODO: Make this flash
				Text retryText = new Text($"PRESS 'space' TO RETRY", font);
				retryText.Origin = new Vector2f((retryText.GetGlobalBounds().Width / 2), (retryText.GetGlobalBounds().Height / 2));
				retryText.Position = new Vector2f((Window.Size.X / 2), (Window.Size.Y - retryText.GetGlobalBounds().Height - 10));
				retryText.FillColor = Color.Black;

				// Render the text
				Window.Draw(gameOverText);
				Window.Draw(scoreText);
				Window.Draw(retryText);
			}
			else Window.Draw(new Text($"SCORE: {Score}", font));


			// Display the contents of the Window
			Window.Display();
		}
	}
}
