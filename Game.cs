using SFML.Graphics;
using SFML.System;
using SFML.Window;

class Game
{
	public void Run()
	{
		// Create the SFML window
		RenderWindow window = new RenderWindow(new VideoMode(800, 600), "Falling Obstacles");
		window.SetFramerateLimit(60);
		window.Closed += (sender, e) => window.Close();



		while (window.IsOpen)
		{
			// Handle events
			window.DispatchEvents();

			

			

			// Clear the window
			window.Clear(Color.Magenta);

			// Display the contents of the window
			window.Display();
		}
	}
}
