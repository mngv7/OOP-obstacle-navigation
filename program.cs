namespace assessment_task_2;

/// <summary>
/// This class is the main interface for the user.
/// </summary>
class program : inputProcessor
{
    static void Main(string[] args)
    {
        // Initialise variables that will be used to process inputs.
        string userInput;

        // Create instances of other classes.
        guardObstacle guard = new();
        fenceObstacle fence = new();
        sensorObstacle sensor = new();
        cameraObstacle camera = new();
        wormholeObstacle wormhole = new();
        asteroidObstacle asteroid = new(wormhole);
        map map = new();

        navigation navigation = new(guard, fence, sensor, camera, wormhole, asteroid);

        safeDirections safeDirections = new(guard, fence, sensor, camera, asteroid);

        // Various error messages that can be returned when needed.
        string errorMessage = "Please enter a valid coordinate (X, Y) or type 'x' to exit to main menu.";
        string errorMessage1 = "Please enter a valid direction (n, e, s, w).";
        string errorMessage2 = "Please enter a valid number or type 'x' to exit to main menu.";
        bool validInput = false;

        do
        {
            // Main menu for the user, where they are presented with options to input.
            Console.WriteLine("Select one of the following options");
            Console.WriteLine("g) Add 'Guard' obstacle");
            Console.WriteLine("f) Add 'Fence' obstacle");
            Console.WriteLine("s) Add 'Sensor' obstacle");
            Console.WriteLine("c) Add 'Camera' obstacle");
            Console.WriteLine("w) Add 'Wormhole' obstacle");
            Console.WriteLine("d) Show safe directions");
            Console.WriteLine("m) Display obstacle map");
            Console.WriteLine("p) Find a safe path");
            Console.WriteLine("x) Exit");
            Console.WriteLine("Enter code:");

            // Read the user input.
            userInput = Console.ReadLine() ?? "";

            validInput = false;

            // This switch block checks the user input and executes the relevant code.

            // It is also being used as an error handling technique, where if the user
            // inputs irrelevant information, it will default to "Invalid option".

            switch (userInput)
            {
                case "g":
                    guard.addGuard(ref validInput, ref errorMessage);
                    break;

                case "f":
                    fence.addFence(ref validInput, ref errorMessage);
                    break;

                case "s":
                    sensor.addSensor(ref validInput, ref errorMessage, ref errorMessage2);
                    break;

                case "c":
                    camera.addCamera(ref validInput, ref errorMessage, ref errorMessage1);
                    
                    break;

                case "w":
                    wormhole.addWormhole(ref validInput, ref errorMessage);
                    break;

                case "d":
                    safeDirections.processSafeDirections(ref validInput, ref errorMessage);
                    break;

                case "m":
                    map.processMapGenerationAndDisplay(ref validInput, ref errorMessage, guard, fence, sensor, camera, asteroid, wormhole);
                    break;

                case "p":
                    navigation.processPathGenerationAndDisplay(ref validInput, ref errorMessage);
                    break;

                case "x":
                    validInput = false;
                    break;

                default:
                    Console.WriteLine("Invalid option.");
                    validInput = true;
                    break;
            }
        } while (validInput);
    }
}