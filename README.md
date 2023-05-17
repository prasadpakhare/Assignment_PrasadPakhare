# Assignment_PrasadPakhare

# Snake Game

This is a Snake3D game implemented in Unity using C#. The game features a grid-based system where the snake moves and consumes food.

## Overview

The project consists of the following classes:
- **Grid:** The base class representing the game Grid. It handles the creation of the grid, placement of game objects, and provides utility methods for cell positions.
- **SnakePlacement:** A derived class from Grid. It manages the placement and movement of the snake on the grid. It handles snake body growth, collision detection, and score tracking.
- **SnakeHead:** It Handles collision with food and rais event of incresmental score.

- **FoodPlacement:** A derived class from Grid. It manages the placement of food on the grid. It ensures the food spawns in unoccupied cells and handles the scoring when the snake consumes the food.
- **Streak:** A Streak class handles : If you eat food of the same color as the previous one, that food’s score will be multiplied by the current streak of “colliding with the same color”. and The streak resets when you eat a food of a different color than the previous one.
- **FoodParser:** A FoodParser class, read foodData.json file and conver it into object for further use.

## Features

- Grid-based system: The game is organized on a 25x20 grid, where the snake and food objects are placed.

- Snake movement: The snake moves continuously in the direction specified by the player. The player can change the snake's direction using touch input.

- Food spawning: Food is spawned randomly in unoccupied cells of the grid. When the snake consumes the food, the score increases, and the snake's length grows by 1 unit.

- Score and streak tracking: The game tracks the player's score and streak using the Observer pattern. The Streak class listens for events when the snake consumes food and updates the score and streak accordingly.

- Game over condition: The game ends when the snake collides with its own body or with the grid boundaries. The final score is displayed, and the player can restart the game.
