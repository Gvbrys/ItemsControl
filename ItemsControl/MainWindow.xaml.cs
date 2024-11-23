using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace ListaGier
{
    public enum RodzajGry
    {
        Planszowa,
        Komputerowa,
        Karciana,
        Puzzle
    }

    public class Gra
    {
        public string Nazwa { get; set; }
        public RodzajGry Rodzaj { get; set; }

        public Gra(string nazwa, RodzajGry rodzaj)
        {
            Nazwa = nazwa;
            Rodzaj = rodzaj;
        }
    }

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = WczytajGryZPliku("gry.txt");
        }

        private List<Gra> WczytajGryZPliku(string sciezkaPliku)
        {
            var listaGier = new List<Gra>();
            try
            {
                if (File.Exists(sciezkaPliku))
                {
                    foreach (var linia in File.ReadAllLines(sciezkaPliku))
                    {
                        var czesci = linia.Split(',');
                        if (czesci.Length == 2 && Enum.TryParse(czesci[1].Trim(), out RodzajGry rodzaj))
                        {
                            listaGier.Add(new Gra(czesci[0].Trim(), rodzaj));
                        }
                    }
                }
                else
                {
                    //gry podstawowe jeśli plik gry.txt nie istnieje
                    listaGier.Add(new Gra("Monopoly", RodzajGry.Planszowa));
                    listaGier.Add(new Gra("Wiedźmin 3", RodzajGry.Komputerowa));
                    listaGier.Add(new Gra("Uno", RodzajGry.Karciana));
                    listaGier.Add(new Gra("Ravensburger 1000", RodzajGry.Puzzle));
                }
            }
            catch (Exception wyjatek)
            {
                MessageBox.Show($"Błąd podczas wczytywania gier: {wyjatek.Message}",
                              "Błąd",
                              MessageBoxButton.OK,
                              MessageBoxImage.Error);
            }
            return listaGier;
        }
    }
}