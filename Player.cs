using SFML.Graphics;
using SFML.System;
using SFML.Window;

class Player
{
	private Game game;
	private Vector2f position;
	private Sprite sprite;
	private float speed;
	private bool dead;

	// New player constructor
	public Player(Game game)
	{
		this.game = game;
		this.speed = 500;

		// Create the player sprite
		this.sprite = new Sprite(new Texture("./assets/img/player.png"));

		// Spawn the player in the centre of the screen
		this.position = new Vector2f(((game.Window.Size.X - sprite.Texture.Size.X) / 2), (game.Window.Size.Y - sprite.Texture.Size.Y));
	}

	// Update the player movement
	private void Move()
	{
		// Create the new movement
		float movementSpeed = speed * game.DeltaTime;
		float x = 0;

		// Get player movement input
		if (Keyboard.IsKeyPressed(Keyboard.Key.Left)) x -= movementSpeed;
		if (Keyboard.IsKeyPressed(Keyboard.Key.Right)) x += movementSpeed;
		Vector2f newPosition = position += new Vector2f(x, 0);

		// Check for if the player is going to go offscreen, and stop them
		if (((newPosition.X + sprite.Texture.Size.X + 10) > game.Window.Size.X) || (newPosition.X < 10)) return;

		// Update the player movement
		position = newPosition;
		sprite.Position = position;
	}

	// Check for if the player hits an obstacle, then kill them
	private void ObstacleCollision()
	{
		// Loop through all obstacles
		for (int i = 0; i < game.Obstacles.Count(); i++)
		{	
			Obstacle obstacle = game.Obstacles[i];

			// Check for if the obstacle is in the correct y position
			if ((obstacle.Position.Y + obstacle.Height) < position.Y) continue;

			// Check for if the obstacle is at the correct x position
			if ((position.X <= (obstacle.Position.X + obstacle.Width)) && (obstacle.Position.X <= (position.X + sprite.Texture.Size.X)))
			{
				// Kill the player
				Die();
			}
		}
	}

	private void Die()
	{
		sprite.Dispose();
		dead = true;
	}



	// Update the player
	public void Update()
	{
		if (dead) return;

		Move();
		ObstacleCollision();
	}

	// Draw the player
	public void Render()
	{
		if (dead) return;

		game.Window.Draw(sprite);
	}
}