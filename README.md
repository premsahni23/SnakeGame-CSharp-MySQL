 ███████╗███╗   ██╗ █████╗  ██████╗███████╗
 ██╔════╝████╗  ██║██╔══██╗██╔════╝██╔════╝
 █████╗  ██╔██╗ ██║███████║██║     █████╗  
 ██╔══╝  ██║╚██╗██║██╔══██║██║     ██╔══╝  
 ███████╗██║ ╚████║██║  ██║╚██████╗███████╗
 ╚══════╝╚═╝  ╚═══╝╚═╝  ╚═╝ ╚═════╝╚══════╝
       
  Classic Snake Game in C# + MySQL       

# 🎮 Windows Forms Snake Game with Leaderboard (C# + MySQL)

This is a fun and simple 2D **Snake Game** built using **C# Windows Forms**, with integrated **MySQL** database support to store and display high scores on a **leaderboard**.  

The game includes:
- ✅ Player name input before saving score
- ✅ Real-time score tracking
- ✅ MySQL leaderboard view
- ✅ Game Over menu with Restart, Show Leaderboard, and Exit

---

## 🧩 Features

- 🕹️ **Simple and interactive 2D Snake gameplay**
- 👤 **Player name input** before saving the score
- 🏆 **Leaderboard page** using MySQL (sorted by high score)
- 🔁 **Restart**, 📊 **Show Leaderboard**, and ❌ **Exit** buttons after Game Over
- 🎨 **Modern UI** using Windows Forms

---

## 🛠️ Technologies Used

| Tool               | Purpose                     |
|--------------------|-----------------------------|
| 💻 C#              | Programming Language         |
| 🧱 .NET Windows Forms | Game UI & App Framework     |
| 🐬 MySQL           | Database for Leaderboard     |
| 🧰 Visual Studio   | Development Environment      |

---

## 🗃️ Database Setup

1. **Create the database** in MySQL:

    ```sql
    CREATE DATABASE game_db;
    ```

2. **Create the leaderboard table**:

    ```sql
    CREATE TABLE leaderboard (
      id INT AUTO_INCREMENT PRIMARY KEY,
      player_name VARCHAR(100),
      score INT,
      play_date DATETIME DEFAULT CURRENT_TIMESTAMP
    );
    ```

3. **Update your MySQL connection string** in your C# code:

    ```csharp
    string connectionString = "server=localhost;user=root;password=your_password;database=game_db;";
    ```

---


## 🚀 How to Run

1. **Clone the repository:**
    ```bash
    git clone https://github.com/your-username/SnakeGame-CSharp-MySQL.git
    cd SnakeGame-CSharp-MySQL
    ```

2. **Open the project** in Visual Studio.

3. **Build the solution** (restore any NuGet packages if prompted).

4. Make sure **MySQL server is running**, and your database credentials are correct.

5. **Run the game** and have fun! 🎉

---

## 📸 Screenshots

> *(Add your gameplay and leaderboard screenshots here)*  
> Example:
> - `GameForm.png` – during play  
> - `LeaderboardForm.png` – leaderboard view  
> - `GameOverForm.png` – end screen with buttons

---

## 🙌 Acknowledgements

- Inspired by classic **retro snake games**
- Built as a **mini project** for learning **C#**, **Windows Forms**, and **MySQL integration**

---

## 📃 License

This project is licensed under the [Apache License](LICENSE).

---

### 🔗 Connect with Us 

- GitHub: [@premsahni23](https://github.com/premsahni23) ,[@kathyyyy498](https://github.com/kathyyyy498)
- Email: *sahniprem568@gmail.com* , *katherinbinu@gmail.com*

---
