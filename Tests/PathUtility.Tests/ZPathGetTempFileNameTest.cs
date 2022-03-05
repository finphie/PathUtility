using FluentAssertions;
using Xunit;

namespace PathUtility.Tests;

public sealed class ZPathGetTempFileNameTest
{
    const int MinLength = 38;

    [Fact]
    public void 拡張子及びバッファー指定なし_ファイル名を返す()
    {
        const string Extension = ".tmp";
        var fileName = ZPath.GetTempFileName();

        fileName.Should().EndWith(Extension);
        Guid.TryParse(fileName[..^Extension.Length], out _).Should().BeTrue();
    }

    [Theory]
    [InlineData(".a")]
    [InlineData(".abc")]
    public void 有効な拡張子かつバッファー指定なし_ファイル名を返す(string extension)
    {
        var fileName = ZPath.GetTempFileName(extension);

        fileName.Should().EndWith(extension);
        Guid.TryParse(fileName[..^extension.Length], out _).Should().BeTrue();
    }

    [Fact]
    public void 拡張子指定なしかつバッファーサイズ40以上_ファイル名を返す()
    {
        const string Extension = ".tmp";

        Span<char> buffer = new char[32 + 4 + 4];
        ZPath.GetTempFileName(buffer);

        var fileName = buffer.ToString();
        fileName.Should().EndWith(Extension);
        Guid.TryParse(buffer[..^Extension.Length], out _).Should().BeTrue();
    }

    [Fact]
    public void 有効な拡張子かつバッファーサイズ38以上_ファイル名を返す()
    {
        const string Extension = ".a";

        Span<char> buffer = new char[MinLength];
        ZPath.GetTempFileName(buffer, Extension);

        var fileName = buffer.ToString();
        fileName.Should().EndWith(Extension);
        Guid.TryParse(buffer[..^Extension.Length], out _).Should().BeTrue();
    }

    [Theory]
    [InlineData("")]
    [InlineData(".")]
    [InlineData("a")]
    public void 不正な拡張子_Error(string extension)
    {
        FluentActions.Invoking(() => ZPath.GetTempFileName(extension)).Should().Throw<ArgumentException>();
        FluentActions.Invoking(() =>
        {
            var buffer = new char[MinLength];
            ZPath.GetTempFileName(buffer, extension);

            return buffer;
        }).Should().Throw<ArgumentException>();
    }

    [Fact]
    public void バッファーサイズ38未満_Error()
    {
        FluentActions.Invoking(() =>
        {
            var buffer = new char[MinLength - 1];
            ZPath.GetTempFileName(buffer, ".a");

            return buffer;
        }).Should().Throw<ArgumentException>();
    }
}
