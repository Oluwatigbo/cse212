public class Node
{
    public int Data { get; set; }
    public Node? Right { get; private set; }
    public Node? Left { get; private set; }

    public Node(int data)
    {
        this.Data = data;
    }

    public void Insert(int value)
{
    if (value < Data)
    {
        if (Left is null)
            Left = new Node(value);
        else
            Left.Insert(value);
    }
    else if (value > Data) // Only insert if value is greater
    {
        if (Right is null)
            Right = new Node(value);
        else
            Right.Insert(value);
    }
    // If value == this.Data, do nothing (no duplicates)
}


    public bool Contains(int value)
{
    if (value < Data)
    {
        return Left != null && Left.Contains(value);
    }
    else if (value > Data)
    {
        return Right != null && Right.Contains(value);
    }
    return true; // value == this.Data
}


    public int GetHeight()
{
    if (this == null) return 0; // If the node is null, height is 0
    int leftHeight = Left != null ? Left.GetHeight() : 0;
    int rightHeight = Right != null ? Right.GetHeight() : 0;
    return 1 + Math.Max(leftHeight, rightHeight);
}


}