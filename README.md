# CVBanken
!! Our old project >>>> https://github.com/MarcusNilsson93/Cv-portalen <<<<
we decided to create a new repo due to huge changes in the project structure. 

REQ: .net core https://dotnet.microsoft.com/download
      Entityframework: dotnet tool install --global dotnet-ef
      node + npm
>>!! Outdated the backend should now create a database based on seeded data once you run it. 
(I leave this here incase above does not work.) 
>>To setup the database: 
>>go to folder CVBanken.data folder 
>>      dotnet ef --startup-project ../CVBanken.Web  migrations add initial2
>>     dotnet ef --startup-project ../CVBanken.Web  database update


To run the backend: 
       go to the folder "CVBanken.Web" and type:
       dotnet run

to run the frontend open a new terminal and navigate to the folder 'cv-banken-vue' .
( if its the first time running it run : npm install before you run)
run npm run serve

The website should now be up on: http://localhost:8080/ 
(no longer possible to start the frontend from localhost:5001)

>>(outdated)The migrations folder will now be located under CVBanken.Data
The Database file "tempdata.db" is now located under CVBanken.Web

Remove these folders when changing branches to make sure the correct data is loaded. After you remove these folders please follow all the steps above. 
