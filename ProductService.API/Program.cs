var builder = WebApplication.CreateBuilder(args);

// add Data access layer and business logic layer
builder.Services.AddDataAccessLayer(builder.Configuration);
builder.Services.AddBusinessLayer();

builder.Services.AddControllers();

// add fluent validation
builder.Services.AddFluentValidationAutoValidation();


// add swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Cors
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});


var app = builder.Build();

app.UseExceptionHandlingMiddleware();
app.UseRouting();

// cors
app.UseCors();

// add swagger
app.UseSwagger();
app.UseSwaggerUI();

// auth
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapProductAPIEndpoints();

app.Run();
