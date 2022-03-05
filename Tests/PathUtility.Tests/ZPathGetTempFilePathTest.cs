using FluentAssertions;
using Xunit;

namespace PathUtility.Tests;

public sealed class ZPathGetTempFilePathTest
{
    [Fact]
    public void 拡張子指定なし_ファイルパスを返す()
    {
        const string Extension = ".tmp";
        var filePath = ZPath.GetTempFilePath();

        filePath.Should().EndWith(Extension);
        Path.IsPathFullyQualified(filePath).Should().BeTrue();
    }

    [Theory]
    [InlineData(".a")]
    [InlineData(".abc")]
    public void 有効な拡張子_ファイルパスを返す(string extension)
    {
        var filePath = ZPath.GetTempFilePath(extension);

        filePath.Should().EndWith(extension);
        Path.IsPathFullyQualified(filePath).Should().BeTrue();
    }

    [Theory]
    [InlineData("")]
    [InlineData(".")]
    [InlineData("a")]
    public void 不正な拡張子_Error(string extension)
        => FluentActions.Invoking(() => ZPath.GetTempFilePath(extension)).Should().Throw<ArgumentException>();
}
