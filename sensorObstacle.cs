using System.Drawing;

/// <summary>
/// Represents an obstacle of the type 'sensor'.
/// This class stores sensor locations and their ranges.
/// </summary>
public class sensorObstacle : obstacles
{
    inputProcessor inputProcessor = new inputProcessor();

    private readonly List<Point> sensorLocationList = new List<Point>();
    private readonly List<double> sensorRangeList = new List<double>();

    /// <summary>
    /// Adds a sensor along with its range to the corresponding lists.
    /// </summary>
    /// <param name="location">The location of the sensor (Point).</param>
    /// <param name="range">The range of the sensor (Point).</param>
    private void addSensor(Point location, double range)
    {
        sensorLocationList.Add(location);
        sensorRangeList.Add(range);
    }

    /// <summary>
    /// Prompts the user to input the sensor's location and range. These values are then validated.
    /// If valid, it will add the input to corresponding lists, if not the user will be asked to re-input.
    /// </summary>
    /// <param name="validInput">A reference to a boolean variable that determines if the input is valid.</param>
    /// <param name="errorMessage">A reference to a string containing an error message for invalid input.</param>
    /// <param name="errorMessage2">A reference to a string containing another error message for invalid input.</param>
    public void addSensor(ref bool validInput, ref string errorMessage, ref string errorMessage2)
    {
        Console.WriteLine("Enter the sensor's location (X, Y):");

        bool isValidInput0 = false;
        bool isValidInput1 = false;

        while (!isValidInput0) // Repeat until the user enters a valid input.
        {
            // Read input for the sensor's location.
            string sensorLocationString = Console.ReadLine() ?? "";

            if (inputProcessor.inputValidator(sensorLocationString)) // Check if the sensor location input is valid.
            {
                isValidInput0 = true; // Mark the input as valid.

                Console.WriteLine("Enter the sensor's range (in klicks):");

                while (!isValidInput1) // Repeat until the user enters a valid input.
                {
                    try
                    {
                        string sensorRangeString = Console.ReadLine() ?? ""; // Read input for the sensor's range.

                        if (sensorRangeString == "x")
                        {
                            validInput = false; // If 'x' is entered, exit the loop and return to main menu.
                            break;
                        }
                        else
                        {
                            // Convert the sensor range input to a doube and sensor location to a Point.
                            double sensorRange = double.Parse(sensorRangeString);
                            Point sensorLocation = inputProcessor.stringToPoint(sensorLocationString);
                            
                            addSensor(sensorLocation, sensorRange); // Add the sensor with its location and range.
                            isValidInput1 = true; // Break the loop.
                        }
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine(errorMessage2); // Display an error message for invalid range input.
                    }
                }
            }
            else if (sensorLocationString == "x")
            {
                break; // If 'x' is entered, exit the loop.
            }
            else
            {
                Console.WriteLine(errorMessage); // Display an error message for invalid location input.
            }
        }
        validInput = true; // Set validInput as true to redisplay menu once this method is complete.
    }


    /// <summary>
    /// A method to check if a sensor is present at a given location.
    /// </summary>
    /// <param name="location"></param>
    /// <returns></returns>
    public override bool isAgentOnObstacle(Point location)
    {
        // Extract the x and y locations of the input.
        int agentX = location.X;
        int agentY = location.Y;

        for (int i = 0; i < sensorLocationList.Count; i++) // For each sensor in the list.
        {
            Point sensorLocation = sensorLocationList[i];
            double sensorRange = sensorRangeList[i];

            // Extract the x and y locations of the sensor.
            int sensorX = sensorLocation.X;
            int sensorY = sensorLocation.Y;

            // Using Pythagorean formula, the distance between the given location and the sensor is calculated.
            double distance = Math.Sqrt(Math.Pow(agentX - sensorX, 2) + Math.Pow(agentY - sensorY, 2));

            // If the given location is within the range of the sensor.
            if (distance <= sensorRange)
            {
                return true;
            }
        }

        return false;
    }
}