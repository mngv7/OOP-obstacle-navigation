using System;
using System.Collections.Generic;
using System.Drawing;

/// <summary>
/// This class represents an obstacle type 'fence'. A fence spans in either horizontal or vertical lines.
/// It contains two lists, one for storing the start of each fence, and another for storing the end for each fence.
/// Both lists store locations as a Point type.
/// The list can be appended in combination, or retrieved individually.
/// </summary>

public class fenceObstacle : obstacles
{
    inputProcessor inputProcessor = new inputProcessor();

    private readonly List<Point> fenceStartLocationList = new List<Point>();
    private readonly List<Point> fenceEndLocationList = new List<Point>();


    /// <summary>
    /// Adds the start and end of each fence to the corresponding lists.
    /// </summary>
    /// <param name="start">The start of the fence.</param>
    /// <param name="end">The end of the fence.</param>
    private void addFence(Point start, Point end)
    {
        fenceStartLocationList.Add(start);
        fenceEndLocationList.Add(end);
    }

    /// <summary>
    /// Prompts the user to input the start and end coordinates of the fence and validates the input.
    /// If the input is valid it will add a fence to the list of fence locations.
    /// </summary>
    /// <param name="validInput">A reference to a boolean variable that determines if the input is valid.</param>
    /// <param name="errorMessage">A reference to a string containing an error message for invalid input.</param>
    public void addFence(ref bool validInput, ref string errorMessage)
    {
        Console.WriteLine("Enter the location where the fence starts (X, Y):");

        // Set bool type variable to false to keep the loop repeating until the user enters a valid input.
        bool isValidInput0 = false;
        bool isValidInput1 = false;

        while (!isValidInput0)
        {
            string fenceStartString = Console.ReadLine() ?? ""; // Read input for the starting location of the fence.

            if (inputProcessor.inputValidator(fenceStartString)) // Check if the input is valid.
            {
                isValidInput0 = true; // Mark the input as valid.

                Console.WriteLine("Enter the location where the fence ends (X, Y):");

                while (!isValidInput1)
                {
                    string fenceEndString = Console.ReadLine() ?? ""; // Read input for the ending location of the fence.

                    if (inputProcessor.inputValidator(fenceEndString)) // Check if the input is valid.
                    {
                        // Convert the input strings to Point objects.
                        Point fenceStart = inputProcessor.stringToPoint(fenceStartString);
                        Point fenceEnd = inputProcessor.stringToPoint(fenceEndString);

                        // Add a fence with the specified start and end points.
                        addFence(fenceStart, fenceEnd);
                        isValidInput1 = true; // Mark the input as valid.
                    }
                    else if (fenceEndString == "x")
                    {
                        break; // If 'x' is entered, exit the loop.
                    }
                    else
                    {
                        Console.WriteLine(errorMessage); // Display an error message for invalid input.
                    }
                }
            }
            else if (fenceStartString == "x")
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
    /// <returns>Returns true if the current location matches the location of a fence, false otherwise.</returns>
    public override bool isAgentOnObstacle(Point currentLocation)
    {
        for (int i = 0; i < fenceStartLocationList.Count; i++) // Iterates for all different fences.
        {
            Point fenceLocationStart = fenceStartLocationList[i];
            Point fenceLocationEnd = fenceEndLocationList[i];

            if (fenceLocationStart.X == fenceLocationEnd.X) // Checks if the fence is vertical.
            {
                if (currentLocation.X == fenceLocationStart.X && // Checks if the agent is vertically aligned with the fence.
                    currentLocation.Y >= Math.Min(fenceLocationStart.Y, fenceLocationEnd.Y) &&
                    currentLocation.Y <= Math.Max(fenceLocationStart.Y, fenceLocationEnd.Y)) // Checks if the agent is within the start and the end of the fence. 
                {
                    return true; // If all conditions are satisfied, the observed location is on the fence.
                }
            }
            else if (fenceLocationStart.Y == fenceLocationEnd.Y) // Checks if the fence is horizontal.
            {
                if (currentLocation.Y == fenceLocationStart.Y && // Checks if the agent is horizontally aligned with the fence.
                    currentLocation.X >= Math.Min(fenceLocationStart.X, fenceLocationEnd.X) &&
                    currentLocation.X <= Math.Max(fenceLocationStart.X, fenceLocationEnd.X)) // Checks if the agent is within the start and the end of the fence.
                {
                    return true;
                }
            }
        }

        return false;
    }
}