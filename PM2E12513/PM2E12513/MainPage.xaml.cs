using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using PM2E12513.Controller;
using PM2E12513.Models;
using Plugin.Geolocator;
using Xamarin.Essentials;

namespace PM2E12513
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        public async void GetLocation()
        {
            Location Location = await Geolocation.GetLastKnownLocationAsync();

            if (Location == null)
            {
                Location = await Geolocation.GetLocationAsync(new GeolocationRequest
                {
                    DesiredAccuracy = GeolocationAccuracy.Medium,
                    Timeout = TimeSpan.FromSeconds(30)
                }); ;
            }

            txtlatitud.Text = Location.Latitude.ToString();
            txtlongitud.Text = Location.Longitude.ToString();


        } 

        

        private void btn01_Clicked(object sender, EventArgs e)
        {
            GetLocation();
        }

        private async void btn02_Clicked(object sender, EventArgs e)
        {
            // var ubicaciones = new Views.ubicacionesPage();
            // await NavigationPage.PushAsync(ubicaciones);
            await Navigation.PushAsync(new Views.ubicacionesPage());
        }

        private async void btnguardar_Clicked(object sender, EventArgs e)
        {
            try
            {
                var ubicaciones = new Models.Ubicaciones
                {
                    latitud = Convert.ToDouble(this.txtlatitud.Text),
                    longitud = Convert.ToDouble(this.txtlongitud.Text),
                    descripcion = this.txtdescripcion.Text,
                    DescripcionCorta = this.txtdcorta.Text,
                    

                };

                var resultado = await App.Basedatos02.GrabarUbicacion(ubicaciones);


                if (resultado == 1)
                {
                    await DisplayAlert("Mensaje", "Ingresada Exitosamente", "OK");
                }
                else
                {
                    await DisplayAlert("Mensaje", "Error, No se logro guardar Ubicacion", "OK");

                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Mensaje", ex.Message.ToString(), "OK");

            }
        }
    }
}
