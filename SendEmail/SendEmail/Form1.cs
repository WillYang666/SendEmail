
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 发送邮件winform
{//在winform中添加一个按钮,Name属性为btnSend,Text属性为 发送
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                string title = "提交的内容";
                string contact = "附件";
                string content = "附件是我要提交的内容";
                string upfile = "";
                //实例化一个发送邮件类。
                MailMessage mailMessage = new MailMessage();

                //发件人邮箱地址，方法重载不同，可以根据需求自行选择。
                mailMessage.From = new MailAddress("1323266208@qq.com");

                //收件人邮箱地址。
                mailMessage.To.Add(new MailAddress("2847225301@qq.com"));

                //邮件标题。
                mailMessage.Subject = title;

                //邮件内容。
                string MailBody = "<p style=\"font-size: 10pt\">联系:" + contact + "</p>";
                MailBody += "<p style=\"font-size: 10pt\">内容：" + content + "</p>";

                //内容编码 
                mailMessage.BodyEncoding = Encoding.Default;

                //发送优先级 
                mailMessage.Priority = MailPriority.High;

                //邮件内容 
                mailMessage.Body = MailBody;

                MessageBox.Show("请选择附件");
                //添加附件，打开添加附件的对话框
                OpenFileDialog dialog = new OpenFileDialog();
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    upfile = dialog.FileName;
                }
                else
                {
                    return;
                }
                mailMessage.Attachments.Add(new Attachment(upfile));
                MessageBox.Show("附件添加完成，等待发送中...");
                //是否HTML形式发送 
                mailMessage.IsBodyHtml = true;  //============这里很重要，以前我也是这里漏了，发出去的是html代码

                //实例化一个SmtpClient类。
                SmtpClient client = new SmtpClient();

                //发件服务器
                client.Host = "smtp.qq.com";

                //使用安全加密连接。
                client.EnableSsl = true;

                //不和请求一块发送。
                client.UseDefaultCredentials = false;

                //验证发件人身份(发件人的邮箱，发送邮箱里的生成授权码);
                client.Credentials = new NetworkCredential("1323266208@qq.com", "邮箱里的生成授权码，需要到发送的qq邮箱里面设置，获取授权码");

                //发送
                client.Send(mailMessage);
                MessageBox.Show("发送成功");
            }
            catch (Exception)
            {
                MessageBox.Show("发送失败");

            }
        }
    }
}