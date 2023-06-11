using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GoruntuSifreleme
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Bitmap image;
        Image encryptedImage;

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image Files(*.BMP;*.JPG;*.PNG)|*.BMP;*.JPG;*.PNG" +
            "|All files(*.*)|*.*";
            dialog.CheckFileExists = true;
            dialog.Multiselect = false;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                image = new Bitmap(dialog.FileName);
                pictureBox1.Image = (Image)image;

            }

            Image originalImage = image;

            encryptedImage = ImagePixelShuffle.EncryptImage(originalImage);


            pictureBox1.Image = ımageList1.Images[0];
            pictureBox2.Image = encryptedImage;
            



        }

      

        private void button2_Click(object sender, EventArgs e)
        {
            Image decryptedImage = ImagePixelShuffle.DecryptImage(encryptedImage);
            
            pictureBox1.Image = image;
            pictureBox2.Image = ımageList1.Images[0];
        }

        public class ImagePixelShuffle
        {
            public static Image EncryptImage(Image originalImage)
            {
                Bitmap bitmap = new Bitmap(originalImage);
                Random random = new Random();

                for (int i = 0; i < bitmap.Width; i++)
                {
                    for (int j = 0; j < bitmap.Height; j++)
                    {
                        int randomX = random.Next(bitmap.Width);
                        int randomY = random.Next(bitmap.Height);

                        Color tempColor = bitmap.GetPixel(i, j);
                        bitmap.SetPixel(i, j, bitmap.GetPixel(randomX, randomY));
                        bitmap.SetPixel(randomX, randomY, tempColor);
                    }
                }

                return bitmap;
            }

            public static Image DecryptImage(Image encryptedImage)
            {
                Bitmap bitmap = new Bitmap(encryptedImage);
                Random random = new Random();

                for (int i = bitmap.Width - 1; i >= 0; i--)
                {
                    for (int j = bitmap.Height - 1; j >= 0; j--)
                    {
                        int randomX = random.Next(bitmap.Width);
                        int randomY = random.Next(bitmap.Height);

                        Color tempColor = bitmap.GetPixel(i, j);
                        bitmap.SetPixel(i, j, bitmap.GetPixel(randomX, randomY));
                        bitmap.SetPixel(randomX, randomY, tempColor);
                    }
                }

                return bitmap;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = ımageList1.Images[0];
            pictureBox2.Image = encryptedImage;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = image;
            pictureBox2.Image = encryptedImage;
        }
    }
}

