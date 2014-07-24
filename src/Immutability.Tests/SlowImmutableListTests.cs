using Shouldly;
using Xunit;

namespace Immutability.Tests
{
    public class SlowImmutableListTests
    {
        [Fact]
        public void AddDoesNotAffectOriginalList()
        {
            var list = new SlowImmutableList<int>();
            var list2 = list.Add(1);

            list2.Count.ShouldBe(1);
            list2.ShouldContain(1);

            list.Count.ShouldBe(0);
            list.ShouldNotContain(1);
        }

        [Fact]
        public void RemoveDoesNotAffectOriginalList()
        {
            var list = new SlowImmutableList<int>(new[] { 1, 2, 3, 4 });
            var list2 = list.Remove(1);

            list2.Count.ShouldBe(3);
            list2.ShouldNotContain(1);

            list.Count.ShouldBe(4);
            list.ShouldContain(1);
        }

        [Fact]
        public void CanEnumerateListWhileSomethingChangesIt()
        {
            var list = new SlowImmutableList<int>(new[] { 1, 2, 3, 4 });
            var enumerator = list.GetEnumerator();
            enumerator.MoveNext().ShouldBe(true);

            var list2 = list.Add(1);
            Should.NotThrow(() => enumerator.MoveNext().ShouldBe(true));
        }
    }
}