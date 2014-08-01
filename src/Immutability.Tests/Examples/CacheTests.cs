using System.Collections.Generic;
using Immutability.Examples;
using Shouldly;
using Xunit;

namespace Immutability.Tests.Examples
{
    public class CacheTests
    {
        [Fact]
        public void CanAdd()
        {
            var cache = new Cache<string, string>();
            cache["Foo"] = "Bar";

            cache.ContainsKey("Foo").ShouldBe(true);
            cache["Foo"].ShouldBe("Bar");
        }

        [Fact]
        public void CanGetFromCacheWhenMissing()
        {
            var cache = new Cache<string, string>(onMissing: x => x);
            cache["Foo"].ShouldBe("Foo");
            cache["Bar"].ShouldBe("Bar");
        }

        [Fact]
        public void ThrowsIfNotProvidedWithFunc()
        {
            var cache = new Cache<string, string>();
            Should.Throw<KeyNotFoundException>(() => { var _ = cache["Foo"]; });
        }

        [Fact]
        public void CanClear()
        {
            var existing = new Dictionary<string, string>();
            existing["Foo"] = "Bar";

            var cache = new Cache<string, string>(existing);
            cache.Count.ShouldBe(1);
            cache.Clear();
            cache.Count.ShouldBe(0);
        }

        [Fact]
        public void CanRemove()
        {
            var cache = new Cache<string, string>();
            cache["Foo"] = "Bar";

            cache.Remove("Foo").ShouldBe(true);
            cache.ContainsKey("Foo").ShouldBe(false);
        }
    }
}