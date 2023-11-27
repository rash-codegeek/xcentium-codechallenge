# xcentium-codechallenge (Project Demo)
Sitecore coding challenge for Xcentium

# Objective
To build a small user-friendly website that allows the user to type a URL into an input box and then do the following:
1. List all images from the target URL and show them to the user in a carousel or gallery control.
2. Count all the words (display the total) and display the top 10 occurring words and their count.

# Functionalities Used
1. ASP.NET MVC Framework 6.0
2. Dependency Injection
3. HTML Agility Pack to retrieve HTML content from a given web page asynchronously
4. Implemented INMEMORY caching for faster access to data
5. Implemented LAZY LOADING of the images on the partial view using INTERSECTION OBSERVER
6. Performed validations and the exceptions were handled
7. Added test cases using Moq

# Tools & Frameworks
1. ASP .NET MVC Framework 6.0
2. Microsoft Visual Studio Community 2022 Version 17.7.5

# Dependencies
The solution requires the below dependencies to be installed for the successful compilation 
1. (NuGet Packages) HTML AGILITY PACK version 1.11.54
2. (NuGet Packages) MOQ version 4.20.69

# Solution Steps:
1. Github Link -  https://github.com/rash-codegeek/xcentium-codechallenge.git
2. Clone Repository to your local path - <> Code -> Click on Open with Visual Studio -> select local path to clone this git repository
3. Install Dependencies through NuGet Package Manager
   Click on Tools in MS Visual Studio -> NuGet Package Manager -> Package Manager Console 
   Once the console prompts, run "dotnet restore" to install all necessary NuGet packages
4. Build & Run (Fn+F5)
