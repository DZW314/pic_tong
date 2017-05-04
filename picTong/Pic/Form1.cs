using System;
using System.IO;
using System.IO.Ports;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using INIFILE;
using System.Text.RegularExpressions;

namespace Pic
{
    public partial class Form1 : Form
    {
        SerialPort sp1 = new SerialPort();
        //sp1.ReceivedBytesThreshold = 1;//只要有1个字符送达端口时便触发DataReceived事件 
        public Form1()
        {
            InitializeComponent();
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
        string fileName = "";
        Image image;
        //打开
        private void button1_Click(object sender, EventArgs e)
        {
            //设置文件名为空
            openFileDialog1.FileName = "";
             //过滤条件
            openFileDialog1.Filter = "Image File(*.bmp,*.wmf,*.ico,*.jpg,*.bng)|*.bmp;*.wmf;*.ico;*.jpg;*.bng";
            //是否可多选
            openFileDialog1.Multiselect = false;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
                fileName = openFileDialog1.FileName;
                //调取图像的像素尺寸
                Object height = pictureBox1.Image.Height;
                Object width = pictureBox1.Image.Width;
                textBoHeight.Text = height.ToString();
                textBoxWidth.Text = width.ToString();
            }
        }
        //保存
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
            //    saveFileDialog1.CheckFileExists = false;//存在相同文件名不提示
                //bmp.Save(saveFileDialog1.FileName, ImageFormat.Bmp);//存储图片，
                bmp.Save(saveFileDialog1.FileName);//存储图片，
            }
            
         }
        //显示实际尺寸
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
        //旋转
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
        //另存
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
        //更改尺寸信息
        private void button6_Click(object sender, EventArgs e)
        {
            //if (pictureBox1.Image != null)
            //{
            //    Bitmap bmp = new Bitmap(pictureBox1.Image);	//指定图像
            //    Graphics graphics = pictureBox1.CreateGraphics(); 				//声明画板
            //    graphics.Clear(this.BackColor);//清除当前窗体的背景色
            //    int width = Convert.ToInt32(Convert.ToDouble(0.5 * bmp.Width));//缩放宽度
            //    int height = Convert.ToInt32(Convert.ToDouble(0.5 * bmp.Height));//缩放高度
            //    graphics.DrawImage(bmp, pictureBox1.Left, pictureBox1.Top, width, height);//绘制图像
            //    image = pictureBox1.Image;
            //}
            //if (pictureBox1.Image != null)
            //{
            //    Bitmap bmp1 = new Bitmap(pictureBox1.Image);//使用图片创建Bitmap对象
            //    bmp1.RotateFlip(RotateFlipType.RotateNoneFlipXY);//旋转
            //    pictureBox1.Image = bmp1;
            //    image = (Image)bmp1.Clone();
            //}            
        }

        //只允许输入数字和退格
        private void number_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8)
            {
                MessageBox.Show("请输入数字！");
                e.Handled = true;
            }
        }
        //输出RGB
        private void button7_Click(object sender, EventArgs e)
        {
            ////定义4个颜色结构体
            //Color c = new Color();

            //int r, g, b;

            //Bitmap bmp1 = new Bitmap(pictureBox1.Image);//创建Bitmap对象
            //for (int i = 0; i < bmp1.Width - 1; i++)
            //{
            //    for (int j = 0; j < bmp1.Height - 1; j++)
            //    {
            //        //获得像素颜色值
            //        c = bmp1.GetPixel(i, j);
            //        //红色分量值
            //        r = c.R;
            //        //绿色分量值
            //        g = c.G;
            //        //蓝色分量值
            //        b = c.B;
            //    }

            //}
            if (pictureBox1.Image != null)
            {
                //定义一个Bitmap对象来存放图片
                Bitmap bmp = new Bitmap(pictureBox1.Image);
                //Bitmap bmp =(Bitmap)Image.FromFile(fileName);

                Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
                //以可读写的方式锁定全部位图像素
                BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);  //bmp.PixelFormat

                //得到首地址
                IntPtr ptr = bmpData.Scan0;
                //位图字节数
                int bytes = bmp.Width * bmp.Height * 3;
                //定义位图数组
                byte[] rgbValues = new byte[bytes];
                //复制被锁定的位图像素值到该数组内
                System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);
                File.WriteAllBytes("./rgb", rgbValues);

                //int blockLength = 4096;
                //int totalLen = rgbValues.Length;

                //byte[] fileblock = new byte[blockLength];

                //int temp = totalLen / blockLength;
                //int remainder = totalLen % blockLength;
                //if (remainder > 0)
                //    temp += 1;
                //for (int i = 0; i < temp; i++)

                //{
                //    Array.Copy(rgbValues, i, fileblock, 0, rgbValues.Length - blockLength);

                //}
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            INIFILE.Profile.LoadProfile();//加载所有

            //检查是否含有串口
            string[] str = SerialPort.GetPortNames();
            if (str == null)
            {
                MessageBox.Show("本机没有串口！", "Error");
                return;
            }

            //添加串口项目
            foreach (string s in System.IO.Ports.SerialPort.GetPortNames())
            {//获取有多少个COM口
             //System.Diagnostics.Debug.WriteLine(s);
                cbSerial.Items.Add(s);
            }

            //串口设置默认选择项
            //DZW
            cbSerial.SelectedIndex = 0;         //note：获得COM1口，但别忘修改

            sp1.BaudRate = 115200;

            Control.CheckForIllegalCrossThreadCalls = false;    //这个类中我们不检查跨线程的调用是否合法(因为.net 2.0以后加强了安全机制,，不允许在winform中直接跨线程访问控件的属性)
            sp1.DataReceived += new SerialDataReceivedEventHandler(sp1_DataReceived);
            //sp1.ReceivedBytesThreshold = 1;

            //准备就绪              
            sp1.DtrEnable = true;
            sp1.RtsEnable = true;
            //设置数据读取超时为1秒
            sp1.ReadTimeout = 1000;

            sp1.Close();

        }
        void sp1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (sp1.IsOpen)     //此处可能没有必要判断是否打开串口，但为了严谨性，我还是加上了
            {             
                byte[] byteRead = new byte[sp1.BytesToRead];    //BytesToRead:sp1接收的字符个数

                //'发送16进制按钮'
                try
                    {
                        Byte[] receivedData = new Byte[sp1.BytesToRead];        //创建接收字节数组
                        sp1.Read(receivedData, 0, receivedData.Length);         //读取数据
                        //string text = sp1.Read();   //Encoding.ASCII.GetString(receivedData);
                        sp1.DiscardInBuffer();                                  //清空SerialPort控件的Buffer
                        //这是用以显示字符串
                        //    string strRcv = null;
                        //    for (int i = 0; i < receivedData.Length; i++ )
                        //    {
                        //        strRcv += ((char)Convert.ToInt32(receivedData[i])) ;
                        //    }
                        //    txtReceive.Text += strRcv + "\r\n";             //显示信息
                        //}
                        string strRcv = null;
                        //int decNum = 0;//存储十进制
                        for (int i = 0; i < receivedData.Length; i++) //窗体显示
                        {

                            strRcv += receivedData[i].ToString("X2");  //16进制显示
                        }
                        
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show(ex.Message, "出错提示");
                    }
            }
            else
            {
                MessageBox.Show("请打开某个串口", "错误提示");
            }
        }

        private void btnSwitch_Click(object sender, EventArgs e)
        {
            //serialPort1.IsOpen
            if (!sp1.IsOpen)
            {
                try
                {
                    //设置串口号
                    string serialName = cbSerial.SelectedItem.ToString();
                    sp1.PortName = serialName;

                    //设置各“串口设置”
                    Int32 iBaudRate = 115200;
                    Int32 iDateBits = 8;
                    string strStopBits = "1";

                    sp1.BaudRate = iBaudRate;       //波特率
                    sp1.DataBits = iDateBits;       //数据位
                    sp1.StopBits = StopBits.One;    //停止位
                    sp1.Parity = Parity.None;       //校验位
                    
                    if (sp1.IsOpen == true)//如果打开状态，则先关闭一下
                    {
                        sp1.Close();
                    }

                    //设置必要控件不可用
                    cbSerial.Enabled = false;

                    sp1.Open();     //打开串口
                    btnSwitch.Text = "关闭串口";
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("Error:" + ex.Message, "Error");
                    //tmSend.Enabled = false; Timer
                    return;
                }
            }
            else
            {
                //恢复控件功能
                //设置必要控件不可用
                cbSerial.Enabled = true;

                sp1.Close();                    //关闭串口
                btnSwitch.Text = "打开串口";
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (!sp1.IsOpen) //如果没打开
            {
                MessageBox.Show("请先打开串口！", "Error");
                return;
            }
            if (pictureBox1.Image != null)
            {
                //定义一个Bitmap对象来存放图片
                Bitmap bmp = new Bitmap(pictureBox1.Image);
                //Bitmap bmp =(Bitmap)Image.FromFile(fileName);

                Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
                //以可读写的方式锁定全部位图像素
                BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);  //bmp.PixelFormat

                //得到首地址
                IntPtr ptr = bmpData.Scan0;
                //位图字节数
                int bytes = bmp.Width * bmp.Height * 3;
                //定义位图数组
                byte[] rgbValues = new byte[bytes];
                //复制被锁定的位图像素值到该数组内
                System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);
                //下载rgb数据
                sp1.Write(rgbValues, 0, rgbValues.Length);
            }
        }
    }
}



 