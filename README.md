# PathUtility

[![Build(.NET)](https://github.com/finphie/PathUtility/actions/workflows/build-dotnet.yml/badge.svg)](https://github.com/finphie/PathUtility/actions/workflows/build-dotnet.yml)
[![NuGet](https://img.shields.io/nuget/v/PathUtility?color=0078d4&label=NuGet)](https://www.nuget.org/packages/PathUtility/)
[![Azure Artifacts](https://feeds.dev.azure.com/finphie/7af9aa4d-c550-43af-87a5-01539b2d9934/_apis/public/Packaging/Feeds/DotNet/Packages/80f310a7-18a5-4a16-9ba4-8acb8568b580/Badge)](https://dev.azure.com/finphie/Main/_artifacts/feed/DotNet/NuGet/PathUtility?preferRelease=true)

パス関連処理を詰め合わせたライブラリです。

## 説明

PathUtilityは、パスの作成などの各種処理を詰め合わせたライブラリです。

## インストール

### NuGet（正式リリース版）

```shell
dotnet add package PathUtility
```

### Azure Artifacts（開発用ビルド）

```shell
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

- .NET 9
- .NET 8
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

- [Microsoft.Testing.Extensions.CodeCoverage](https://github.com/microsoft/codecoverage)
- [Shouldly](https://github.com/shouldly/shouldly)
- [xunit.v3](https://github.com/xunit/xunit)

### アナライザー

- [DocumentationAnalyzers](https://github.com/DotNetAnalyzers/DocumentationAnalyzers)
- [IDisposableAnalyzers](https://github.com/DotNetAnalyzers/IDisposableAnalyzers)
- [Microsoft.CodeAnalysis.NetAnalyzers](https://github.com/dotnet/roslyn-analyzers)
- [Microsoft.VisualStudio.Threading.Analyzers](https://github.com/Microsoft/vs-threading)
- [Roslynator.Analyzers](https://github.com/dotnet/roslynator)
- [Roslynator.Formatting.Analyzers](https://github.com/dotnet/roslynator)
- [StyleCop.Analyzers](https://github.com/DotNetAnalyzers/StyleCopAnalyzers)

### その他

- [PolySharp](https://github.com/Sergio0694/PolySharp)
