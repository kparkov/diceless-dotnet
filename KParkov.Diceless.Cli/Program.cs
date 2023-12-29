// See https://aka.ms/new-console-template for more information

using Cocona;
using KParkov.Diceless.Dice;

var builder = CoconaApp.CreateBuilder();

var app = builder.Build();

app.Run(() =>
{
    var dicer = Dicer.System;
    var pool = dicer.Pool() 
               + dicer.D(6) 
               + dicer.D(6) 
               + dicer.D(6);
    
    Console.WriteLine(pool.Sum().ToString());
    Console.WriteLine(string.Join(", ", pool.NumericalValues));
});