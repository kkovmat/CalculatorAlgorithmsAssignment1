class CustomQueue<T>
{
    private T[] array = new T[50];
    private int startPointer = 0;
    private int endPointer = 0;

    public void Enqueue(T value)
    {
        array[endPointer] = value;
        endPointer++;
    }
    public T Dequeue()
    {
        if (startPointer == endPointer)
        {
            throw new Exception("Queue is empty.");
        }
        T firstEl = array[startPointer];
        startPointer++;
        return firstEl;
    }
    public T Peek()
    {
        return array[startPointer];

    }
    public T GetAt(int i) // for showing the result in console only
    {
        return array[i];
    }
    public int Count()
    {
        // Console.WriteLine($"{endPointer} - {startPointer}");
        return endPointer - startPointer;
    }
}