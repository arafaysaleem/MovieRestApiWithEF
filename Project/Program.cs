using MovieRestApiWithEF.Extensions;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.ConfigureDbContext(configuration); // For initializing and injecting database provider
builder.Services.ConfigureJwtService(configuration); // For setting up jwt helpers and injecting it

var policyName = "CorsPolicy";
builder.Services.ConfigureCors(policyName); // For HTTP request control
builder.Services.ConfigureLoggerService(); // For logging all requests, responses and errors
builder.Services.ConfigureRepositoryManager(); // For initiating an IoC container with instances of all repositories

// Read here for understanding Authentication in .NET Core
// https://docs.microsoft.com/en-us/aspnet/core/security/authentication/?view=aspnetcore-6.0
builder.Services.AddJwtAuthentication(configuration); // For adding an authentication service along with a handler
builder.Services.AddRoleBasedAuthorization(); // For adding authorization to all controllers and setup role policies
builder.Services.AddAutoMapper(typeof(Program)); // For auto mapping of DTOs to Models and vice versa
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGenWithAuth(); // For adding swagger support

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); // For getting a swagger interactive documentation
    app.UseDeveloperExceptionPage(); // For unhandled exceptions
}
else
{
    app.UseHsts(); // Forces browser to communicate only over https
}

app.UseHttpsRedirection();

app.UseCors(policyName); // To enable cors using our previously configured policy

app.UseAuthentication(); // To add a middleware that validates requests using the initially added auth service

// To enable role based access to actions. Reads ClaimsPrinciple from previous middleware to check if authorized
app.UseAuthorization();

app.MapControllers(); // To map controller to routes

app.Run();
