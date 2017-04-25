using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Ex175
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
        string fileName = "";
        Image image;
        private void button1_Click(object sender, EventArgs e)
        {
             //过滤条件
            openFileDialog1.Filter = "Image File(*.bmp,*.wmf,*.ico,*.jpg,*.bng)|*.bmp;*.wmf;*.ico;*.jpg;*.bng";
            //是否可多选
            openFileDialog1.Multiselect = false;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
                fileName = openFileDialog1.FileName;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                Bitmap bmp = new Bitmap(pictureBox1.Image);
                //过滤条件
                saveFileDialog1.Filter = "Bitmap File(*.bmp)|*.bmp|" +
                   "Gif File(*.gif)|*.gif|" +
                   "JPEG File(*.jpg)|*.jpg|" +
                   "PNG File(*.png)|*.png";
                saveFileDialog1.FileName = fileName;//原文件名保存
                saveFileDialog1.CheckFileExists = false;//存在相同文件名不提示
                bmp.Save(saveFileDialog1.FileName, ImageFormat.Bmp);//存储图片，
            }
            
         }

        private void button4_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                Bitmap bmp = new Bitmap(pictureBox1.Image);	//指定图像
                Graphics graphics = pictureBox1.CreateGraphics(); 				//声明画板
                graphics.Clear(this.BackColor);//清除当前窗体的背景色
                int width = Convert.ToInt32(Convert.ToDouble(0.5 * bmp.Width));//缩放宽度
                int height = Convert.ToInt32(Convert.ToDouble(0.5 * bmp.Height));//缩放高度
                graphics.DrawImage(bmp, pictureBox1.Left, pictureBox1.Top, width, height);//绘制图像
                image = pictureBox1.Image;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                Bitmap bmp1 = new Bitmap(pictureBox1.Image);//使用图片创建Bitmap对象
                bmp1.RotateFlip(RotateFlipType.RotateNoneFlipXY);//旋转
                pictureBox1.Image = bmp1;
                image = (Image)bmp1.Clone();
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //保存文件对话框过滤条件
            saveFileDialog1.Filter = "Bitmap File(*.bmp)|*.bmp|" +
                "Gif File(*.gif)|*.gif|" +
                "JPEG File(*.jpg)|*.jpg|" +
                "PNG File(*.png)|*.png";
            saveFileDialog1.FileName = fileName;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                image = Image.FromFile(fileName); 
                string strFilExtn =
                    fileName.Remove(0, fileName.Length - 3);
                //存储图片，可重新设置文件名
                switch (strFilExtn)
                {
                    case "bmp":
                       image.Save(saveFileDialog1.FileName, ImageFormat.Bmp);
                        break;
                    case "jpg":
                        image.Save(saveFileDialog1.FileName, ImageFormat.Jpeg);
                        break;
                    case "gif":
                        image.Save(saveFileDialog1.FileName, ImageFormat.Gif);
                        break;
                    case "tif":
                        image.Save(saveFileDialog1.FileName, ImageFormat.Tiff);
                        break;
                    case "png":
                        image.Save(saveFileDialog1.FileName, ImageFormat.Png);
                        break;
                    default:
                        break;
                }
             
                this.Text = "画图\t" + saveFileDialog1.FileName;
                fileName = saveFileDialog1.FileName;
            }
        }
    }
}
