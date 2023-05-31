using atividadeApp.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text.Json.Serialization;
using Windows.System;

namespace atividadeApp;

public partial class MainPage : ContentPage
{
	int count = 0;
    
    ObservableCollection<Atividade> atividades = new ObservableCollection<Atividade>();
    private readonly string urlBase = "https://localhost:7037/atividade";

    public MainPage()
	{
		InitializeComponent();
       
        

        myListView.ItemsSource = atividades;
        GetAtividades();
    }
   
        
        
    private async void GetAtividades()
    {
        var httpClient = new HttpClient();
       using var response = await httpClient.GetAsync(urlBase);

        var json = await response.Content.ReadAsStringAsync();

        List<Atividade> objs = JsonConvert.DeserializeObject<List<Atividade>>(json);

        objs.ForEach(x => atividades.Add(x));



    }    

    private async void NewClicked(object sender, EventArgs e)
	{
		count++;


        await Navigation.PushAsync(new CadastrarPage());
    }
    private void Deletar(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        var item = button.CommandParameter as Atividade;
        Debug.WriteLine(item.Id);
        Debug.WriteLine(item.Nome);
        Debug.WriteLine(item.DataFinalizacao);
        
       

    }
    private void Editar(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        
        var item = button.CommandParameter as Atividade;
        Debug.WriteLine("Editar ", item.Id);
        Debug.WriteLine(item.Id);
        Debug.WriteLine(item.Nome);
        Debug.WriteLine(item.DataFinalizacao);

    }
    private void Finalizar(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        var item = button.CommandParameter as Atividade;
        Debug.WriteLine("Finalizar ",item.Id);
        

    }

}

