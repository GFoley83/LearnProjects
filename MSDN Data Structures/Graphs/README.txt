The skmDataStructures folder in this ZIP contains a VS.NET 2003 solution that references the BTTester and SkipListTester projects as well.  So, to open the code for the SkipList class and the testing application, simply double-click the skmDataStructures.sln file in the skmDataStructures folder.

Note: If you are using VS.NET 2002, you will need to create a VS.NET 2002 solution and manually add in the projects...

Questions?  Comments?  Suggestions?  I can be reached at mitchell@4guysfromrolla.com



COMMENT ON DIJKSTRA'S ALGORITHM:
--------------------------------
The first line in the while loop of Dijkstra's Algorithm extracts the node from the set Q that has the minimum value in the distance table.  In the GraphTester application included in this download, the minimum Node is found by iterating through all of the elements in Q.  Let N be the number of nodes in the graph.  Therefore, Q will start off with N elements, then on the next iteration N-1, then N-2, and so on, down until there are no elements left.  Since there are N iterations in total, the total number of accesses needed is O(N^2).

Instead of modeling Q as an array, it can me modeled as a priority queue.  We haven't discussed priority queues in this article series - just realize that when dequeueing from a priority queue you'll always get the highest prioritized item.  For this problem we'd define highest prioritized item as the node with the smallest distance table value.  The good thing about priority queues is that when implemented using a binary heap, they have a dequeue running time and reoginization running time of lg N, where N is the number of elements in the queue.  Since changing the distance table values might change the structure of the priority queue, the number of times the lg N operation for reoginizing might occur is E times, where E is the number of edges.  (Since every edge is relaxed.)  Using this, we get a running time of: O(E * lg N).  This is great news if E << N^2, so using a binary heap priority queue is ideal of the graph is sparse.