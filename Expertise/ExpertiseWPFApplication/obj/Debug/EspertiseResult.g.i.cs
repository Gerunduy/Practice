﻿#pragma checksum "..\..\EspertiseResult.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "B3AEBAEE410ECED951159C915DC5BC21"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using ExpertiseWPFApplication;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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


namespace ExpertiseWPFApplication {
    
    
    /// <summary>
    /// EspertiseResult
    /// </summary>
    public partial class EspertiseResult : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 20 "..\..\EspertiseResult.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock tblWait;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\EspertiseResult.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid Grid;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\EspertiseResult.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid gHierarchy;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\EspertiseResult.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas cnvsHierarchy;
        
        #line default
        #line hidden
        
        
        #line 68 "..\..\EspertiseResult.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dgExpertiseResult;
        
        #line default
        #line hidden
        
        
        #line 70 "..\..\EspertiseResult.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid gSupport;
        
        #line default
        #line hidden
        
        
        #line 82 "..\..\EspertiseResult.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnSupport;
        
        #line default
        #line hidden
        
        
        #line 98 "..\..\EspertiseResult.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid gInsideStage1;
        
        #line default
        #line hidden
        
        
        #line 114 "..\..\EspertiseResult.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid gGraphic;
        
        #line default
        #line hidden
        
        
        #line 130 "..\..\EspertiseResult.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid gInsideStage2;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/ExpertiseWPFApplication;component/espertiseresult.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\EspertiseResult.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.tblWait = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 2:
            this.Grid = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            this.gHierarchy = ((System.Windows.Controls.Grid)(target));
            return;
            case 4:
            this.cnvsHierarchy = ((System.Windows.Controls.Canvas)(target));
            return;
            case 5:
            this.dgExpertiseResult = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 6:
            this.gSupport = ((System.Windows.Controls.Grid)(target));
            return;
            case 7:
            this.btnSupport = ((System.Windows.Controls.Button)(target));
            
            #line 82 "..\..\EspertiseResult.xaml"
            this.btnSupport.Click += new System.Windows.RoutedEventHandler(this.btnSupport_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.gInsideStage1 = ((System.Windows.Controls.Grid)(target));
            return;
            case 9:
            this.gGraphic = ((System.Windows.Controls.Grid)(target));
            return;
            case 10:
            this.gInsideStage2 = ((System.Windows.Controls.Grid)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
