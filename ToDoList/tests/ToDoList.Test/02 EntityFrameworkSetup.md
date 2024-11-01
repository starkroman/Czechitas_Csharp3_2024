Entity Framework Setup
Microsoft Article

❕Install Entity Framework CLI tools

dotnet tool install --global dotnet-ef

🦄 Make sure your Entity Framework CLI tools are properly installed

dotnet ef

Output should show a unicorn logo text art and your installed version

                 _/\__       
           ---==/    \\
     ___  ___   |.    \|\
    | __|| __|  |  )   \\\
    | _| | _|   \_/ |  //|\\
    |___||_|       /   \\\/\\

Entity Framework Core .NET Command-line Tools 8.0.7 <your version>

<Usage documentation follows, not shown.>
📦 Before you can use the tools on a specific project, you'll need to add the Microsoft.EntityFrameworkCore.Design package to it. Use NuGet manager or following command

 dotnet add package Microsoft.EntityFrameworkCore.Design
