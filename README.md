# PathUtility

[![Build(.NET)](https://github.com/finphie/PathUtility/actions/workflows/build-dotnet.yml/badge.svg)](https://github.com/finphie/PathUtility/actions/workflows/build-dotnet.yml)
[![NuGet](https://img.shields.io/nuget/v/PathUtility?color=0078d4&label=NuGet)](https://www.nuget.org/packages/PathUtility/)

パス関連処理を詰め合わせたライブラリです。

## 説明

PathUtilityは、パスの作成などの各種処理を詰め合わせたライブラリです。

## インストール

### NuGet（正式リリース版）

```console
dotnet add package PathUtility
```

### Azure Artifacts（開発用ビルド）

```console
dotnet add package PathUtility -s https://pkgs.dev.azure.com/finphie/Main/_packaging/DotNet/nuget/v3/index.json
```

## 使い方

```csharp
using PathUtility;

var tempFileNameWithoutExtension = ZPath.GetTempFileNameWithoutExtension();

var tempFileName1 = ZPath.GetTempFileName();
var tempFileName2 = ZPath.GetTempFileName(".cs");

var tempFilePath1 = ZPath.GetTempFilePath();
var tempFilePath2 = ZPath.GetTempFilePath(".cs");
```

## サポートフレームワーク

- .NET 7
- .NET 6
- .NET Standard 2.1

## 作者

finphie

## ライセンス

MIT

## クレジット

このプロジェクトでは、次のライブラリ等を使用しています。

### ライブラリ

- [CommunityToolkit.Diagnostics](https://github.com/CommunityToolkit/dotnet)

### テスト

- [FluentAssertions](https://github.com/fluentassertions/fluentassertions)
- [Microsoft.NET.Test.Sdk](https://github.com/microsoft/vstest)
- [Newtonsoft.Json](https://github.com/JamesNK/Newtonsoft.Json)
- [NuGet.Frameworks](https://github.com/NuGet/NuGet.Client)
- [xunit](https://github.com/xunit/xunit)
- [xunit.runner.visualstudio](https://github.com/xunit/visualstudio.xunit)

### アナライザー

- Microsoft.CodeAnalysis.NetAnalyzers (SDK組み込み)
- [Microsoft.VisualStudio.Threading.Analyzers](https://github.com/Microsoft/vs-threading)
- [StyleCop.Analyzers](https://github.com/DotNetAnalyzers/StyleCopAnalyzers)

### その他

- [Microsoft.SourceLink.GitHub](https://github.com/dotnet/sourcelink)
