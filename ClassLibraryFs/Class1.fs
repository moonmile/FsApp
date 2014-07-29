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
        
