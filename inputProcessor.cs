using System.Drawing;
/// <summary>
/// A class used to process and validate user inputs (locations, and directions).
/// </summary>
public class inputProcessor
{
    /// <summary>
    /// Converts string inputs to a Point type.
    /// </summary>
    /// <param name="input">String in the format 'x,y'.</param>
    /// <returns>A Point object representing the inputted string.</returns>
    public static Point stringToPoint(string input)
    {
        string[] coordinates = input.Split(','); // Split the input by ',' for an array with the x and y values.

        // Parse the coordinates as integers.
        int x = int.Parse(coordinates[0]);
        int y = int.Parse(coordinates[1]);

        return new Point(x, y); // Create a Point object using the parsed string.
    }

    /// <summary>
    /// A validator that checks if an input is valid and won't cause errors.
    /// </summary>
    /// <param name="input">Input string to validate.</param>
    /// <returns>True if the input is valid, false otherwise.</returns>
    public static bool inputValidator(string input)
    {
        string[] inputCoordinates = input.Split(','); // // Split the input by ','.

        // Check if the split was successful by checking the length.
        // With the correct number of commas in the input, the length of the checked array should be 2.
        if (inputCoordinates.Length == 2)
        {
            if (int.TryParse(inputCoordinates[0], out int x) && int.TryParse(inputCoordinates[1], out int y)) // Attempt to parse the coordinates as integers.
            {
                return true; // If parse was successful return true. 
            }
        }

        return false; // If any of the condiditions were not met, return false.
    }

    /// <summary>
    /// Validates a char input to check if it represents a valid direction (n, e, s, or w).
    /// </summary>
    /// <param name="input">The input char to validate.</param>
    /// <returns>True if the input is a valid direction, false otherwise.</returns>
    public static bool inputValidator(char input)
    {
        switch (input)
        {
            case 'n':
            case 'e':
            case 's':
            case 'w':
                return true; // Valid direction input.
            default: 
                return false; // Invalid direction input.
        }
    }
}