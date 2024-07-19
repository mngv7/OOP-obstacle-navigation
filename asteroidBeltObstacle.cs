using System.Drawing;

/// <summary>
/// Represents an obstacle of the type 'asteroid'.
/// This class generates and stores information on asteroids.
/// </summary>
public class asteroidObstacle : obstacles
{
    // Reference to 'wormholeObstacle'.
    private wormholeObstacle wormhole;
    
    // Constructor initialises with a 'wormholeObstacle' reference.
    public asteroidObstacle(wormholeObstacle wormhole)
    {
        this.wormhole = wormhole;
    }

    private List<Point> asteroidLocations = new List<Point>();

    /// <summary>
    /// Generates random locations for asteroids around the centre of a wormhole.
    /// </summary>
    /// <param name="wormholeCenter">Location of the centre of the wormhole (Point).</param>
    /// <returns>Locations of randomly generated asteroids (List of Point).</returns>
    public List<Point> generateAsteroidBelt(Point wormholeCenter)
    {
        List<Point> asteroids = new List<Point>();
        Random random = new Random();

        for (int i = 0; i < 10; i++) // Repeat for desired amount of asteroids (10).
        {
            int angle = random.Next(0, 360); // Generate a random angle (degrees).
            double radians = angle * (Math.PI / 180); // Convert angle to radians.
            int distance = random.Next(3, 6); // Generate a random distance between the asteroid and the wormhole.

            // Calculate the coordinates of the asteroid based on the angle, distance, and wormhole center.
            int x = (int)(wormholeCenter.X + distance * Math.Cos(radians));
            int y = (int)(wormholeCenter.Y + distance * Math.Sin(radians));

            Point asteroid = new Point(x, y); // Create a new point based on the calculated x and y values.
            asteroids.Add(asteroid); // Add to 'asteroids' list.
        }

        return asteroids;
    }

    /// <summary>
    /// This boolean method checks if an asteroids is present at a given location.
    /// </summary>
    /// <param name="currentLocation">The location to check (Point).</param>
    /// <returns>True if the inputed location matches on of the asteroid locations, false otherwise.</returns>
    public override bool isAgentOnObstacle(Point currentLocation)
    {
        // Get wormhole location and destination from the 'wormholeObstacle' class.
        Point wormholeCentre = wormhole.getWormholeLocation(); 
        Point wormholeDestination = wormhole.getWormholeDestination();

        // Checks if asteroids have not been generated, if so:
        // Create asteroid locations using the asteroid generator around the wormholes.
        if (asteroidLocations.Count == 0)
        {
            asteroidLocations = generateAsteroidBelt(wormholeCentre);
            asteroidLocations.AddRange(generateAsteroidBelt(wormholeDestination));
        }

        if (wormhole.checkHasWormhole()) // Checks if a wormhole exists.
        {
            foreach (Point asteroidLocation in asteroidLocations) // Iterate for each asteroid.
            {
                if (currentLocation == asteroidLocation)
                {
                    // If the location being checked matches on of the asteroid's locations, return true.
                    return true;
                }
            }
        }

        return false; // If identical locations cannot be found, return false.
    }
}