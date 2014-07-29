﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace FsApp
{
    /// <summary>
    /// Frame 内へナビゲートするために利用する空欄ページ。
    /// </summary>
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
        // var obj = new ClassLibraryFs.Class1();
        // text1.Text = obj.Name;

        var obj = new ClassLibraryFs.Class1();
        // ここで実行時エラーになる
        obj.Modify(this.text1);
            
        // int ans = obj.Calc(10, 20);
        // text1.Text = ans.ToString();
    }
}
}
