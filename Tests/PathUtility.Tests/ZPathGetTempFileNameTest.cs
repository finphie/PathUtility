using FluentAssertions;
using Xunit;

namespace PathUtility.Tests;

public sealed class ZPathGetTempFileNameTest
{
    [Fact]
    public void 拡張子指定なし_ファイル名を返す()
    {
        const string Extension = ".tmp";
        var fileName = ZPath.GetTempFileName();

        fileName.Should().EndWith(Extension);
        Guid.TryParse(fileName[..^Extension.Length], out _).Should().BeTrue();
    }

    [Theory]
    [InlineData(".a")]
    [InlineData(".abc")]
    public void 有効な拡張子_ファイル名を返す(string extension)
    {
        var fileName = ZPath.GetTempFileName(extension);

        fileName.Should().EndWith(extension);
        Guid.TryParse(fileName[..^extension.Length], out _).Should().BeTrue();
    }

    [Theory]
    [InlineData("")]
    [InlineData(".")]
    [InlineData("a")]
    public void 不正な拡張子_Error(string extension)
        => FluentActions.Invoking(() => ZPath.GetTempFileName(extension)).Should().Throw<ArgumentException>();
}
