// See https://aka.ms/new-console-template for more information

using Skovorodki;

var connection = new SkovorodkiConnection();

var skovorodki = connection.GetSkovorodki();

var brands = connection.GetBrand();


using var database = new SkovorodkiDatabase("skovorodki.db");

database.AddProdusers(brands);

database.AddSkovorodki(skovorodki,brands);