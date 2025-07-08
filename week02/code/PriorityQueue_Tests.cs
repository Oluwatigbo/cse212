using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Enqueue items with different priorities
    // Expected Result: Items are added to the queue successfully.
    // Defect(s) Found: None expected.
    public void TestPriorityQueue_1()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Task 1", 1);
        priorityQueue.Enqueue("Task 2", 3);
        priorityQueue.Enqueue("Task 3", 2);

        // Check the length of the queue
        Assert.AreEqual(3, priorityQueue.Length);
    }

    [TestMethod]
    // Scenario: Dequeue items and check the order based on priority
    // Expected Result: "Task 2" (priority 3) should be dequeued first, then "Task 3" (priority 2), then "Task 1" (priority 1).
    // Defect(s) Found: None expected.
    public void TestPriorityQueue_2()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Task 1", 1);
        priorityQueue.Enqueue("Task 2", 3);
        priorityQueue.Enqueue("Task 3", 2);

        Assert.AreEqual("Task 2", priorityQueue.Dequeue());
        Assert.AreEqual("Task 3", priorityQueue.Dequeue());
        Assert.AreEqual("Task 1", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Enqueue items with the same priority
    // Expected Result: Items with the same priority should be dequeued in the order they were added.
    // Defect(s) Found: None expected.
    public void TestPriorityQueue_SamePriority()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Task 1", 2);
        priorityQueue.Enqueue("Task 2", 2);
        priorityQueue.Enqueue("Task 3", 2);

        Assert.AreEqual("Task 1", priorityQueue.Dequeue());
        Assert.AreEqual("Task 2", priorityQueue.Dequeue());
        Assert.AreEqual("Task 3", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Try to dequeue from an empty queue
    // Expected Result: An exception should be thrown with an appropriate error message.
    // Defect(s) Found: None expected.
    public void TestPriorityQueue_Empty()
    {
        var priorityQueue = new PriorityQueue();

        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Exception should have been thrown.");
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("No items in the queue.", e.Message);
        }
    }
}
