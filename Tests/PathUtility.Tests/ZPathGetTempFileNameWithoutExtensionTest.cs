using Shouldly;
using Xunit;

namespace PathUtility.Tests;

public sealed class ZPathGetTempFileNameWithoutExtensionTest
{
    [Fact]
    public void ファイル名を返す()
        => Guid.TryParse(ZPath.GetTempFileNameWithoutExtension(), out _).ShouldBeTrue();
}
