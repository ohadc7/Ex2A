// ***********************************************************************
// Assembly         : SearchAlgorithmsLib
// Author           : Ohad and Ido
// Created          : 04-05-2017
//
// Last Modified By : IDO1
// Last Modified On : 04-21-2017
// ***********************************************************************
// <copyright file="PriorityQueue.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;

namespace SearchAlgorithmsLib
{
    //Credit to: http://stackoverflow.com/questions/102398/priority-queue-in-net
    /// <summary>
    /// priority queue that is implemented by minimum heap. 
    /// Credit to: http://stackoverflow.com/questions/102398/priority-queue-in-net (the most of the implementation is of them).
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PriorityQueue<T>
    {
        /// <summary>
        /// The comparer of the heap.
        /// </summary>
        private readonly IComparer<T> comparer;
        /// <summary>
        /// The heap
        /// </summary>
        private T[] heap;

        /// <summary>
        /// Initializes a new instance of the <see cref="PriorityQueue{T}"/> class with default values.
        /// </summary>
        public PriorityQueue() : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PriorityQueue{T}"/> class with default comparer.
        /// </summary>
        /// <param name="capacity">The capacity.</param>
        public PriorityQueue(int capacity) : this(capacity, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PriorityQueue{T}"/> class with default capacity 0f 16 and given comparer.
        /// </summary>
        /// <param name="comparer">The comparer.</param>
        public PriorityQueue(IComparer<T> comparer) : this(16, comparer)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PriorityQueue{T}"/> class.
        /// </summary>
        /// <param name="capacity">The initial capacity of the heap.</param>
        /// <param name="comparer">The comparer.</param>
        public PriorityQueue(int capacity, IComparer<T> comparer)
        {
            this.comparer = comparer == null ? Comparer<T>.Default : comparer;
            heap = new T[capacity];
        }

        /// <summary>
        /// Gets the count. number of elements that are currently in the heap.
        /// </summary>
        /// <value>The count.</value>
        public int Count { get; private set; }

        /// <summary>
        /// Pushes the specified value.
        /// </summary>
        /// <param name="v">The value.</param>
        public void push(T v)
        {
            if (Count >= heap.Length) Array.Resize(ref heap, Count * 2);
            heap[Count] = v;
            SiftUp(Count++);
        }

        /// <summary>
        /// Pops the preferred value (minimum).
        /// </summary>
        /// <returns>T.</returns>
        public T pop()
        {
            var v = top();
            heap[0] = heap[--Count];
            if (Count > 0) SiftDown(0);
            return v;
        }

        /// <summary>
        /// return the preferred value (minimum).
        /// </summary>
        /// <returns>T.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        public T top()
        {
            if (Count > 0) return heap[0];
            throw new InvalidOperationException("");
        }

        /// <summary>
        /// Sifts up.
        /// </summary>
        /// <param name="n">The n.</param>
        private void SiftUp(int n)
        {
            var v = heap[n];
            for (var n2 = n / 2; n > 0 && comparer.Compare(v, heap[n2]) < 0; n = n2, n2 /= 2) heap[n] = heap[n2];
            heap[n] = v;
        }

        /// <summary>
        /// Sifts down.
        /// </summary>
        /// <param name="n">The n.</param>
        private void SiftDown(int n)
        {
            var v = heap[n];
            for (var n2 = n * 2; n2 < Count; n = n2, n2 *= 2)
            {
                if (n2 + 1 < Count && comparer.Compare(heap[n2 + 1], heap[n2]) < 0) n2++;
                if (comparer.Compare(v, heap[n2]) <= 0) break;
                heap[n] = heap[n2];
            }
            heap[n] = v;
        }

        /// <summary>
        /// check whether the heap contains the element.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool DoesContain(T element)
        {
            return heap.Contains(element);
        }

        /// <summary>
        /// Decreases the key.
        /// </summary>
        /// <param name="oldElement">The old element.</param>
        /// <param name="newElement">The new element.</param>
        public void DecreaseKey(T oldElement, T newElement)
        {
            var indexOfElement = Array.IndexOf(heap, oldElement);
            heap[indexOfElement] = newElement;
            SiftUp(indexOfElement);
        }

        /// <summary>
        /// Tries to decrease the key of the element. replace the old element if the new element is better than the element that equals to it and is currently in the heap.
        /// </summary>
        /// <param name="element">The element.</param>
        public void TryToDecreaseTheKeyOfTheElement(T element)
        {
            var indexOfElement = Array.IndexOf(heap, element);
            if (comparer.Compare(element, heap[indexOfElement]) < 0)
            {
                heap[indexOfElement] = element;
                SiftUp(indexOfElement);
            }
        }
    }
}