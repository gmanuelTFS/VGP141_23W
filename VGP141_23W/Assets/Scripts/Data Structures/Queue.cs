using System.Collections.Generic;

namespace VGP141_23W
{
	public class Queue<T>
	{
		private LinkedList<T> data;

		public Queue()
        {
			data = new LinkedList<T>();
        }

		public void Enqueue(T value)
		{
			data.AddLast(value);
		}

		public void Dequeue()
		{
			data.RemoveFirst();
		}

		public T Peek() // (or top)
		{
			return data.First.Value;
		}

		public bool IsEmpty()
		{
			return data.Count == 0;
		}

		public int Size()
		{
			return data.Count;
		}
		
	}
}