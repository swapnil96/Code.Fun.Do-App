﻿

#pragma checksum "C:\Users\suyash1212\Desktop\Colour_mania_start\App2\App2.Windows\Instructions.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "25888CAEA04BB965FB59AA035F8C19DE"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace App2
{
    partial class Instructions : global::Windows.UI.Xaml.Controls.Page, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 28 "..\..\Instructions.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).PointerPressed += this.next_page;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 29 "..\..\Instructions.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).PointerPressed += this.prev_page;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}


