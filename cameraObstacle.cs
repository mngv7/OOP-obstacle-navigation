using System.Collections.Generic;
using System.Drawing;

/// <summary>
/// Represents an obstacle of the type 'camera'.
/// This class stores camera locations and their corresponding directions.
/// </summary>
public class cameraObstacle : obstacles
{
    inputProcessor inputProcessor = new inputProcessor();

    private readonly List<Point> cameraLocationList = new List<Point>();
    private readonly List<char> cameraDirectionList = new List<char>();


    /// <summary>
    /// Adds a camera with its location and direction.
    /// </summary>
    /// <param name="location">The location of the camera (Point).</param>
    /// <param name="direction">The corresponding direction of the camera (string).</param>
    private void addCamera(Point location, char direction)
    {
        cameraLocationList.Add(location);
        cameraDirectionList.Add(direction);
    }

    /// <summary>
    /// This method handles the camera creation process. Prompts the user to input the camera's location
    /// and direction, then validates the input. If valid a camera will be added to the list of camera locations and directions. 
    /// </summary>
    /// <param name="validInput">A reference to a boolean variable that determines if the input is valid.</param>
    /// <param name="errorMessage">A reference to a string containing an error message for invalid input.</param>
    /// <param name="errorMessage1">A reference to a string containing another error message for invalid input.</param>
    public void addCamera(ref bool validInput, ref string errorMessage, ref string errorMessage1)
    {
        Console.WriteLine("Enter the camera's location (X, Y):");

        // Set bool type variable to false to keep the loop repeating until the user enters a valid input.
        bool isValidInput0 = false;
        bool isValidInput1 = false;
        
        while (!isValidInput0) // Repeat until the user input is valid.
        {
            string cameraLocationString = Console.ReadLine() ?? "";

            if (inputProcessor.inputValidator(cameraLocationString)) // Check if the input is valid.
            {
                isValidInput0 = true;
                Console.WriteLine("Enter the direction the camera is facing (n, s, e or w):");

                while (!isValidInput1) // Repeat until the user input is valid.
                {
                    char cameraDirection = (Console.ReadLine() ?? "")[0]; // Read the input for the camera's direction as char.

                    if (inputProcessor.inputValidator(cameraDirection)) // Check if the input for the camera direction is valid.
                    {
                        Point cameraLocation = inputProcessor.stringToPoint(cameraLocationString); // Convert the camera location string to a Point object.

                        addCamera(cameraLocation, cameraDirection); // Add the camera with its location and direction.
                        isValidInput1 = true;  // Exit the inner while loop.
                    }
                    else if (cameraDirection == 'x')
                    {
                        break; // If 'x' is entered, exit the loop.
                    }
                    else
                    {
                        Console.WriteLine(errorMessage1); // Display an error message if the input is invalid.
                    }
                }
            }
            else if (cameraLocationString == "x")
            {
                break; // If 'x' is entered, exit the loop.
            }
            else
            {
                Console.WriteLine(errorMessage); // Display an error message if the input is invalid.
            }
        }
        validInput = true; // Set validInput as true to redisplay menu once this method is complete.
    }


    /// <summary>
    /// A boolean method to check whether a given location is within the view of any camera.
    /// 
    /// This method uses linear equations (y = mx + c) to bound the view of the camera.
    /// The general equation for each of the lines are: (1) ... y = x + c, (2) ... y = -x + d.
    /// 
    /// Using the location of the camera, the intercepts can be calculated to solve for c and d.
    /// </summary>
    /// <param name="location">The location to check, usually the location of the agent.</param>
    /// <returns>True if the inputed location is within the view of any camera, false otherwise.</returns>
    public override bool isAgentOnObstacle(Point location)
    {
        int agentY = location.Y; // y-value of the agent's coordinate.
        int agentX = location.X; // x-value of the agent's coordinate.

        for (int i = 0; i < cameraLocationList.Count; i++) // Repeat for each camera added by the user.
        {
            int cameraY = cameraLocationList[i].Y; // y-value of the current camera's location.
            int cameraX = cameraLocationList[i].X; // x-value of the current camera's location.

            int c = cameraY - cameraX; // Solve c to complete equation 1.
            int d = cameraY + cameraX; // Solve d to complete equation 2.

            int xboundOne = d - agentY; // For equation 2, solve for x.
            int xboundTwo = agentY - c; // For equation 1, solve for x.

            int yboundOne = agentX + c; // For equation 1, solve for y.
            int yboundTwo = d - agentX; // For equation 2, solve for y.

            switch (cameraDirectionList[i]) // Check the direction of the camera to determine which bounds to use and to differentiate upper/lower.
            {
                // If the camera is facing a vertical direction (NS), the x values from equation 1 and 2 will be used to determine the bounds.
                // If the camera is facing a horizontal direction (EW), y values will be used instead.

                // The if statements will then check if the agent is between the two lines (equation 1 and 2), and if the agent is in front of the camera.
                // If all conditions are met, it will return true, false otherwise.
                case 'n':
                    int leftBound = xboundTwo;
                    int rightBound = xboundOne;

                    if (leftBound <= agentX && agentX <= rightBound && cameraY >= agentY)
                    {
                        return true;
                    }
                    break;
                case 'e':
                    int upperBound = yboundOne; 
                    int lowerBound = yboundTwo; 

                    if (upperBound >= agentY && agentY >= lowerBound && cameraX <= agentX)
                    {
                        return true;
                    }
                    break;
                case 's':
                    rightBound = xboundTwo;
                    leftBound = xboundOne;

                    if (leftBound <= agentX && agentX <= rightBound && cameraY <= agentY)
                    {
                        return true;
                    }
                    break;
                case 'w':
                    upperBound = yboundTwo;
                    lowerBound = yboundOne;

                    if (upperBound >= agentY && agentY >= lowerBound && cameraX >= agentX)
                    {
                        return true;
                    }
                    break;
            }
        }
        return false;
    }
}