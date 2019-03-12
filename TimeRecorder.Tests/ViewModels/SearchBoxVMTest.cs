using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeRecorder.ViewModels;
using Xunit;

namespace TimeRecorder.Tests.ViewModels
{
    public class SearchBoxVMTest
    {
        class Dummy
        {
            public string stringItem { get; set; }
            public int otherValue { get; set; }
        }

        List<Dummy> GetDummiesWithoutKey()
        {
            return new List<Dummy>
            {
                new Dummy {stringItem = "stringValue1", otherValue = 0},
                new Dummy {stringItem = "stringValue2", otherValue = 0},
                new Dummy {stringItem = "stringValue3", otherValue = 0}
            };
        }

        List<Dummy> GetDummiesWithKey()
        {
            return new List<Dummy>
            {
                new Dummy {stringItem = "stringwithkeylast", otherValue = 0},
                new Dummy {stringItem = "stringKeyInMiddle", otherValue = 42},
                new Dummy {stringItem = "key in start"}
            };
        }

        List<Dummy> GetDummies()
        {
            var dummies = new List<Dummy>();
            dummies.AddRange(GetDummiesWithKey());
            dummies.AddRange(GetDummiesWithoutKey());
            return dummies;
        }
        string converter(Dummy dummy) => dummy.stringItem;

        [Fact]
        public void Test_FilterItems_Sets_FilteredItems_To_Items_Matching_SearchText()
        {
            var dummies = GetDummies();
            var searchVM = new SearchBoxVM<Dummy>(dummies, converter);
            searchVM.SearchText = "key";

            searchVM.FilterItems();
            var result = searchVM.Filtered;

            Assert.Equal(3, result.Count);
            Assert.True(GetDummiesWithKey().TrueForAll(dummy => result.Contains(converter(dummy))));
        }

        [Fact]
        public void Test_FilterItems_Sets_AutoCompleteItem_To_Entry_When_Only_Result()
        {
            var dummies = GetDummiesWithoutKey();
            var dummyWithKey = new Dummy { stringItem = "with key", otherValue = 0 };
            dummies.Add(dummyWithKey);
            var searchVM = new SearchBoxVM<Dummy>(dummies, converter);
            searchVM.SearchText = "key";

            searchVM.FilterItems();
            var result = searchVM.Filtered;

            Assert.Single(result);
            Assert.Equal(dummyWithKey, searchVM.AutoCompleteItem);
        }

        [Fact]
        public void Test_FilterItems_Sets_AutoCompleteItem_To_Default_When_Not_Only_Result()
        {
            var dummies = GetDummiesWithoutKey();
            var dummyWithKey = new Dummy { stringItem = "with key", otherValue = 0 };
            dummies.Add(dummyWithKey);
            var searchVM = new SearchBoxVM<Dummy>(dummies, converter);
            searchVM.SearchText = "key";

            searchVM.FilterItems();
            Assert.NotNull(searchVM.AutoCompleteItem);

            dummies.AddRange(GetDummiesWithKey());
            searchVM.FilterItems();
            var result = searchVM.Filtered;

            Assert.Equal(4, result.Count);
            Assert.Null(searchVM.AutoCompleteItem);
        }
    }
}
