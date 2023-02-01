using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game_2048{
    public partial class Game_2048 : Form{

        public int[,] map = new int[4, 4];
        public Label[,] labels = new Label[4, 4];
        public PictureBox[,] pics = new PictureBox[4, 4];
        private int score = 0;

        public Game_2048(){
            InitializeComponent();
            map[0, 0] = 1;
            map[0, 1] = 1;
            CreateMap();
            CreatePics();
            GenerateNewPic();
        }

        private void CreateMap(){

            for (int i = 0; i < 4; i++){
                for (int j = 0; j < 4; j++){
                    PictureBox pic = new PictureBox();

                    pic.Location = new Point(12 + 56 * j, 73 + 56 * i);
                    pic.Size = new Size(50, 50);
                    pic.BackColor = Color.Gray;

                    this.Controls.Add(pic);
                }
            }
        }

        private void GenerateNewPic(){

            Random rnd = new Random();

            int a = rnd.Next(0, 4);
            int b = rnd.Next(0, 4);

            while (pics[a, b] != null){
                a = rnd.Next(0, 4);
                b = rnd.Next(0, 4);
            }

            map[a, b] = 1;

            labels[a, b] = new Label();
            labels[a, b].Text = "2";
            labels[a, b].Size = new Size(50, 50);
            labels[a, b].TextAlign = ContentAlignment.MiddleCenter;
            labels[a, b].Font = new Font(new FontFamily("Microsoft Sans Serif"), 15);

            pics[a, b] = new PictureBox();
            pics[a, b].Controls.Add(labels[a, b]);
            pics[a, b].Location = new Point(12 + b * 56, 73 + 56 * a);
            pics[a, b].Size = new Size(50, 50);
            pics[a, b].BackColor = Color.Yellow;

            this.Controls.Add(pics[a, b]);
            pics[a, b].BringToFront();
        }

        private void CreatePics(){

            labels[0, 0] = new Label();
            labels[0, 0].Text = "2";
            labels[0, 0].Size = new Size(50, 50);
            labels[0, 0].TextAlign = ContentAlignment.MiddleCenter;
            labels[0, 0].Font = new Font(new FontFamily("Microsoft Sans Serif"), 15);

            pics[0, 0] = new PictureBox();
            pics[0, 0].Controls.Add(labels[0, 0]);
            pics[0, 0].Location = new Point(12, 73);
            pics[0, 0].Size = new Size(50, 50);
            pics[0, 0].BackColor = Color.Yellow;
            this.Controls.Add(pics[0, 0]);
            pics[0, 0].BringToFront();
            pics[0, 1] = new PictureBox();

            labels[0, 1] = new Label();
            labels[0, 1].Text = "2";
            labels[0, 1].Size = new Size(50, 50);
            labels[0, 1].TextAlign = ContentAlignment.MiddleCenter;
            labels[0, 1].Font = new Font(new FontFamily("Microsoft Sans Serif"), 15);

            pics[0, 1].Controls.Add(labels[0, 1]);
            pics[0, 1].Location = new Point(68, 73);
            pics[0, 1].Size = new Size(50, 50);
            pics[0, 1].BackColor = Color.Yellow;
            this.Controls.Add(pics[0, 1]);
            pics[0, 1].BringToFront();
        }

        private void ChangeColor(int sum, int k, int j){
            switch (sum){
                case 2:
                    pics[k, j].BackColor = Color.Yellow;
                    break;
                case 4:
                    pics[k, j].BackColor = Color.YellowGreen;
                    break;
                case 8:
                    pics[k, j].BackColor = Color.GreenYellow;
                    break;
                case 16:
                    pics[k, j].BackColor = Color.LightGoldenrodYellow;
                    break;
                case 32:
                    pics[k, j].BackColor = Color.LightYellow;
                    break;
                case 64:
                    pics[k, j].BackColor = Color.IndianRed;
                    break;
                case 128:
                    pics[k, j].BackColor = Color.MediumVioletRed;
                    break;
                case 256:
                    pics[k, j].BackColor = Color.OrangeRed;
                    break;
                case 512:
                    pics[k, j].BackColor = Color.PaleVioletRed;
                    break;
                case 1024:
                    pics[k, j].BackColor = Color.CadetBlue;
                    break;
                case 2048:
                    pics[k, j].BackColor = Color.Red;
                    break;
            }
        }

        private void label1_Click(object sender, EventArgs e) { }

        private void button4_Click_1(object sender, EventArgs e){//Обработка нажатия кнопки "Вправо"

            bool ifPicWasMoved = false;

            for (int k = 0; k < 4; k++){
                for (int l = 2; l >= 0; l--){
                    if (map[k, l] == 1){
                        for (int j = l + 1; j < 4; j++){
                            if (map[k, j] == 0){
                                ifPicWasMoved = true;

                                map[k, j - 1] = 0;
                                map[k, j] = 1;

                                pics[k, j] = pics[k, j - 1];
                                pics[k, j - 1] = null;

                                labels[k, j] = labels[k, j - 1];
                                labels[k, j - 1] = null;

                                pics[k, j].Location = new Point(pics[k, j].Location.X + 56, pics[k, j].Location.Y);
                            }
                            else{
                                int a = int.Parse(labels[k, j].Text);
                                int b = int.Parse(labels[k, j - 1].Text);
                                if (a == b){
                                    ifPicWasMoved = true;

                                    labels[k, j].Text = (a + b).ToString();
                                    score += (a + b);

                                    ChangeColor(a + b, k, j);

                                    label1.Text = "Score: " + score;
                                    map[k, j - 1] = 0;

                                    this.Controls.Remove(pics[k, j - 1]);
                                    this.Controls.Remove(labels[k, j - 1]);

                                    pics[k, j - 1] = null;
                                    labels[k, j - 1] = null;
                                }
                            }
                        }
                    }
                }
            }
            if (ifPicWasMoved)
                GenerateNewPic();
        }

        private void button2_Click(object sender, EventArgs e){//Обработка нажатия кнопки "Вверх"

            bool ifPicWasMoved = false;

            for (int k = 1; k < 4; k++){
                for (int l = 0; l < 4; l++){
                    if (map[k, l] == 1){
                        for (int j = k - 1; j >= 0; j--){
                            if (map[j, l] == 0){

                                ifPicWasMoved = true;

                                map[j + 1, l] = 0;
                                map[j, l] = 1;

                                pics[j, l] = pics[j + 1, l];
                                pics[j + 1, l] = null;

                                labels[j, l] = labels[j + 1, l];
                                labels[j + 1, l] = null;

                                pics[j, l].Location = new Point(pics[j, l].Location.X, pics[j, l].Location.Y - 56);
                            }
                            else{
                                int a = int.Parse(labels[j, l].Text);
                                int b = int.Parse(labels[j + 1, l].Text);
                                if (a == b){
                                    ifPicWasMoved = true;

                                    labels[j, l].Text = (a + b).ToString();
                                    score += (a + b);

                                    ChangeColor(a + b, j, l);

                                    label1.Text = "Score: " + score;
                                    map[j + 1, l] = 0;

                                    this.Controls.Remove(pics[j + 1, l]);
                                    this.Controls.Remove(labels[j + 1, l]);

                                    pics[j + 1, l] = null;
                                    labels[j + 1, l] = null;
                                }
                            }
                        }
                    }
                }
            }
            if (ifPicWasMoved)
                GenerateNewPic();
        }

        private void button5_Click(object sender, EventArgs e){//Обработка нажатия кнопки "влево"

            bool ifPicWasMoved = false;

            for (int k = 0; k < 4; k++){
                for (int l = 1; l < 4; l++){
                    if (map[k, l] == 1){
                        for (int j = l - 1; j >= 0; j--){
                            if (map[k, j] == 0){
                                ifPicWasMoved = true;

                                map[k, j + 1] = 0;
                                map[k, j] = 1;

                                pics[k, j] = pics[k, j + 1];
                                pics[k, j + 1] = null;

                                labels[k, j] = labels[k, j + 1];
                                labels[k, j + 1] = null;

                                pics[k, j].Location = new Point(pics[k, j].Location.X - 56, pics[k, j].Location.Y);
                            }
                            else{
                                int a = int.Parse(labels[k, j].Text);
                                int b = int.Parse(labels[k, j + 1].Text);
                                if (a == b)
                                {
                                    ifPicWasMoved = true;

                                    labels[k, j].Text = (a + b).ToString();
                                    score += (a + b);

                                    ChangeColor(a + b, k, j);

                                    label1.Text = "Score: " + score;
                                    map[k, j + 1] = 0;

                                    this.Controls.Remove(pics[k, j + 1]);
                                    this.Controls.Remove(labels[k, j + 1]);

                                    pics[k, j + 1] = null;
                                    labels[k, j + 1] = null;
                                }
                            }
                        }
                    }
                }
            }
            if (ifPicWasMoved)
                GenerateNewPic();
        }

        private void button3_Click(object sender, EventArgs e){//Обработка нажатия кнопки "Вниз"
            bool ifPicWasMoved = false;

            for (int k = 2; k >= 0; k--){
                for (int l = 0; l < 4; l++){
                    if (map[k, l] == 1){
                        for (int j = k + 1; j < 4; j++){
                            if (map[j, l] == 0){

                                ifPicWasMoved = true;

                                map[j - 1, l] = 0;
                                map[j, l] = 1;

                                pics[j, l] = pics[j - 1, l];
                                pics[j - 1, l] = null;

                                labels[j, l] = labels[j - 1, l];
                                labels[j - 1, l] = null;

                                pics[j, l].Location = new Point(pics[j, l].Location.X, pics[j, l].Location.Y + 56);
                            }
                            else{
                                int a = int.Parse(labels[j, l].Text);
                                int b = int.Parse(labels[j - 1, l].Text);
                                if (a == b){
                                    ifPicWasMoved = true;

                                    labels[j, l].Text = (a + b).ToString();
                                    score += (a + b);

                                    ChangeColor(a + b, j, l);

                                    label1.Text = "Score: " + score;
                                    map[j - 1, l] = 0;

                                    this.Controls.Remove(pics[j - 1, l]);
                                    this.Controls.Remove(labels[j - 1, l]);

                                    pics[j - 1, l] = null;
                                    labels[j - 1, l] = null;
                                }
                            }
                        }
                    }
                }
            }
            if (ifPicWasMoved)
                GenerateNewPic();
        }

    }
}
