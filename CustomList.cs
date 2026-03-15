namespace Calculator;
class CustomList<T>
{
    private T[] array = new T[10];
    private int pointer = 0;
    
    public CustomList(T[]? array = null)
    {
        if (array == null)
        {
            array = new T[10];
            pointer = 0;
        }
        else
        {
            foreach (T el in array)
            {
                Add(el);
            }    
        }
        
    }

    public void Add(T value)
    {
        array[pointer] = value;
        pointer++;
        if (pointer == array.Length)
        {
            T[] extendedArray = new T[array.Length * 2];
            for (int i = 0; i < array.Length; i++)
            {
                extendedArray[i] = array[i];
            }
            array = extendedArray;
        }
    }
    
    public void Remove(T value)
    {
        for (int i = 0; i < pointer; i++)
        {
            if (EqualityComparer<T>.Default.Equals(array[i], value))
            {
                for (var j = i; j < pointer - 1; j++)
                {
                    array[j] = array[j + 1];
                }

                pointer -= 1;
                break;
            }
        }
    }
    public void Remove(int index)
    {
        for (var j = index; j < pointer - 1; j++)
        {
            array[j] = array[j + 1];
        }
        pointer -= 1;
    }
    public T GetAt(int index)
    {
        return array[index];
    }
    public int IndexOf(T value)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (EqualityComparer<T>.Default.Equals(array[i], value))
            {
                return i;
            }
        }
        return -1;
    }
    public bool Contains(T value)
    {
        return IndexOf(value) != -1;
    }
    public int Length()
    {
        return pointer;
    }
}