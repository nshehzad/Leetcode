using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Collections.Specialized;


namespace ConsoleApp2

{
    ///traverse postOrder tree  https://www.youtube.com/watch?v=sMI4RBEZyZ4
    //https://leetcode.com/problems/best-time-to-buy-and-sell-stock-ii/

    //public int maxProfit(int[] prices)
    //{
    
    //    int maxprofit = 0;
    //    for (int i = 1; i < prices.length; i++)
    //    {
    //        if (prices[i] > prices[i - 1])
    //            maxprofit += prices[i] - prices[i - 1];
    //    }
    //    return maxprofit;
    //}
    //most frequently asked question facebook
    ////https://www.youtube.com/watch?v=jZBnFxIe4Y8
    ///Candidate celeberity question: https://www.youtube.com/watch?v=LZJBZEnoYLQ
    ///adding binaries: https://www.youtube.com/watch?v=tRpusgdZxrE
    /// 
    ///https://www.youtube.com/watch?v=zLcNwcR6yO4
    ///https://www.youtube.com/watch?v=zLcNwcR6yO4 - Merge K sorted list
    ///https://leetcode.com/problems/longest-substring-without-repeating-characters/
    ///


    public class TreeNode
    {
        public TreeNode(int NodeVal) {
            this.val = NodeVal;
        }
        public int val;
        public TreeNode right;
        public TreeNode left;
    }

    //public class ListNode
    //{
    //    public ListNode(int NodeVal) 
    //    {
    //         this.val = NodeVal;
    //    }
    //    public ListNode()
    //    {
             
    //    }
    //    public int val;
    //    public ListNode next;
  
    //}

    public class ListNode
    {
        public int val;
        public ListNode next;
        
     
        public ListNode(int val = 0, ListNode next = null)
        {
            string r = "test";
            
             
            this.val = val;
            this.next = next;
        }
    }


    class BuildTreeClass

    {
        //https://leetcode.com/problems/construct-binary-tree-from-inorder-and-postorder-traversal/
        //Given two integer arrays inorder and postorder where inorder is the inorder traversal of a binary tree and postorder is the postorder traversal of the same tree, construct and return the binary tree.
        //Input: inorder = [9,3,15,20,7], postorder = [9,15,7,20,3]
        //Output: [3,9,20,null,null,15,7]

        //post order goes like left node right node, then parent, then left right, parent
        //in order left parent, right, parent, left right parent
        // https://www.geeksforgeeks.org/tree-traversals-inorder-preorder-and-postorder/
        //https://www.youtube.com/watch?v=WLvU5EQVZqY
        //https://leetcode.com/problems/construct-binary-tree-from-inorder-and-postorder-traversal/discuss/1160542/Easy-Java-solution-for-both-question-105-and-106
        // https://www.youtube.com/watch?v=TblhNX9AQ3M
        //int postorderIdx = 0;
 


        //https://www.youtube.com/watch?v=TblhNX9AQ3M
        //Input: inorder = [9,3,15,20,7], postorder = [9,15,7,20,3]

        //        Input: lists = [[1,4,5],[1,3,4],[2,6]]
        //Output: [1,1,2,3,4,4,5,6]
        public ListNode MergeKLists(ListNode[] lists)
        {
            if (lists.Length == 0)
            {
                return null;
            }
            List<int> numbers = new List<int>();
            ListNode current;
            foreach (var item in lists)
            {
                current = item; //go through each current Nodes, have a pointer to each node
                while (current != null)
                {
                    numbers.Add(current.val);
                    current = current.next;
                }
            }
            numbers.Sort();  //sort it.
            ListNode result = new ListNode();
            ListNode head = result;  
           
            foreach (var num in numbers) //go through all of those 
            {
                result.next = new ListNode(num);
                result = result.next;
            }
            return head.next;
        }
 
        public virtual TreeNode buildInOrderTree(int[] inorder, int start,
                              int end, TreeNode node)
        {
            if (start > end)
            {
                return null;
            }

            /* Find index of the maximum 
            element from Binary Tree */
            int i = max(inorder, start, end);

            /* Pick the maximum value and
            make it root */
            node = new TreeNode(inorder[i]);

            /* If this is the only element in 
            inorder[start..end], then return it */
            if (start == end)
            {
                return node;
            }

            /* Using index in Inorder traversal, 
            construct left and right subtress */
            node.left = buildInOrderTree(inorder, start,
                                  i - 1, node.left);
            node.right = buildInOrderTree(inorder, i + 1,
                                   end, node.right);

            return node;
        }

        /* UTILITY FUNCTIONS */

        /* Function to find index of the
        maximum value in arr[start...end] */
        public virtual int max(int[] arr,
                               int strt, int end)
        {
            int i, max = arr[strt], maxind = strt;
            for (i = strt + 1; i <= end; i++)
            {
                if (arr[i] > max)
                {
                    max = arr[i];
                    maxind = i;
                }
            }
            return maxind;
        }

        /* This funtcion is here just 
           to test buildTree() */
        public virtual void printInorder(TreeNode node)
        {
            if (node == null)
            {
                return;
            }

            /* first recur on left child */
            printInorder(node.left);

            /* then print the data of node */
            Console.Write(node.val + " ");

            /* now recur on right child */
            printInorder(node.right);
        }

                          //rootindex                      //index
        //Input: inorder = [9,3,15,20,7], postorder = [9,15,7,20,3]

        //Output: [3,9,20,null,null,15,7]
        public TreeNode buildTree(int[] inorder, int[] postorder){
            return constructTreeFromInPost(inorder, postorder, 0, inorder.Length - 1, postorder.Length - 1);
        }

        private TreeNode constructTreeFromInPost(int[] inorder, int[] postorder, int start, int end, int index)
        {
            //index is where to find postorder index
            if (start > end) return null;
            TreeNode root = new TreeNode(postorder[index]); // search through post tree 3, 20, then 7 index is for 3, 20, 7

            int inRootIndex = start; //start of preOrder tree
            while (postorder[index] != inorder[inRootIndex]) inRootIndex++; //found root index, which becomes start

            root.right = constructTreeFromInPost(inorder, postorder, inRootIndex + 1, end, index - 1); //end is preorder length so right of root is right tree, left of tree is left tree
            
            //end - inRootIndex gives right tree values. Subtract this from Index
            //think of it as firt iteration
            root.left = constructTreeFromInPost(inorder, postorder, start, inRootIndex - 1, index - (end - inRootIndex) - 1); 

            return root;
        
        }


        //public TreeNode BuildTree(int[] inorder, int[] postorder)
        //{
        //    if (postorder.Length == 0) return null;
        //    TreeNode root = new TreeNode(postorder[postorder.Length - 1]);
        //    int index = 0;
        //    while (inorder[index] != root.val) index++;
        //    root.left = Helper(inorder, 0, index, postorder, index - 1);
        //    root.right = Helper(inorder, index + 1, inorder.Length, postorder, inorder.Length - 2);
        //    return root;
        //}
        //private TreeNode Helper(int[] inorder, int inStart, int inEnd, int[] postorder, int postEnd)
        //{
        //    if (inStart >= inEnd) return null;
        //    TreeNode root = new TreeNode(postorder[postEnd]);
        //    int index = -1;
        //    for (int i = inStart; i < inEnd; i++)
        //    {
        //        if (inorder[i] == root.val)
        //        {
        //            index = i;
        //            break;
        //        }
        //    }
        //    root.left = Helper(inorder, inStart, index, postorder, postEnd - inEnd + index);
        //    root.right = Helper(inorder, index + 1, inEnd, postorder, postEnd - 1);
        //    return root;
        //}

    }

    class node
    {
 
        //serialize and deserialize nodes
        //idea is to add all the nodes to the Queue recursively as going through, and process one at a time.
        public string Serialize(TreeNode root)
        {
            var queue = new Queue<TreeNode>();
            var result = new List<string>();

            queue.Enqueue(root);

            while (queue.Count > 0)
            {
                var actual = queue.Dequeue();

                result.Add(actual == null ? "null" : actual.val.ToString());

                if (actual != null)
                {
                    queue.Enqueue(actual.left);
                }

                if (actual != null)
                {
                    queue.Enqueue(actual.right);
                }
            }

            return string.Join(',', result);
        }

//Given an array of intervals where intervals[i] = [starti, endi], merge all overlapping intervals,
//and return an array of the non-overlapping intervals that cover all the intervals in the input.

////Example 1:

////Input: intervals = [[1, 3], [2,6], [8,10], [15,18]]
////Output: [[1,6],[8,10],[15,18]]
//Explanation: Since intervals[1, 3] and[2, 6] overlaps, merge them into[1, 6].
        public int[][] Merge(int[][] intervals)
        {
            intervals = intervals.OrderBy(x => x[0]).ToArray();

            List<int[]> output = new List<int[]>();

            output.Add(new int[] { intervals[0][0], intervals[0][1] });

            for (int i = 1; i < intervals.Length; i++)
            {
                if (output.Last()[1] >= intervals[i][0]) //if last eleement second item is greater than first item of next
                {
                    output.Last()[1] = Math.Max(output.Last()[1], intervals[i][1]);
                }
                else
                {
                    output.Add(new int[] { intervals[i][0], intervals[i][1] });
                }
            }

            return output.ToArray();
        }

        // Decodes your encoded data to tree.
        public TreeNode Deserialize(string data)
        {
            var encoded = data.Split(',').ToArray();

            if (encoded.Length < 1 || encoded[0] == "null")
            {
                return null;
            }

            var head = new TreeNode(int.Parse(encoded[0].ToString()));
            var actual = head;
            var queue = new Queue<TreeNode>();

            int i = 1;
            //left nodes are added first to Queue, and then get processed.
            while (i < encoded.Length)
            {
                if (actual != null)
                {
                    if (i < encoded.Length && actual != null)
                    {
                        var val = encoded[i].ToString();
                        TreeNode left = null;

                        if (val != "null")
                        {
                            left = new TreeNode(int.Parse(val));
                        }

                        actual.left = left;
                        queue.Enqueue(left);
                    }

                    i++;

                    if (i < encoded.Length && actual != null)
                    {
                        var val = encoded[i].ToString();
                        TreeNode right = null;

                        if (val != "null")
                        {
                            right = new TreeNode(int.Parse(val));
                        }

                        actual.right = right;
                        queue.Enqueue(right);
                    }
                    i++;
                }
                actual = queue.Dequeue();
            }

            return head;
        }

    }
    //https://leetcode.com/problems/find-k-closest-elements/
    //https://leetcode.com/problems/k-closest-points-to-origin/

    //int GetDistance(int[] points)
    //{
    //    return (int)points[0] * points[0] + points[1] * points[1];
    //}

    //public int[][] KClosest(int[][] points, int k)
    //{
    //    Array.Sort(points, (x, y) =>
    //    {
    //        return GetDistance(x).CompareTo(GetDistance(y));
    //    });
    //    int[][] output = new int[k][];
    //    Array.ConstrainedCopy(points, 0, output, 0, k);
    //    return output;
    //}

    //    public static int[][] KClosest(int[][] points, int k)
    //    {
    //        int[] dists = new int[points.Length];
    //        for (int i = 0; i < points.Length; i++)
    //            dists[i] = Distance(points[i]);

    //        Array.Sort(dists);
    //        int distK = dists[k - 1];

    //        int[][] output = new int[k][];
    //        int j = 0;
    //        for (int i = 0; i < points.Length; i++)
    //        {
    //            if (Distance(points[i]) <= distK)
    //            {
    //                output[j] = points[i];
    //                j++;
    //            }
    //        }

    //        return output;
    //    }

    //    public static int Distance(int[] points)
    //    {
    //        return (int)points[0] * points[0] + points[1] * points[1];
    //    }
    //}
    //class Solution
    //{
    //    public int maxProduct(int[] nums)
    //    {
    //        if (nums.length == 0) return 0;

    //        int result = nums[0];

    //        for (int i = 0; i < nums.length; i++)
    //        {
    //            int accu = 1;
    //            for (int j = i; j < nums.length; j++)
    //            {
    //                accu *= nums[j];
    //                result = Math.max(result, accu);
    //            }
    //        }

    //        return result;
    //    }
    //}
    //public int[] twoSum(int[] nums, int target)
    //{
    //    Map<Integer, Integer> map = new HashMap<>();
    //    for (int i = 0; i < nums.length; i++)
    //    {
    //        int complement = target - nums[i];
    //        if (map.containsKey(complement))
    //        {
    //            return new int[] { map.get(complement), i };
    //        }
    //        map.put(nums[i], i);
    //    }
    //    throw new IllegalArgumentException("No two sum solution");
    //}
    static class Program

        //approach: save order of dictionary in array
        //split to array, and then have a fuction that goes through each word, and compare
    {
//        Input: words = ["hello","leetcode"], order = "hlabcdefgijkmnopqrstuvwxyz"
//Output: true
//Explanation: As 'h' comes before 'l' in this language, then the sequence is sorted.
//Example 2:

//Input: words = ["word","world","row"], order = "worldabcefghijkmnpqstuvxyz"
//Output: false
//Explanation: As 'd' comes after 'l' in this language, then words[0] > words[1], hence the sequence is unsorted.
        //public boolean isAlienSorted(String[] words, String order)
        //{
        //    int[] arr = new int[26];
        //    for (int i = 0; i < order.length(); i++)
        //    {
        //        arr[order.charAt(i) - 'a'] = i;
        //    }
        //    boolean flag = true;
        //    for (int i = 0; i < words.length - 1; i++)
        //    {
        //        flag = compare(words[i], words[i + 1], arr);
        //        if (flag == false)
        //            break;
        //    }

        //    return flag;
        //}
        //public boolean compare(String str1, String str2, int[] arr)
        //{
        //    for (int i = 0; i < str1.length(); i++)
        //    {
        //        if (i + 1 > str2.length())
        //            return false;

        //        else if (arr[str1.charAt(i) - 'a'] > arr[str2.charAt(i) - 'a'])
        //            return false;
        //        else if (arr[str1.charAt(i) - 'a'] == arr[str2.charAt(i) - 'a'])
        //            continue;
        //        else if (arr[str1.charAt(i) - 'a'] < arr[str2.charAt(i) - 'a'])
        //            return true;
        //    }
        //    return true;
        //}

        //        Given an array of integers nums and an integer k, return the total number of continuous subarrays whose sum equals to k.
        //Example 1:

        //Input: nums = [1,1,1], k = 2
        //Output: 2
        //Example 2:

        //Input: nums = [1,2,3], k = 3
        //Output: 2


        //serialize/deserialize the tree.


        public static int subarraySum(int[] nums, int k)
        {
           

            int count = 0;
            for (int start = 0; start < nums.Length; start++)
            {
                int sum = 0;
                for (int end = start; end < nums.Length; end++)
                {
                    sum += nums[end];
                    if (sum == k)
                        count++;
                }
            }
            return count;
        }


        //input = "abcd"
        //MainString = "defabcefh"; 
        //returns true;
        public static bool FindSubstring(string input, string MainString)
        {
            //My approach is split input into array and loop through it
            //Split MainString into array and loop through it
            //If match found, then remember the index and skip the loop//go to the next element
            //if no match found, use the rememeberd index to start again

            char[] arrInput = input.ToCharArray();
            char[] arrMain = MainString.ToCharArray();
            bool nextIndex = false;
            char c;
            int matchingIndex = 0;
            char chr;
            HashSet<char> chars = new HashSet<char>();

            for (int i = 0; i <= arrInput.Length; i++)// c in arrInput) //a
            {
                if (matchingIndex == arrMain.Length -1)
                     return false;
                c = arrInput[i];
                nextIndex = false;
               
                for (int j=matchingIndex;(j<arrMain.Length && !nextIndex);j++)
                {
                    chr = arrMain[j];
                    if (c.Equals(chr) && chars.Count < arrInput.Length) 
                    {
                        chars.Add(chr);
                        nextIndex = true;
                        matchingIndex =j+1;

                        if (i == arrInput.Length - 1)
                            return true;
                    }
                    else 
                    {
                        if (chars.Count > 0)
                        {
                            i = -1;
                            matchingIndex++;
                            nextIndex = true;
                        }
                    }
                }
            }
            return false;
         }


        public static int subarraySumDict(int[] nums, int k)
        {
            int count = 0, sum = 0;
            Dictionary<int, int> map = new Dictionary<int, int>();
            map.Add(0, 1);
            for (int i = 0; i < nums.Length; i++)
            {
                sum += nums[i];
                if (map.ContainsKey(sum - k))
                    count += map[sum - k];
                map.Add(sum, map.GetValueOrDefault(sum, 0) + 1);
            }
            return count;
        }
        // Minimum Remove to Make Valid Parentheses
        //        Input: s = "lee(t(c)o)de)"
        //Output: "lee(t(c)o)de"
        //algorithm: loop through the string, use string builder, and stack, remove from stack if valid paran found, find the pop
        public static string MinRemoveToMakeValid(string s)
        {
            if (s == null || s.Length == 1) return "";

            StringBuilder ans = new StringBuilder();  // The key to the time limit issue is to use the StringBuilder insdead of string
            Stack<int> stack = new Stack<int>();

            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '(')
                {
                    ans.Append(s[i]); //add (
                    stack.Push(ans.Length - 1);  //push location
                }
                else if (s[i] == ')') 
                {
                    if (stack.Count  > 0)
                    {
                        stack.Pop();  //remove location
                        ans.Append(s[i]); //append )
                    }
                }
                else
                {
                    ans.Append(s[i]); 
                }
            }

            while (stack.Count > 0)
            {
                int index = stack.Pop(); //find the locaiont of orphan ( and remove it
                ans = ans.Remove(index, 1); 
            }

            return ans.ToString();
        }

        //int[][] intervals = {
        //                new int[2]{15,20},
        //                new int[2]{5,10},
        //                new int[2]{0,30} 

        // };



        //minMeetingRooms(intervals);
        public static int minMeetingRooms(int[][] intervals)
        {
            if (intervals == null || intervals.Length == 0) return 0;

            int[] start = new int[intervals.Length];
            int[] end = new int[intervals.Length];

            for (int i = 0; i < intervals.Length; i++)
            {
                start[i] = intervals[i][0];
                end[i] = intervals[i][1];
            }

            Array.Sort(start);
            Array.Sort(end);

            int count = 1;
            int j = 0;
            for (int i = 1; i < intervals.Length; i++)
            {
                //Compare next to the closest end (Since it is sorted)
                //if conflict then we need a new room so count++, 
                //if no conflict then update/replace closest end with the next one
                if (start[i] < end[j])
                {
                    count++;
                }
                else
                {
                    j++;
                }
            }

            return count;
        }




        //public int[][] insert(int[][] intervals, int[] newInterval)
        //{
        //    // init data
        //    int newStart = newInterval[0], newEnd = newInterval[1];
        //    int idx = 0, n = intervals.length;
        //    LinkedList<int[]> output = new LinkedList<int[]>();

        //    // add all intervals starting before newInterval
        //    while (idx < n && newStart > intervals[idx][0])
        //        output.add(intervals[idx++]);

        //    // add newInterval
        //    int[] interval = new int[2];
        //    // if there is no overlap, just add the interval
        //    if (output.isEmpty() || output.getLast()[1] < newStart)
        //        output.add(newInterval);
        //    // if there is an overlap, merge with the last interval
        //    else
        //    {
        //        interval = output.removeLast();
        //        interval[1] = Math.max(interval[1], newEnd);
        //        output.add(interval);
        //    }

        //    // add next intervals, merge with newInterval if needed
        //    while (idx < n)
        //    {
        //        interval = intervals[idx++];
        //        int start = interval[0], end = interval[1];
        //        // if there is no overlap, just add an interval
        //        if (output.getLast()[1] < start) output.add(interval);
        //        // if there is an overlap, merge with the last interval
        //        else
        //        {
        //            interval = output.removeLast();
        //            interval[1] = Math.max(interval[1], end);
        //            output.add(interval);
        //        }
        //    }
        //    return output.toArray(new int[output.size()][2]);
        //}

        //        Input: s = "abcabcbb"
        //Output: 3
        //Explanation: The answer is "abc", with the length of 3.
        //abcdbcef
        //https://leetcode.com/problems/longest-substring-without-repeating-characters/

        //public int lengthOfLongestSubstring(String s)
        //{
        //    Integer[] chars = new Integer[128];

        //    int left = 0;
        //    int right = 0;

        //    int res = 0;
        //    while (right < s.length())
        //    {
        //        char r = s.charAt(right);  //add it to hashmap, char, int 

        //        Integer index = chars[r];
        //        if (index != null && index >= left && index < right) //it means it's already already exists have index.
        //        {
        //            left = index + 1;
        //        }

        //        res = Math.max(res, right - left + 1);

        //        chars[r] = right;
        //        right++;
        //    }

        //    return res;
        //}

        //Algorithm, go through string one char at a time, if 
        //find the index in the array of if it's not null
        // set the value to right
        //get that value and if the value is left or greter than left 

        //ublic class Solution
        //{
        //        Given a set of non-overlapping intervals, insert a new interval into the intervals(merge if necessary).

        //You may assume that the intervals were initially sorted according to their start times.


        //Input: intervals = [[1, 3], [6,9]], newInterval = [2,5]
        //        Output: [[1,5],[6,9]]
        public static int[][] insert(int[][] intervals, int[] newInterval)
        {
            // init data
            int newStart = newInterval[0], newEnd = newInterval[1]; //get start and end data
            int i = 0, n = intervals.Length;
            List<int[]> output = new List<int[]>();

            // add all intervals before newInterval
            while (i < n && intervals[i][1] < newStart)
                output.Add(intervals[i++]); //all intervals before new

            // merge newInterval
           ///int[] interval = new int[2];
            while (i < n && intervals[i][0] <= newEnd)
            {
                newStart = Math.Min(newStart, intervals[i][0]);
                newEnd = Math.Max(newEnd, intervals[i][1]);
                ++i;
            }
            output.Add(new int[] { newStart, newEnd });

            // add all intervals after newInterval  
            while (i < n)
                output.Add(intervals[i++]);

            return output.ToArray();
        }
 


//Given a string s, find the length of the longest substring without repeating characters.
//Example 1:

//Input: s = "abcabcbb"
//Output: 3
//Explanation: The answer is "abc", with the length of 3.

        //have right and left pointers
        //get chars index of all chars/integers and store the right index in that array
        //get index value if it's greater than left or equal, increment left
        public static int lengthOfLongestSubstring(String s)
        {
            int[] chars = new int[128];

            int left = 0;
            int right = 0;

            int res = 0;
            while (right < s.Length)
            {
                char r = s[right]; //a, b, c, d, it has a value of this is the referenced

                int? index = chars[r]; //returns the value which is right value
                if (index != null && index >= left && index < right) //it's already there
                {
                    left = index.Value + 1;
                }

                res = Math.Max(res, right - left + 1);

                chars[r] = right; //find the index and then specify the value to the right, r represents a numeric index value upto 128
                right++;
            }

            return res;
        }

        //https://leetcode.com/problems/find-all-anagrams-in-a-string/
        //Input: s = "cbaebabacd", p = "abc"
        //out 0, 6
        //Find All Anagrams in a String
        //public static IList<int> FindAnagrams(string s, string p)
        //{


        //    IList<int> result = new List<int>();
        //    int left = 0, sLen = s.Length, pLen = p.Length; //get lenght of both
        //    int[] alpha = new int[26];
        //    foreach (var c in p) alpha[c - 'a']++;
        //    int[] track = new int[26];
        //    for (int right = 0; right < sLen; right++)
        //    {
        //        track[s[right] - 'a']++;
        //        if (right - left + 1 == pLen)
        //        {
        //            bool flag = true;
        //            for (int i = 0; i < 26; i++)
        //            {aster
        //                if (track[i] != alpha[i])
        //                {
        //                    flag = false;
        //                    break;
        //                }
        //            }
        //            if (flag) result.Add(left);
        //            track[s[left++] - 'a']--;
        //        }
        //    }
        //    return result;
        //}
        //public int lengthOfLongestSubstring(String s)
        //{
        //    int n = s.length(), ans = 0;
        //    Map<Character, Integer> map = new HashMap<>(); // current index of character
        //                                                   // try to extend the range [i, j]
        //    for (int j = 0, i = 0; j < n; j++)
        //    {
        //        if (map.containsKey(s.charAt(j))) 
        //        {
        //            i = Math.max(map.get(s.charAt(j)), i);
        //        }
        //        ans = Math.max(ans, j - i + 1);
        //        map.put(s.charAt(j), j + 1);
        //    }
        //    return ans;
        //}

        //Input: s = "cbaebabacd", p = "abc"
        //out cba, bac
        //Find All Anagrams in a String
        // public static IList<int> FindAnagrams
        //Algorithm
        //have an array of 26 values but p string 
        //have an array of 26 values but s string
        //stores all char values of p string as integ reference in p array
        //loop through the s string and store integer reference in s array
        //like sCount[(int)s[right] - 'a']]++;
        //compare arrays and if equal get position by right -  p lenght + 1
        //if lenght of s is greater or equal to lenght of p, subtract/remove the most left index like sCount(s[left-pcount] -'a')]--;
        public static List<int> findAnagrams(String s, String p)
        {
            int ns = s.Length, np = p.Length;
            if (ns < np) return new List<int>();

            int[] pCount = new int[26];
            int[] sCount = new int[26];
            // build reference array using string p
            //for (char ch : p.toCharArray())
            //{
            //    pCount[(int)(ch - 'a')]++;
            //}

            foreach (var c in p) pCount[c - 'a']++;   //increment the value of that char a is 1, b is 2, d = 1

            List<int> output = new List<int>();
            // sliding window on the string s
            for (int i = 0; i < ns; ++i) //array will keep all 3 elements at one time.
            {
                // add one more letter 
                // on the right side of the window
 
                sCount[(int)(s[i]- 'a')]++; //store the values from s array as well c is 1, d - 1, j - 1
                // remove one letter 
                // from the left side of the window

                if (i >= np)  //if right is bigger than or equal to string to find, remove most left 
                {
                    sCount[(int)(s[i - np] - 'a')]--;  //so you only have those which has the value 
                }
                // compare array in the sliding window
                // with the reference array
                //i (Arr.equals(pCount, sCount))
               if  (sCount.SequenceEqual(pCount)) //comparing both arrays.
                {
                    output.Add(i - np + 1);  //you'll go through the last eleement so must subtract -np
                }
            }
            return output;
        }


        public static int resultStartIndex = 0;
        public static int resultLength = 0;
        public static string LongestPalindrome(string s)
        {
            if (s.Length == 1)
            {
                return s;
            }

            for (int start = 0; start < s.Length - 1; start++)
            {
                 extend(s, start, start);
                 extend(s, start, start + 1);
            }

            return s.Substring(resultStartIndex, resultLength);
        }

        public static void extend(string s, int right, int left)
        {
            while (right < s.Length && left >= 0 && s.Substring(right, 1).Equals(s.Substring(left, 1)))
            {
                right++;
                left--;
            }

            if (resultLength < right - left - 1)
            {
                resultStartIndex = left + 1;
                resultLength = right - left - 1;

               
            }
        }



        //        The idea is to start from the middle and expand the search, by moving the left and right pointers.For every index we search to check if it's a palidrome of even length or odd length. For odd length we are keeping the left and right pointer the same and for even we are increasing the right pointer by 1.

        //class Solution
        //        {
        //            int resultStartIndex;
        //            int resultLength;
        //            public String longestPalindrome(String s)
        //            {
        //                int len = s.length();
        //                if (len < 2) return s;

        //                for (int start = 0; start < len; start++)
        //                {
        //                    // searching for palindrome with even length
        //                    expandSearch(s, start, start);

        //                    // searching for palindrome with odd length
        //                    expandSearch(s, start, start + 1);
        //                }

        //                return s.substring(resultStartIndex, resultStartIndex + resultLength);
        //            }

        //            public void expandSearch(String s, int left, int right)
        //            {
        //                while (left >= 0 && right < s.length() && s.charAt(left) == s.charAt(right))
        //                {
        //                    // as we are starting from middle, we are moving 
        //                    // the pointers to the left and right accordingly
        //                    left--;
        //                    right++;
        //                }

        //                if (right - left - 1 > resultLength)
        //                {
        //                    // we are doing left+1, as in the above loop,
        //                    // we keep on moving left, and at some point
        //                    // the value of left might become -1 or might go
        //                    // outside the horizon, that's why, we are adding 1
        //                    //  to the resultStartIndex
        //                    resultStartIndex = left + 1;

        //                    // we are doing right+1, as in the above loop,
        //                    // we keep on moving right, and at some point
        //                    // the value of right might become n or might go
        //                    // outside the horizon, that's why,
        //                    resultLength = (right - 1) - (left + 1) + 1;
        //                }
        //            }
        //        }


        /*

         1     2     3
              abc   def

         4     5     6
        ghi   jkl   mno

         7     8     9
        pqrs  tuv  wxyz



        228 -> cat, bat, act, ...

        43556 -> hello

        */


        function foo(int param)
        {


            Dictionary<int, char[]> MyDictionary = new Dictionary<int, char[]>();

            MyDictionary.Add(1, new char[]);
            MyDictionary.Add(2, new char[] { "a,b,c" });
            MyDictionary.Add(3, new char[] { "d, e, f" });


            char[] val MyDictionary[param]; //abc, abc, tuv , all permutation

            char[] allinput;


            bool IsValid = validateWord(char[] t);

            return param;
        }


        funtion bool ValidateWord(char[] charArray) //abc
        {
            HashSet(char[]) allchars = new HashSet(char[])();
            Dictionary <string, char[] input> validWordDictionary
        
             //Take the input and add to the dictionary
             //master list which adds to the hashtable.

            //cach of valid permutated words, //cached dicitonay

        }
        /*

         1     2     3
              abc   def

         4     5     6
        ghi   jkl   mno

         7     8     9
        pqrs  tuv  wxyz



        228 -> cat, bat, act, ...

        43556 -> hello

        */






        private static bool Ispalindrome(string s)
        {
            int j = s.Length;
            char[] chars = s.ToCharArray();
            for (int i = 0; i < j; i++, j--)
            {
                if (chars[i] !=chars[j-1])
                {
                    return false;
                }

                

            }
            return true;

        }
        //        Given a string s and an integer k, return the length of the longest substring of s that contains at most k distinct characters.
        //       Example 1:
        //Input: s = "eceba", k = 2
        //Output: 3
        //Explanation: The substring is "ece" with length 3.
        public static int LengthOfLongestSubstringKDistinct(string s, int k)
        {
            int[] freq = new int[256];
            int i = 0, count = 0, res = 0;

            for (int j = 0; j < s.Length; j++)
            {
                if (freq[s[j]]++ == 0) count++; //New Char at the right

                while (count == k + 1 && i < s.Length)
                {
                    freq[s[i]]--;   // Continue sliding left pointer to the right until count <= k
                    if (freq[s[i]] == 0) count--;
                    i++;
                }
                res = Math.Max(res, j - i + 1);
            }
            return res;
        }
        //algorithm:

        // *https://leetcode.com/problems/find-k-closest-elements/
        //have left and right pointer
        //move toward the right, add to the map char and index
        //replace index if dup found, if hash size is k + 1, then remove left index lowest value, start++
        //    {
        //    https://leetcode.com/problems/longest-substring-with-at-most-k-distinct-characters/solution/


        //public int lengthOfLongestSubstringKDistinctJavaSolution(String s, int k)
        //{
        //    if (s == null || s.length() == 0)
        //        return 0;
        //    Map<Character, Integer> map = new HashMap<>();
        //    int windowStart = 0, maxLen = 0;
        //    for (int windowEnd = 0; windowEnd < s.length(); windowEnd++)
        //    {
        //        char rightChar = s.charAt(windowEnd);
        //        map.put(rightChar, map.getOrDefault(rightChar, 0) + 1);
        //        while (map.size() > k)
        //        { //we increase the window as long as its valid
        //          //move the start pointer and shrink it until its valid, record the maxLength as well
        //            char leftChar = s.charAt(windowStart);
        //            map.put(leftChar, map.get(leftChar) - 1);
        //            if (map.get(leftChar) == 0)
        //            {
        //                map.remove(leftChar);
        //            }
        //            windowStart++;
        //        }
        //        maxLen = Math.max(maxLen, windowEnd - windowStart + 1);
        //    }
        //    return maxLen;
        //}

        //Input: s = "cbaebabacd", p = "abc"
        //out cba, bac
        //Find All Anagrams in a String
       // public static IList<int> FindAnagrams
      
        static   void Main(string[] args)
        {

           //string longest= LongestPalindrome("abcdamomracexecartrakracecax");
            int a = lengthOfLongestSubstring("abcabcbb");
            //string input = "abce";
            //string MainString = "defabcefh";
            ////returns true;
            //bool validate = Ispalindrome("deleveled");
            ////bool stringFound = FindSubstring(input, MainString);
             int longestsubstring = lengthOfLongestSubstring("abcabcbb");
            ////string finalString = MinRemoveToMakeValid("a)b(c)d");
            //IList<int> lst = findAnagrams("cbaebabacd", "abc");



            // BuildTreeClass tree = new BuildTreeClass();
            //int longest = LengthOfLongestSubstring("abdcb");

            // int longest=  LengthOfLongestSubstringKDistinct("dcedefedc", 2);
            // Console.WriteLine(longest);
            //int[] inorder = new int[] { 5, 10, 40, 30, 28 };
            //int len = inorder.Length;
            //Node mynode = tree.buildTree(inorder, 0,
            //                             len - 1, tree.root);
            //int[] inorder = {9, 3, 15, 20, 7};
            //int[] postorder = {9, 15, 7, 20, 3};
            //LinkedList[] XMLList = new LinkedList[2];





            //L//inkedListNode list = 
            //Output: [1,1,2,3,4,4,5,6]

            //Output: [3,9,20,null,null,15,7]
            //reeNode treeRoot = tree.buildTree(inorder, upostorder);

            //int[][] intervals = {
            //                new int[2]{15,20},
            //                new int[2]{5,10},
            //                new int[2]{0,30} 

            // };


            //minMeetingRooms(intervals);
        }
    }
}
