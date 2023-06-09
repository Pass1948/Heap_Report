﻿using System.Collections.Generic;

namespace Heap_Report
{
    internal class Program
    {
        /******************************************************
		 * 트리구조
		 * 선형구조을 가진 배열에서 우선순위를 설정하면 O(N)만큼의 효율도가 나오는걸 확인할수있는데 이경우 C#에서는 가비지 컬렉터에 부담이 가는 
		 * 효율성을 가진 연산을 진행하여 우선순위를 결정해준다
		 * 그래서 우선순위에 특화된 구조를 발견하여 그에 맞게 배열을 대치시키는데 이 구조를 트리구조라 부른다
		 * 트리구조는 데이터+노드의 형태로 데이터가 할당저장되고
		 * 부모-자식의 세대 형태로 표현이 되는데 최상단 0(부모)로 부터 1 부터 많게는 6까지 한 부모에 자식을 삼는게 가능한대
		 * 우선순위큐에서는 한부모에 두자식을 갖는 이진트리구조를 많이 갖게 된다 
		 * 이 경우 부모는 자식보단 항상 높은 우선순위를 유지하는걸 힙(Heap)상태라고 부르며 
		 * 전세대에 자리가 다 차야 다음세대를 받을 수 있다는 구조를 완전이진트리라 부른다
		 * 
		 * 힙(Heap)
		 * 힙상태를 이룬 이진트리에서는 탐색을 진행할 경우 해당 노드만 입력하여 찾는 연산 한번만 진행하면 되는 O(1)의 효율성을 갖게 되는데
		 * 그럼 추가와 삭제의 경우는 얼만큼의 효율이 나오는지 확인한다면
		 * 우선 추가는 배열의 특성으로 만들어진 트리구조는 중간에 삽입이 힙상태를 부수거나 더욱 연산이 많이 들어가기에 불가능하며 배열맨뒤에 새롭게 추가하여
		 * 바로위 부모와 비교하여 자리를 바꾸는 연산을 반복하여 정렬된다 이경우 다른 자식과 비교하지 않아도 힙상태를 생각하여 부모의 우선순위만 비교하는것으로 충분하다
		 * 삭제의 경우 추가때 처럼 중간에서 이뤄지는일은 불가능하여 맨앞과 맨뒤요소만 삭제하는식으로 이뤄지는데
		 * 만약 맨앞에 삭제가 이뤄질 경우 맨뒤 삭제와 다르게 삭제된 자리에 뒤에 요소들이 이동하는 연산이 들어가지만 이경우 비효율적이며 
		 * 복사-불여넣기 형태로 c#은 가비지컬렉터의 부담을 주는 연산이다
		 * 그래서 다른 식의 연산으로 이뤄저야하는데
		 * 우선 맨앞과 맨뒤의 값을 변경하여 맨뒤를 삭제한뒤 최상단이된 값을 바로 밑 자식 또는 자식들과 비교하여 바꿔는 반복연산으로 위치를 정렬시켜줘야한다
		 * 추가와 삭제는 부모와 자식을 비교하는 연산이 추가되어 구조상 한 부모-자식 관계에서 연산이 이뤄지고 다른 부모-자식들 관계에서는 연산이 이뤄지지 않기애
		 * helf->helf&helf식의 연산구조로 이뤄서 추가와 삭제의 효율성을 O(logN)으로 표현된다
		 ******************************************************/


        static void Main(string[] args)
        {
            // 우선순위큐는 기본적으로 오름차순으로 정렬한다
            PriorityQueue<string, int> priorityQueue = new PriorityQueue<string,int>();    // 구성 : <데이터 자료형/우선순위 자료형>
            PriorityQueue2<string> priorityQueue2 = new PriorityQueue2<string>();		   // 

            priorityQueue.Enqueue("데이터1", 1);     
            priorityQueue.Enqueue("데이터2", 3);
            priorityQueue.Enqueue("데이터3", 5);
            priorityQueue.Enqueue("데이터4", 2);
            priorityQueue.Enqueue("데이터5", 4);
            priorityQueue.Dequeue();

            while (priorityQueue.Count > 0) Console.WriteLine(priorityQueue.Dequeue());
            
            priorityQueue2.Enqueue("데이터1", 1);
            priorityQueue2.Enqueue("데이터2", 3);
            priorityQueue2.Enqueue("데이터3", 5);
            priorityQueue2.Enqueue("데이터4", 2);
            priorityQueue2.Enqueue("데이터5", 4);
            priorityQueue2.DequeueLast();
        }
    }
}