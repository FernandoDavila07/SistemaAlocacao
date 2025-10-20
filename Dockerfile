# --- Estágio 1: Build (Construção) ---
# Usamos a imagem oficial do .NET 8 SDK (que tem todas as ferramentas de build)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia os arquivos de projeto (.csproj) e o arquivo de solução (.sln)
# Fazemos isso primeiro para que o Docker use o cache se os pacotes não mudarem
COPY *.sln .
COPY API/API.csproj API/
COPY Application/Application.csproj Application/
COPY Domain/Domain.csproj Domain/
COPY Infrastructure/Infrastructure.csproj Infrastructure/

# Restaura todos os pacotes NuGet da solução
RUN dotnet restore

# Copia todo o resto do código-fonte da solução
COPY . .

# Publica a API em modo "Release" (otimizado) e joga o resultado na pasta /app/publish
# (Note que usamos o caminho do .csproj da API)
RUN dotnet publish "API/API.csproj" -c Release -o /app/publish

# --- Estágio 2: Final (Execução) ---
# Começamos com uma imagem limpa e leve, que só tem o necessário para RODAR a API
# (Note que é "aspnet" e não "sdk")
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
    
# Copia os arquivos publicados (do estágio 'build') para a imagem final
COPY --from=build /app/publish .

# Informa ao Docker que a API vai rodar na porta 8080
# (Imagens ASP.NET padrão são configuradas para porta 8080)
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

# O comando que será executado quando o container iniciar
# (API.dll é o nome do seu projeto API. Se o seu projeto tiver outro nome, troque aqui)
ENTRYPOINT ["dotnet", "API.dll"]