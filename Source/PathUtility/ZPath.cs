using System.Buffers;
using System.Runtime.CompilerServices;
using CommunityToolkit.Diagnostics;

namespace PathUtility;

/// <summary>
/// パス関連の処理を行うクラスです。
/// </summary>
public static class ZPath
{
    // GUIDは36文字（ハイフンあり）
    const int GuidLength = 32 + 4;

    const string TempExtension = ".tmp";

    /// <summary>
    /// 拡張子なしの一時ファイル名を取得します。
    /// </summary>
    /// <returns>拡張子なしの一時ファイル名を返します。</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string GetTempFileNameWithoutExtension()
        => Guid.NewGuid().ToString();

    /// <summary>
    /// 一時ファイル名を取得します。
    /// </summary>
    /// <returns>一時ファイル名を返します。</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string GetTempFileName() => GetTempFileNameInternal(TempExtension);

    /// <summary>
    /// 一時ファイル名を取得します。
    /// </summary>
    /// <param name="extension">拡張子</param>
    /// <returns>一時ファイル名を返します。</returns>
    /// <exception cref="ArgumentException">拡張子が空文字または先頭の文字が"."ではありません。</exception>
    /// <exception cref="ArgumentOutOfRangeException">"."を含む拡張子の長さが1以下です。</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string GetTempFileName(ReadOnlySpan<char> extension)
    {
        Guard.IsNotEmpty(extension, nameof(extension));
        Guard.IsInRangeFor(1, extension, nameof(extension));
        Guard.IsEqualTo(extension[0], '.', nameof(extension));

        return GetTempFileNameInternal(extension);
    }

    /// <summary>
    /// 指定された拡張子の一時ファイルパスを取得します。
    /// </summary>
    /// <returns>指定された拡張子の一時ファイルパスを返します。</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string GetTempFilePath() => GetTempFilePathInternal(TempExtension);

    /// <summary>
    /// 指定された拡張子の一時ファイルパスを取得します。
    /// </summary>
    /// <param name="extension">拡張子</param>
    /// <returns>指定された拡張子の一時ファイルパスを返します。</returns>
    /// <exception cref="ArgumentException">拡張子が空文字または先頭の文字が"."ではありません。</exception>
    /// <exception cref="ArgumentOutOfRangeException">"."を含む拡張子の長さが1以下です。</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string GetTempFilePath(ReadOnlySpan<char> extension)
    {
        Guard.IsNotEmpty(extension, nameof(extension));
        Guard.IsInRangeFor(1, extension, nameof(extension));
        Guard.IsEqualTo(extension[0], '.', nameof(extension));

        return GetTempFilePathInternal(extension);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static void GetTempFileNameWithoutExtensionInternal(Span<char> destination)
    {
        var guid = Guid.NewGuid();
        guid.TryFormat(destination, out _);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static string GetTempFileNameInternal(ReadOnlySpan<char> extension)
    {
        const int StackallocThreshold = 512;

        var length = GuidLength + extension.Length;
        char[]? pool = null;
        Span<char> buffer = length <= StackallocThreshold
            ? stackalloc char[GuidLength + extension.Length]
            : (pool = ArrayPool<char>.Shared.Rent(length));

        try
        {
            GetTempFileNameInternal(buffer, extension);
            return buffer.ToString();
        }
        finally
        {
            if (pool is not null)
            {
                ArrayPool<char>.Shared.Return(pool);
            }
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static void GetTempFileNameInternal(Span<char> destination, ReadOnlySpan<char> extension)
    {
        GetTempFileNameWithoutExtensionInternal(destination);
        extension.CopyTo(destination[GuidLength..]);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static string GetTempFilePathInternal(ReadOnlySpan<char> extension)
    {
        const int StackallocThreshold = 512;

        var length = GuidLength + extension.Length;
        char[]? pool = null;
        Span<char> buffer = length <= StackallocThreshold
            ? stackalloc char[GuidLength + extension.Length]
            : (pool = ArrayPool<char>.Shared.Rent(length));

        try
        {
            GetTempFileNameInternal(buffer, extension);
            return Path.Join(Path.GetTempPath(), buffer);
        }
        finally
        {
            if (pool is not null)
            {
                ArrayPool<char>.Shared.Return(pool);
            }
        }
    }
}
