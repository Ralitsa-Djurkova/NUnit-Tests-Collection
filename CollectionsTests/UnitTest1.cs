using Collections;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CollectionsTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test_Collection_EmptyConstructor()
        {
            var nums = new Collection<int>();

            Assert.That(nums.ToString(), Is.EqualTo("[]"));
        }
        [Test]
        public void Test_Collection_EmptyConstor_With_Single_Item()
        {
            var nums = new Collection<int>(5);

            Assert.That(nums.ToString(), Is.EqualTo("[5]"));
        }
        [Test]
        public void Test_Constructor_With_MultipleItem()
        {
            var nums = new Collection<int>(5, 10, 15);

            Assert.That(nums.ToString(), Is.EqualTo("[5, 10, 15]"));
        }
        [Test]
        public void Test_Collection_Add()
        {
            Collection<int> nums = new Collection<int>();
            nums.Add(8);
            nums.Add(9);

            Assert.That(nums.ToString(), Is.EqualTo("[8, 9]"));
        }
        [Test]
        public void Test_Collection_Add_With_MoreNumbers()
        {
            Collection<int> nums = new Collection<int>(4, 5, 6);

            nums.Add(10);
            nums.Add(15);

            Assert.That(nums.ToString(), Is.EqualTo("[4, 5, 6, 10, 15]"));
        }
        [Test]
        public void Test_Collection_Add_With_SameNumbers()
        {
            Collection<int> nums = new Collection<int>(4, 5, 6);

            nums.Add(10);
            nums.Add(15);
            nums.Add(10);

            Assert.That(nums.ToString(), Is.EqualTo("[4, 5, 6, 10, 15, 10]"));
        }
        [Test]
        public void Test_Collection_With_OneNegative_Number()
        {
            Collection<int> nums = new Collection<int>(4, 5, 6);

            nums.Add(10);
            nums.Add(15);
            nums.Add(-10);

            Assert.That(nums.ToString(), Is.EqualTo("[4, 5, 6, 10, 15, -10]"));
        }
        [Test]
        public void Test_collection_AddRangeWithGrow()
        {
            Collection<int> nums = new Collection<int>();
            int oldCollection = nums.Capacity;
            int[] newNums = Enumerable.Range(1000, 2000).ToArray();

            nums.AddRange(newNums);
            string expectedNums = "[" + string.Join(", ", newNums) + "]";
            Assert.That(nums.ToString, Is.EqualTo(expectedNums));
            Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(oldCollection));
            Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(nums.Count));
        }
        [Test]
        public void Test_collection_AddRangeIsNull()
        {
            Collection<int> nums = new Collection<int>();
            int oldCollection = nums.Capacity;
            int[] newNums = Enumerable.Range(0, 0).ToArray();

            nums.AddRange(newNums);
            string expectedNums = "[" + string.Join(", ", newNums) + "]";
            Assert.That(nums.ToString, Is.EqualTo(expectedNums));
            Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(oldCollection));
            Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(nums.Count));
        }
        [Test]
        public void Test_collection_AddRangeCapacity_And_Count_Are_Equal()
        {
            Collection<int> nums = new Collection<int>();
            int oldCollection = nums.Capacity;
            int[] newNums = Enumerable.Range(16, 16).ToArray();

            nums.AddRange(newNums);
            string expectedNums = "[" + string.Join(", ", newNums) + "]";
            Assert.That(nums.ToString, Is.EqualTo(expectedNums));
            Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(oldCollection));
            Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(nums.Count));
        }
        [Test]
        public void Test_collection_AddRangeCapacity_And_Coun_Invalid()
        {
            Collection<int> nums = new Collection<int>();
            int oldCollection = nums.Capacity;
            int[] newNums = Enumerable.Range(0, 0).ToArray();

            nums.AddRange(newNums);

            Assert.That(() => { int[] newNums = Enumerable.Range(0, -20).ToArray(); ; }, Throws.InstanceOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void Test_Collection_GetByIndex()
        {
            Collection<string> names = new Collection<string>("Ivan", "Peter");

            string firstName = names[0];
            string secondName = names[1];

            Assert.AreEqual(names[0], firstName);
            Assert.AreEqual(names[1], secondName);
        }
        [Test]
        public void Test_Collcection_GetByInvalidIndex()
        {
            Collection<string> names = new Collection<string>("Ivan", "Peter");

            Assert.That(() => { string name = names[-1]; }, Throws.InstanceOf<ArgumentOutOfRangeException>());

            Assert.That(() => { string name = names[2]; }, Throws.InstanceOf<ArgumentOutOfRangeException>());

            Assert.That(() => { string name = names[500]; }, Throws.InstanceOf<ArgumentOutOfRangeException>());

            Assert.That(names.ToString(), Is.EqualTo("[Ivan, Peter]"));

        }
        [Test]
        public void Test_Collection_Nested_Collection_Tostring()
        {
            Collection<string> names = new Collection<string>("Ivan", "Peter");
            Collection<int> nums = new Collection<int>(10, 20);
            Collection<DateTime> dates = new Collection<DateTime>();

            Collection<object> nested = new Collection<object>(names, nums, dates);
            string nestedToString = nested.ToString();

            Assert.That(nestedToString, Is.EqualTo("[[Ivan, Peter], [10, 20], []]"));
        }
        [Test]
        public void Test_Collection_Nested_Collection_Tostring_With_LowerCase()
        {
            Collection<string> names = new Collection<string>("ivan", "peter");
            Collection<int> nums = new Collection<int>(10, 20);
            Collection<DateTime> dates = new Collection<DateTime>();

            Collection<object> nested = new Collection<object>(names, nums, dates);
            string nestedToString = nested.ToString();

            Assert.That(nestedToString, Is.EqualTo("[[ivan, peter], [10, 20], []]"));
        }
        //Performance Test with 1 Million Items
        [Test]
        public void Test_Collection_1MillionItems()
        {
            const int itemsCount = 1000000;
            Collection<int> nums = new Collection<int>();
            nums.AddRange(Enumerable.Range(1, itemsCount).ToArray());
            Assert.That(nums.Count == itemsCount);
            Assert.That(nums.Capacity >= nums.Count);

            for (int i = itemsCount - 1; i >= 0; i--)
            {
                nums.RemoveAt(i);
            }

            Assert.That(nums.ToString() == "[]");
            Assert.That(nums.Capacity >= nums.Count);
        }
        [Test]
        public void Test_Collection_Insert_Items_AtTheStart_Correctrly()
        {
            Collection<int> numbers = new Collection<int>();
            int index = 0;
            int items = 10;

            numbers.InsertAt(index, items);

            Assert.That(numbers.ToString(), Is.EqualTo("[10]"));

            index = 1;
            items = 500;

            numbers.InsertAt(index, items);

            Assert.That(numbers.ToString(), Is.EqualTo("[10, 500]"));

        }
        [Test]
        public void Test_Collection_Insert_If_Indexis_Negative_Number()
        {

            Collection<int> nums = new Collection<int>(1, 2, 3, 4, 5);

            int index = 5;
            int item = 2;

            nums.InsertAt(index, item);

            Assert.Throws<ArgumentOutOfRangeException>(() => nums[-1] = 500);

        }
        [Test]
        public void Test_Collection_InserAt_Capacity_Bigger_Then_MaxCapacity()
        {
            Collection<int> nums = new Collection<int>(1, 2, 3, 4, 5, 6, 7);
            int index = 5;
            int item = 1;

            nums.InsertAt(index, item);

            Assert.Throws<ArgumentOutOfRangeException>(() => nums[33] = 0);
        }
        [Test]
        public void Test_Collection_Exchange()
        {
            Collection<int> nums = new Collection<int>(1, 2, 3, 4, 5, 6, 7);

            int oldNumber = 3;
            int firstIndex = 2;
            int secondIndex = 3;
            nums.Exchange(firstIndex, secondIndex);

            Assert.That(oldNumber, Is.EqualTo(secondIndex));
        }
        [Test]
        public void Test_Collection_Exchange_Invalid_Index()
        {
            Collection<int> nums = new Collection<int>(1, 2, 3, 4, 5, 6, 7);

            int oldNumber = -3;
            int firstIndex = 2;
            int secondIndex = -3;
            
            Assert.Throws<ArgumentOutOfRangeException>(() => nums.Exchange(firstIndex, secondIndex));

        }
        [Test]
        public void Test_Collection_Remove()
        {
            Collection<int> nums = new Collection<int>(1, 2, 3, 4);

            int index = 1;

            nums.RemoveAt(index);

            Assert.That(nums.ToString(), Is.EqualTo("[1, 3, 4]"));
        }
        [Test]
        public void Test_Collection_Remove_OutofBoundary()
        {
            Collection<int> nums = new Collection<int>(1, 2, 3, 4);

            int index = -1;

            

            Assert.Throws<ArgumentOutOfRangeException>(() => nums.RemoveAt(index));
        }
        [Test]
        public void Test_Collection_Clear()
        {
            Collection<int> nums = new Collection<int>(1, 2, 3, 4);

            nums.Clear();

            Assert.That(nums.ToString(), Is.EqualTo("[]"));
        }
        [Test]
        public void Test_Collection_With_Empty_Collection()
        {
            Collection<int> nums = new Collection<int>();

            nums.Clear();

            Assert.That(nums.ToString(), Is.EqualTo("[]"));
        }



    }
}