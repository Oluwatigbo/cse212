using System.Collections;

public class LinkedList : IEnumerable<int>
{
    private Node? _head;
    private Node? _tail;

    /// <summary>
    /// Insert a new node at the front (i.e. the head) of the linked list.
    /// </summary>
    public void InsertHead(int value)
    {
        // Create new node
        Node newNode = new(value);
        // If the list is empty, then point both head and tail to the new node.
        if (_head is null)
        {
            _head = newNode;
            _tail = newNode;
        }
        // If the list is not empty, then only head will be affected.
        else
        {
            newNode.Next = _head; // Connect new node to the previous head
            _head.Prev = newNode; // Connect the previous head to the new node
            _head = newNode; // Update the head to point to the new node
        }
    }

    /// <summary>
    /// Insert a new node at the back (i.e. the tail) of the linked list.
    /// </summary>
 public void InsertTail(int value)
{
    Node newNode = new(value);
    if (_tail is null)
    {
        // List is empty
        _head = newNode;
        _tail = newNode;
    }
    else
    {
        // List is not empty
        _tail.Next = newNode; // Link the current tail to the new node
        newNode.Prev = _tail; // Link the new node back to the current tail
        _tail = newNode; // Update the tail to the new node
    }
}



    /// <summary>
    /// Remove the first node (i.e. the head) of the linked list.
    /// </summary>
    public void RemoveHead()
    {
        // If the list has only one item in it, then set head and tail 
        // to null resulting in an empty list.  This condition will also
        // cover an empty list.  Its okay to set to null again.
        if (_head == _tail)
        {
            _head = null;
            _tail = null;
        }
        // If the list has more than one item in it, then only the head
        // will be affected.
        else if (_head is not null)
        {
            _head.Next!.Prev = null; // Disconnect the second node from the first node
            _head = _head.Next; // Update the head to point to the second node
        }
    }


    /// <summary>
    /// Remove the last node (i.e. the tail) of the linked list.
    /// </summary>
    public void RemoveTail()
{
    if (_tail is null) return; // List is empty

    if (_head == _tail)
    {
        // Only one node in the list
        _head = null;
        _tail = null;
    }
    else
    {
        // More than one node
        _tail = _tail.Prev; // Move the tail reference to the previous node
        _tail.Next = null; // Disconnect the last node
    }
}

    /// <summary>
    /// Insert 'newValue' after the first occurrence of 'value' in the linked list.
    /// </summary>
    public void InsertAfter(int value, int newValue)
    {
        // Search for the node that matches 'value' by starting at the 
        // head of the list.
        Node? curr = _head;
        while (curr is not null)
        {
            if (curr.Data == value)
            {
                // If the location of 'value' is at the end of the list,
                // then we can call insert_tail to add 'new_value'
                if (curr == _tail)
                {
                    InsertTail(newValue);
                }
                // For any other location of 'value', need to create a 
                // new node and reconnect the links to insert.
                else
                {
                    Node newNode = new(newValue);
                    newNode.Prev = curr; // Connect new node to the node containing 'value'
                    newNode.Next = curr.Next; // Connect new node to the node after 'value'
                    curr.Next!.Prev = newNode; // Connect node after 'value' to the new node
                    curr.Next = newNode; // Connect the node containing 'value' to the new node
                }

                return; // We can exit the function after we insert
            }

            curr = curr.Next; // Go to the next node to search for 'value'
        }
    }

    /// <summary>
    /// Remove the first node that contains 'value'.
    /// </summary>
    public void Remove(int value)
{
    if (_head is null) return; // List is empty

    if (_head.Data == value)
    {
        RemoveHead(); // Reuse RemoveHead if the head needs to be removed
        return;
    }

    Node current = _head;
    while (current != null)
    {
        if (current.Data == value)
        {
            if (current.Next != null)
            {
                current.Next.Prev = current.Prev; // Link the next node back to the previous node
            }
            if (current.Prev != null)
            {
                current.Prev.Next = current.Next; // Link the previous node to the next node
            }
            return; // Exit after removing the node
        }
        current = current.Next; // Move to the next node
    }
}


    /// <summary>
    /// Search for all instances of 'oldValue' and replace the value to 'newValue'.
    /// </summary>
    public void Replace(int oldValue, int newValue)
{
    Node current = _head;
    while (current != null)
    {
        if (current.Data == oldValue)
        {
            current.Data = newValue; // Replace the value
        }
        current = current.Next; // Move to the next node
    }
}

    /// <summary>
    /// Yields all values in the linked list
    /// </summary>
    IEnumerator IEnumerable.GetEnumerator()
    {
        // call the generic version of the method
        return this.GetEnumerator();
    }

    /// <summary>
    /// Iterate forward through the Linked List
    /// </summary>
    public IEnumerator<int> GetEnumerator()
    {
        var curr = _head; // Start at the beginning since this is a forward iteration.
        while (curr is not null)
        {
            yield return curr.Data; // Provide (yield) each item to the user
            curr = curr.Next; // Go forward in the linked list
        }
    }

    /// <summary>
    /// Iterate backward through the Linked List
    /// </summary>
    public IEnumerable<int> Reverse()
{
    Stack<int> stack = new Stack<int>();
    Node current = _head;

    while (current != null)
    {
        stack.Push(current.Data); // Push each value onto the stack
        current = current.Next; // Move to the next node
    }

    while (stack.Count > 0)
    {
        yield return stack.Pop(); // Yield each value in reverse order
    }
}

    public override string ToString()
    {
        return "<LinkedList>{" + string.Join(", ", this) + "}";
    }

    // Just for testing.
    public Boolean HeadAndTailAreNull()
    {
        return _head is null && _tail is null;
    }

    // Just for testing.
    public Boolean HeadAndTailAreNotNull()
    {
        return _head is not null && _tail is not null;
    }
}

public static class IntArrayExtensionMethods {
    public static string AsString(this IEnumerable array) {
        return "<IEnumerable>{" + string.Join(", ", array.Cast<int>()) + "}";
    }
}