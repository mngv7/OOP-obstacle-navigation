using System.Collections.Generic;
using System.Drawing;

/// <summary>
/// This class represents an obstacle type 'guard'. 
/// It contains a list that stores the location of each guard,
/// The list stores guard locations as a Point type.
/// This list can be appended and retrieved.
/// </summary>
public class guardObstacle : obstacles
{
    inputProcessor inputProcessor = new inputProcessor();

    private readonly List<Point> guardLocationList = new List<Point>();

    /// <summary>
    /// Adds a guard location to the list of guard locations.
    /// </summary>
    /// <param name="location"> The location of the guard to add.</param>
    private void addGuard(Point location)
    {
        guardLocationList.Add(location);
    }

    /// <summary>
    /// Prompts the user asking for a location of the guard. It will then check the user's input.
    /// If valid, the guard location will be added to the corresponding list. If invalid, the user will
    /// be asked to re-enter.
    /// </summary>
    /// <param name="validInput">A reference to a boolean variable that determines if the input is valid.</param>
    /// <param name="errorMessage">A reference to a string containing an error message for invalid input.</param>
    public void addGuard(ref bool validInput, ref string errorMessage)
    {
        Console.WriteLine("Enter the guard's location (X, Y):");
        bool isValidInput = false;

        while (!isValidInput) // Repeat until the user enters a valid input.
        {
            string guardLocationString = Console.ReadLine() ?? ""; // Read input for the location of the guard.

            if (inputProcessor.inputValidator(guardLocationString)) // If the input is valid.
            {
                Point guardLocation = inputProcessor.stringToPoint(guardLocationString); // Convert the input strings to Point objects.
                addGuard(guardLocation); // Add to guardLocationList.
                isValidInput = true; // Exit the loop.
            }
            else if (guardLocationString == "x")
            {
                break; // If 'x' is entered, exit the loop.
            }
            else
            {
                Console.WriteLine(errorMessage); // Display an error message for invalid input.
            }
        }
        validInput = true; // Set validInput as true to redisplay menu once this method is complete.
    }

    /// <summary>
    /// A boolean method to check if a guard is present at a given location.
    /// </summary>
    /// <param name="currentLocation">The location to check, usually the location of the agent.</param>
    /// <returns>Returns true if there is a guard present on the checked location, false otherwise.</returns>
    public override bool isAgentOnObstacle(Point currentLocation)
    {
        // Check through all the locations in the guard locations list to see if any match the current location.
        foreach (Point guardLocation in guardLocationList)
        {
            if (currentLocation == guardLocation)
            {
                return true;
            }
        }

        return false;
    }
}