using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using S7.Net;
using System.Net.NetworkInformation;
using System.Threading;
using System.Collections;

namespace MagnaUruguay
{
    public partial class km1 : Form
    {
        public Plc plc = new Plc(CpuType.S71200, "10.25.0.244", 0, 1);
        public Ping plcPing = new Ping();
        

        //Barkod koduyla oluşturulacak taşınması gereken klasör yolu
        private static string pathServer = "";

        //Görüntülerin kaydedildiği FTP yolu
        private static string pathFtp = "";

        //Kameraların klasör ismi ve ftp klasör ismi
        
        //İSTASYON1
        //KAMERA 1 İSTASYON 1
        private static string camName1_1 = "5L02D97PAG82802";
        private static string camDir1_1 = "0_238";
        //KAMERA 2 İSTASYON 1
        private static string camName2_1 = "5L02D97PAGDE793";
        private static string camDir2_1 = "0_239";
        //KAMERA 3 İSTASYON 1
        private static string camName3_1 = "5L02D97PAG12C4D";
        private static string camDir3_1 = "0_240";

        //İSTASYON2
        //KAMERA 1 İSTASYON 2
        private static string camName1_2 = "5J09FDCPAG86A3F";
        private static string camDir1_2 = "0_241";
        //KAMERA 2 İSTASYON 2
        private static string camName2_2 = "5L02D97PAGA5103";
        private static string camDir2_2 = "0_242";
        //KAMERA 3 İSTASYON 2
        private static string camName3_2 = "5L02D97PAGB05DA";
        private static string camDir3_2 = "0_243";

        bool Cam1AktifST1;
        bool Cam2AktifST1;
        bool Cam3AktifST1;
        bool Cam1AktifST2;
        bool Cam2AktifST2;
        bool Cam3AktifST2;

        int toplamAktifKameraST1;
        int toplamAktifKameraST2;


        public km1()
        {
            InitializeComponent();
        }

        

        private void Form1_Load(object sender, EventArgs e)
        {
            lblDate.Text = DateTime.Now.ToString("dd.MM.yyyy");
            lblTime.Text = DateTime.Now.ToString("H:mm");
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

            timerTryConn.Start();


            string PathCfg = Environment.CurrentDirectory + @"\path.cfg";
            if (System.IO.File.Exists(PathCfg) == true)
            {
                string[] result = File.ReadAllLines(PathCfg);
                for (int i = 0; i < 2; i++)
                {
                    string[] resArr = result[i].ToString().Split('=');
                    if (resArr[0].ToString() == "pathServer")
                    {
                        pathServer = resArr[1].ToString();
                    }
                    else if (resArr[0].ToString() == "pathFtp")
                    {
                        pathFtp = resArr[1].ToString();
                    }
                }
            }
            
        }
        public static int bildirim_izin = 0;

        private void timerTryConn_Tick(object sender, EventArgs e)
        {
            if (!plc.IsConnected)
            {
                pnlPlcConnectionStatus.BackColor = Color.DarkGray;
                txtBoxLog.AppendText("PLC bağlantısı başarısız. Tekrar deneniyor..." + "\r\n");
                btnConnectPlc_Click(sender, e);
                bildirim_izin = 0;
            }
            else
            {
                if (bildirim_izin == 0)
                {
                    txtBoxLog.AppendText("PLC bağlantısı başarılı bir şekilde oluşturuldu" + "\r\n");
                    bildirim_izin = 1;
                }
                pnlPlcConnectionStatus.BackColor = Color.DarkGreen;
            }
        }

        private void btnConnectPlc_Click(object sender, EventArgs e)
        {
            plcConn();
        }

        private void plcConn()
        {
            try
            {
                if (!plc.IsConnected)
                {
                    PingReply PR = plcPing.Send("10.25.0.244");
                    txtBoxLog.AppendText(PR.Status.ToString() + "\r\n");
                    if (PR.Status.ToString() == "Success")
                    {
                        plc.Open();
                        timerSync.Start();
                        timerConn.Start();
                    }
                }
            }
            catch (PlcException ex)
            {
                txtBoxLog.AppendText("Hata : PLC BAĞLANTI HATASI (" + ex.ErrorCode.ToString() + "," + DateTime.Now.ToString("g") + ")" + "\r\n");
            }
        }
        private void timerConn_Tick(object sender, EventArgs e)
        {
            try
            {
                plc.WriteBit(DataType.DataBlock, 50, 36, 0, Convert.ToInt32(DateTime.Now.ToString("ss")) % 2);
            }
            catch (PlcException ex)
            {
                txtBoxLog.AppendText("Hata : PLC KONTROL BİTİ YAZMA HATASI (" + ex.ErrorCode.ToString() + "," + DateTime.Now.ToString("g") + ")" + "\r\n");
            }
        }
        
        
        private void timerSync_Tick(object sender, EventArgs e)
        {
            lblDate.Text = DateTime.Now.ToString("dd.MM.yyyy");
            lblTime.Text = DateTime.Now.ToString("H:mm");
            

            if (plc.IsConnected)
            {
                pnlPlcConnectionStatus.BackColor = Color.DarkGreen;
            }
            else
            {
                pnlPlcConnectionStatus.BackColor = Color.DarkGray;
            }
            
            try
            {
                var Barkod1 = plc.Read(DataType.DataBlock, 50, 0, VarType.S7String, 10); //1. İSTASYON BARKOD
                var Barkod2 = plc.Read(DataType.DataBlock, 50, 18, VarType.S7String, 10); //2. İSTASYON BARKOD


                var ftpEmptyReadST1 = plc.Read("DB51.DBX0.2");  //1. İSTASYON FTP KLASÖRÜ BOŞ
                var ftpEmptyReadST2 = plc.Read("DB51.DBX0.3");  //2. İSTASYON FTP KALSÖRÜ BOŞ

                var ArsivleST1 = plc.Read("DB50.DBX12.4");  //1. İSTASYON ARŞİVLE BİTİ 
                var ArsivleST2 = plc.Read("DB50.DBX30.4");  //2. İSTASYON ARŞİVLE BİTİ

                var CevrimBasladiST1 = plc.Read("DB50.DBX12.0"); //1. İSTASYON ÇEVRİM BAŞLADI
                var CevrimBasladiST2 = plc.Read("DB50.DBX30.0"); //2. İSTASYON ÇEVRİM BAŞLADI

                var allCams = plc.Read(DataType.DataBlock,2,49,VarType.Bit,6);
                BitArray kameralar = new BitArray((BitArray)allCams);
                Cam1AktifST1 = kameralar[0];
                Cam2AktifST1 = kameralar[1];
                Cam3AktifST1 = kameralar[2];
                Cam1AktifST2 = kameralar[3];
                Cam2AktifST2 = kameralar[4];
                Cam3AktifST2 = kameralar[5];

               
                //txtBoxLog.AppendText(toplamAktifKameraST1.ToString());
               

                if (ArsivleST1.ToString() == "True")
                {
                    string destdirst1 = pathServer + @"\" + "İSTASYON_1" + @"\" + DateTime.Now.Year.ToString() + @"\" + DateTime.Now.Month.ToString() + @"\" + DateTime.Now.Day.ToString() + @"\" + Barkod1;
                    
                    if (!Directory.Exists(destdirst1))
                    {
                        DirectoryInfo diIST1 = Directory.CreateDirectory(pathServer + @"\" + "İSTASYON_1" + @"\" + DateTime.Now.Year.ToString() + @"\" + DateTime.Now.Month.ToString() + @"\" + DateTime.Now.Day.ToString() + @"\" + Barkod1);
                        txtBoxStation1.AppendText("İstasyon 1 arşivleme isteği alındı : " + Barkod1.ToString() + "\r\n");

                        txtBoxStation1.AppendText(diIST1.ToString() + " yolu oluşturuldu." + "\r\n");

                        //Kamera1_1 yolu
                        string sourceDirName1_1 = pathFtp + @"\" + camDir1_1 + @"\" + camName1_1 + @"\" + DateTime.Now.ToString("yyyy-MM-dd") + @"\" + "001" + @"\" + "dav" + @"\" + DateTime.Now.Hour.ToString();
                        //Kamera2_1 yolu
                        string sourceDirName2_1 = pathFtp + @"\" + camDir2_1 + @"\" + camName2_1 + @"\" + DateTime.Now.ToString("yyyy-MM-dd") + @"\" + "001" + @"\" + "dav" + @"\" + DateTime.Now.Hour.ToString();
                        //Kamera3_1 yolu
                        string sourceDirName3_1 = pathFtp + @"\" + camDir3_1 + @"\" + camName3_1 + @"\" + DateTime.Now.ToString("yyyy-MM-dd") + @"\" + "001" + @"\" + "dav" + @"\" + DateTime.Now.Hour.ToString();


                        //Taşınacak klasör
                        string destDirIST1 = diIST1.ToString();

                        //Kamera video taşıma1_1
                        if (Directory.Exists(sourceDirName1_1))
                        {
                            string[] kameraCekimleri1_1 = Directory.GetFiles(sourceDirName1_1);
                            foreach (string video in kameraCekimleri1_1)
                            {
                                string videoAdi = camDir1_1 + "_2_" + Path.GetFileName(video);
                                if (!File.Exists(destDirIST1 + "\\" + videoAdi))
                                {
                                    File.Move(video, destDirIST1 + "\\" + videoAdi);
                                }
                            }
                        }

                        //Kamera video taşıma2_1
                        if (Directory.Exists(sourceDirName2_1))
                        {
                            string[] kameraCekimleri2_1 = Directory.GetFiles(sourceDirName2_1);
                            foreach (string video in kameraCekimleri2_1)
                            {
                                string videoAdi = camDir2_1 + "_3_" + Path.GetFileName(video);
                                if (!File.Exists(destDirIST1 + "\\" + videoAdi))
                                {
                                    File.Move(video, destDirIST1 + "\\" + videoAdi);
                                }
                            }
                        }

                        //Kamera video taşıma3_1
                        if (Directory.Exists(sourceDirName3_1))
                        {
                            string[] kameraCekimleri3_1 = Directory.GetFiles(sourceDirName3_1);
                            foreach (string video in kameraCekimleri3_1)
                            {
                                string videoAdi = camDir3_1 + "_1_" + Path.GetFileName(video);
                                if (!File.Exists(destDirIST1 + "\\" + videoAdi))
                                {
                                    File.Move(video, destDirIST1 + "\\" + videoAdi);
                                }
                            }
                        }

                        if (Cam1AktifST1)
                        {
                            toplamAktifKameraST1++;
                        }
                        if (Cam2AktifST1)
                        {
                            toplamAktifKameraST1++;
                        }
                        if (Cam3AktifST1)
                        {
                            toplamAktifKameraST1++;
                        }

                        if (System.IO.Directory.GetFiles(destDirIST1).Length == (toplamAktifKameraST1 * 2))
                        {
                            txtBoxStation1.AppendText("Videolar arşivlendi." + "\r\n");
                            var Arsivle1OK = plc.WriteBitAsync(DataType.DataBlock, 51, 0, 0, 1); // 51,0,0,1
                        }
                    }
                    else
                    {
                        txtBoxStation1.AppendText("Videolar arşivlendi." + "\r\n");
                        var Arsivle1OK = plc.WriteBitAsync(DataType.DataBlock, 51, 0, 0, 1); // 51,0,0,1
                    }
                }
               
                if (ArsivleST2.ToString() == "True")
                {
                    string destdirst2 = pathServer + @"\" + "İSTASYON_2" + @"\" + DateTime.Now.Year.ToString() + @"\" + DateTime.Now.Month.ToString() + @"\" + DateTime.Now.Day.ToString() + @"\" + Barkod2;
                   

                    if (!Directory.Exists(destdirst2))
                    {
                        DirectoryInfo diIST2 = Directory.CreateDirectory(pathServer + @"\" + "İSTASYON_2" + @"\" + DateTime.Now.Year.ToString() + @"\" + DateTime.Now.Month.ToString() + @"\" + DateTime.Now.Day.ToString() + @"\" + Barkod2);
                        txtBoxStation2.AppendText("İstasyon 2 arşivleme isteği alındı : " + Barkod2.ToString() + "\r\n");

                        txtBoxStation2.AppendText(diIST2.ToString() + " yolu oluşturuldu." + "\r\n");

                        //Kamera1_2 yolu
                        string sourceDirName1_2 = pathFtp + @"\" + camDir1_2 + @"\" + camName1_2 + @"\" + DateTime.Now.ToString("yyyy-MM-dd") + @"\" + "001" + @"\" + "dav" + @"\" + DateTime.Now.Hour.ToString();
                        //Kamera2_2 yolu
                        string sourceDirName2_2 = pathFtp + @"\" + camDir2_2 + @"\" + camName2_2 + @"\" + DateTime.Now.ToString("yyyy-MM-dd") + @"\" + "001" + @"\" + "dav" + @"\" + DateTime.Now.Hour.ToString();
                        //Kamera3_2 yolu
                        string sourceDirName3_2 = pathFtp + @"\" + camDir3_2 + @"\" + camName3_2 + @"\" + DateTime.Now.ToString("yyyy-MM-dd") + @"\" + "001" + @"\" + "dav" + @"\" + DateTime.Now.Hour.ToString();

                        //Taşınacak klasör
                        string destDirIST2 = diIST2.ToString();


                        //Kamera video taşıma1_2
                        if (Directory.Exists(sourceDirName1_2))
                        {
                            string[] kameraCekimleri1_2 = Directory.GetFiles(sourceDirName1_2);
                            foreach (string video in kameraCekimleri1_2)
                            {
                                string videoAdi = camDir1_2 + Path.GetFileName(video);
                                if (!File.Exists(destDirIST2 + "\\" + videoAdi))
                                {
                                    File.Move(video, destDirIST2 + "\\" + videoAdi);
                                }
                            }
                        }

                        //Kamera video taşıma2_2
                        if (Directory.Exists(sourceDirName2_2))
                        {
                            string[] kameraCekimleri2_2 = Directory.GetFiles(sourceDirName2_2);
                            foreach (string video in kameraCekimleri2_2)
                            {
                                string videoAdi = camDir2_2 + Path.GetFileName(video);
                                if (!File.Exists(destDirIST2 + "\\" + videoAdi))
                                {
                                    File.Move(video, destDirIST2 + "\\" + videoAdi);
                                }
                            }
                        }

                        //Kamera video taşıma3_2
                        if (Directory.Exists(sourceDirName3_2))
                        {
                            string[] kameraCekimleri3_2 = Directory.GetFiles(sourceDirName3_2);
                            foreach (string video in kameraCekimleri3_2)
                            {
                                string videoAdi = camDir3_2 + Path.GetFileName(video);
                                if (!File.Exists(destDirIST2 + "\\" + videoAdi))
                                {
                                    File.Move(video, destDirIST2 + "\\" + videoAdi);
                                }
                            }
                        }

                        if (Cam1AktifST2)
                        {
                            toplamAktifKameraST2++;
                        }
                        if (Cam2AktifST2)
                        {
                            toplamAktifKameraST2++;
                        }
                        if (Cam3AktifST2)
                        {
                            toplamAktifKameraST2++;
                        }

                        if (System.IO.Directory.GetFiles(destDirIST2).Length == (toplamAktifKameraST1 * 2))
                        {
                            txtBoxStation1.AppendText("Videolar arşivlendi." + "\r\n");
                            var Arsivle2OK = plc.WriteBitAsync(DataType.DataBlock, 51, 0, 1, 1); // 51,0,0,1
                        }

                    }
                    else
                    {
                        txtBoxStation1.AppendText("Videolar arşivlendi." + "\r\n");
                        var Arsivle2OK = plc.WriteBitAsync(DataType.DataBlock, 51, 0, 1, 1); // 51,0,0,1
                    }


                }

                if (CevrimBasladiST1.ToString() == "False")
                {
                    if (ftpEmptyReadST1.ToString() == "False")
                    {
                        //Kamera1_1 yolu
                        string sourceDirName1_1 = pathFtp + @"\" + camDir1_1 + @"\" + camName1_1 + @"\" + DateTime.Now.ToString("yyyy-MM-dd") + @"\" + "001" + @"\" + "dav" + @"\" + DateTime.Now.Hour.ToString();
                        //Kamera2_1 yolu
                        string sourceDirName2_1 = pathFtp + @"\" + camDir2_1 + @"\" + camName2_1 + @"\" + DateTime.Now.ToString("yyyy-MM-dd") + @"\" + "001" + @"\" + "dav" + @"\" + DateTime.Now.Hour.ToString();
                        //Kamera3_1 yolu
                        string sourceDirName3_1 = pathFtp + @"\" + camDir3_1 + @"\" + camName3_1 + @"\" + DateTime.Now.ToString("yyyy-MM-dd") + @"\" + "001" + @"\" + "dav" + @"\" + DateTime.Now.Hour.ToString();

                        if ((Directory.Exists(sourceDirName1_1)) ||
                            (Directory.Exists(sourceDirName2_1)) ||
                            (Directory.Exists(sourceDirName3_1)))
                        {
                            int i = 0;
                           
                            System.IO.DirectoryInfo di = new DirectoryInfo(sourceDirName1_1);
                            if (di.Exists)
                            {
                                if (System.IO.Directory.GetFiles(sourceDirName1_1).Length == 0)
                                {
                                    i++;
                                }
                                else
                                {
                                    foreach (FileInfo file in di.GetFiles())
                                    {
                                        file.Delete();
                                    }
                                    i++;
                                }
                            }

                            System.IO.DirectoryInfo di2 = new DirectoryInfo(sourceDirName2_1);
                            if (di2.Exists)
                            {
                                if (System.IO.Directory.GetFiles(sourceDirName2_1).Length == 0)
                                {
                                    i++;
                                }
                                else
                                {
                                    foreach (FileInfo file in di2.GetFiles())
                                    {
                                        file.Delete();
                                    }
                                    i++;
                                }
                            }

                            System.IO.DirectoryInfo di3 = new DirectoryInfo(sourceDirName3_1);
                            if (di3.Exists)
                            {
                                if (System.IO.Directory.GetFiles(sourceDirName3_1).Length == 0)
                                {
                                    i++;
                                }
                                else
                                {
                                    foreach (FileInfo file in di3.GetFiles())
                                    {
                                        file.Delete();
                                    }
                                    i++;
                                }
                            }

                            if (i>0)
                            {
                                var ftpEmptyST1 = plc.WriteBitAsync(DataType.DataBlock, 51, 0, 2, 1);
                            }
                        }
                        else
                        {
                            var ftpEmptyST1 = plc.WriteBitAsync(DataType.DataBlock, 51, 0, 2, 1);
                        }

                    }
                            
                }                

                if (CevrimBasladiST2.ToString() == "False")
                {
                    if (ftpEmptyReadST2.ToString() == "False")
                    {
                        //Kamera1_2 yolu
                        string sourceDirName1_2 = pathFtp + @"\" + camDir1_2 + @"\" + camName1_2 + @"\" + DateTime.Now.ToString("yyyy-MM-dd") + @"\" + "001" + @"\" + "dav" + @"\" + DateTime.Now.Hour.ToString();
                        //Kamera2_2 yolu
                        string sourceDirName2_2 = pathFtp + @"\" + camDir2_2 + @"\" + camName2_2 + @"\" + DateTime.Now.ToString("yyyy-MM-dd") + @"\" + "001" + @"\" + "dav" + @"\" + DateTime.Now.Hour.ToString();
                        //Kamera3_2 yolu
                        string sourceDirName3_2 = pathFtp + @"\" + camDir3_2 + @"\" + camName3_2 + @"\" + DateTime.Now.ToString("yyyy-MM-dd") + @"\" + "001" + @"\" + "dav" + @"\" + DateTime.Now.Hour.ToString();
                        
                        if ((Directory.Exists(sourceDirName1_2)) ||
                            (Directory.Exists(sourceDirName2_2)) ||
                            (Directory.Exists(sourceDirName3_2)))
                        {
                            int j = 0;

                            System.IO.DirectoryInfo di = new DirectoryInfo(sourceDirName1_2);
                            if (di.Exists)
                            {
                                if (System.IO.Directory.GetFiles(sourceDirName1_2).Length == 0)
                                {
                                    j++;
                                }
                                else
                                {
                                    foreach (FileInfo file in di.GetFiles())
                                    {
                                        file.Delete();
                                    }
                                    j++;
                                }
                            }

                            System.IO.DirectoryInfo di2 = new DirectoryInfo(sourceDirName2_2);
                            if (di2.Exists)
                            {
                                if (System.IO.Directory.GetFiles(sourceDirName2_2).Length == 0)
                                {
                                    j++;
                                }
                                else
                                {
                                    foreach (FileInfo file in di2.GetFiles())
                                    {
                                        file.Delete();
                                    }
                                    j++;
                                }
                            }

                            System.IO.DirectoryInfo di3 = new DirectoryInfo(sourceDirName3_2);
                            if (di3.Exists)
                            {
                                if (System.IO.Directory.GetFiles(sourceDirName3_2).Length == 0)
                                {
                                    j++;
                                }
                                else
                                {
                                    foreach (FileInfo file in di3.GetFiles())
                                    {
                                        file.Delete();
                                    }
                                    j++;
                                }
                            }

                            if (j > 0)
                            {
                                var ftpEmptyST2 = plc.WriteBitAsync(DataType.DataBlock, 51, 0, 3, 1);
                            }
                        }
                        else
                        {
                            var ftpEmptyST2 = plc.WriteBitAsync(DataType.DataBlock, 51, 0, 3, 1);
                        }

                    }
                    
                }
            }
            catch (PlcException ex)
            {
                plc.Close();
                pnlPlcConnectionStatus.BackColor = Color.DarkGray;
                
                if (ex.ErrorCode.ToString() == "ReadData")
                {
                    txtBoxLog.AppendText("Hata : PLC BAĞLANTI HATASI (" + DateTime.Now.ToString("g") + ")" + "\r\n");
                }
                else
                {
                    txtBoxLog.AppendText("Hata : " + ex.ErrorCode.ToString() + "(" + DateTime.Now.ToString("g") + ")" + "\r\n");
                }
            }
        }

        private void km1_FormClosing(object sender, FormClosingEventArgs e)
        {
            plc.Close();
        }

    }
}
