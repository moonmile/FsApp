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
