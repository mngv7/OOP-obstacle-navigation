using System.Drawing;

/// <summary>
/// Represents a class for determining safe directions based on obstacles.
/// </summary>
public class safeDirections
{
    inputProcessor inputProcessor = new inputProcessor();

    private guardObstacle guard;
    private fenceObstacle fence;
    private sensorObstacle sensor;
    private cameraObstacle camera;
    private asteroidObstacle asteroid;

    /// <summary>
    /// Initializes a new instance of the safeDirections class with obstacle objects.
    /// </summary>
    /// <param name="guard">An instance of the guard obstacle class.</param>
    /// <param name="fence">An instance of the fence obstacle class.</param>
    /// <param name="sensor">An instance of the sensor obstacle class.</param>
    /// <param name="camera">An instance of the camera obstacle class.</param>
    /// <param name="asteroid">An instance of the asteroid obstacle class.</param>
    public safeDirections(guardObstacle guard,
        fenceObstacle fence,
        sensorObstacle sensor,
        cameraObstacle camera,
        asteroidObstacle asteroid)
    {
        this.guard = guard;
        this.fence = fence;
        this.sensor = sensor;
        this.camera = camera;
        this.asteroid = asteroid;
    }

    /// <summary>
    /// Handles and validates the user input to returning safe directions.
    /// </summary>
    /// <param name="validInput">A reference to a boolean variable that determines if the input is valid.</param>
    /// <param name="errorMessage">A reference to a string containing an error message for invalid input.</param>
    public void processSafeDirections(ref bool validInput, ref string errorMessage)
    {
        // Set bool type variable to false to keep the loop repeating until the user enters a valid input.
        bool isValidInput0 = false;

        Console.WriteLine("Enter your current location (X, Y):");

        while (!isValidInput0) // Repeat until the user enters a valid input.
        {
            string currentLocationString = Console.ReadLine() ?? ""; // Read input for the current location.

            if (inputProcessor.inputValidator(currentLocationString)) // Check if the start location input is valid.
            {
                Point currentLocation = inputProcessor.stringToPoint(currentLocationString); // Convert the input string to a Point object.

                Console.WriteLine(outputMessage(currentLocation)); // Display the output message for the current location.

                isValidInput0 = true; // Break the loop.
            }
            else if (currentLocationString == "x")
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

    private string? message;

    /// <summary>
    /// Determines and returns a message indicating safe directions or the need to abort the mission.
    /// </summary>
    /// <param name="currentLocation">The current location of the agent (Point).</param>
    /// <returns>A message indicating safe directions or the need to abort the mission (string).</returns>
    public string outputMessage(Point currentLocation)
    {
        // Check if the agent is on any obstacle.
        if (guard.isAgentOnObstacle(currentLocation) ||
            fence.isAgentOnObstacle(currentLocation) ||
            sensor.isAgentOnObstacle(currentLocation) ||
            camera.isAgentOnObstacle(currentLocation) ||
            asteroid.isAgentOnObstacle(currentLocation))
        {
            message = "Agent, your location is compromised. Abort mission.";
        }
        else
        {
            message = "You can safely take any of the following directions: ";

            // Check if the north direction is safe.
            if (!guard.isObstacleNorth(currentLocation) &&
                !fence.isObstacleNorth(currentLocation) &&
                !sensor.isObstacleNorth(currentLocation) &&
                !camera.isObstacleNorth(currentLocation) &&
                !asteroid.isObstacleNorth(currentLocation))
            {
                message += "N";
            }

            // Check if the east direction is safe.
            if (!guard.isObstacleEast(currentLocation) &&
                !fence.isObstacleEast(currentLocation) &&
                !sensor.isObstacleEast(currentLocation) &&
                !camera.isObstacleEast(currentLocation) &&
                !asteroid.isObstacleEast(currentLocation))
            {
                message += "E";
            }

            // Check if the south direction is safe.
            if (!guard.isObstacleSouth(currentLocation) &&
                !fence.isObstacleSouth(currentLocation) &&
                !sensor.isObstacleSouth(currentLocation) &&
                !camera.isObstacleSouth(currentLocation) &&
                !asteroid.isObstacleSouth(currentLocation))
            {
                message += "S";
            }

            // Check if the west direction is safe.
            if (!guard.isObstacleWest(currentLocation) &&
                !fence.isObstacleWest(currentLocation) &&
                !sensor.isObstacleWest(currentLocation) &&
                !camera.isObstacleWest(currentLocation) &&
                !asteroid.isObstacleWest(currentLocation))
            {
                message += "W";
            }
        }

        if (message == "You can safely take any of the following directions: ") // Check if the 'message' string has been changed.
        {
            // When N,E,S, or W have not been added to the message, this means that none of the directions are safe.
            message = "You cannot safely move in any direction. Abort mission.";
        }

        return message;
    }
}

/// <summary>
/// Represents an abstract class for handling obstacles and determining surrounding locations.
/// </summary>
public abstract class obstacles
{
    /// <summary>
    /// Checks if an agent is on the obstacle at the given location.
    /// </summary>
    /// <param name="currentLocation">The current location of the agent (Point).</param>
    /// <returns>True if the agent is on the obstacle; otherwise, false (bool).</returns>
    public abstract bool isAgentOnObstacle(Point currentLocation);

    /// <summary>
    /// Calculates and returns a new location based on the current location and offsets.
    /// </summary>
    /// <param name="currentLocation">The current location of the agent (Point).</param>
    /// <param name="xOffset">The horizontal offset to apply (int).</param>
    /// <param name="yOffset">The vertical offset to apply (int).</param>
    /// <returns>The new location after applying the offsets (Point).</returns>
    protected Point getNewLocation(Point currentLocation, int xOffset, int yOffset)
    {
        return new Point(currentLocation.X + xOffset, currentLocation.Y + yOffset);
    }

    /// <summary>
    /// Checks if there is an obstacle to the north of the current location.
    /// </summary>
    /// <param name="currentLocation">The current location of the agent (Point).</param>
    /// <returns>True if there is an obstacle to the north, false otherwise (bool).</returns>
    public bool isObstacleNorth(Point currentLocation)
    {
        Point northPoint = getNewLocation(currentLocation, 0, -1);
        return isAgentOnObstacle(northPoint);
    }

    /// <summary>
    /// Checks if there is an obstacle to the east of the current location.
    /// </summary>
    /// <param name="currentLocation">The current location of the agent (Point).</param>
    /// <returns>True if there is an obstacle to the east, false otherwise. (bool).</returns>
    public bool isObstacleEast(Point currentLocation)
    {
        Point eastPoint = getNewLocation(currentLocation, 1, 0);
        return isAgentOnObstacle(eastPoint);
    }

    /// <summary>
    /// Checks if there is an obstacle to the south of the current location.
    /// </summary>
    /// <param name="currentLocation">The current location of the agent (Point).</param>
    /// <returns>True if there is an obstacle to the south, false otherwise (bool).</returns>
    public bool isObstacleSouth(Point currentLocation)
    {
        Point southPoint = getNewLocation(currentLocation, 0, 1);
        return isAgentOnObstacle(southPoint);
    }

    /// <summary>
    /// Checks if there is an obstacle to the west of the current location.
    /// </summary>
    /// <param name="currentLocation">The current location of the agent (Point).</param>
    /// <returns>True if there is an obstacle to the west, false otherwise (bool).</returns>
    public bool isObstacleWest(Point currentLocation)
    {
        Point westPoint = getNewLocation(currentLocation, -1, 0);
        return isAgentOnObstacle(westPoint);
    }
}