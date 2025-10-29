using System.Collections.ObjectModel;

namespace MauiAppHotel.Views;

public class Suite
{
	public string Nome { get; set; } = string.Empty;
	public double DiariaAdulto { get; set; }
	public double DiariaCrianca { get; set; }
}

public partial class ContratacaoHospedagem : ContentPage
{
	private ObservableCollection<Suite> lista_suites = new ObservableCollection<Suite>();
	public ContratacaoHospedagem()
	{
		InitializeComponent();

		PreencherPicker();

		dtpck_checkin.MinimumDate = DateTime.Now;
		dtpck_checkout.MaximumDate = DateTime.Now.AddDays(180);
	}

	private void PreencherPicker()
	{
		lista_suites.Clear();

		lista_suites.Add(new Suite()
		{
			Nome = "Suíte Super Luxo",
			DiariaAdulto = 250.0,
			DiariaCrianca = 125.0
		});

        lista_suites.Add(new Suite()
        {
            Nome = "Suíte Luxo",
            DiariaAdulto = 210.0,
            DiariaCrianca = 105.0
        });

        lista_suites.Add(new Suite()
        {
            Nome = "Suíte Single",
            DiariaAdulto = 180.0,
            DiariaCrianca = 90.0
        });        

        lista_suites.Add(new Suite()
        {
            Nome = "Suíte Crise",
            DiariaAdulto = 90.0,
            DiariaCrianca = 45.0
        });

		pck_quarto.ItemsSource = lista_suites;

		pck_quarto.ItemDisplayBinding = new Binding("Nome");
    }

	private void dtpck_checkin_DateSelected(object sender, DateChangedEventArgs e)
	{
		dtpck_checkout.MinimumDate = DateTime.Now.AddDays(1);
		dtpck_checkout.Date = e.NewDate.AddDays(1);
	}
	
	private async void btn_sobre_Clicked(object sender, EventArgs e)
	{
		try
		{
			await Navigation.PushAsync(new Sobre());
		}
		catch (Exception ex)
		{
			await DisplayAlert("Ops!", "Ocorreu um erro ao tentar navegar: " + ex.Message, "OK");
		}
	}
}