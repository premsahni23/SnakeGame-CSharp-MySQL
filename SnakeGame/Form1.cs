using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace SnakeGame
{
    public partial class Form1 : Form
    {
        private List<Point> snake = new List<Point>();
        private Point food;
        private int direction = 1; // 0=Up, 1=Right, 2=Down, 3=Left
        private int score = 0;
        private bool gameOver = false;
        private Random random = new Random();
        private string connectionString = "Server=localhost;Database=snake_game;Uid=root;Pwd=merp@2005;";

        public Form1()
        {
            InitializeComponent();
            this.KeyPreview = true; // Ensure form captures key events
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
        }

        private void InitializeGame()
        {
            snake.Clear();
            snake.Add(new Point(200, 200)); // Start in center
            direction = 1;
            score = 0;
            lblScore.Text = "Score: 0";
            gameOver = false;
            SpawnFood();
            gameTimer.Enabled = true;
            btnStart.Visible = false;
            btnLeaderboard.Visible = false;
            btnExit.Visible = false;
            txtName.Visible = false;
            btnSubmit.Visible = false;
            this.Focus(); // Ensure form has focus for key events
        }

        private void SpawnFood()
        {
            int x = random.Next(0, picCanvas.Width / 10) * 10;
            int y = random.Next(0, picCanvas.Height / 10) * 10;
            food = new Point(x, y);
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            if (gameOver)
                return;

            // Move snake
            Point head = snake[0];
            Point newHead = head;
            switch (direction)
            {
                case 0: newHead.Y -= 10; break; // Up
                case 1: newHead.X += 10; break; // Right
                case 2: newHead.Y += 10; break; // Down
                case 3: newHead.X -= 10; break; // Left
            }

            // Check collisions
            if (newHead.X < 0 || newHead.X >= picCanvas.Width ||
                newHead.Y < 0 || newHead.Y >= picCanvas.Height ||
                snake.Contains(newHead))
            {
                GameOver();
                return;
            }

            snake.Insert(0, newHead);

            // Check food
            if (newHead == food)
            {
                score++;
                lblScore.Text = $"Score: {score}";
                SpawnFood();
            }
            else
            {
                snake.RemoveAt(snake.Count - 1);
            }

            picCanvas.Invalidate();
        }

        private void picCanvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            // Draw snake
            foreach (var segment in snake)
            {
                g.FillRectangle(Brushes.Green, segment.X, segment.Y, 10, 10);
            }
            // Draw food
            g.FillEllipse(Brushes.Red, food.X, food.Y, 10, 10);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // Optional debug: Uncomment to verify key events
            // MessageBox.Show($"Key pressed: {e.KeyCode}");
            // Handle arrow keys, prevent reversing
            if (e.KeyCode == Keys.Up && direction != 2)
                direction = 0;
            else if (e.KeyCode == Keys.Right && direction != 3)
                direction = 1;
            else if (e.KeyCode == Keys.Down && direction != 0)
                direction = 2;
            else if (e.KeyCode == Keys.Left && direction != 1)
                direction = 3;
        }

        private void GameOver()
        {
            gameTimer.Enabled = false;
            gameOver = true;
            btnStart.Visible = true;
            btnStart.Text = "Restart Game";
            btnLeaderboard.Visible = true;
            btnExit.Visible = true;
            txtName.Visible = true;
            btnSubmit.Visible = true;
            txtName.Focus();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            InitializeGame();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtName.Text))
            {
                try
                {
                    using (var conn = new MySqlConnection(connectionString))
                    {
                        conn.Open();
                        string sql = "INSERT INTO leaderboard (name, score) VALUES (@name, @score)";
                        using (var cmd = new MySqlCommand(sql, conn))
                        {
                            cmd.Parameters.AddWithValue("@name", txtName.Text);
                            cmd.Parameters.AddWithValue("@score", score);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    txtName.Text = "";
                    txtName.Visible = false;
                    btnSubmit.Visible = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Database error: {ex.Message}", "Error");
                }
            }
            else
            {
                MessageBox.Show("Please enter a name.", "Input Required");
            }
        }

        private void btnLeaderboard_Click(object sender, EventArgs e)
        {
            try
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "SELECT name, score FROM leaderboard ORDER BY score DESC LIMIT 10";
                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            string leaderboard = "Leaderboard\n\nName\tScore\n";
                            while (reader.Read())
                            {
                                leaderboard += $"{reader["name"]}\t{reader["score"]}\n";
                            }
                            MessageBox.Show(leaderboard, "Leaderboard");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database error: {ex.Message}", "Error");
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}