using atividadeApp.Models;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Runtime.Intrinsics.Arm;
using System.Text;

namespace atividadeApp;

public partial class Editar : ContentPage
{
    Atividade edit;
    private readonly string urlBase = "https://localhost:7037/atividade";
    string IdAtividade;
    public Editar(string idAtividade)
	{
		InitializeComponent();
        GetAtividade();
        IdAtividade = idAtividade;
        
	}

	public async void GetAtividade()
    {
        var httpClient = new HttpClient();
        using var response = await httpClient.GetAsync(urlBase + '/'+ IdAtividade);

        var json = await response.Content.ReadAsStringAsync();
        

        

        List<Atividade> objs = JsonConvert.DeserializeObject<List<Atividade>>(json);
        edit = objs.FirstOrDefault((a => a.Id == IdAtividade));
        entNome.Text = edit.Nome;


    }
    public async void EditarClicked(object sender, EventArgs e)
	{
        Debug.WriteLine(entNome.Text);
        var httpClient = new HttpClient();
        var data = new
            {
                
                nome = entNome.Text,
                status = edit.Status,
                dataCadastro = edit.DataCadastro,
                dataFinalizacao = edit.DataFinalizacao
            };
        var json = JsonConvert.SerializeObject(data);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        try
        {
            var response = await httpClient.PutAsync(urlBase + '/' + IdAtividade, content);

            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("Sucesso", "Atualização bem-sucedida","Ok");
                entNome.Text = "";
                await Navigation.PushAsync(new MainPage());
            }
            else
            {
                await DisplayAlert("Failed",$"Ocorreu um erro na atualização. Código de status: {response.StatusCode}","Ok");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error",$"Ocorreu um erro ao fazer a solicitação PUT: {ex.Message}","OK");
        }
    }
}