using System;
using System.Collections.Generic;

public class Maze
{
    private readonly Dictionary<(int, int), bool[]> _mazeMap;
    private int _currX = 1;
    private int _currY = 1;

    public Maze(Dictionary<(int, int), bool[]> mazeMap)
    {
        _mazeMap = mazeMap ?? throw new ArgumentNullException(nameof(mazeMap));
    }

    public void MoveLeft()
    {
        if (!CanMove(0))
            throw new InvalidOperationException("Can't go that way!");
        _currX--;
    }

    public void MoveRight()
    {
        if (!CanMove(1))
            throw new InvalidOperationException("Can't go that way!");
        _currX++;
    }

    public void MoveUp()
    {
        if (!CanMove(2))
            throw new InvalidOperationException("Can't go that way!");
        _currY--;
    }

    public void MoveDown()
    {
        if (!CanMove(3))
            throw new InvalidOperationException("Can't go that way!");
        _currY++;
    }

    private bool CanMove(int directionIndex)
    {
        return _mazeMap.TryGetValue((_currX, _currY), out var directions) && 
               directions != null && 
               directionIndex < directions.Length && 
               directions[directionIndex];
    }

    public string GetStatus()
    {
        return $"Current location (x={_currX}, y={_currY})";
    }
}
