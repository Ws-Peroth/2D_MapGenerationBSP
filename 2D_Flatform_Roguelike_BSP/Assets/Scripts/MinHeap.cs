using System;
using System.Collections.Generic;

public class MinHeap<T> where T : IComparable<T>
{ 
    private readonly List<T> heap = new List<T>();

    public int Count => heap.Count;

    public void Add(T node)
    {
        heap.Add(node);

        for (var i = heap.Count - 1; i > 0;)
        {
            var parent = (i - 1) / 2;

            if (heap[parent].CompareTo(heap[i]) > 0)  // heap[parent] > heap[i]
            {
                Swap(parent, i);
                i = parent;
            }
            else break;
        }
    }

    public T Remove()
    {
        if (heap.Count == 0)
        {
            throw new InvalidOperationException();
        }

        T root = heap[0];

        heap[0] = heap[heap.Count - 1];
        heap.RemoveAt(heap.Count - 1);

        var currentIndex = 0;
        var lastIndex = heap.Count - 1;

        while (currentIndex < lastIndex)
        {
            var child = currentIndex * 2 + 1;

            if (child < lastIndex && heap[child].CompareTo(heap[child + 1]) > 0)    // heap[child] > heap[child + 1]
            {
                child++;
            }

            if (child > lastIndex || heap[currentIndex].CompareTo(heap[child]) <= 0)   // heap[i] <= heap[child]
            {
                break;
            }

            Swap(currentIndex, child);
            currentIndex = child;
        }

        return root;
    }

    private void Swap(int indexA, int indexB)
    {
        (heap[indexA], heap[indexB]) = (heap[indexB], heap[indexA]);
    }
}
