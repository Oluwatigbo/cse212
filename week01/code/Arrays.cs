using System;
using System.Collections.Generic;

public static class Arrays
{
    /*
     * MultiplesOf Function Plan:
     * 1. Takes a starting number (double) and count of multiples (int)
     * 2. Creates a results array of size 'count'
     * 3. For each index in results array:
     *    - Calculate multiple as start * (index + 1)
     *    - Store in array
     * 4. Return the completed array
     */
    public static double[] MultiplesOf(double start, int count)
    {
        // Create results array
        double[] multiples = new double[count];
        
        // Populate with calculated multiples
        for (int i = 0; i < count; i++)
        {
            multiples[i] = start * (i + 1);
        }
        
        return multiples;
    }

    /*
     * RotateListRight Function Plan:
     * 1. Takes a List<int> and rotation amount (int)
     * 2. Normalize rotation amount using modulo operation
     * 3. If no rotation needed (amount = 0), return immediately
     * 4. Split list into two parts:
     *    - Right part: last 'amount' elements
     *    - Left part: remaining elements
     * 5. Clear original list and rebuild by:
     *    - First adding right part
     *    - Then adding left part
     */
    public static void RotateListRight(List<int> data, int amount)
    {
        // Normalize rotation amount
        amount %= data.Count;
        if (amount == 0) return;
        
        // Split the list
        List<int> rightPart = data.GetRange(data.Count - amount, amount);
        List<int> leftPart = data.GetRange(0, data.Count - amount);
        
        // Rebuild rotated list
        data.Clear();
        data.AddRange(rightPart);
        data.AddRange(leftPart);
    }
}
