using SFML.Graphics;
using SFML.System;
using SFML.Window;

class Player
{
	private Game game;
	private Vector2f position;
	private Sprite sprite;
	private float speed;

	// New player constructor
	public Player(Game game, float groundPosition)
	{
		this.game = game;
		this.speed = 500;

		// Create the player sprite
		this.sprite = new Sprite(new Texture("./assets/img/player.png"));

		// Spawn the player in the centre of the screen
		this.position = new Vector2f(((game.Window.Size.X - sprite.Texture.Size.X) / 2), groundPosition);
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
		Vector2f newPosition = new Vector2f(x, 0);



		// Update the player movement
		position += newPosition;
		sprite.Position = position;
	}



	// Update the player
	public void Update()
	{
		Move();
	}

	// Draw the player
	public void Render()
	{
		game.Window.Draw(sprite);
	}
}