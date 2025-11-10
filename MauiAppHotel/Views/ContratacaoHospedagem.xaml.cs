using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MauiAppHotel.Models;
using MauiAppHotel.Views;

namespace MauiAppHotel.Views;

public partial class ContratacaoHospedagem : ContentPage
{
	App PropriedadesApp;

	public ContratacaoHospedagem()
	{
		InitializeComponent();

		PropriedadesApp = (App)Application.Current!;

		if(PropriedadesApp.lista_quartos != null )
		{
			pck_quarto.ItemsSource = PropriedadesApp.lista_quartos;
		}
		else
		{
			DisplayAlert("Erro", "Não foi possível carregar a lista de quartos.", "OK");
		}

		DateTime hoje = DateTime.Now;
		DateTime dataMaximaCheckIn = hoje.AddMonths(1);
		     
        dtpck_checkin.MinimumDate = hoje;
		dtpck_checkin.MaximumDate = dataMaximaCheckIn;
		
		dtpck_checkout.MinimumDate = hoje.AddDays(1);
		dtpck_checkout.MaximumDate = dataMaximaCheckIn.AddDays(30);
		dtpck_checkout.Date = hoje.AddDays(1);
	}
	private void dtpck_checkin_DateSelected(object sender, DateChangedEventArgs e)
	{
		DateTime dataCheckinSelecionada = e.NewDate;
		DateTime dataMinimaCheckout = dataCheckinSelecionada.AddDays(1);
		
		dtpck_checkout.MinimumDate = dataMinimaCheckout;

		DateTime dataMaximaCheckOut = dataCheckinSelecionada.AddDays(30);
		dtpck_checkout.MaximumDate = dataMaximaCheckOut;

		if (dtpck_checkout.Date < dataMinimaCheckout)
		{
			dtpck_checkout.Date = dataMinimaCheckout;
		}
	}

	private void dtpck_checkout_DateSelected(object sender, DateChangedEventArgs e)
	{
		DateTime dataCheckIn = dtpck_checkin.Date;
		DateTime dataCheckOut = e.NewDate;

		DateTime dataMinimaCheckOutPermitida = dataCheckIn.AddDays(1);
		DateTime dataMaximaCheckOutPermitida = dataCheckIn.AddDays(30);

		if (dataCheckOut < dataMinimaCheckOutPermitida)
		{
			DisplayAlert ("Data Inválida", "A data de check-out deve ser pelo menos um dia depois do check-in.", "OK");
			dtpck_checkout.Date = dataMinimaCheckOutPermitida;
			return;
		}

		if (dataCheckOut > dataMaximaCheckOutPermitida)
		{
			DisplayAlert("Data Inválida", "O tempo de estadia não pode ser maior que 30 dias.", "OK");
			dtpck_checkout.Date = dataMaximaCheckOutPermitida;
		}

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

    private async void Button_Clicked(object sender, EventArgs e)
    {
		try
		{
			if (pck_quarto.SelectedItem == null)
			{
				await DisplayAlert("Ops!", "Por favor, selecione um quarto.", "OK");
				return;
			}

			if (stp_adultos.Value == 0)
			{
				await DisplayAlert("Ops!", "Pelo menos 1 adulto deve ser selecionado.", "OK");
				return;
			}

			TimeSpan diff = dtpck_checkout.Date.Subtract(dtpck_checkin.Date);
			int diasEstadia = diff.Days;

			Quarto quartoSelecionado = (Quarto)pck_quarto.SelectedItem!;

			double diaria_adultos = quartoSelecionado.ValorDiariaAdulto * stp_adultos.Value;
			double diaria_criancas = quartoSelecionado.ValorDiariaCrianca * stp_criancas.Value;

			double valor_total_estadia = (diaria_adultos + diaria_criancas) * diasEstadia;

			Hospedagem nova_hospedagem = new Hospedagem()
			{
				QuartoSelecionado = quartoSelecionado,
				QtdAdultos = (int)stp_adultos.Value,
				QtdCriancas = (int)stp_criancas.Value,
				DataCheckIn = dtpck_checkin.Date,
				DataCheckOut = dtpck_checkout.Date,
				EstadiaDias = diasEstadia,
				ValorTotal = valor_total_estadia

			};

			await Navigation.PushAsync(new ResumoEstadia(nova_hospedagem));
		}

		catch (Exception ex)
		{
			await DisplayAlert("Ops!", "Ocorreu um erro!", "OK");
		}

    }
}