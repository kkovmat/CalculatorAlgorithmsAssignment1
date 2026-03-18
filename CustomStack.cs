class CustomStack<T>
{
    public T[] array = new T[50];
    public int pointer = 0;

    public void Push(T value)
    {
        if (pointer == array.Length)
        {
            T[] extendedArray = new T[array.Length * 2];
            for (int i = 0; i < array.Length; i++)
            {
                extendedArray[i] = array[i];
            }
            array = extendedArray;
        }
        array[pointer] = value;
        pointer++;
    }
    
    public T Pull()
    {
        if (pointer == 0)
        {
            throw new Exception("Stack is empty");
        }
        
        T value = array[pointer - 1];
        pointer--;
        return value;
    }
    public T Peek()
    {
        if (pointer == 0)
        {
            throw new Exception("Stack is empty");
        }
        
        T value = array[pointer - 1];
        return value;
    }
    public int Count()
    {
        return pointer;
    }
}