using System.Data;
using System.Net.Http.Json;

namespace atividadeApp;

public partial class CadastrarPage : ContentPage
{
    private readonly string urlBase = "https://localhost:7037/atividade";
    public CadastrarPage()
	{
		InitializeComponent();
	}
    private async void SubmitClicked(object sender, EventArgs e)
	{
        try
        {
            var httpClient = new HttpClient();
            
            var data = new
            {
                
                nome = entNome.Text,
                status = "Em andamento",
                dataCadastro = DateTime.Now.ToUniversalTime(),
                dataFinalizacao = DateTime.MinValue.ToUniversalTime()
            };
            Console.WriteLine(data.ToString());

            var response = await httpClient.PostAsJsonAsync(urlBase, data);

            response.EnsureSuccessStatusCode();

            await DisplayAlert("Sucesso", "Post enviado com sucesso!", "OK");
            entNome.Text = "";
            await Navigation.PushAsync(new MainPage());
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", ex.Message, "OK");
        }

    }
}