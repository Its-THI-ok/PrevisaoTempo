using App.Models;
using App.Services;

namespace App;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

	private async void OnSearchClicked(object sender, EventArgs e)
	{
		try {
			if (!string.IsNullOrEmpty(EntradaCidade.Text))
			{
				var DataService = new DataService();
				var previsao = await DataService.GetPrevisao(EntradaCidade.Text);


				if (string.IsNullOrEmpty(previsao?.ErrorMessage))
				{
					// Criando e Setando o valor da cadeia de caracteres
					string resultado =
                                $"Descrição:\t\t {previsao?.Description}\n" +
                                $"Latitude:\t\t\t {previsao?.Lat}\n" +
								$"Longitude:\t\t {previsao?.Lon}\n" +
								$"Nascer do Sol:\t\t {previsao?.Sunrise:HH:mm}\n" +
								$"Pôr do Sol:\t\t {previsao?.Sunset:HH:mm}\n" +
								$"Temperatura Máxima:\t {(previsao?.Temp_max - 273.15):F1}°C\n" +
								$"Temperatura Mínima:\t {(previsao?.Temp_min - 273.15):F1}°C\n" +
								$"Velocidade do Vento:\t {previsao?.Speed}\n" +
								$"Umidade:\t\t\t {previsao?.Humidity}%\n" +
								$"Visibilidade:\t\t {previsao?.Visibility/1000}km";
					// Mostrando ao usuário
					await DisplayAlertAsync("Previsão do Tempo", resultado, "OK");
				}
				else if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
				{
					await DisplayAlertAsync("Alerta", $"Não foi possível obter a previsão do tempo para a cidade informada.\n" +
						$"Verifique o nome da cidade e tente novamente." +
						$"\n{(int?)previsao.HttpCode ?? -1}\n{previsao?.ErrorMessage ?? "Error Inesperado"}", "OK");
				} else
				{
					await DisplayAlertAsync("Alerta", $"Não foi possível obter a previsão do tempo para a cidade informada." +
						$"Verifique sua conexão com a internet e tente novamente.", "OK");
                }
			}
            } catch (Exception ex) {
				await DisplayAlertAsync("Erro", $"Ocorreu um erro ao obter a previsão do tempo: {ex.Message}", "OK");
        }
	}
}