# Estágio 1: Build da aplicação
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia o arquivo de projeto apontando para a subpasta onde ele realmente está
COPY ["API_WHATSAPP_SELLER/API_WHATSAPP_SELLER.csproj", "API_WHATSAPP_SELLER/"]
RUN dotnet restore "API_WHATSAPP_SELLER/API_WHATSAPP_SELLER.csproj"

# Copia todo o restante do código do repositório
COPY . .
RUN dotnet publish "API_WHATSAPP_SELLER/API_WHATSAPP_SELLER.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Estágio 2: Imagem final para execução (mais leve)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Define a porta padrão que o Render exige
ENV ASPNETCORE_URLS=http://+:8080

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "API_WHATSAPP_SELLER.dll"]
