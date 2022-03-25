# HotelPrices

Olá,

Aplicação realizada utilizando SQL Server Express 2014 e .Net Core 3.1.

O projeto utiliza a ferramenta ChromeDriver para realizar o Web Scraper. Esta ferramenta se comunica com o navegador Google Chrome, assim, especialmente para executar a aplicação, anteriormente deve-se verificar se o Google Chrome encontra-se instalado na máquina e na versão 99 ou superior.

Antes de executar a aplicação também deve-se efetuar a configuração de conexão com a instância SQL Server no arquivo appsettings.json, mais precisamente na propriedade ConnectionStrings/HotelsConnection, e após executar o comando update-database no Package Manager Console do Visual Studio para a criação da base de dados.

Atenciosamente,
Guilherme Pinheiro
