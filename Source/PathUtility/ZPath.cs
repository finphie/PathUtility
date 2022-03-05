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

    /// <summary>
    /// 拡張子なしの一時ファイル名を取得します。
    /// </summary>
    /// <returns>拡張子なしの一時ファイル名を返します。</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string GetTempFileNameWithoutExtension()
        => Guid.NewGuid().ToString();

    /// <summary>
    /// 拡張子なしの一時ファイル名を取得します。
    /// </summary>
    /// <param name="destination">拡張子なしの一時ファイル名</param>
    /// <exception cref="ArgumentOutOfRangeException">バッファサイズが不足しています。</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void GetTempFileNameWithoutExtension(Span<char> destination)
    {
        Guard.IsInRangeFor(GuidLength - 1, destination, nameof(destination));
        GetTempFileNameWithoutExtensionInternal(destination);
    }

    /// <summary>
    /// 一時ファイル名を取得します。
    /// </summary>
    /// <returns>一時ファイル名を返します。</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string GetTempFileName() => GetTempFileNameInternal(".tmp");

    /// <summary>
    /// 一時ファイル名を取得します。
    /// </summary>
    /// <param name="extension">拡張子</param>
    /// <returns>一時ファイル名を返します。</returns>
    /// <exception cref="ArgumentException">拡張子が空文字または先頭の文字が「.」ではありません。</exception>
    /// <exception cref="ArgumentOutOfRangeException">「.」を含む拡張子の長さが1以下です。</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string GetTempFileName(ReadOnlySpan<char> extension)
    {
        Guard.IsNotEmpty(extension, nameof(extension));
        Guard.IsInRangeFor(1, extension, nameof(extension));
        Guard.IsEqualTo(extension[0], '.', nameof(extension));

        return GetTempFileNameInternal(extension);
    }

    /// <summary>
    /// 一時ファイル名を取得します。
    /// </summary>
    /// <param name="extension">拡張子</param>
    /// <param name="destination">一時ファイル名</param>
    /// <exception cref="ArgumentException">拡張子が空文字または先頭の文字が「.」ではありません。</exception>
    /// <exception cref="ArgumentOutOfRangeException">「.」を含む拡張子の長さが1以下か、バッファサイズが不足しています。</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void GetTempFileName(ReadOnlySpan<char> extension, Span<char> destination)
    {
        Guard.IsNotEmpty(extension, nameof(extension));
        Guard.IsInRangeFor(1, extension, nameof(extension));
        Guard.IsEqualTo(extension[0], '.', nameof(extension));
        Guard.IsInRangeFor(GuidLength + 2 - 1, destination, nameof(destination));

        GetTempFileNameInternal(extension, destination);
    }

    /// <summary>
    /// 指定された拡張子の一時ファイルパスを取得します。
    /// </summary>
    /// <param name="extension">拡張子</param>
    /// <returns>指定された拡張子の一時ファイルパスを返します。</returns>
    /// <exception cref="ArgumentException">拡張子が空文字または先頭の文字が「.」ではありません。</exception>
    /// <exception cref="ArgumentOutOfRangeException">「.」を含む拡張子の長さが1以下です。</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string GetTempFilePath(ReadOnlySpan<char> extension)
    {
        Guard.IsNotEmpty(extension, nameof(extension));
        Guard.IsInRangeFor(1, extension, nameof(extension));
        Guard.IsEqualTo(extension[0], '.', nameof(extension));

        const int StackallocThreshold = 512;

        var length = GuidLength + extension.Length;
        char[]? pool = null;
        Span<char> buffer = length <= StackallocThreshold
            ? stackalloc char[GuidLength + extension.Length]
            : (pool = ArrayPool<char>.Shared.Rent(length));

        try
        {
            GetTempFileNameInternal(extension, buffer);
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
            GetTempFileNameInternal(extension, buffer);
        }
        finally
        {
            if (pool is not null)
            {
                ArrayPool<char>.Shared.Return(pool);
            }
        }

        return buffer.ToString();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static void GetTempFileNameInternal(ReadOnlySpan<char> extension, Span<char> destination)
    {
        GetTempFileNameWithoutExtensionInternal(destination);
        extension.CopyTo(destination[GuidLength..]);
    }
}
