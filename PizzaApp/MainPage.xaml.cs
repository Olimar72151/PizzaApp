using Newtonsoft.Json;
using PizzaApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PizzaApp
{
    public partial class MainPage : ContentPage
    {
        //List<Pizza> pizzas;

        public MainPage()
        {
            InitializeComponent();

            

            //string pizzasJson = "";
            listView.RefreshCommand = new Command((obj) =>
            {
                DownloadData();

                
            });

            listView.IsVisible = false;
            waitLayout.IsVisible = true;

            DownloadData();



            //pizzas = JsonConvert.DeserializeObject<List<Pizza>>(pizzasJson);

            //pizzas = new List<Pizza>();
            /*
            pizzas.Add(new Pizza { nom = "végétarienne",prix=7, ingredients = new string[] {"tomate", "poivrons", "oignons"}, imageUrl = "https://img.cuisineaz.com/660x660/2013/12/20/i18445-margherite.jpeg" });
            pizzas.Add(new Pizza { nom = "montagnarde", prix = 17, ingredients = new string[] { "tomate", "poivrons", "oignons", "fromage", "lardon" }, imageUrl = "https://cdn.shopify.com/s/files/1/0408/5655/1588/articles/Margherita-9920.jpg?crop=center&height=800&v=1644590066&width=800" });
            pizzas.Add(new Pizza { nom = "carnivore", prix = 14, ingredients = new string[] { "tomate", "poivrons", "oignons", "lardon", "steak haché", "saucisse" }, imageUrl = "https://www.toutlevin.com/img/a0fc4ba355969aa491841847eb63dcc1004740003000-960.jpg" });
            */
            //listView.ItemsSource = pizzas;
        }


        public void DownloadData()
        {
            const string URL = "https://drive.google.com/uc?export=download&id=1jYPojvFu04HQRfl_WXT3bg3zQ1Bw4fSw";

            using (var webclient = new WebClient())
            {
                try
                {
                    //pizzasJson = webclient.DownloadString(URL);

                    webclient.DownloadStringCompleted += (object sender, DownloadStringCompletedEventArgs e) =>
                    {
                        //Console.WriteLine("Donnée téléchargées: " + e.Result);

                        string pizzasJson = e.Result;

                        List<Pizza> pizzas = JsonConvert.DeserializeObject<List<Pizza>>(pizzasJson);

                        Device.BeginInvokeOnMainThread(() =>
                        {
                            listView.ItemsSource = pizzas;

                            listView.IsVisible = true;
                            waitLayout.IsVisible = false;
                            listView.IsRefreshing = false;
                        });

                    };

                    webclient.DownloadStringAsync(new Uri(URL));
                }
                catch (Exception ex)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        DisplayAlert("Erreur", "Une erreur réseau s'est produite : " + ex.Message, "OK");
                    });

                    return;

                }

            }
        }
    }
}
