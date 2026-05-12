var builder = WebApplication.CreateBuilder(args);

// --- 1. ADD SERVICES TO THE CONTAINER ---
builder.Services.AddControllers(); // Required for [ApiController] to work
builder.Services.AddEndpointsApiExplorer(); // Required for Swagger
builder.Services.AddSwaggerGen(); // Required to generate the Swagger document

var app = builder.Build();

// --- 2. CONFIGURE THE HTTP REQUEST PIPELINE ---
// Make sure these are OUTSIDE any 'if' statements for now to ensure they run
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers(); // Required to find your ProduceController

app.Run();
