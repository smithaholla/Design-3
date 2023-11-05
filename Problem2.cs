public class LRUCache
    {
        // Time Complexity : RemoveNode - O(1), AddToHead - O(1), Get - O(1), Put - O(1), Remove from and add to hashmap is O(1)
        // Space Complexity : O(n) - exact is 2n where n is the capacity of the cache for hashmap and length of linked list
        // Did this code successfully run on Leetcode : Yes
        // Any problem you faced while coding this : No
        public class Node
        {
            public int key;
            public int val;
            public Node next;
            public Node prev;
            public Node(int key, int val)
            {
                this.key = key;
                this.val = val;
            }
        }

        Node head;
        Node tail;
        Dictionary<int, Node> map;
        int capacity;
        public LRUCache(int capacity)
        {
            this.capacity = capacity;
            head = new Node(-1, -1);
            tail = new Node(-1, -1);
            //initially head <-> tail
            head.next = tail;
            tail.prev = head;
            map = new Dictionary<int, Node>();
        }

        private void RemoveNode(Node node)
        {
            node.prev.next = node.next;
            node.next.prev = node.prev;
        }

        private void AddToHead(Node node)
        {
            node.next = head.next;
            node.prev = head;
            head.next = node;
            node.next.prev = node;
        }

        public int Get(int key)
        {
            if (!map.ContainsKey(key)) return -1;

            Node node = map[key];
            RemoveNode(node);
            AddToHead(node);
            return node.val;
        }

        public void Put(int key, int value)
        {
            if(map.ContainsKey(key))
            {
                Node node = map[key];
                node.val = value;
                RemoveNode(node);
                AddToHead(node);
            }
            else
            {
                if(capacity == map.Count)
                {
                    Node tailPrev = tail.prev;
                    RemoveNode(tailPrev);
                    map.Remove(tailPrev.key);

                }
                Node newNode = new Node(key, value);
                AddToHead(newNode);
                map.Add(key, newNode); 

            }
        }
    }
