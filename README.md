# DevMindSpeed
# DevMindSpeed

## Project Overview

DevMindSpeed is an interactive, fast-paced math game that tests players' mental speed and accuracy.  
Players can enter their name, select a difficulty level, answer timed math questions, and view a detailed game summary with scores, best answers, and history.

The project consists of two main parts:  
- **Backend API** built with ASP.NET Core 9.0  
- **Frontend** built with Angular  

---

## Prerequisites

Make sure you have the following installed:  
- [.NET 9.0 SDK](https://dotnet.microsoft.com/download)  
- [Node.js & npm](https://nodejs.org/) (required for Angular frontend)  
- An IDE or code editor like [Visual Studio Code](https://code.visualstudio.com/)

---

## Setup & Run Instructions

### Backend API

1. Open a terminal and navigate to the backend API folder:
    ```bash
    cd api
    ```

2. Restore dependencies:
    ```bash
    dotnet restore
    ```

3. Apply Entity Framework migrations to create/update the SQLite database:
    ```bash
    dotnet ef database update
    ```

4. Run the backend API:
    ```bash
    dotnet run
    ```

The API will be available at `https://localhost:<port>/` or `http://localhost:<port>/` depending on your launch settings.

---

### Frontend (Angular)

1. Open a terminal and navigate to the frontend folder:
    ```bash
    cd client
    ```

2. Install the dependencies:
    ```bash
    npm install
    ```

3. Run the Angular development server:
    ```bash
    ng serve
    ```

The frontend will open automatically in your browser, typically at `http://localhost:4200/`.

---

## What We Did

- Developed a backend REST API with .NET 9 to handle game logic, questions, scoring, and game summary.
- Created Angular components for:
  - Player registration and difficulty selection
  - Interactive gameplay with questions, timer, and answer submission
  - Game summary with detailed stats, history, and best score display
- Implemented a responsive and user-friendly UI with Angular Forms and validation
- Used **Entity Framework Core** for database interactions and implemented **migrations to manage the SQLite database schema**
- Connected frontend and backend seamlessly through HTTP client services

---

## Contact

For questions or feedback, feel free to contact Husam.


