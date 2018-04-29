using System;
using System.Windows;

namespace WindowsClassic {
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window {
		public MainWindow() {
			InitializeComponent();
			TextEngine.Instance.OnStart();
			TextEngine.Instance.OnWrite += OnWrite;
		}

		protected override void OnClosed(EventArgs e) {
			TextEngine.Instance.OnWrite -= OnWrite;
		}

		void OnSubmitButtonClick(object sender, RoutedEventArgs e) {
			var text = TextInput.Text;
			TextEngine.Instance.OnRead(text);
			TextInput.Clear();
		}

		void OnWrite(string msg) {
			TextView.Text += msg;
		}
	}
}
