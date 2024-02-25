using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace BMI_Calculator
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            SetCustomTitleBar();
        }


        private void SetCustomTitleBar()
        {
            // Verstecke die Standard-Titelleiste
            WindowChrome.SetWindowChrome(this, new WindowChrome { CaptionHeight = 0 });

            // Füge deine benutzerdefinierte Titelleiste hinzu
            var appTitleBar = new Border
            {
                Background = Brushes.Gray,
                Height = 32,
                VerticalAlignment = VerticalAlignment.Top,
                Child = new TextBlock { Text = "App title", VerticalAlignment = VerticalAlignment.Center, Margin = new Thickness(28, 0, 0, 0) }
            };
            Grid.SetRow(appTitleBar, 0);
        }


        private void AppTitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                // Wenn die linke Maustaste gedrückt wurde, verschiebe das Fenster
                this.DragMove();
            }
        }


        private void FensterSchließenButton_Gegklickt(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void GewichtBestimmen(object sender, RoutedEventArgs e)
        {
            // Den Sender in einen Button umwandeln
            Button button = sender as Button;

            // Den Namen des Buttons auslesen
            string buttonName = button.Name;

            // Überprüfen, ob der Cast erfolgreich war
            if (button != null)
            {
                ButtonGeklickt(buttonName);        
            }
        }


        private void ButtonGeklickt( string buttonName)
        {
            if (!AktuellesGewichtAuslesen(out double currentValue))
            {
                return;
            }

            switch (buttonName)
            {
                case "MinusGroß":
                    currentValue = currentValue - 1.0;
                    break;
                case "MinusKlein":
                    currentValue = currentValue - 0.1;
                    break;
                case "PlusKlein":
                    currentValue = currentValue + 0.1;
                    break;
                case "PlusGroß":
                    currentValue = currentValue + 1.0;
                    break;
                default:
                    break;
            }

            if (currentValue < 0)
            {
                currentValue = 0;
            }
            double currentBmi = currentValue / (slider.Value / 100 * (slider.Value / 100));
            eingestelltesGewicht.Content = currentValue.ToString();
            BMILabel.Content = Math.Round(currentBmi, 1).ToString();
        }

        
        private void SliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (!AktuellesGewichtAuslesen(out double currentValue))
            {
                return;
            }

            double currentBmi = currentValue / (slider.Value / 100 * (slider.Value / 100));
            BMILabel.Content = Math.Round(currentBmi, 1).ToString();
        }


        private bool AktuellesGewichtAuslesen(out double currentValue)
        {
            if (!double.TryParse(eingestelltesGewicht.Content.ToString(), out currentValue))
            {
                // Fehlermeldung in der Konsole ausgeben, wenn die Konvertierung fehlschlägt
                Console.WriteLine("Fehler: Konnte das Gewicht nicht in eine Zahl konvertieren.");
                return false;
            }
            return true;
        }
    }   
}
