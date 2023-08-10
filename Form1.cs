using NFA_RCOM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
//using Newtonsoft.Json.Linq;
//using System.Threading.Tasks;


namespace WindowsFormsApp84
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            RemotingConfiguration.Configure("WindowsFormsApp84.exe.config", false);
            // RemotingConfiguration.Configure("NFA.exe.config", false); //복사 붙여넣기
            NFA_RCOM.clsRCOM _r = new clsRCOM();
            //isChecked(alarmBox.Checked);
            Dictionary<string, clsDevice> dicDV = _r.getDeviceList();
            foreach (KeyValuePair<string, clsDevice> dv in dicDV)
            {
                Dictionary<string, clsPacket> dicPK = dv.Value.PacketList;
                if (dv.Value.DeviceID.Contains("BAT1"))
                {
                    foreach (KeyValuePair<string, clsPacket> pk in dicPK)
                    {
                        Dictionary<string, clsTag> dicTg = pk.Value.TagList;
                        foreach (KeyValuePair<string, clsTag> tg in dicTg)
                        {
                            if (pk.Value.PacketID == "BAT1_R_PK01")//GET_PACKET_ID
                            {
                                if (tg.Value.TagID == "Bat_SOC")
                                { Console.WriteLine(tg.Value.TagValue);
                                    if (tg.Value.TagValue == "0.600000") 
                                    {
                                        alarmBox.Checked = true;
                                        button1.Enabled = true;
                                    }
               
                                    else
                                    { 
                                        alarmBox.Checked = false;
                                        button1.Enabled = false;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        public void isChecked(bool Checked)
        {
            if (Checked)
            {
                button1.Enabled = true;
            }

            else
            {
                button1.Enabled = false;
            }
        }

        //private void Form1_Load(object sender, EventArgs e)
        //{

        //}

        private void alarmBox_CheckedChanged(object sender, EventArgs e)
        {
            //if(clsRCOM =
            // clsRCOM _r.open();
            if (alarmBox.Checked) ;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (HttpClient client = new HttpClient())
            {
                MultipartFormDataContent formData = new MultipartFormDataContent();
                formData.Add(new StringContent("nextsq"), "user_id");            // SMS 아이디
                formData.Add(new StringContent("3xtk0zw4ti95nr2yce41v6fywhd6f96v"), "key");           // 인증키
                formData.Add(new StringContent("ex)00현장 00장비 폴트나감"), "msg");        // 메세지 내용
                formData.Add(new StringContent("01035125789"), "receiver");     // 수신번호
                formData.Add(new StringContent("07048206884"), "sender");         // 발신번호
                formData.Add(new StringContent("N"), "testmode_yn"); // Y 인경우 실제문자 전송X , 자동취소(환불) 처리



                client.DefaultRequestHeaders.Add("Accept", "*/*");



                var response = client.PostAsync("https://apis.aligo.in/send/", formData).Result;
                if (!response.IsSuccessStatusCode)
                    Console.WriteLine(response.StatusCode);




                else
                {
                    var content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    Console.WriteLine(content);
                    Close();
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}