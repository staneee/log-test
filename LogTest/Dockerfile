#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM hub.gct-china.com/staneee/aspnet:5-focal-gdi-fontconfig AS base
WORKDIR /app
EXPOSE 80

FROM hub.gct-china.com/staneee/dotnet:5-focal AS build
WORKDIR /src
COPY ["LogTest/LogTest.csproj", "LogTest/"]
RUN dotnet restore "LogTest/LogTest.csproj"
COPY . .
WORKDIR "/src/LogTest"
RUN dotnet build "LogTest.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LogTest.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LogTest.dll"]