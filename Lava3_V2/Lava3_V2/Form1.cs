using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lava3_V2
{
    public partial class Form1 : Form
    {
        private Bitmap bitmap;
        private Bitmap originalBitmap;
        private object lockObject = new object();
        private bool isRunning = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void openPictureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.openFileDialog1.Filter = "Файлы изображений| *.bmp; *.png;* .jpg; |Все файлы|*.*";
            if (openFileDialog1.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            this.bitmap = (Bitmap)Bitmap.FromFile(openFileDialog1.FileName);
            this.originalBitmap = bitmap;
            this.saveFileDialog1.FileName = openFileDialog1.FileName;
            this.pictureBox1.Image = bitmap;
        }

        private void buttonRotatorLeft_Click(object sender, EventArgs e)
        {
            if (bitmap == null || isRunning)
            {
                return;
            }
            
            bitmap.RotateFlip(RotateFlipType.Rotate270FlipNone);
            pictureBox1.Image = bitmap;
        }

        private void buttonRotatorRight_Click(object sender, EventArgs e)
        {
            if (bitmap == null || isRunning)
            {
                return;
            }

            bitmap.RotateFlip(RotateFlipType.Rotate90FlipNone);
            pictureBox1.Image = bitmap;
        }

        private void buttonReflectHorizontal_Click(object sender, EventArgs e)
        {
            if (bitmap == null || isRunning)
            {
                return;
            }

            bitmap.RotateFlip(RotateFlipType.RotateNoneFlipX);
            pictureBox1.Image = bitmap;
        }

        private void buttonReflectVertical_Click(object sender, EventArgs e)
        {
            if (bitmap == null || isRunning)
            {
                return;
            }

            bitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);
            pictureBox1.Image = bitmap;
        }

        private async void buttonEnlarge_Click(object sender, EventArgs e)
        {
            if (bitmap == null || isRunning)
            {
                return;
            }

            var run = false;
            if (!isRunning)
            {
                lock (lockObject)
                {
                    if (!isRunning)
                    {
                        isRunning = true;
                        run = true;
                    }
                }
            }

            if (run)
            {
                await Task.Run(() =>
                {
                    try
                    {

                        var size = new Size(bitmap.Size.Width * 3 / 2, bitmap.Size.Height * 3 / 2);
                        bitmap = new Bitmap(this.originalBitmap, size);
                        pictureBox1.Image = bitmap;

                    }
                    catch (Exception exception)
                    {
                        throw new ArgumentException(exception.Message);
                    }
                });

                lock (lockObject)
                {
                    isRunning = false;
                }
            }
        }

        private async void buttonReduce_Click(object sender, EventArgs e)
        {
            if (bitmap == null || isRunning)
            {
                return;
            }

            var run = false;
            if (!isRunning)
            {
                lock (lockObject)
                {
                    if (!isRunning)
                    {
                        isRunning = true;
                        run = true;
                    }
                }
            }

            if (run)
            {
                await Task.Run(() =>
                {
                    try
                    {
                        var size = new Size(bitmap.Size.Width / 3 * 2, bitmap.Size.Height / 3 * 2);
                        bitmap = new Bitmap(this.originalBitmap, size);
                        pictureBox1.Image = bitmap;
                    }
                    catch (Exception exception)
                    {
                        throw new ArgumentException(exception.Message);
                    }
                });

                lock (lockObject)
                {
                    isRunning = false;
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var bitmapSave = bitmap;
            bitmapSave.Save(saveFileDialog1.FileName);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "Файлы изображений| *.bmp; *.png;* .jpg; |Все файлы|*.*";
            if (saveFileDialog1.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            bitmap.Save(saveFileDialog1.FileName);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
