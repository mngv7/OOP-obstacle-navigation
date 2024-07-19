using System.Drawing;


/// <summary>
/// A class containing logic for path finding and console output.
/// </summary>
public class navigation
{
    /// <summary>
    /// Dictionary to store the previous points for path reconstruction.
    /// </summary>
    private static readonly Dictionary<Point, Point> PreviousPoints = new Dictionary<Point, Point>();

    private guardObstacle guard;
    private fenceObstacle fence;
    private sensorObstacle sensor;
    private cameraObstacle camera;
    private wormholeObstacle wormhole;
    private asteroidObstacle asteroid;

    /// <summary>
    /// Initializes a new instance of the navigation class with obstacle objects.
    /// </summary>
    /// <param name="guard">An instance of the guard obstacle class.</param>
    /// <param name="fence">An instance of the fence obstacle class.</param>
    /// <param name="sensor">An instance of the sensor obstacle class.</param>
    /// <param name="camera">An instance of the camera obstacle class.</param>
    /// <param name="wormhole">An instance of the wormhole obstacle class.</param>
    /// <param name="asteroid">An instance of the asteroid obstacle class.</param>
    public navigation(guardObstacle guard,
        fenceObstacle fence,
        sensorObstacle sensor,
        cameraObstacle camera,
        wormholeObstacle wormhole,
        asteroidObstacle asteroid)
    {
        this.guard = guard;
        this.fence = fence;
        this.sensor = sensor;
        this.camera = camera;
        this.wormhole = wormhole;
        this.asteroid = asteroid;
    }

    public void processPathGenerationAndDisplay(ref bool validInput, ref string errorMessage)
    {
        // Set bool type variable to false to keep the loop repeating until the user enters a valid input.
        bool isValidInput0 = false;
        bool isValidInput1 = false;

        Console.WriteLine("Enter your current location (X, Y):");

        while (!isValidInput0) // Repeat until the user enters a valid input.
        {
            string startLocationString = Console.ReadLine() ?? "";

            if (inputProcessor.inputValidator(startLocationString)) // Check if the start location input is valid.
            {
                Console.WriteLine("Enter the location of the mission objective (X, Y):");

                while (!isValidInput1) // Repeat until the user enters a valid input.
                {
                    string objectiveLocationString = Console.ReadLine() ?? "";

                    if (inputProcessor.inputValidator(objectiveLocationString)) // Check if the objective location input is valid.
                    {
                        // Convert input strings to Point objects.
                        Point startLocation = inputProcessor.stringToPoint(startLocationString);
                        Point objectiveLocation = inputProcessor.stringToPoint(objectiveLocationString);

                        string straightPath = findPath(startLocation, objectiveLocation); // Create a path from the start location to the objective.

                        if (wormhole.checkHasWormhole()) // Check if a wormhole has been added by the user.
                        {
                            Console.WriteLine(compareRoutes(startLocation, objectiveLocation)); // Determine the shortest path.
                        }
                        else // If there are no wormholes, there is nothing to compare to, so you instantly output the straight path.
                        {
                            Console.WriteLine("The following path will take you to the objective:");
                            Console.WriteLine(straightPath);
                        }
                        isValidInput1 = true; // Break the loop.
                    }
                    else if (objectiveLocationString == "x")
                    {
                        break; // Exit the function if 'x' is entered.
                    }
                    else
                    {
                        Console.WriteLine(errorMessage);
                    }
                }
                isValidInput0 = true; // Break the loop.
            }
            else if (startLocationString == "x")
            {
                break; // Exit the function if 'x' is entered.
            }
            else
            {
                Console.WriteLine(errorMessage);
            }
        }
        validInput = true; // Set validInput as true to redisplay menu once this method is complete.
    }

    /// <summary>
    /// Checks if a location is walkable (contains no obstacles).
    /// </summary>
    /// <param name="location">Location to to check (Point).</param>
    /// <returns>True if there are no obstacles and the location is walkable, false otherwise.</returns>
    private bool isObstacleWalkable(Point location)
    {
        if (guard.isAgentOnObstacle(location) ||
            fence.isAgentOnObstacle(location) ||
            sensor.isAgentOnObstacle(location) ||
            camera.isAgentOnObstacle(location) ||
            asteroid.isAgentOnObstacle(location))
        {
            return false;
        }
            return true;
    }

    /// <summary>
    /// Finds a path from the 'start' location to the 'objective' location using breadth-first search.
    /// </summary>
    /// <param name="start">The starting location (Point).</param>
    /// <param name="objective">The objective location to reach (Point).</param>
    /// <returns>A string of directions to reach the objective.</returns>
    public string findPath(Point start, Point objective)
    {
        PreviousPoints.Clear(); // Clear the previous points.
        Queue<Point> queue = new Queue<Point>();
        queue.Enqueue(start); // Add the starting point to the queue.

        if (!isObstacleWalkable(objective)) // Check if the objective is occupied by an obstacle.
        {
            return "There is no safe path to the objective."; // If no safe path is found, return message.
        }

        while (queue.Count > 0) // While there are points to explore in the queue.
        {
            Point current = queue.Dequeue();

            if (current.Equals(objective)) // If the objective is reached.
            {
                return generateDirections(start, objective); // Return the route.
            }

            // Define the possible moves on each plane {N,S,E,W}.
            int[] dx = { 0, 0, 1, -1 };
            int[] dy = { -1, 1, 0, 0 };

            for (int i = 0; i < 4; i++) // For each movable direction.
            {
                // Create new point using possible moves and current location.
                int newX = current.X + dx[i]; 
                int newY = current.Y + dy[i];

                Point next = new Point(newX, newY);

                // Check if the created point is walkable and has not been visited.
                if (isObstacleWalkable(next) && !PreviousPoints.ContainsKey(next))
                {
                    PreviousPoints[next] = current; // Record the previous point for path reconstruction.
                    queue.Enqueue(next); // Add the created point to for further exploration.
                }
            }
        }
        return "There is no safe path to the objective."; // If no safe path is found, return message.
    }

    /// <summary>
    /// Generates directions based on the recorded points by the 'findPath' method.
    /// </summary>
    /// <param name="start">The starting location (Point).</param>
    /// <param name="objective">The objective location to reach (Point).</param>
    /// <returns></returns>
    private string generateDirections(Point start, Point objective)
    {
        Point current = objective;
        string directions = ""; // Initialize the directions string.

        while (!current.Equals(start))
        {
            Point previous = PreviousPoints[current];

            // Compares the x and y values between the current and previous point.
            // Appends N,E,S, or W depending on the change in direction.
            if (previous.X < current.X)
                directions = "E" + directions;
            else if (previous.X > current.X)
                directions = "W" + directions;
            else if (previous.Y < current.Y)
                directions = "S" + directions;
            else if (previous.Y > current.Y)
                directions = "N" + directions;

            current = previous;
        }

        return directions;
    }

    /// <summary>
    /// Compares two different routes to determine whether using a wormhole is faster or not.
    /// Once this is determined, the corresponding message will be outputted.
    /// </summary>
    /// <param name="startLocation">The starting location (Point).</param>
    /// <param name="objectiveLocation">The objective location to reach (Point).</param>
    /// <returns>A message representing the shortest path to the objective (String).</returns>
    public string compareRoutes(Point startLocation, Point objectiveLocation)
    {
        string straightPath = findPath(startLocation, objectiveLocation); // Create shortest path string.

        // Create two strings of directions, the first going from the start to the wormhole opening, the second going from the wormhole destination to the objective.
        // Append the two strings for a complete wormhole path.
        string wormholePath = findPath(startLocation, wormhole.getWormholeLocation()) + findPath(wormhole.getWormholeDestination(), objectiveLocation);

        // Find the shorter path.
        // Return a message guiding the agent to their objective.
        if (straightPath.Length > wormholePath.Length)
        {
            string wormholePathMessage = findPath(startLocation, wormhole.getWormholeLocation()) + " *traverse through wormhole* " + findPath(wormhole.getWormholeDestination(), objectiveLocation);
            Console.WriteLine("The following path will take you to the objective via the wormhole:");
            return wormholePathMessage;
        }
        else
        {
            Console.WriteLine("The following path will take you to the objective:");
            return straightPath;
        }
    }
}