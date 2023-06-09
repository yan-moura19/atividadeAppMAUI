﻿using atividadeApp.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
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
        atividades.Clear();
        objs.ForEach(x => atividades.Add(x));



    }    

    private async void NewClicked(object sender, EventArgs e)
	{
		count++;


        await Navigation.PushAsync(new CadastrarPage());
    }
    private async void Deletar(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        var item = button.CommandParameter as Atividade;
        Debug.WriteLine(item.Id);
        Debug.WriteLine(item.Nome);
        Debug.WriteLine(item.DataFinalizacao);
        using var client = new HttpClient();
         // URL do recurso que você deseja excluir

        HttpResponseMessage response = await client.DeleteAsync(urlBase + "/" +item.Id);

        if (response.IsSuccessStatusCode)
        {
            await DisplayAlert("sucesso","Recurso excluído com sucesso!","ok");
            GetAtividades();
        }
        else
        {
            await DisplayAlert("erro", $"Ocorreu um erro ao excluir o recurso. Código de status: {response.StatusCode}", "ok");
        }



    }
    private async void Editar(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        
        var item = button.CommandParameter as Atividade;
        Debug.WriteLine("Editar ", item.Id);
        Debug.WriteLine(item.Id);
        Debug.WriteLine(item.Nome);
        Debug.WriteLine(item.DataFinalizacao);
        await Navigation.PushAsync(new Editar(item.Id));

    }
    private async void Finalizar(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        var item = button.CommandParameter as Atividade;
        Debug.WriteLine("Finalizar ",item.Id);
        var data = new
        {

            
        };
        var json = JsonConvert.SerializeObject(data);
        var content = new StringContent(string.Empty, Encoding.UTF8, "application/json");
        try
        {
            var httpClient = new HttpClient();
            Debug.WriteLine("https://localhost:7037/finalizar/" + item.Id);
            var response = await httpClient.PutAsync("https://localhost:7037/finalizar/" + item.Id, content);

            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("Sucesso", "Atividade finalizada", "Ok");
               
                await Navigation.PushAsync(new MainPage());
            }
            else
            {
                await DisplayAlert("Failed", $"Ocorreu um erro na atualização. Código de status: {response.StatusCode}", "Ok");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Ocorreu um erro ao fazer a solicitação PUT: {ex.Message}", "OK");
        }


    }

}

