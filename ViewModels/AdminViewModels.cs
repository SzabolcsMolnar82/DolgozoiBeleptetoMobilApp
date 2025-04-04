﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using DolgozoiBeleptetoMobilApp.Models;

namespace DolgozoiBeleptetoMobilApp.ViewModels
{
    public partial class AdminViewModel : ObservableObject
    {
        [ObservableProperty] private ObservableCollection<DolgozoDto> dolgozok = new();
        [ObservableProperty] private string ujNev;
        [ObservableProperty] private string ujFelhasznaloNev;
        [ObservableProperty] private string ujJelszo;


        public AdminViewModel()
        {
            LoadDolgozok();
        }

        public IRelayCommand RefreshCommand => new RelayCommand(LoadDolgozok);

        public IRelayCommand<int> DeleteCommand => new RelayCommand<int>(async (id) =>
        {
            var response = await new HttpClient().DeleteAsync($"{ApiConstants.BaseUrl}/api/dolgozo/{id}");
            if (response.IsSuccessStatusCode)
            {
                var torlendo = Dolgozok.FirstOrDefault(d => d.Id == id);
                if (torlendo != null) Dolgozok.Remove(torlendo);
            }
        });

        private async void LoadDolgozok()
        {
            var client = new HttpClient();
            var result = await client.GetFromJsonAsync<List<DolgozoDto>>($"{ApiConstants.BaseUrl}/api/dolgozo");
            if (result != null)
                Dolgozok = new ObservableCollection<DolgozoDto>(result);
        }

        public IRelayCommand AddCommand => new RelayCommand(async () =>
        {
            var dolgozo = new
            {
                Nev = UjNev,
                FelhasznaloNev = UjFelhasznaloNev,
                Jelszo = UjJelszo
            };

            var response = await new HttpClient().PostAsJsonAsync($"{ApiConstants.BaseUrl}/api/dolgozo", dolgozo);
            if (response.IsSuccessStatusCode)
            {
                UjNev = UjFelhasznaloNev = UjJelszo = string.Empty;
                LoadDolgozok();
            }
        });


    }

}

