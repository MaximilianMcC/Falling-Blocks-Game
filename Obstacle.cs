using SFML.Graphics;
using SFML.System;
using SFML.Window;

class Obstacle
{
    private Game game;
    private float speed;
    private Vector2f Position;
    private Sprite sprite;

    // Create a new obstacle
    public Obstacle(Game game)
    {
        this.game = game;
        Random random = new Random();

        // Get a random speed, and position
        this.speed = random.NextInt64(100, 200);
        float x = random.NextInt64(0, game.Window.Size.X);
        this.Position = new Vector2f(x, -100);


        // Get a random texture for the sprite
        string[] textures = new string[] { "obstacle1", "obstacle2", "obstacle3" };
        Texture texture = new Texture("./assets/img/" + textures[random.Next(textures.Length)] + ".png");
        
        // Make the obstacle sprite
        this.sprite = new Sprite(texture);
    }


    // Make the obstacle fall
    private void Fall()
    {
        // Create the new movement
        Vector2f movement = new Vector2f(0, speed * game.DeltaTime);
        Vector2f newPosition = Position + movement;

        // Move the player
        Position = newPosition;
        sprite.Position = Position;
    }







    // Update the obstacle
    public void Update()
    {
        Fall();

        //TODO: Destroy the obstacle when it goes offscreen
    }

    // Draw the obstacle
    public void Render()
    {
        game.Window.Draw(sprite);
    }

}