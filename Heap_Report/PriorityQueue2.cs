using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Heap_Report
{
    public class PriorityQueue2<TElement>      // PriorityQueue는 데이터값과 우선순위 변수를 필요로 하는데 이중에
                                                // 우선순위는 기본적으로 int로 되어있어서 별도에 매개변수 넣지 않고 사용해도 문제없다
    {
        
        // PriorityQueue는 배열의 형태를 띄지만 노드를 사용하는 비선형적인 구조를 갖고있다
        // 그래서 노드를 구현하지만 머리, 꼬리 구현없이 한쪽만 구현하여 비선형적인 트리구조로 컨셉을 지키면서 구현해 줘야한다
        private struct Node                     // 노드에 데이터값과 우선순위 int형을 넣어준다
        {
            public TElement element;
            public int priority;
        }

        private List<Node> nodes;               // 배열을 이용해도 되지만 추가와 삭제에 제한없이 효율적인 List를 활용해준다
     
        public PriorityQueue2()
        {
            this.nodes = new List<Node>();      // 노드 초기화
        }

        // '추가' 구현
        // 힙상태가 이뤄진 트리구조에서 새로운 데이터를 추가할경우
        // 기존에 있는 데이터들과 우선순위를 비교해서 자리를 정렬해야하는데
        // 이 경우 전체 데이터들을 비교할 필요없이 구조상 바로 위 부모와 비교하는 과정을
        // 반복하여 올려주는 과정(승격과정)을 진행해줘서 힙상태를 유지하게 구현을 한다
        public void Enqueue(TElement element, int priority) 
        {
            Node newNode = new Node() { element = element, priority = priority }; // 변수에 대입하는 과정을 진행한다

            nodes.Add(newNode);
            int newNondeIndex = nodes.Count - 1;                 // 힙상태가 유지되는 중간에 추가는 불가능 하니
                                                                 // 배열 맨뒤에 추가를 진행한다 (맨처음도 추가는 불가능하다)

            while (newNondeIndex > 0)                            // 0이 최상단이어서 음수인 경우는 제외하도록 조건을 구현한다   
            {
                int parentIndex = GetParentIndex(newNondeIndex); // 새로운 데이터와 부모를 비교하도록 부모변수를 생성한다
                Node parentNode = nodes[parentIndex];            // 부모 위치와 값을 노드 변수화 한다

                if (newNode.priority < parentNode.priority)     // 자식이 부모 보다 우선순위가 높을경우(우선순위는 기본적 오름차순이기애 1->2의 형태로 1이 더 작은수여서 조건과 같이 표현된다
                {
                    nodes[newNondeIndex] = parentNode;          // 부모노드가 새로운 자리에 대입됨
                    nodes[parentIndex] = newNode;               // 새로운 노드가 부모자리에 대입됨
                    newNondeIndex = parentIndex;                // 부모값을 새로운 값으로 변경해주는 이유는 반복문이어서 
                                                                // 그위에 부모와 다시 비교하여야 하기 때문에 변수를 대입해준다
                }
                else                                            // 부모와 비교해서 자식이 더 우선순위가 낮은 경우 반복을 멈춤
                {
                    break;
                }
            }

        }
        // '삭제'구현
        // 삭제의 메커니즘은 최상단의 데이터를 맨뒤의 데이터를 교환하고 맨뒤의 크기를 삭제하는것으로 이뤄진다
        // 배열의 특성상 중간은 삭제가 불가능하고 맨앞 삭제도 삭제시 배열전체가 움직이는 가비지컬렉터에 부담가는 연산은
        // 회피하면서 맨뒤에 요소만 삭제하는 것으로 효율적인 비교적 효율적인 삭제가 가능하다
        public TElement Dequeue()
        {
            Node rootNode = nodes[0];                   // 최상단 노드위치

            Node lastNode = nodes[nodes.Count - 1];     // 가장 뒤에 노드 
            nodes[0] = lastNode;                        // 가장 뒤에 노드를 최상단에 위치 시킴
            nodes.RemoveAt(nodes.Count - 1);            // 가장뒤를 삭제

            // 최상단과 가장뒤 데이터를 교환하는 과정에서 힙상태가 무너진 결과를 복구하기위해
            // 추가적으로 최상단된 데이터와 자식데이터와 비교하여 정렬하는 과정이 필요하다
            int index = 0;                                          
            while (index < nodes.Count)                             // 배열 크기 만큼 반복을 진행해준다           
            {
                int leftChildIndex = GetLeftChildIndex(index);       // 왼쪽 자식
                int righttChildIndex = GetRightChildIndex(index);    // 오른쪽 자식

                // 부모가 자식 또는 자식둘과 비교하기 위해서 3가지 경우의수가 필요하다
                if (righttChildIndex < nodes.Count)                                                         // 배열 특성상 왼쪽->오른쪽으로 순서로 채워지기 때문에
                                                                                                            // 오른쪽에 데이터가 채워저 있다면 자식이 둘다 있는 경우로 본다
                {
                    int lessCildIndex = nodes[leftChildIndex].priority < nodes[righttChildIndex].priority   // 두 자식다 부모가 비교할 필요없이 두자식중 우선순위가 높은 자식과 비교하기만 하면 되는 형태로 구현
                                                                                                            // 두 자식의 우선순위를 비교하고 왼쪽이 더 우선순위가 높을경우(오름차순기준) 왼쪽자식으로 아닐경우 오르쪽자식을 변수에 대입한다
                        ? leftChildIndex : righttChildIndex;

                    if (nodes[lessCildIndex].priority < nodes[index].priority)                              // 부모와 자식의 비교로 자식이 더 높을경우 위치를 바꿔야한다
                    {
                        nodes[index] = nodes[lessCildIndex];                                                // 자식을 부모로 올리고
                        nodes[lessCildIndex] = lastNode;                                                    // 부모에 있던 새로운 노드를 자식위치에 내린다
                        index = lessCildIndex;                                                              // 다음 자식들과 비교하기위해 변수를 바꿔준다
                    }
                    else                                                                                    // 부모가 더 높을경우 반복을 중지한다
                    {
                        break;
                    }
                }
                else if (leftChildIndex < nodes.Count)                                                      // 오른쪽 자식에 데이터가 없다면 자동적으로 해당 조건문이 작동한다
                {
                    if (nodes[leftChildIndex].priority < nodes[index].priority)                             // 왼쪽의 자식만 비교하면 되서 위 조건문과 같은 연산을 행해준다
                    {
                        nodes[index] = nodes[leftChildIndex];
                        nodes[leftChildIndex] = lastNode;
                        index = leftChildIndex;
                    }
                    else            
                    {
                        break;
                    }
                }
                else                                                                                        // 자식이 둘다 없을경우 비교대상이 없기때문에 반복을 종료시켜준다
                {
                    break;         
                }

            }
            return rootNode.element;                                                                        // 해당 값을 반환한다
        }

        // 맨뒤 데이터 삭제
        public TElement DequeueLast()
        {
            Node lastNode = nodes[nodes.Count - 1];
            nodes.RemoveAt(nodes.Count - 1);
            return lastNode.element;
        }

        // 최상단의 데이터 호출 함수구현
        public TElement Peek()
        {
            return nodes[0].element;                        // 최상단은 0이기애 0에 데이터값을 반환한다
        }

        private int GetParentIndex(int childIndex)          // 부모의 위치구현
        {
            return (childIndex - 1) / 2;                    // 현재 만드는 구조는 이진트리로 부모는 자식2를
                                                            // 갖고있는 형태로 자식의 1에 2를 나눈자리가 곧 해당 부모의 자리이다
                                                            // ex. 부모(2) == 자식(5), 자식(6) == 5-1/2 = 2 or 6-1/2(int이기애 소수점은 제외된다)
        }

        private int GetLeftChildIndex(int parentIndex)      // 왼쪽 자식의 위치 확인 (왼쪽은 홀수)
                                                            // ex. 부모(2)- 자식(5), 자식(6)
        {
            return parentIndex * 2 + 1;
        }
        private int GetRightChildIndex(int parentIndex)     // 오른쪽 자식의 위치 확인 (오른쪽은 짝수)
                                                            // ex. 부모(2)- 자식(5), 자식(6)
        {
            return parentIndex * 2 + 2;
        }

    }
}
