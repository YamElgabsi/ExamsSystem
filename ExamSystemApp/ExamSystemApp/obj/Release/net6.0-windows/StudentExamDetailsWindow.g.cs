﻿#pragma checksum "..\..\..\StudentExamDetailsWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "46576495C89D76EBB7582A97B942F7668AB4FB35"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using ExamSystemApp;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace ExamSystemApp {
    
    
    /// <summary>
    /// StudentExamDetailsWindow
    /// </summary>
    public partial class StudentExamDetailsWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 28 "..\..\..\StudentExamDetailsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label examNameLbl;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\StudentExamDetailsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label teacherNameLbl;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\StudentExamDetailsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label minutesLbl;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\StudentExamDetailsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button beckBtn;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\StudentExamDetailsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button enterBtn;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.2.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/ExamSystemApp;component/studentexamdetailswindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\StudentExamDetailsWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.2.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.examNameLbl = ((System.Windows.Controls.Label)(target));
            return;
            case 2:
            this.teacherNameLbl = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.minutesLbl = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.beckBtn = ((System.Windows.Controls.Button)(target));
            
            #line 36 "..\..\..\StudentExamDetailsWindow.xaml"
            this.beckBtn.Click += new System.Windows.RoutedEventHandler(this.beckBtn_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.enterBtn = ((System.Windows.Controls.Button)(target));
            
            #line 37 "..\..\..\StudentExamDetailsWindow.xaml"
            this.enterBtn.Click += new System.Windows.RoutedEventHandler(this.enterBtn_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

