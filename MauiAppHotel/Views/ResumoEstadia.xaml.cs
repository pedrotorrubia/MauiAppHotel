using System.Globalization;
using System.Runtime.InteropServices;
using MauiAppHotel.Models;

namespace MauiAppHotel.Views;

public partial class ResumoEstadia : ContentPage
{
	Hospedagem dadosHospedagem;

	public ResumoEstadia(Hospedagem hospedagem)
	{
		InitializeComponent();

		this.dadosHospedagem = hospedagem;
		string formatoData = "dd/MM/yyyy";
		CultureInfo cultura = new CultureInfo ("pt-BR");

		lblQuarto.Text = dadosHospedagem.QuartoSelecionado.Descricao ?? "Quarto não informado.";
		lblCheckIn.Text = dadosHospedagem.DataCheckIn.ToString(formatoData);
		lblCheckOut.Text = dadosHospedagem.DataCheckOut.ToString(formatoData);
		lblPessoas.Text = $"{dadosHospedagem.QtdAdultos} Adultos, {dadosHospedagem.QtdCriancas} Crianças";
		lblDias.Text = dadosHospedagem.EstadiaDias.ToString();
		lblValorTotal.Text = dadosHospedagem.ValorTotal.ToString("C", cultura);
		
	}

    private async void btnConfirmar_Clicked(object sender, EventArgs e)
	{
		await DisplayAlert("Sucesso!", "Sua reserva foi confirmada!", "OK");
		await Navigation.PopAsync();
	}

	private async void btnVoltar_Clicked(object sender, EventArgs e)
	{
		await Navigation.PopAsync();
	}

}