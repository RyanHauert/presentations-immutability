using System;
using Immutability.Demo;
using Shouldly;
using Xunit;

namespace Immutability.Tests.Demo
{
    public class FunctionalListTests
    {
        [Fact]
        public void AddDoesNotAffectOriginalList()
        {
            var list = FunctionalList<int>.Empty;
            var list2 = list.Add(1);

            list.IsEmpty.ShouldBe(true);
            list2.IsEmpty.ShouldBe(false);
            list2.Count().ShouldBe(1);
        }

        [Fact]
        public void RemoveDoesNotAffectOriginalList()
        {
            var list = FunctionalList<int>.Empty.Add(1).Add(2);
            var list2 = list.Remove(2);

            list.Count().ShouldBe(2);
            list.Contains(2).ShouldBe(true);

            list2.Count().ShouldBe(1);
            list2.Contains(2).ShouldBe(false);
        }
    }
}