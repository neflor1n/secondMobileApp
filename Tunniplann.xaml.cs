using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace secondMobileApp
{
    public partial class Tunniplann : ContentPage
    {
        public List<ContentPage> lehed = new List<ContentPage>
        {
            new TunniplaanPaev.Esmaspaev(1),
            new TunniplaanPaev.Teisipaev(2),
            new TunniplaanPaev.Kolmapaev(3),
            new TunniplaanPaev.Neljapaev(4),
            new TunniplaanPaev.Reede(5)
        };
        public List<String> tekstid = new List<string> 
        {
            "Esmaspäev",
            "Teisipäev",
            "Kolmapäev",
            "Neljapäev",
            "Reede"
        };
        public Tunniplann(int k)
        {
            
                
        }
    }
}
