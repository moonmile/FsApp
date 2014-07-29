FsApp
===============
F# の PCL で Windows.winmd を参照させて Windows.UI.Xaml を利用できるようにする試みです。
C# の Profile32 の PCL を作ったときと同じ状態にすることを目的としています。

# プロジェクト構成と作成手順

- C# で Windows Store 8.1, Windows Phone 8.1 のユニバーサルアプリ
- ClassLibraryCs を Profile32 で作成する（Windows 8.1, Windows Phone 8.1）のみチェックを入れる。
- ClassLibraryFs を PCL で作成する
- FsAapp.Windows から ClassLibraryCs と ClassLibraryFs を参照させる。

## ClassLibraryFs.fsproj を編集する

フレームワークターゲットを変更する

```
    <TargetFrameworkVersion>v4.6</TargetFrameworkVersion>
    <TargetFrameworkProfile>Profile32</TargetFrameworkProfile>
```

ターゲットプラットフォームを追加する

```
  <ItemGroup>
    <TargetPlatform Include="Windows, Version=8.1" />
    <TargetPlatform Include="WindowsPhoneApp, Version=8.1" />
    <Compile Include="Class1.fs" />
  </ItemGroup>
```

こうすることで、参照設定には出てこないが、コードから Windows.UI.Xaml 名前空間が参照できるようになる。

# テストコード

Modify メソッドを TextBlock オブジェクトを引数にして呼び出す。メソッド内で TextBlock 等を使ってもよいのだが、ランタイムエラーが取りづらいので、この方式にｓてある。

## C# の場合

```
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Windows.UI.Xaml.Controls;

namespace ClassLibraryCs
{
    public class Class1
    {
        public string Name { get { return "ClassLibraryCs project"; } }
        public void Modify( TextBlock text )
        {
            text.Text = "**" + text.Text + "**";
        } 
    }
}
```

## F# の場合

```
namespace ClassLibraryFs
open System
open System.Collections.Generic
open System.IO
open System.Linq
open System.Runtime.InteropServices.WindowsRuntime
open Windows.Foundation
open Windows.Foundation.Collections
open Windows.UI.Xaml
open Windows.UI.Xaml.Controls
open Windows.UI.Xaml.Controls.Primitives
open Windows.UI.Xaml.Data
open Windows.UI.Xaml.Input
open Windows.UI.Xaml.Media
open Windows.UI.Xaml.Navigation
open System.Runtime.CompilerServices

[<Extension>]
type Class1() = 
    member val Name = "ClassLibraryFs F# lib" with get, set

    member this.Modify(text:obj) =
        // text.Text <- "<<" + text.Text + ">>"
        let tx = text :?> TextBlock
        tx.Text <- "new message"
        ()

    member this.Calc(x:int, y:int) = x + y
```

## XAML 

ボタンをクリックしたときに、それぞれのコードを呼び出す。

```
public sealed partial class MainPage : Page
{
    public MainPage()
    {
        this.InitializeComponent();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        var obj = new ClassLibraryCs.Class1();
        // text1.Text = obj.Name;
        obj.Modify(text1);

    }

    private void Button_ClickFs(object sender, RoutedEventArgs e)
    {
        var obj = new ClassLibraryFs.Class1();
        // ここで実行時エラーになる
        obj.Modify(this.text1);
    }
}
```        

# 実験結果

C# の場合は、問題なく呼び出される。
F# の場合は、以下のようなエラーが出る。

```
ファイルまたはアセンブリ 'Windows, Version=255.255.255.255, Culture=neutral, PublicKeyToken=null'、またはその依存関係の 1 つが読み込めませんでした。指定されたファイルが見つかりません。":"Windows, Version=255.255.255.255, Culture=neutral, PublicKeyToken=null
```

おそらく、ClassLibraryFs で参照している Windows.winmd と本体の FsApp.Windows で参照している Windows.winmd とが異なっているためと思われる。あるいは、バージョンが 255.255.255.255 なところから、ClassLibraryFs 自体がまずいことになっているか。
適当なバージョンを alias してやればうまく動きそうな気もするのだが、いまいちよくわからん。


