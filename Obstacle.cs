using SFML.Graphics;
using SFML.System;
using SFML.Window;

class Obstacle
{
	public bool Destroyed { get; private set; }
	public Vector2f Position { get; private set; }
	public float Width { get; private set; }
	public float Height { get; private set; }
	private Game game;
	private float speed;
	private Sprite sprite;

	// Create a new obstacle
	public Obstacle(Game game)
	{
		this.game = game;
		Random random = new Random();

		// Get a random texture for the sprite
		string[] textures = new string[] { "obstacle1", "obstacle2", "obstacle3" };
		Texture texture = new Texture("./assets/img/" + textures[random.Next(textures.Length)] + ".png");

		// Make the obstacle sprite
		this.sprite = new Sprite(texture);
		this.Width = sprite.Texture.Size.X;
		this.Height = sprite.Texture.Size.Y;

		// Get a random speed, and position
		this.speed = random.NextInt64(150, 250);
		float x = random.NextInt64(sprite.Texture.Size.X, (game.Window.Size.X - sprite.Texture.Size.X));
		this.Position = new Vector2f(x, -100);
	}


	// Make the obstacle fall
	private void Fall()
	{
		// Create the new movement
		Vector2f movement = new Vector2f(0, speed * game.DeltaTime);
		Vector2f newPosition = Position + movement;

		// Check for if the obstacle is off the screen
		if (Position.Y > game.Window.Size.Y)
		{
			// destroy it and increase the players score
			Destroy();
			game.Score++;
		}

		// Move the player
		Position = newPosition;
		sprite.Position = Position;
	}

	// Destroy the obstacle
	public void Destroy()
	{
		game.Obstacles.Remove(this);
		sprite.Dispose();
		Destroyed = true;
	}

	



	// Update the obstacle
	public void Update()
	{
		Fall();
	}

	// Draw the obstacle
	public void Render()
	{
		if (Destroyed == true) return;
		game.Window.Draw(sprite);
	}

}