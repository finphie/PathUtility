using FluentAssertions;
using Xunit;

namespace PathUtility.Tests;

public sealed class ZPathGetTempFileNameWithoutExtensionTest
{
    const int MinLength = 36;

    [Fact]
    public void バッファー指定なし_ファイル名を返す()
        => Guid.TryParse(ZPath.GetTempFileNameWithoutExtension(), out _).Should().BeTrue();

    [Fact]
    public void バッファーサイズ36以上_ファイル名を返す()
    {
        var buffer = new char[MinLength];
        ZPath.GetTempFileNameWithoutExtension(buffer);

        Guid.TryParse(buffer, out _).Should().BeTrue();
    }

    [Fact]
    public void 長さ36未満のバッファー_Error()
    {
        FluentActions.Invoking(() =>
        {
            var buffer = new char[MinLength - 1];
            ZPath.GetTempFileNameWithoutExtension(buffer);

            return buffer;
        }).Should().Throw<ArgumentOutOfRangeException>();
    }
}
