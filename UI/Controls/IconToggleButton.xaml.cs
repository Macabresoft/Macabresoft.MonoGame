﻿namespace Macabre2D.UI.Controls {

    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Shapes;

    public partial class IconToggleButton : UserControl {

        public static readonly DependencyProperty CollapsedIconProperty = DependencyProperty.Register(
            nameof(CollapsedIcon),
            typeof(Path),
            typeof(IconToggleButton),
            new PropertyMetadata());

        public static readonly DependencyProperty IsToggledProperty = DependencyProperty.Register(
            nameof(IsToggled),
            typeof(bool),
            typeof(IconToggleButton),
            new PropertyMetadata(false));

        public static readonly DependencyProperty UncollapsedIconProperty = DependencyProperty.Register(
            nameof(UncollapsedIcon),
            typeof(Path),
            typeof(IconToggleButton),
            new PropertyMetadata());

        public IconToggleButton() {
            this.InitializeComponent();
        }

        public Path CollapsedIcon {
            get { return (Path)this.GetValue(CollapsedIconProperty); }
            set { this.SetValue(CollapsedIconProperty, value); }
        }

        public bool IsToggled {
            get { return (bool)this.GetValue(IsToggledProperty); }
            set { this.SetValue(IsToggledProperty, value); }
        }

        public Path UncollapsedIcon {
            get { return (Path)this.GetValue(UncollapsedIconProperty); }
            set { this.SetValue(UncollapsedIconProperty, value); }
        }
    }
}