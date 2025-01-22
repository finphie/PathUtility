using Shouldly;
using Xunit;

namespace PathUtility.Tests;

public sealed class ZPathGetTempFilePathTest
{
    [Fact]
    public void 拡張子指定なし_ファイルパスを返す()
    {
        const string Extension = ".tmp";
        var filePath = ZPath.GetTempFilePath();

        filePath.ShouldEndWith(Extension);
        Path.IsPathFullyQualified(filePath).ShouldBeTrue();
    }

    [Theory]
    [InlineData(".a")]
    [InlineData(".abc")]
    public void 有効な拡張子_ファイルパスを返す(string extension)
    {
        var filePath = ZPath.GetTempFilePath(extension);

        filePath.ShouldEndWith(extension);
        Path.IsPathFullyQualified(filePath).ShouldBeTrue();
    }

    [Theory]
    [InlineData("")]
    [InlineData(".")]
    [InlineData("a")]
    public void 不正な拡張子_Error(string extension)
        => Should.Throw<ArgumentException>(() => ZPath.GetTempFilePath(extension));
}
