using System;
using System.Collections.Generic;

public class Maze
{
    private readonly Dictionary<(int, int), bool[]> _mazeMap;
    private int _currX;
    private int _currY;

    /// <summary>
    /// Initializes a new instance of the Maze class with a specified maze map and starting position.
    /// </summary>
    /// <param name="mazeMap">A dictionary mapping coordinates to an array of booleans indicating possible moves (left, right, up, down).</param>
    /// <param name="startX">Starting X coordinate.</param>
    /// <param name="startY">Starting Y coordinate.</param>
    /// <exception cref="ArgumentNullException">Thrown when mazeMap is null.</exception>
    /// <exception cref="ArgumentException">Thrown when mazeMap is empty or starting position is invalid.</exception>
    public Maze(Dictionary<(int, int), bool[]> mazeMap, int startX = 1, int startY = 1)
    {
        if (mazeMap == null)
            throw new ArgumentNullException(nameof(mazeMap), "Maze map cannot be null.");
        if (mazeMap.Count == 0)
            throw new ArgumentException("Maze map cannot be empty.", nameof(mazeMap));
        if (!mazeMap.ContainsKey((startX, startY)))
            throw new ArgumentException($"Starting position ({startX}, {startY}) is not in the maze map.", nameof(startX));
        if (mazeMap[(startX, startY)] == null || mazeMap[(startX, startY)].Length != 4)
            throw new ArgumentException("Maze map entry must have exactly 4 directions.", nameof(mazeMap));

        _mazeMap = mazeMap;
        _currX = startX;
        _currY = startY;
    }

    /// <summary>
    /// Attempts to move left if possible.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown when moving left is not allowed.</exception>
    public void MoveLeft()
    {
        if (_mazeMap[(_currX, _currY)][0])
        {
            if (!_mazeMap.ContainsKey((_currX - 1, _currY)))
                throw new InvalidOperationException("Can't go that way!");
            _currX--;
        }
        else
        {
            throw new InvalidOperationException("Can't go that way!");
        }
    }

    /// <summary>
    /// Attempts to move right if possible.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown when moving right is not allowed.</exception>
    public void MoveRight()
    {
        if (_mazeMap[(_currX, _currY)][1])
        {
            if (!_mazeMap.ContainsKey((_currX + 1, _currY)))
                throw new InvalidOperationException("Can't go that way!");
            _currX++;
        }
        else
        {
            throw new InvalidOperationException("Can't go that way!");
        }
    }

    /// <summary>
    /// Attempts to move up if possible.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown when moving up is not allowed.</exception>
    public void MoveUp()
    {
        if (_mazeMap[(_currX, _currY)][2])
        {
            if (!_mazeMap.ContainsKey((_currX, _currY - 1)))
                throw new InvalidOperationException("Can't go that way!");
            _currY--;
        }
        else
        {
            throw new InvalidOperationException("Can't go that way!");
        }
    }

    /// <summary>
    /// Attempts to move down if possible.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown when moving down is not allowed.</exception>
    public void MoveDown()
    {
        if (_mazeMap[(_currX, _currY)][3])
        {
            if (!_mazeMap.ContainsKey((_currX, _currY + 1)))
                throw new InvalidOperationException("Can't go that way!");
            _currY++;
        }
        else
        {
            throw new InvalidOperationException("Can't go that way!");
        }
    }

    /// <summary>
    /// Gets the current location in the maze.
    /// </summary>
    /// <returns>A string indicating the current (x, y) position.</returns>
    public string GetStatus()
    {
        return $"Current location (x={_currX}, y={_currY})";
    }
}