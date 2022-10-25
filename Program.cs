using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

SecurityKey key = new SymmetricSecurityKey(
                        System.Text.Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Password"])//"ysa868gOND7rwkHG5Z^a2rEyoi&")
                      );

builder.Services.AddAuthentication(authOptions => {
    authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    authOptions.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(bearerOptions => {
    bearerOptions.SaveToken = true;
    bearerOptions.RequireHttpsMetadata = false;

    var paramsValidation = bearerOptions.TokenValidationParameters;
    
    paramsValidation.IssuerSigningKey = key;
    paramsValidation.ValidateAudience = true;
    paramsValidation.ValidateIssuer = true;
    paramsValidation.ValidIssuer = "http://localhost:5043";    
    paramsValidation.ValidAudience = "http://localhost:5043";

    paramsValidation.ValidateIssuerSigningKey = true;
    paramsValidation.ValidateLifetime = true;

    paramsValidation.ClockSkew = TimeSpan.Zero;
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
        .RequireAuthenticatedUser().Build());
    options.AddPolicy("admin", policy => policy.RequireRole("manager"));
    options.AddPolicy("employee", policy => policy.RequireRole("employee"));
});


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Use(async (context, next) =>
{    
    //await next.Invoke();   
    Console.WriteLine(context.GetEndpoint());
    await next(context);
});

app.Run();