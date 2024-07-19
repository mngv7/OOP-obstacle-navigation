using System.Drawing;

/// <summary>
/// Contains methods and lists relevant to map generation, storage, and output.
/// </summary>
public class map
{
    private readonly List<Point> mapBoundaries = new List<Point>();
    private char[,]? Map;

    /// <summary>
    /// Adds top left and bottom right locations of a map to 'mapBoundaries'.
    /// </summary>
    /// <param name="topLeft">Top left location of the map (Point).</param>
    /// <param name="bottomRight">Bottom right location of the map (Point).</param>
    private void addMapBoundaries(Point topLeft, Point bottomRight)
    {
        mapBoundaries.Clear();
        mapBoundaries.Add(topLeft);
        mapBoundaries.Add(bottomRight);
    }

    /// <summary>
    /// Process the user's input for map boundaries and validates those inputs. Once ald
    /// </summary>
    /// <param name="validInput">A reference to a boolean variable that determines if the input is valid.</param>
    /// <param name="errorMessage">A reference to a string containing an error message for invalid input.</param>
    /// <param name="guard">An instance of the guard obstacle class.</param>
    /// <param name="fence">An instance of the fence obstacle class.</param>
    /// <param name="sensor">An instance of the sensor obstacle class.</param>
    /// <param name="camera">An instance of the camera obstacle class.</param>
    /// <param name="asteroid">An instance of the asteroid obstacle class.</param>
    /// <param name="wormhole">An instance of the wormhole obstacle class.</param>
    public void processMapGenerationAndDisplay(ref bool validInput, ref string errorMessage, guardObstacle guard, fenceObstacle fence, sensorObstacle sensor, cameraObstacle camera, asteroidObstacle asteroid, wormholeObstacle wormhole)
    {
        // Set bool type variable to false to keep the loop repeating until the user enters a valid input.
        bool isValidInput0 = false;
        bool isValidInput1 = false;

        Console.WriteLine("Enter the location of the top-left cell of the map (X,Y):");

        while (!isValidInput0) // Repeat until the user enters a valid input.
        {
            string topLeftLocationString = Console.ReadLine() ?? ""; // Read input for the top-left location.

            if (inputProcessor.inputValidator(topLeftLocationString)) // Check if the top-left location input is valid.
            {
                Console.WriteLine("Enter the location of the bottom-right cell of the map (X,Y):");

                while (!isValidInput1) // Repeat until the user enters a valid input.
                {
                    string bottomRightLocationString = Console.ReadLine() ?? ""; // Read input for the bottom-right location.

                    if (inputProcessor.inputValidator(bottomRightLocationString))
                    {
                        // Convert input strings to Point objects.
                        Point topLeftLocation = inputProcessor.stringToPoint(topLeftLocationString);
                        Point bottomRightLocation = inputProcessor.stringToPoint(bottomRightLocationString);

                        // Add map boundaries, generate the map, and display it.
                        addMapBoundaries(topLeftLocation, bottomRightLocation);
                        generateMap(guard, fence, sensor, camera, asteroid, wormhole);
                        displayMap();

                        isValidInput1 = true; // Break the loop.
                    }
                    else if (bottomRightLocationString == "x")
                    {
                        return; // Exit the function if 'x' is entered.
                    }
                    else
                    {
                        Console.WriteLine(errorMessage); // Display an error message for invalid input.
                    }
                }
                isValidInput0 = true;
            }
            else if (topLeftLocationString == "x")
            {
                return; // Exit the function if 'x' is entered.
            }
            else
            {
                Console.WriteLine(errorMessage); // Display an error message for invalid input.
            }
            validInput = true; // Set validInput as true to redisplay menu once this method is complete.
        }
    }

    /// <summary>
    /// Generates a map based on the presence of various obstacles and agents' positions.
    /// </summary>
    /// <param name="guard">An instance of the guard obstacle class.</param>
    /// <param name="fence">An instance of the fence obstacle class.</param>
    /// <param name="sensor">An instance of the sensor obstacle class.</param>
    /// <param name="camera">An instance of the camera obstacle class.</param>
    /// <param name="asteroid">An instance of the asteroid obstacle class.</param>
    /// <param name="wormhole">An instance of the wormhole obstacle class.</param>
    private void generateMap(guardObstacle guard, fenceObstacle fence, sensorObstacle sensor, cameraObstacle camera, asteroidObstacle asteroid, wormholeObstacle wormhole)
    {
        // Calculate the number of rows and columns based off the map boundaries. +1 to account for inclusive range.
        int numberOfRows = Math.Max(mapBoundaries[0].Y, mapBoundaries[1].Y) - Math.Min(mapBoundaries[0].Y, mapBoundaries[1].Y) + 1;
        int numberOfColumns = Math.Max(mapBoundaries[0].X, mapBoundaries[1].X) - Math.Min(mapBoundaries[0].X, mapBoundaries[1].X) + 1;

        Map = new char[numberOfRows, numberOfColumns]; // Create 2D array using calculated row and columns lengths.

        // Extract the coordinates of the top-left and bottom-right map boundaries.
        int topLeftMapBoundaryX = mapBoundaries[0].X; 
        int topLeftMapBoundaryY = mapBoundaries[0].Y;

        int bottomRightMapBoundaryX = mapBoundaries[1].X;
        int bottomRightMapBoundaryY = mapBoundaries[1].Y;

        // Iterate for all possible x and y coordinates within the boundaries.
        for (int i = 0; i < numberOfRows; i++)
        {
            for (int j = 0; j < numberOfColumns; j++)
            {
                // Observed location calcalated by adding i and j to the x and y boundaries.
                // By finding the min of the x and y boundaries, the map will work as normal even if the user accidentally inverts the boundaries.
                // For example: top-left(10,10) and bottom-right(0,0), will act the same as top-left(0,0) and bottom-right(10,10).
                Point observedLocation = new Point(Math.Min(topLeftMapBoundaryX, bottomRightMapBoundaryX) + j, Math.Min(topLeftMapBoundaryY, bottomRightMapBoundaryY) + i);

                // Check if there is an obstacles at the observed location.
                // If true, then the corresponding object will be added to the 2D array at i, j.
                if (guard.isAgentOnObstacle(observedLocation))
                {
                    Map[i, j] = 'g'; // 'g' represents guard.
                }
                else if (fence.isAgentOnObstacle(observedLocation))
                {
                    Map[i, j] = 'f'; // 'f' represents a fence.
                }
                else if (sensor.isAgentOnObstacle(observedLocation))
                {
                    Map[i, j] = 's'; // 's' represents a sensor.
                }
                else if (camera.isAgentOnObstacle(observedLocation))
                {
                    Map[i, j] = 'c'; // 'c' represents a camera.
                } 
                else if (asteroid.isAgentOnObstacle(observedLocation))
                {
                    Map[i, j] = '*'; // '*' represents an asteroid.
                }
                else if (wormhole.isAgentOnObstacle(observedLocation))
                {
                    Map[i, j] = 'w'; // 'w' represents a wormhole.
                }
                else
                {
                    Map[i, j] = '.'; // '.' represents an empty space.
                }
            }
        }
    }

    /// <summary>
    /// Display the generated map onto the console.
    /// </summary>
    private void displayMap()
    {
        for (int row = 0; row < Map?.GetLength(0); row++)
        {
            for (int col = 0; col < Map?.GetLength(1); col++)
            {
                Console.Write(Map?[row, col]);
            }
            Console.WriteLine();
        }
    }
}