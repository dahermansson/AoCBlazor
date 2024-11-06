using AoC.Solvers.Extensions;
using AoC.Web.Components;
using AoC.InputHandling.Extensions;
using Microsoft.AspNetCore.Components;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureInputHandler().ConfiguresolversManager();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents().AddCircuitOptions(c => c.DetailedErrors = true);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    
}

// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
app.UseHsts();

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

app.Run();
