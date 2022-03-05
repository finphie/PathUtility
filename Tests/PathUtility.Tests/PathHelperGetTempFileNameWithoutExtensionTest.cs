using FluentAssertions;
using Xunit;

namespace PathUtility.Tests;

public sealed class PathHelperGetTempFileNameWithoutExtensionTest
{
    const int MinLength = 36;

    [Fact]
    public void バッファ指定なし_ファイル名を返す()
        => Guid.TryParse(PathHelper.GetTempFileNameWithoutExtension(), out _).Should().BeTrue();

    [Fact]
    public void バッファサイズ36以上_ファイル名を返す()
    {
        var buffer = new char[MinLength];
        PathHelper.GetTempFileNameWithoutExtension(buffer);

        Guid.TryParse(buffer, out _).Should().BeTrue();
    }

    [Fact]
    public void 長さ36未満のバッファ_Error()
    {
        FluentActions.Invoking(() =>
        {
            var buffer = new char[MinLength - 1];
            PathHelper.GetTempFileNameWithoutExtension(buffer);

            return buffer;
        }).Should().Throw<ArgumentOutOfRangeException>();
    }
}
