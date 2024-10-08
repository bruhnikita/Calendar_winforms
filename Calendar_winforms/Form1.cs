using Microsoft.VisualBasic;
using System.Windows.Forms;

namespace Calendar_winforms
{
    public partial class MainForm : Form
    {
        private List<Task> tasks;
        System.Windows.Forms.Timer timer;
        TableLayoutPanel tableLayoutPanel;
        public MainForm()
        {
            InitializeComponent();
            tasks = new List<Task>();

            timer = new System.Windows.Forms.Timer();
            timer.Tick += Timer_tick;
            timer.Interval = 60000;
            timer.Start();
        }

        protected override void OnLoad(EventArgs e)
        {
            int countOfButtons = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);

            tableLayoutPanel = new TableLayoutPanel();
            tableLayoutPanel.ColumnCount = 7;
            tableLayoutPanel.RowCount = 6;
            tableLayoutPanel.Dock = DockStyle.Fill;

            for (int i = 0; i < countOfButtons; i++)
            {
                Button button = new Button();
                button.Text = (i + 1).ToString();
                button.Width = 50;
                button.Height = 30;

                tableLayoutPanel.Controls.Add(button, i % 7, i / 7);

                int dayOfMonth = i + 1;
                button.Click += (sender, args) =>
                {
                    string title = Microsoft.VisualBasic.Interaction.InputBox("Введите задачу: ");

                    if (!string.IsNullOrEmpty(title))
                    {
                        try
                        {
                            string time = Microsoft.VisualBasic.Interaction.InputBox("Введите время (HH:mm): ");
                            DateTime dueDate = DateTime.Now;
                            dueDate = dueDate.Date.AddHours(int.Parse(time.Split(':')[0]));
                            dueDate = dueDate.AddMinutes(int.Parse(time.Split(':')[1]));

                            Task task = new Task(title, dueDate, button);
                            tasks.Add(task);
                            button.BackColor = Color.Green;
                        }

                        catch (Exception ex)
                        {
                            MessageBox.Show($"Ошибка создания задачи: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                };
            }

            this.Controls.Add(tableLayoutPanel);
        }

        private void Timer_tick(object sender, EventArgs e)
        {
            if (tasks.Count > 0)
            {
                foreach (Task task in tasks)
                {
                    if (task.Date.Hour == DateTime.Now.Hour && task.Date.Minute == DateTime.Now.Minute && task.Date.Date == DateTime.Now.Date)
                    {
                        MessageBox.Show($"Пришло время для выполнения задачи {task.Title}!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        task.Button.BackColor = Color.Blue;
                        tasks.Remove(task);
                    }
                }
            }
        }
    }
}
