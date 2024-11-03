# Advent of Code

[https://aoc.azurewebsites.net](https://aoc.azurewebsites.net)

# Input download

To run the website in Azure this project contains a downloader for Input data. It will save the inputs in a local folder if configured so, the CLI is doing that by default. The web too in Development ("ASPNETCORE_ENVIRONMENT": "Development"). In production the web assumes it can use Azure FileStorage and will download Input on request and save in Azure Storage. 

To get the downloader to work both locally and i Azure the 'AOC_SESSION' must be set as an environment variable. 