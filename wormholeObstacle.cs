using System.Drawing;

public class wormholeObstacle
{
    inputProcessor inputProcessor = new inputProcessor();

    // Stores the coordinates of the wormhole's location and destination.
    private Point wormholeLocation;
    private Point wormholeDestination;

    // Indicates whether a wormhole exists.
    private bool hasWormhole = false;

    /// <summary>
    /// A method to retrieve 'hasWormhole'.
    /// </summary>
    /// <returns>True or false, indicative of the presence of a wormhole (bool).</returns>
    public bool checkHasWormhole()
    {
        return hasWormhole;
    }

    /// <summary>
    /// Adds a wormhole with the specified location and destination.
    /// </summary>
    /// <param name="wormholeLocationUpdate">The location of the wormhole (Point).</param>
    /// <param name="wormholeDestinationUpdate">The destination of the wormhole (Point).</param>
    private void addWormhole(Point wormholeLocationUpdate, Point wormholeDestinationUpdate)
    {
        wormholeLocation = wormholeLocationUpdate;
        wormholeDestination = wormholeDestinationUpdate;
        hasWormhole = true; // Set 'hasWormhole' to true.
    }

    /// <summary>
    /// This method handles the wormhole creation process. Prompts the user to input the wormhole's location
    /// and destination, and validates the input. Adds a wormhole to the list of wormhole locations and destinations. 
    /// </summary>
    /// <param name="validInput">A reference to a boolean variable that determines if the input is valid.</param>
    /// <param name="errorMessage">A reference to a string containing an error message for invalid input.</param>
    public void addWormhole(ref bool validInput, ref string errorMessage)
    {
        Console.WriteLine("Enter the wormhole's location (X, Y):");
        bool isValidInput0 = false;
        bool isValidInput1 = false;

        while (!isValidInput0) // Repeat until the user enters a valid input.
        {
            string wormholeLocationString = Console.ReadLine() ?? "";

            if (inputProcessor.inputValidator(wormholeLocationString)) // Check if the wormhole location input is valid.
            {
                isValidInput0 = true;
                Console.WriteLine("Enter the wormhole's destination (X, Y):");

                while (!isValidInput1) // Repeat until the user enters a valid input.
                {
                    string wormholeDestinationString = Console.ReadLine() ?? "";

                    if (inputProcessor.inputValidator(wormholeDestinationString)) // Check if the wormhole destination input is valid.
                    {
                        // Convert user input string to a Point object.
                        Point wormholeDestination = inputProcessor.stringToPoint(wormholeDestinationString);
                        Point wormholeLocation = inputProcessor.stringToPoint(wormholeLocationString);

                        addWormhole(wormholeLocation, wormholeDestination);
                        isValidInput1 = true;
                    }
                    else if (wormholeDestinationString == "x")
                    {
                        break; // If 'x' is entered, exit the loop.
                    }
                    else
                    {
                        Console.WriteLine(errorMessage); // Display an error message for invalid input.
                    }
                }
            }
            else if (wormholeLocationString == "x")
            {
                break; // If 'x' is entered, exit the loop.
            }
            else
            {
                Console.WriteLine(errorMessage); // Display an error message for invalid input.
            }
        }

        validInput = true;
    }

    /// <summary>
    /// Gets the coordinates of the wormhole's location.
    /// </summary>
    /// <returns>The location of the wormhole (Point).</returns>
    public Point getWormholeLocation()
    {
        return wormholeLocation;
    }

    /// <summary>
    /// Gets the coordinates of the wormhole's destination.
    /// </summary>
    /// <returns>The destination of the wormhole (Point).</returns>
    public Point getWormholeDestination()
    {
        return wormholeDestination;
    }

    /// <summary>
    /// Checks if there is a wormhole in a given location.
    /// </summary>
    /// <param name="currentLocation">The location to check (Point).</param>
    /// <returns>True if the given location is on the wormhole, false otherwise.</returns>
    public bool isAgentOnObstacle(Point currentLocation)
    {
        if (hasWormhole) // Check if a wormhole exists.
        {
            // Check if the given location matches either the wormhole location or destination.
            if (currentLocation == wormholeLocation || currentLocation == wormholeDestination)
            {
                return true;
            }
        }

        return false;
    }
}
