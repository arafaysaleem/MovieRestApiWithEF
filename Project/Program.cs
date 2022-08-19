using Contracts;
using Microsoft.AspNetCore.HttpOverrides;
using MovieRestApiWithEF.Extensions;
using Repositories;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.ConfigureDbContext(configuration); // for initializing and injecting database provider

var policyName = "CorsPolicy";
builder.Services.ConfigureCors(policyName); // for HTTP request control
builder.Services.ConfigureIISIntegration(); // for server configuration
builder.Services.ConfigureLoggerService(); // for logging all requests, responses and errors
builder.Services.ConfigureRepositoryManager(); // for initiating an IoC container with instances of all repositories

builder.Services.AddAutoMapper(typeof(Program)); // for auto mapping of DTOs to Models and vice versa
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); // for adding swagger support

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); // for getting a swagger interactive documentation
    app.UseDeveloperExceptionPage(); // for unhandled exceptions
}
else
{
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.All
}); // for forwarding headers from a proxy to the main server

app.UseCors(policyName); // to enable cors using our previously configured policy

app.UseAuthorization(); // to enable role based access to actions

app.MapControllers(); // to map controller to routes

app.Run();
