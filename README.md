# SMALL-SCALE PROJECT MANAGEMENT SYSTEM 




### Installation


1. Clone the repo
   ```sh
   git clone git@github.com:paulikarels/smallProjectManagementSystem.git
   ```

2. Back end:
   * Build and run the solution file from the "Backend" folder in Visual Studio.
   * In our case the solution file is named *backend.sln*

3. Front end:
   * Navigate to the "Frontend" directory 
   * Install required npm packages   

     ```sh
     npm install
     ```
     
   * Start the front end development server   

     ```sh
     npm run dev
     ```

### About

This project is a demonstration of my acquired skills in front-end and back-end development.  Technologies such as C# Web API, JWT tokens, Bcrypts password salting and hashing, HTTP methods, and general API development methods were used. The program utilizes Azure Cloud and the Microsoft SQL Server database engine.

### Built With

* React.js
* Microsoft SQL Server
* .NET Web API

### Choices

Initially, the decision was to make a local MS SQL Server without Azure Cloud, but I decided to stick with Azure since I hadn’t “played” with it in the past and saw it as a learning opportunity. The back-end is solely built on REST utilizing HTTP protocol's request types (POST, GET, PUT, and DELETE), and the front-end was built with React utilizing a tool called Vite.

### Challenges

Before development, I thought the hardest part of the project would be the backend, due to the fact I hadn’t used C# and.NET in a while with the utilization of Azure Cloud. Turned out the front-end was the most tedious part of the project because I had carte blanche(“open arms”) regarding the design and implementation choices. Turns out that more freedom isn’t always better.  Also initially, the plan was to implement tests for cases, but confidently I thought I could just leave the test implementation as the last step of the project, but it turned out to be more complicated and tedious this way.  It shows that tests should be made throughout the development and not at the end.

### Extra
* [SQL Schema used](./backend/Schema.sql)