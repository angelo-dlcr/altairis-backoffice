FROM mcr.microsoft.com/dotnet/sdk:10.0 AS development
WORKDIR /backend

COPY *.slnx ./
COPY src/Altairis.Api/*.csproj ./src/Altairis.Api/
COPY src/Altairis.Application/*.csproj ./src/Altairis.Application/
COPY src/Altairis.Domain/*.csproj ./src/Altairis.Domain/
COPY src/Altairis.Infrastructure/*.csproj ./src/Altairis.Infrastructure/

RUN dotnet restore

COPY . .

WORKDIR /backend/src/Altairis.Api

EXPOSE 5212

CMD ["dotnet", "watch", "run", "--urls", "http://0.0.0.0:5212"]
