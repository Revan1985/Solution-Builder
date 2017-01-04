using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace SolutionBuilder.WPF
{
  public class TextBoxBehaviour
  {
    static readonly Dictionary<TextBox, Capture> _associations = new Dictionary<TextBox, Capture>();

    public static Boolean GetScrollOnTextChanged(DependencyObject dependencyObject)
    {
      return (Boolean)dependencyObject.GetValue(ScrollOnTextChangedProperty);
    }

    public static void SetScrollOnTextChanged(DependencyObject dependencyObject, Boolean value)
    {
      dependencyObject.SetValue(ScrollOnTextChangedProperty, value);
    }

    public static readonly DependencyProperty ScrollOnTextChangedProperty = DependencyProperty.RegisterAttached("ScrollOnTextChanged", typeof(Boolean), typeof(TextBoxBehaviour), new UIPropertyMetadata(false, OnScrollOnTextChanged));

    static void OnScrollOnTextChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
    {
      var textBox = dependencyObject as TextBox;
      if (textBox == null) { return; }

      Boolean oldValue = (Boolean)e.OldValue;
      Boolean newValue = (Boolean)e.NewValue;
      if (newValue == oldValue) { return; }

      if (newValue)
      {
        textBox.Loaded += TextBoxLoaded;
        textBox.Unloaded += TextBoxUnloaded;
      }
      else
      {
        textBox.Loaded -= TextBoxLoaded;
        textBox.Unloaded -= TextBoxUnloaded;
        if (_associations.ContainsKey(textBox))
        {
          _associations[textBox].Dispose();
        }
      }
    }

    static void TextBoxUnloaded(Object sender, RoutedEventArgs routedEventArgs)
    {
      TextBox textBox = (TextBox)sender;
      _associations[textBox].Dispose();
      textBox.Unloaded -= TextBoxUnloaded;
    }

    static void TextBoxLoaded(Object sender, RoutedEventArgs routedEventArgs)
    {
      TextBox textBox = (TextBox)sender;
      textBox.Loaded -= TextBoxLoaded;
      _associations[textBox] = new Capture(textBox);
    }

    class Capture : IDisposable
    {
      private TextBox TextBox { get; set; }

      public Capture(TextBox textBox)
      {
        TextBox = textBox;
        TextBox.TextChanged += OnTextBoxOnTextChanged;
      }
      ~Capture()
      {
        Dispose(false);
      }

      private void OnTextBoxOnTextChanged(Object sender, TextChangedEventArgs args)
      {
        TextBox.ScrollToEnd();
      }

      public void Dispose()
      {
        Dispose(true);
        GC.SuppressFinalize(this);
      }
      private void Dispose(Boolean disposeManaegedResources)
      {
        if (disposeManaegedResources)
        {
          TextBox.TextChanged -= OnTextBoxOnTextChanged;
        }
      }
    }
  }
}