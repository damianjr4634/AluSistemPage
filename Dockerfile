FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR webapp

#EXPOSE 5001
EXPOSE 5000

#COPY PROJECT FILE
COPY ./*.csproj ./
RUN dotnet restore

#copy everyhing else
COPY . .
RUN dotnet publish -c Release -o out

# libgdi plus es para el fast-report
RUN apt update -y
RUN apt install -y software-properties-common
RUN add-apt-repository -y universe
RUN apt update -y
RUN apt install -y libgdiplus

#build image
FROM mcr.microsoft.com/dotnet/sdk:6.0-focal
WORKDIR /webapp
COPY --from=build /webapp/out .
ENTRYPOINT ["dotnet", "EsbaBlazorAppAuth.dll"]
