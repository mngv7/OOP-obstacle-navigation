# Obstacle Detection and Navigation System

## Overview

This project is an obstacle detection and navigation system implemented in C#. It involves various obstacle types such as asteroids, fences, cameras, guards, sensors, and wormholes. The program allows a user to define these obstacles, visualize them on a map, and then determine safe paths for navigation based on the positions of these obstacles.

## Key Features

1. **Obstacle Types**:
    - **Asteroid**: Generates asteroids around a wormhole.
    - **Fence**: Represents vertical or horizontal barriers.
    - **Camera**: Simulates cameras with directional vision (N, S, E, W).
    - **Wormhole**: Simulates a teleportation feature with entry and exit points.
    - **Guard**: Stationary obstacles with defined locations.
    - **Sensor**: Detects agent positions within a specific radius.

2. **User Interaction**:
    - The system is menu-driven, allowing users to add obstacles, view safe directions, generate maps, and find paths.
    - The input is validated for correctness and provides feedback when the input is invalid.

3. **Map Visualization**:
    - The system generates and displays a map showing the layout of obstacles.

4. **Pathfinding and Safe Directions**:
    - Based on the positions of obstacles, the system suggests safe directions and generates paths that avoid detected obstacles.

## Installation and Usage

### Prerequisites

- .NET SDK
- C# Compiler

### How to Run

1. Clone the repository:
    ```
    git clone <repository-url>
    ```
2. Open the project in your preferred IDE (e.g., Visual Studio, Rider) or run the program using the terminal:
    ```
    dotnet run
    ```

### Running the Program

Upon running the program, you will be presented with a menu containing several options:

- **g)** Add a 'Guard' obstacle
- **f)** Add a 'Fence' obstacle
- **s)** Add a 'Sensor' obstacle
- **c)** Add a 'Camera' obstacle
- **w)** Add a 'Wormhole' obstacle
- **d)** Show safe directions
- **m)** Display obstacle map
- **p)** Find a safe path
- **x)** Exit

Choose any of the options by entering the corresponding code. The system will prompt you for necessary inputs and guide you through the process.

### Adding Obstacles

- For most obstacles, you’ll be asked to enter a location (X, Y) and sometimes additional details like direction (N, S, E, W). The system will validate your inputs and store the obstacle information.

### Viewing Safe Directions and Paths

- You can view the safe directions for navigation or generate a path that avoids all detected obstacles by choosing the appropriate options.

## Achievements

- **Total Points**: 43/50
- **Autograder Score**: 20.0/20.0 (Passed Tests: 80/80)
- **Functionality (Custom Obstacle)**: 3/3 pts
- **Object-Oriented Design and Implementation**: 2/3 pts
- **Visibility**: 3/3 pts
- **Coupling**: 1/3 pts
- **Cohesion**: 3/3 pts
- **Abstraction**: 6/6 pts
- **Code Clarity and Comments**: 4/5 pts
- **Exception Handling**: 1/4 pts

## Code Structure

### Key Classes

- **asteroidObstacle**: Handles the generation and positioning of asteroids near wormholes.
- **cameraObstacle**: Simulates directional cameras with specific views.
- **fenceObstacle**: Represents vertical or horizontal fences with defined start and end points.
- **inputProcessor**: Handles input validation and conversion.
- **map**: Generates a visual representation of the obstacles on a map.
- **navigation**: Implements pathfinding logic to avoid obstacles.
- **program**: Main interface for user interaction and menu handling.

## Future Work

- Implement more advanced pathfinding algorithms (e.g., A*).
- Expand obstacle types with more complex behaviors.
- Add a graphical interface for a more interactive experience.

## Author

- **Author**: Zackariya Taylor
