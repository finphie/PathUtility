using FluentAssertions;
using Xunit;

namespace PathUtility.Tests;

public sealed class PathHelperGetTempFilePathTest
{
    [Theory]
    [InlineData(".a")]
    [InlineData(".abc")]
    public void 有効な拡張子_ファイルパスを返す(string extension)
    {
        var filePath = PathHelper.GetTempFilePath(extension);
        Path.IsPathFullyQualified(filePath).Should().BeTrue();
    }

    [Theory]
    [InlineData("")]
    [InlineData(".")]
    [InlineData("a")]
    public void 不正な拡張子_Error(string extension)
        => FluentActions.Invoking(() => PathHelper.GetTempFilePath(extension)).Should().Throw<ArgumentException>();
}
