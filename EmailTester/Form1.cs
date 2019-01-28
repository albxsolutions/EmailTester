using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmailTester
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {

            try
            {

                this.cmdSend.Enabled = false;

                String fromEmailAddress = txtFromEmail.Text;
                String toEmailAddress = txtTo.Text;
                String userName = txtUsername.Text;
                String password = txtPassword.Text;
                String host = txtHost.Text;
                String subject = txtSubject.Text;
                String body = txtMessage.Text;

                int port = Convert.ToInt32(txtPort.Text);

                MailMessage msg = new MailMessage();
                msg.To.Add(new MailAddress(toEmailAddress));
                msg.From = new MailAddress(fromEmailAddress);
                msg.Subject = subject;
                msg.Body = body;
                msg.IsBodyHtml = true;

                SmtpClient client = new SmtpClient();
                client.Host = host;
                client.Credentials = new System.Net.NetworkCredential(userName, password);
                client.Port = Convert.ToInt32(port);
                client.EnableSsl = chkUseSSL.Checked;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Send(msg);

                MessageBox.Show("Email sent");

                this.cmdSend.Enabled = true;

            }

            catch (SmtpException smtpEx)
            {
                var msg = $"SmtpException: {smtpEx.Message} - {smtpEx.InnerException.Message}";
                MessageBox.Show(msg, "SmtpException");

            }

            catch (Exception ex)
            {
                var msg = $"Exception: {ex.Message}";
                MessageBox.Show(msg, "Exception");
            }

            finally
            {
                this.cmdSend.Enabled = true;
            }
            

        }

        private void cmdCopy_Click(object sender, EventArgs e)
        {

            this.txtFromEmail.Text = this.txtUsername.Text;

        }
    }
}
