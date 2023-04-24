using System.Collections.Generic;

namespace Heap_Report
{
    internal class Program
    {

        static void Main(string[] args)
        {
			// 우선순위큐는 기본적으로 오름차순으로 정렬한다
            PriorityQueue<string, int> priorityQueue = new PriorityQueue<string, int>();    // 구성 : <데이터 자료형/우선순위 자료형>

            priorityQueue.Enqueue("데이터1", 1);     
            priorityQueue.Enqueue("데이터2", 3);
            priorityQueue.Enqueue("데이터3", 5);
            priorityQueue.Enqueue("데이터4", 2);
            priorityQueue.Enqueue("데이터5", 4);

            while (priorityQueue.Count > 0) Console.WriteLine(priorityQueue.Dequeue());

        }
    }
}